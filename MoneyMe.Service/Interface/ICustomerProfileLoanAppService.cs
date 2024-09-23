using MoneyMe.Model;
using MoneyMe.Model.DataTransferObjects;

namespace MoneyMe.Service.Interface
{
    public interface ICustomerProfileLoanAppService : IDisposable
    {
        Task<ServiceResponse> CreateLoanApplication(LoanApplicationDto dto);
    }
}
