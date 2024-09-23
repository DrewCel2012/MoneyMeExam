using System.ComponentModel.DataAnnotations;

namespace MoneyMe.Model.DataTransferObjects
{
    public sealed class LoanApplicationDto
    {
        public int CustomerProfileId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Finance Amount")]
        public decimal AmountRequired { get; set; }

        public int Term { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Birthdate")]
        public DateOnly DateOfBirth { get; set; }

        public string Mobile { get; set; } = null!;

        public string Email { get; set; } = null!;

        [Display(Name = "Repayments From")]
        public decimal Repayment { get; set; }

        public decimal TotalRepayment { get; set; }
    }
}
