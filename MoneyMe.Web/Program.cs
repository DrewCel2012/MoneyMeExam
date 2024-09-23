using MoneyMe.Common.Helpers;
using MoneyMe.Model;
using MoneyMe.Service.Extensions;
using MoneyMe.Service.MapsterSettings;
using MoneyMe.Web.Filters;

namespace MoneyMe.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews(options => options.Filters.Add(typeof(ExceptionHandlingFilter)));


            // Initialize Dependency Injection Components:
            builder.Services.Initialize(builder.Configuration);

            // Initialize Mapster Settings:
            MapsterSettings.Configure();

            //Deserialize appsettings.json values to model class per section:
            builder.Services.Configure<LoanApplicationDetails>(builder.Configuration.GetSection("LoanApplicationDetails"));

            // Initialize Log4net Logging tool:
            string configFolder = Path.Combine(builder.Environment.ContentRootPath, "Configuration");
            LogHelper.InitializeConfiguration(configFolder);
            LogHelper.LogInfo("Log4net initialized and ready to log.");


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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=CustomerProfile}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
