using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CFT.ViewModels.Account
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Username is required")]
		[StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
		public string Login { get; set; }
         
		[Required(ErrorMessage = "Password is required")]
		[StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
         
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password mismatch")]
		public string ConfirmPassword { get; set; }

		[StringLength(20, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
		public string FirstName { get; set; }

		[StringLength(20, ErrorMessage = "Must be between 2 and 16 characters", MinimumLength = 2)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Avatar is required")]
		public IFormFile Avatar { get; set; }
	}
}
