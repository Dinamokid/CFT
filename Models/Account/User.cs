using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFT.Models.Account
{
	public class User
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Login { get; set; }
		public string Password { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }

		public byte[] Avatar { get; set; }

		public DateTime RegisterDateTime { get; set; }

		public string GetFullName()
		{
			return string.Join(" ", LastName, FirstName).Trim();
		}
	}
}
