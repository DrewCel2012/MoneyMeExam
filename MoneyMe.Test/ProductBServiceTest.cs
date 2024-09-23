using MoneyMe.Service.Interface;
using MoneyMe.Service.RepaymentTypes;

namespace MoneyMe.Test
{
    public sealed class ProductBServiceTest
    {
        [Theory]
        [InlineData(6, 5000.00, 1125.05)]
        public void ComputePMT_ShouldComputePMTForProductBRepaymentType(int totalNumberOfMonths, double loanAmount, decimal actual)
        {
            // Arrange:
            IRepaymentTypeService repaymentTypeService = new ProductBService();

            // Act:
            decimal result = repaymentTypeService.ComputePMT(totalNumberOfMonths, loanAmount);

            // Assert:
            Assert.Equal(Math.Round(result, 2), actual);
        }
    }
}
