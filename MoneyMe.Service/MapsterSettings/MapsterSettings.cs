using Mapster;
using MoneyMe.Model.DataTransferObjects;
using MoneyMe.Model.ViewModels;
using MoneyMe.Repository.Entities;

namespace MoneyMe.Service.MapsterSettings
{
    public static class MapsterSettings
    {
        public static void Configure()
        {
            TypeAdapterConfig<CustomerProfileDto, LoanApplicationDto>
                .NewConfig()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
                .Map(dest => dest.CustomerProfileId, src => src.Id);

            TypeAdapterConfig<CustomerProfileLoanApplication, CustomerProfileLoanViewModel>
                .NewConfig()
                .Map(dest => dest.ProductName, src => src.Product.Name);
        }
    }
}
