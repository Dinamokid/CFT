using System.ComponentModel.DataAnnotations;

namespace CFT.ViewModels.Account
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Login not specified")]
		public string Login { get; set; }
         
		[Required(ErrorMessage = "Password not specified")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
