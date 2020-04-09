using CFT.Models;
using CFT.Services;
using CFT.SignalR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CFT
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages()
				.AddRazorRuntimeCompilation();

			string connection = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<CftDbContext>(options =>
				options.UseSqlServer(connection));

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => 
				{
					options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
				});

			services.AddSignalR();

			services.AddSingleton<FileService>();

			services.AddControllersWithViews();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization(); 

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<ChatHub>("/chat");
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
