using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CFT.Models;
using CFT.Models.Account;
using CFT.Services;
using CFT.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CFT.Controllers
{
	public class AccountController : BaseController
	{
		protected readonly FileService FileService;

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await DbContext.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
				if (user != null)
				{
					await Authenticate(model.Login);

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Incorrect username and / or password");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await DbContext.Users.FirstOrDefaultAsync(u => u.Login == model.Login);

				if (user == null)
				{
					var avatarPath = FileService.UploadFileInBd(model.Avatar);

					DbContext.Users.Add(new User
					{
						Login = model.Login,
						Password = model.Password,
						LastName = model.LastName,
						FirstName = model.FirstName,
						RegisterDateTime = DateTime.UtcNow,
						Avatar = avatarPath
					});

					await DbContext.SaveChangesAsync();

					await Authenticate(model.Login);

					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError("", "This login already registered");
			}
			return View(model);
		}

		private async Task Authenticate(string login)
		{
			User user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
				new Claim(ClaimsIdentity.DefaultNameClaimType, login)
			};

			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}

		public AccountController(CftDbContext dbContext, IWebHostEnvironment env, FileService fileService) : base(dbContext, env)
		{
			FileService = fileService;
		}
	}
}