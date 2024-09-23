using System;
using System.Collections.Generic;

namespace MoneyMe.Repository.Entities;

public partial class MobileBlacklist
{
    public int Id { get; set; }

    public string Mobile { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
