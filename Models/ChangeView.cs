using System.ComponentModel.DataAnnotations;
namespace Back.Models
{
	public class ChangeView
	{
		[DataType(DataType.Password)]
		public string ClaveVieja { get; set; }
		[DataType(DataType.Password)]
		public string ClaveNueva { get; set; }
		[DataType(DataType.Password)]
		public string RepetirClaveNueva { get; set; }
	}
}