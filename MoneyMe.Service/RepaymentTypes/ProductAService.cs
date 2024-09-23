using MoneyMe.Service.Interface;

namespace MoneyMe.Service.RepaymentTypes
{
    public sealed class ProductAService : IRepaymentTypeService
    {
        public decimal ComputePMT(int totalNumberOfMonths, double loanAmount)
        {
            return totalNumberOfMonths > 0 ? new decimal(loanAmount / totalNumberOfMonths) : 0;
        }
    }
}
