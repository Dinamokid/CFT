using CFT.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CFT.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		public IActionResult Index()
		{
			var messages = DbContext.Messages.Include(p => p.Author).AsNoTracking().OrderBy(u => u.CreatedDate).ToList();
			return View(messages);
		}

		public HomeController(CftDbContext dbContext, IWebHostEnvironment env) : base(dbContext, env)
		{
		}
	}
}
