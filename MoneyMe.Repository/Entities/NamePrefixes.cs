using System;
using System.Collections.Generic;

namespace MoneyMe.Repository.Entities;

public partial class NamePrefixes
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<Customers> Customers { get; set; } = new List<Customers>();
}
