namespace MoneyMe.Service.Interface
{
    public interface IRepaymentTypeService
    {
        decimal ComputePMT(int totalNumberOfMonths, double loanAmount);
    }
}
