#pragma warning disable CA5394 // Random is insecure

using Grpc.Core;

namespace BackendService.Services;

public class CustomerService : Customers.CustomersBase
{
    private readonly FakeDatabase _database;
    private readonly Random _random = Random.Shared;

    private static int _requestCount;

    public CustomerService(FakeDatabase database) => _database = database;

    public override async Task<QueryCustomersResponse> QueryCustomers(
        QueryCustomersRequest request,
        ServerCallContext context)
    {
        var cancellationToken = context.CancellationToken;

        // simulate network delay
        var millisecondsDelay = _random.Next(300);
        await Task.Delay(millisecondsDelay, cancellationToken);

        var count = Interlocked.Increment(ref _requestCount);
        Console.WriteLine("{0}: received request for {1} items, served after {2}ms",
            count, request.PageSize, millisecondsDelay);

        var page = Math.Max(request.Page, 1);
        var pageSize = Math.Max(request.PageSize, 1);

        var totalItems = _database.Customers.Count();
        var items = _database.Customers
            .Skip((request.Page - 1) * request.PageSize)
            .Take(pageSize);

        return new QueryCustomersResponse
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            Items = { items }
        };
    }
}
