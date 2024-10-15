using MongoDB.Driver;
using MongoDB.Bson;

public class BankService
{
    private readonly IMongoCollection<BankAccount> _bankAccounts;

    public BankService(IMongoClient client)
    {
        var database = client.GetDatabase("Bank");
        _bankAccounts = database.GetCollection<BankAccount>("BankAccount"); 
    }

    public BankService(IMongoCollection<BankAccount> bankAccountCollection)
    {
        _bankAccounts = bankAccountCollection;
    }

    public BankAccount GetAccountById(string id)
    {
        return _bankAccounts.Find(account => account.Id == id).FirstOrDefault();
    }

    public void InsertAccount(BankAccount account)
    {
        _bankAccounts.InsertOne(account);
    }

    public void UpdateAccount(BankAccount account)
    {
        var filter = Builders<BankAccount>.Filter.Eq(a => a.Id, account.Id);
        _bankAccounts.ReplaceOne(a => a.Id == account.Id, account);
    }

    // Extra uppgift A
    public int GetTotalBalance()
    {
        var totalBalanceResult = _bankAccounts.Aggregate()
            .Group(new BsonDocument { { "_id", BsonNull.Value }, { "totalBalance", new BsonDocument("$sum", "$TotalBalance") } })
            .FirstOrDefault();

        return totalBalanceResult != null ? totalBalanceResult["totalBalance"].AsInt32 : 0;
    }
}