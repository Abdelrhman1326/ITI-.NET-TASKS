using System;
using System.Collections.Generic;

namespace BankApp.Models;

public partial class Bankaccount
{
    public int AccountId { get; set; }

    public decimal Balance { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
