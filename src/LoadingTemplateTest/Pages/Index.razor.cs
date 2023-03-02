using Blazorise.DataGrid;
using LoadingTemplateTest.Services;
using Microsoft.AspNetCore.Components;

namespace LoadingTemplateTest.Pages;

public partial class Index
{
    private List<Customer>? _items;
    private int? _totalItems;
    private string? _errorMessage;

    [Inject]
    private CustomerService Service { get; set; } = null!;

    private bool HasError => !string.IsNullOrWhiteSpace(_errorMessage);

    private async Task OnReadData(DataGridReadDataEventArgs<Customer> arg)
    {
        try
        {
            var result = await Service.Query(arg.Page, arg.PageSize, arg.CancellationToken);
            _totalItems = result.TotalItems;
            _items = result.Items;
            _errorMessage = null;
        }
        catch (Exception exception)
        {
            _errorMessage = exception.Message;
        }
    }
}
