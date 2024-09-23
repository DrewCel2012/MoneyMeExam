using MoneyMe.Service.Interface;
using MoneyMe.Service.RepaymentTypes;

namespace MoneyMe.Test
{
    public sealed class ProductCServiceTest
    {
        [Theory]
        [InlineData(36, 5000.00, 156.68)]
        public void ComputePMT_ShouldComputePMTForProductCRepaymentType(int totalNumberOfMonths, double loanAmount, decimal actual)
        {
            // Arrange:
            IRepaymentTypeService repaymentTypeService = new ProductCService();

            // Act:
            decimal result = repaymentTypeService.ComputePMT(totalNumberOfMonths, loanAmount);

            // Assert:
            Assert.Equal(Math.Round(result, 2), actual);
        }
    }
}
