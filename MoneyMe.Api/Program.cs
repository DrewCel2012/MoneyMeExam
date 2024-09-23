using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoneyMe.Api.Filters;
using MoneyMe.Common.Helpers;
using MoneyMe.Model;
using MoneyMe.Service.Extensions;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace MoneyMe.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionHandlingFilter)));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();


            // This has been modified to test JWT Token Authorization in Swagger:
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            // JWT Token Authentication Initialization:
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("AuthDetails:Hash-SHA512-Key"))),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            // Initialize Dependency Injection Components:
            builder.Services.Initialize(builder.Configuration);

            // Enable CORS (Cross Origin Resource Sharing):
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: builder.Configuration[key: "CORSSettings:Name"] ?? "", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            //Deserialize appsettings.json values to model class per section:
            builder.Services.Configure<UserRedirectDetails>(builder.Configuration.GetSection("UserRedirectDetails"));

            // Initialize Log4net Logging tool:
            string configFolder = Path.Combine(builder.Environment.ContentRootPath, "Configuration");
            LogHelper.InitializeConfiguration(configFolder);
            LogHelper.LogInfo("Log4net initialized and ready to log.");


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
