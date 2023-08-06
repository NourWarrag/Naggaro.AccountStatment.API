using Bogus;
using Naggaro.AccountStatment.Domain.Entities;

namespace Naggaro.AccountStatment.Application.UnitTest;
public class FackeData
{

    public static IEnumerable<Account> FakeAccounts()
    {
        Randomizer.Seed = new Random(8675309);
        var faker = new Faker();
        return Enumerable.Range(1, 300).Select( x => new Domain.Entities.Account {             
                AccountNumber = faker.Finance.Account(),
                AccountType = faker.Finance.AccountName(),
                AccountStatments = Enumerable.Range(1, 300).Select(i =>
                new Domain.Entities.AccountStatement
                {
                    Amount = faker.Finance.Amount(1, 5000),
                    DateField = faker.Date.Between(DateTime.Now, DateTime.Now.AddMonths(-3))
                }).ToList()
            });
        

       
    }
}

