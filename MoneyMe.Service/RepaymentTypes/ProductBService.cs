using MoneyMe.Common.Helpers;
using MoneyMe.Service.Interface;

namespace MoneyMe.Service.RepaymentTypes
{
    public sealed class ProductBService : IRepaymentTypeService
    {
        private readonly double _yearlyInterestRate;

        public ProductBService()
        {
            _yearlyInterestRate = ConfigurationHelper.GetConfigurationValue<double>("RepaymentDetails:YearlyInterestRate");
        }


        public decimal ComputePMT(int totalNumberOfMonths, double loanAmount)
        {
            decimal twoMonthsInterestFree = new(loanAmount / totalNumberOfMonths);
            twoMonthsInterestFree *= 2;

            var rate = _yearlyInterestRate / 100 / 12;
            var denominator = Math.Pow((1 + rate), (totalNumberOfMonths - 2)) - 1;
            decimal withInterest = new((rate + (rate / denominator)) * loanAmount);
            withInterest *= (totalNumberOfMonths - 2);

            return (twoMonthsInterestFree + withInterest) / totalNumberOfMonths;
        }
    }
}
