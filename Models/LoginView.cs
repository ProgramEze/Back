using System.ComponentModel.DataAnnotations;
namespace Back.Models
{
	public class LoginView
	{
		public int Matricula { get; set; }
		[DataType(DataType.Password)]
		public string Clave { get; set; }
	}
}