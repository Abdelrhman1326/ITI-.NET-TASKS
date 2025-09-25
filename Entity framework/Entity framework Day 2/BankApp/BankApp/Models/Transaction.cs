using System;
using System.Collections.Generic;

namespace BankApp.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string TransactionType { get; set; } = null!;

    public int AccountId { get; set; }

    public virtual Bankaccount Account { get; set; } = null!;
}
