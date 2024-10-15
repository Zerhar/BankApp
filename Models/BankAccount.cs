using System.Collections.Generic;

public class BankAccount
{
    public string Id { get; set; }
    public int TotalBalance { get; set; }
    public List<int> Transactions { get; set; }

    public BankAccount(string id, int initialBalance)
    {
        Id = id;
        TotalBalance = initialBalance;
        Transactions = new List<int>();
    }
}

