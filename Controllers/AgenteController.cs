using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using Back.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

namespace Back.Controllers
{
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class AgentesController : ControllerBase
	{
		private readonly DataContext contexto;
		private readonly IConfiguration config;

		public AgentesController(DataContext contexto, IConfiguration config)
		{
			this.contexto = contexto;
			this.config = config;
		}

		[HttpGet]
		public async Task<ActionResult<Agente>> Get()
		{
			try
			{
				var matricula = int.Parse(User.FindFirstValue("Matricula"));
				return await contexto.Agente.SingleOrDefaultAsync(x => x.Matricula == matricula);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromForm] LoginView loginView)
		{
			try
			{
				byte[] saltBytes = Encoding.ASCII.GetBytes(config["Salt"]);
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: loginView.Clave,
					salt: saltBytes,
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 1000,
					numBytesRequested: 256 / 8));
				var a = await contexto.Agente.FirstOrDefaultAsync(x => x.Matricula == loginView.Matricula);
				if (a == null || a.Clave != hashed)
				{
					return BadRequest("Nombre de usuario o Password incorrecta");
				}
				else
				{
					var key = new SymmetricSecurityKey(
						Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
					var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var claims = new List<Claim>
					{
						new Claim("Matricula", a.Matricula.ToString()),
						new Claim("FullName", a.Nombre + " " + a.Apellido),
						new Claim(ClaimTypes.Role, "Administrador"),
					};
					var token = new JwtSecurityToken(
						issuer: config["TokenAuthentication:Issuer"],
						audience: config["TokenAuthentication:Audience"],
						claims: claims,
						expires: DateTime.Now.AddHours(60000),
						signingCredentials: credenciales
					);
					return Ok(new JwtSecurityTokenHandler().WriteToken(token));
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		private string GetLocalIpAddress()
		{
			string localIp = null;
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					localIp = ip.ToString();
					break;
				}
			}
			return localIp;
		}

		[HttpPost("olvidecontraseña")]
		[AllowAnonymous]
		public async Task<IActionResult> EnviarMatricula([FromForm] int matricula)
		{
			try
			{
				var agente = await contexto.Agente.FirstOrDefaultAsync(x => x.Matricula == matricula);
				if (agente == null)
				{
					return NotFound("No se encontró ningún usuario con esa matricula.");
				}
				var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
				var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
				var claims = new List<Claim>
				{
					new Claim("Matricula", agente.Matricula.ToString()),
					new Claim("FullName", agente.Nombre + " " + agente.Apellido),
					new Claim(ClaimTypes.Role, "Administrador"),
				};
				var token = new JwtSecurityToken(
					issuer: config["TokenAuthentication:Issuer"],
					audience: config["TokenAuthentication:Audience"],
					claims: claims,
					expires: DateTime.Now.AddHours(5),
					signingCredentials: credenciales
				);
				var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
				var dominio = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
				var resetLink = Url.Action("CambiarPassword", "Agentes");
				var rutaCompleta = Request.Scheme + "://" + GetLocalIpAddress() + ":" + Request.Host.Port + resetLink;
				var message = new MimeMessage();
				message.To.Add(new MailboxAddress(agente.Nombre, agente.Email));
				message.From.Add(new MailboxAddress("Sistema", config["SMTPUser"]));
				message.Subject = "Restablecimiento de Contraseña";
				message.Body = new TextPart("html")
				{
					Text = $@"<h1>Hola {agente.Nombre},</h1>
						   <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta.
							<p>Por favor, haz clic en el siguiente enlace para crear una nueva contraseña:</p>
						   <a href='{rutaCompleta}?access_token={tokenString}'>{rutaCompleta}?access_token={tokenString}</a>"
				};
				using var client = new SmtpClient();
				client.ServerCertificateValidationCallback = (s, c, h, e) => true;
				await client.ConnectAsync("sandbox.smtp.mailtrap.io", 587, MailKit.Security.SecureSocketOptions.StartTls);
				await client.AuthenticateAsync(config["SMTPUser"], config["SMTPPass"]);
				await client.SendAsync(message);
				await client.DisconnectAsync(true);
				return Ok("Se ha enviado el enlace de restablecimiento de contraseña correctamente.");
			}
			catch (Exception ex)
			{
				return BadRequest($"Error: {ex.Message}");
			}
		}

		[HttpGet("cambiarpassword")]
		public async Task<IActionResult> CambiarPassword()
		{
			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]);
				var symmetricKey = new SymmetricSecurityKey(key);
				Random rand = new Random(Environment.TickCount);
				string randomChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
				string nuevaClave = "";
				for (int i = 0; i < 8; i++)
				{
					nuevaClave += randomChars[rand.Next(0, randomChars.Length)];
				}
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: nuevaClave,
					salt: Encoding.ASCII.GetBytes(config["Salt"]),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 1000,
					numBytesRequested: 256 / 8));
				var matricula = int.Parse(User.FindFirstValue("Matricula"));
				var a = await contexto.Agente.FirstOrDefaultAsync(x => x.Matricula == matricula);
				if (a == null)
				{
					return BadRequest("Nombre de usuario incorrecto");
				}
				else
				{
					a.Clave = hashed;
					contexto.Agente.Update(a);
					await contexto.SaveChangesAsync();
					var message = new MimeMessage();
					message.To.Add(new MailboxAddress(a.Nombre, a.Email));
					message.From.Add(new MailboxAddress("Sistema", config["SMTPUser"]));
					message.Subject = "Restablecimiento de Contraseña";
					message.Body = new TextPart("html")
					{
						Text = $"<h1>Hola {a.Nombre},</h1>" +
							   $"<p>Has cambiado tu contraseña de forma correcta. " +
							   $"Tu nueva contraseña es la siguiente: {nuevaClave}</p>"
					};
					using var client = new SmtpClient();
					client.ServerCertificateValidationCallback = (s, c, h, e) => true;
					await client.ConnectAsync("sandbox.smtp.mailtrap.io", 587, MailKit.Security.SecureSocketOptions.StartTls);
					await client.AuthenticateAsync(config["SMTPUser"], config["SMTPPass"]);
					await client.SendAsync(message);
					await client.DisconnectAsync(true);

					return Ok("Se ha restablecido la contraseña correctamente.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Agente entidad)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var agente = await contexto.Agente.AsNoTracking().FirstOrDefaultAsync(x => x.Matricula == entidad.Matricula);
					if (agente == null)
					{
						return NotFound("Agente no encontrado");
					}
					entidad.Clave = agente.Clave;
					Console.WriteLine("Contraseña: " + entidad.Clave);
					contexto.Agente.Update(entidad);
					await contexto.SaveChangesAsync();
					var key = new SymmetricSecurityKey(
						Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
					var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var claims = new List<Claim>
					{
						new Claim("Matricula", agente.Matricula.ToString()),
						new Claim("FullName", agente.Nombre + " " + agente.Apellido),
						new Claim(ClaimTypes.Role, "Administrador"),
					};
					var token = new JwtSecurityToken(
						issuer: config["TokenAuthentication:Issuer"],
						audience: config["TokenAuthentication:Audience"],
						claims: claims,
						expires: DateTime.Now.AddHours(60000),
						signingCredentials: credenciales
					);
					return Ok(new JwtSecurityTokenHandler().WriteToken(token));
				}

				return BadRequest("Modelo inválido");
			}
			catch (Exception ex)
			{
				// Agregar más detalles del error en el registro para depuración
				return BadRequest($"Error: {ex.Message}");
			}
		}

		[HttpPut("cambiarviejacontraseña")]
		public async Task<IActionResult> CambiarPasswordPorInput([FromForm] ChangeView changeView)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var matricula = int.Parse(User.FindFirstValue("Matricula"));
					var agente = await contexto.Agente.AsNoTracking().FirstOrDefaultAsync(x => x.Matricula == matricula);
					if (agente == null)
					{
						return NotFound("Agente no encontrado");
					}
					string hashedVieja = Convert.ToBase64String(KeyDerivation.Pbkdf2(
							password: changeView.ClaveVieja,
							salt: Encoding.ASCII.GetBytes(config["Salt"]),
							prf: KeyDerivationPrf.HMACSHA1,
							iterationCount: 1000,
							numBytesRequested: 256 / 8));

					if (agente.Clave != hashedVieja)
					{
						return BadRequest("Clave incorrecta");
					}
					string hashedNueva = Convert.ToBase64String(KeyDerivation.Pbkdf2(
						password: changeView.ClaveNueva,
						salt: Encoding.ASCII.GetBytes(config["Salt"]),
						prf: KeyDerivationPrf.HMACSHA1,
						iterationCount: 1000,
						numBytesRequested: 256 / 8));
					string hashedRepetir = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: changeView.RepetirClaveNueva,
					salt: Encoding.ASCII.GetBytes(config["Salt"]),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 1000,
					numBytesRequested: 256 / 8));
					if (hashedNueva != hashedRepetir)
					{
						return BadRequest("La clave nueva no coincide");
					}
					else
					{
						agente.Clave = hashedNueva;
						contexto.Agente.Update(agente);
						await contexto.SaveChangesAsync();
						return Ok("Contraseña cambiada con exito");
					}
				} else {
					return BadRequest("Modelo inválido");
				}
			}
			catch (Exception ex)
			{
				// Agregar más detalles del error en el registro para depuración
				return BadRequest($"Error: {ex.Message}");
			}
		}
	}
}