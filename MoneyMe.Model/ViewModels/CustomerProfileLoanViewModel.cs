namespace MoneyMe.Model.ViewModels
{
    public sealed class CustomerProfileLoanViewModel
    {
        public string ProductName { get; set; } = string.Empty;

        public decimal Repayment { get; set; }

        public decimal TotalRepayment { get; set; }
    }
}
