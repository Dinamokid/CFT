using CFT.Models.Account;
using CFT.Models.Chat;
using Microsoft.EntityFrameworkCore;

namespace CFT.Models
{
	public sealed class CftDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Message> Messages { get; set; }
		public CftDbContext(DbContextOptions<CftDbContext> options)
			: base(options)
		{}
	}
}
