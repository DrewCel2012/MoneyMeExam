using System;
using System.Collections.Generic;

namespace MoneyMe.Repository.Entities;

public partial class Customers
{
    public int Id { get; set; }

    public decimal AmountRequired { get; set; }

    public int Term { get; set; }

    public int TitleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Mobile { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual NamePrefixes Title { get; set; } = null!;
}
