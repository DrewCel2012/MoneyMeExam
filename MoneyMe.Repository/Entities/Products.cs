using System;
using System.Collections.Generic;

namespace MoneyMe.Repository.Entities;

public partial class Products
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<CustomerProfileLoanApplication> CustomerProfileLoanApplication { get; set; } = new List<CustomerProfileLoanApplication>();
}
