namespace MoneyMe.Model
{
    public sealed class LoanApplicationDetails
    {
        public int AmountMin { get; set; }
        public int AmountMax { get; set; }
        public int TermMin { get; set; }
        public int TermMax { get; set; }
    }
}
