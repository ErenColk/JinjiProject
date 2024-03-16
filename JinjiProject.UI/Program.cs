using AspNetCoreHero.ToastNotification.Extensions;
using JinjiProject.BusinessLayer.Extensions;
using JinjiProject.DataAccess.EFCore.Extensions;
using JinjiProject.DataAccessLayer.Extensions;
using JinjiProject.Dtos.SendMails;
using JinjiProject.UI.Extensions;

namespace JinjiProject.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.
                AddDataAccessServices(builder.Configuration)
                .AddEFCoreServices()
                .AddBusinessServices()
                .AddMvcServices()
                .Configure<EmailConfigurationDto>(builder.Configuration.GetSection("EmailConfiguration"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseNotyf();
            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}