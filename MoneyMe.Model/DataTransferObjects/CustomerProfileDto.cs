using System.ComponentModel.DataAnnotations;

namespace MoneyMe.Model.DataTransferObjects
{
    public sealed class CustomerProfileDto
    {
        public int Id { get; set; }

        [Display(Name = "Amount Required")]
        public decimal AmountRequired { get; set; }

        public int Term { get; set; }
        
        [MaxLength(10)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [MaxLength(20)]
        public string Mobile { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; } = null!;

        [Display(Name = "Product")]
        public int ProductId { get; set; }

        public bool IsActive { get; set; } = true;


        public LoanApplicationDetails? LoanApplicationDetails { get; set; }
    }
}
