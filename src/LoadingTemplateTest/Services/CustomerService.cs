namespace LoadingTemplateTest.Services;

public class CustomerService
{
    private readonly Customers.CustomersClient _customersClient;

    public CustomerService(Customers.CustomersClient customersClient)
    {
        _customersClient = customersClient;
    }

    public Task<CustomerResult> Query(int page, int pageSize, CancellationToken cancellationToken)
    {
        if (page < 0) throw new ArgumentOutOfRangeException(nameof(page));
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

        cancellationToken.ThrowIfCancellationRequested();

        // split argument checks (throw) and async state machine
        async Task<CustomerResult> Get()
        {
            var response = await _customersClient
                .QueryCustomersAsync(new QueryCustomersRequest
                {
                    Page = page,
                    PageSize = pageSize
                }, cancellationToken: cancellationToken);

            return new CustomerResult(response.TotalItems, response.Items.ToList());
        }

        return Get();
    }
}
