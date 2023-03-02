using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using LoadingTemplateTest.Services;

var builder = WebApplication.CreateBuilder(args);

// TODO: adjust backend URI
builder.Services.AddGrpcClient<Customers.CustomersClient>(opts => opts.Address = new Uri("https://localhost:7891"));
builder.Services.AddTransient<CustomerService>();

// configure Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// configure Blazorise
builder.Services
    .AddBlazorise( options =>
    {
        options.Immediate = false;
    } )
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
