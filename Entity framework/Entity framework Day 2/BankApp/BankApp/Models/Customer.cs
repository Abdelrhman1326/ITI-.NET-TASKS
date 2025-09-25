using System;
using System.Collections.Generic;

namespace BankApp.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Bankaccount> Bankaccounts { get; set; } = new List<Bankaccount>();
}
