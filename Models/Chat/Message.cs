using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CFT.Models.Account;

namespace CFT.Models.Chat
{
	public class Message
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("Author")]
		public int AuthorId { get; set; }
		public virtual User Author { get; set; }

		public string Text { get; set; }
		public DateTime CreatedDate { get; set; }

		public bool IsReaded { get; set; }

		[NotMapped]
		public int TotalCount {get; set;}
	}
}
