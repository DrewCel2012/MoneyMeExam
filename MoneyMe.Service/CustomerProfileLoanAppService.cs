using Microsoft.Data.SqlClient;
using MoneyMe.Model;
using MoneyMe.Model.DataTransferObjects;
using MoneyMe.Repository.Interface;
using MoneyMe.Service.Interface;
using System.Data;

namespace MoneyMe.Service
{
    public sealed class CustomerProfileLoanAppService(IUnitOfWork unitOfWork) : ICustomerProfileLoanAppService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task<ServiceResponse> CreateLoanApplication(LoanApplicationDto dto)
        {
            ServiceResponse response = new();
            SqlParameter[]? parameters = {
                new ("@CustomerProfileId", dto.CustomerProfileId),
                new ("@ProductId", dto.ProductId),
                new ("@Repayment", dto.Repayment),
                new ("@TotalRepayment", dto.TotalRepayment),
                new ("@DateOfBirth", dto.DateOfBirth),
                new ("@Mobile", dto.Mobile),
                new ("@Email", dto.Email),
                new ("@Error", SqlDbType.VarChar, int.MaxValue) { Direction = ParameterDirection.Output }
            };

            response = await _unitOfWork.CustomerProfileLoanApp.ExecuteProcAsync("usp_CreateLoanApplication", parameters);

            return response;
        }


        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
