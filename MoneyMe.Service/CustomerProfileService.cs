using Mapster;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Model.DataTransferObjects;
using MoneyMe.Model.ViewModels;
using MoneyMe.Repository.Entities;
using MoneyMe.Repository.Interface;
using MoneyMe.Service.Interface;

namespace MoneyMe.Service
{
    public sealed class CustomerProfileService(IUnitOfWork unitOfWork) : ICustomerProfileService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public int CustomerProfileId { get; set; }


        public async Task<bool> AddAsync(CustomerProfileDto dto)
        {
            int result = 0;
            CustomerProfile customer = dto.Adapt<CustomerProfile>();

            await _unitOfWork.CustomerProfile.AddAsync(customer);
            result = await _unitOfWork.SaveChangesAsync();

            CustomerProfileId = customer.Id;

            return (result > 0);
        }

        public async Task<IEnumerable<CustomerProfileViewModel>> GetAllAsync()
        {
            IEnumerable<CustomerProfile> customers = await _unitOfWork.CustomerProfile.GetAllAsync(
                    filter: f => f.IsActive == true,
                    orderBy: o => o.OrderBy(x => x.FirstName),
                    includes: null);

            return customers.Adapt<IEnumerable<CustomerProfileViewModel>>();
        }

        public async Task<T> GetByIDAsync<T>(int id)
        {
            CustomerProfile customer = await _unitOfWork.CustomerProfile.GetByFilterAsync(
                    filter: f => f.Id == id && f.IsActive == true,
                    includes: i => i.Include(x => x.CustomerProfileLoanApplication).ThenInclude(x => x.Product));

            return customer.Adapt<T>();
        }


        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
