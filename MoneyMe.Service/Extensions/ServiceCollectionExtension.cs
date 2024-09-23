using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyMe.Repository.Entities;
using MoneyMe.Repository.Interface;
using MoneyMe.Repository.UnitOfWork;
using MoneyMe.Service.Interface;

namespace MoneyMe.Service.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void Initialize(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MoneyMeDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INamePrefixesService, NamePrefixesService>();
            services.AddScoped<ICustomerProfileService, CustomerProfileService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICustomerProfileLoanAppService, CustomerProfileLoanAppService>();
        }
    }
}
