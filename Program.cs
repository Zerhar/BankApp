using System;
using MongoDB.Driver;

public class Program
{
    public static void Main(string[] args) 
    {
        var client = new MongoClient("mongodb://localhost:27017");

        var bankService = new BankService(client);

        Console.WriteLine("Welcome to the bank\n");

        while (true)
        {
            Console.Write("Enter account name [empty to exit]: ");
            string accountName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountName))
                break;

            var account = bankService.GetAccountById(accountName);

            if (account == null)
            {
                account = new BankAccount(accountName, 0);
                bankService.InsertAccount(account);
            }

            int amount;

            while (true)
            {
                Console.Write("Please enter amount: ");
                if (int.TryParse(Console.ReadLine(), out amount))
                {
                    var totalBankBalance = bankService.GetTotalBalance();

                    if (account.TotalBalance + amount < 0)
                    {
                        Console.WriteLine($"Error: The entered amount exceeds the total balance of the bank: {totalBankBalance}");
                        Console.WriteLine("Please try again.");
                    }
                    else
                    {
                        account.TotalBalance += amount;
                        account.Transactions.Add(amount);
                        bankService.UpdateAccount(account);

                        Console.WriteLine($"Current balance: {account.TotalBalance}");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please try again."); 
                }
            }
        }

        ShowTotalBalance(bankService);
    }

    static void ShowTotalBalance(BankService bankService)
    {
        var totalBalance = bankService.GetTotalBalance();
        Console.WriteLine($"\nBank total balance: {totalBalance}\n");
        Console.WriteLine("Please press [ENTER] to exit");
        Console.ReadLine();
    }
}
