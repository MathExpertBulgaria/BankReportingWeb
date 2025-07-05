using BankReportingLibrary.Models;
using BankReportingLibrary.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add WebAPI
builder.Services.AddControllers()
    .AddXmlSerializerFormatters()
    .AddXmlOptions(xO => { });

var connStrCfg = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
builder.Services.AddBankReportingLibrary(new LibraryConfig
{
    ConnectionStringsData = connStrCfg
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
