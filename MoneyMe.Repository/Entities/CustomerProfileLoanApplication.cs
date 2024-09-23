using System;
using System.Collections.Generic;

namespace MoneyMe.Repository.Entities;

public partial class CustomerProfileLoanApplication
{
    public int Id { get; set; }

    public int CustomerProfileId { get; set; }

    public int ProductId { get; set; }

    public decimal Repayment { get; set; }

    public decimal TotalRepayment { get; set; }

    public bool IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public string? DateModified { get; set; }

    public virtual CustomerProfile CustomerProfile { get; set; } = null!;

    public virtual Products Product { get; set; } = null!;
}
