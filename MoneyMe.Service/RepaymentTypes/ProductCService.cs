using MoneyMe.Common.Helpers;
using MoneyMe.Service.Interface;

namespace MoneyMe.Service.RepaymentTypes
{
    public sealed class ProductCService : IRepaymentTypeService
    {
        private readonly double _yearlyInterestRate;

        public ProductCService()
        {
            _yearlyInterestRate = ConfigurationHelper.GetConfigurationValue<double>("RepaymentDetails:YearlyInterestRate");
        }


        public decimal ComputePMT(int totalNumberOfMonths, double loanAmount)
        {
            var rate = _yearlyInterestRate / 100 / 12;
            var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
            return new decimal((rate + (rate / denominator)) * loanAmount);
        }
    }
}
