using Bogus;

namespace BackendService;

public class FakeDatabase
{
    private const int TotalCount = 10000;

    private readonly List<Customer> _customers;

    public IQueryable<Customer> Customers => _customers.AsQueryable();

    public FakeDatabase()
    {
        var id = 0;
        var testCustomers = new Faker<Customer>()
            .CustomInstantiator(f => new Customer
            {
                Id = id++,
                GivenName = f.Name.FirstName(),
                Lastname = f.Name.LastName(),
                Description = f.Lorem.Slug(),
                City = f.Address.City(),
                Country = f.Address.Country(),
                Street = f.Address.StreetAddress(),
                ZipCode = f.Address.ZipCode()
            });
        _customers = testCustomers.Generate(TotalCount);
    }
}
