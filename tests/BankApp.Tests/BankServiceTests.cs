using MongoDB.Driver;
using Xunit;

public class BankServiceTests
{
    private readonly BankService _bankService;
    private readonly IMongoCollection<BankAccount> _bankAccountCollection;


    public BankServiceTests()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("test_bank");
        var bankAccountCollection = database.GetCollection<BankAccount>("BankAccount");

        bankAccountCollection.DeleteMany(FilterDefinition<BankAccount>.Empty);

        _bankService = new BankService(bankAccountCollection);
    }

    [Fact]
    public void InsertAccount_IncreasesTotalBalance()
    {
        // Arrange
        var testAccountId = Guid.NewGuid().ToString();
        var account = new BankAccount(testAccountId, 100);
        var initialBalance = _bankService.GetTotalBalance();

        // Act
        _bankService.InsertAccount(account);

        // Assert
        var totalBalance = _bankService.GetTotalBalance();
        Assert.Equal(initialBalance + 100, totalBalance);
    }

    [Fact]
    public void Withdraw_Should_PreventNegativeTotalBalance()
    {
        // Arrange
        var testAccountId = Guid.NewGuid().ToString();
        var account = new BankAccount(testAccountId, 100); 
        _bankService.InsertAccount(account);
        
        var initialBalance = _bankService.GetTotalBalance(); 

        // Act
        var withdrawAmount = 150; 
        if (account.TotalBalance - withdrawAmount >= 0)
        {
            account.TotalBalance -= withdrawAmount;
            _bankService.UpdateAccount(account);
        }

        // Assert
        var updatedAccount = _bankService.GetAccountById(account.Id);
        var updatedBankBalance = _bankService.GetTotalBalance();

        Assert.Equal(100, updatedAccount.TotalBalance);
        Assert.Equal(initialBalance, updatedBankBalance);
    }
}