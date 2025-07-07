using BankReportingLibrary.Models;
using BankReportingLibrary.Utils;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add WebAPI
builder.Services.AddControllers()
    .AddControllersAsServices()
    .AddJsonOptions(jo =>
    {
        jo.JsonSerializerOptions.AllowTrailingCommas = true;
        jo.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        jo.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
        jo.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        jo.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        jo.JsonSerializerOptions.WriteIndented = false;
        jo.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        jo.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        jo.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


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
