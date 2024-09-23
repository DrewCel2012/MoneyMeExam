using MoneyMe.Service.Interface;
using MoneyMe.Service.RepaymentTypes;

namespace MoneyMe.Test
{
    public sealed class ProductAServiceTest
    {
        [Theory]
        [InlineData(12, 5000.00, 416.67)]
        public void ComputePMT_ShouldComputePMTForProductARepaymentType(int totalNumberOfMonths, double loanAmount, decimal actual)
        {
            // Arrange:
            IRepaymentTypeService repaymentTypeService = new ProductAService();

            // Act:
            decimal result = repaymentTypeService.ComputePMT(totalNumberOfMonths, loanAmount);

            // Assert:
            Assert.Equal(Math.Round(result, 2), actual);
        }
    }
}
