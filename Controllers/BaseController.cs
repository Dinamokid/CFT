using System.Linq;
using System.Threading.Tasks;
using CFT.Models;
using CFT.Models.Account;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CFT.Controllers
{
	public class BaseController : Controller
	{
		protected readonly CftDbContext DbContext;
		protected readonly IWebHostEnvironment HostingEnv;

		public BaseController(CftDbContext dbContext, IWebHostEnvironment env)
		{
			DbContext = dbContext;
			HostingEnv = env;
		}

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var user = GetCurrentUser();
			if (user != null)
			{
				ViewBag.CurrentUserId = user.Id;
				ViewBag.CurrentUserFullName = user.GetFullName();
			}
			await next();
		}

		protected User GetCurrentUser() => DbContext.Users.FirstOrDefault(u => u.Id.ToString() == User.Identity.Name);
	}
}