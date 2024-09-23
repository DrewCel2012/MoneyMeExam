namespace MoneyMe.Model.ViewModels
{
    public sealed class CustomerProfileViewModel
    {
        public int Id { get; set; }

        public decimal AmountRequired { get; set; }

        public int Term { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string Mobile { get; set; } = null!;

        public string Email { get; set; } = null!;


        public ICollection<CustomerProfileLoanViewModel> CustomerProfileLoanApplication { get; set; } = [];
    }
}
