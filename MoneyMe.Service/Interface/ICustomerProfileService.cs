using MoneyMe.Model.DataTransferObjects;
using MoneyMe.Model.ViewModels;

namespace MoneyMe.Service.Interface
{
    public interface ICustomerProfileService : IDisposable
    {
        Task<IEnumerable<CustomerProfileViewModel>> GetAllAsync();
        Task<T> GetByIDAsync<T>(int id);        
        Task<bool> AddAsync(CustomerProfileDto dto);

        int CustomerProfileId { get; set; }
    }
}
