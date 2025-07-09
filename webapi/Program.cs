using BankReportingLibrary.Models;
using BankReportingLibrary.Utils;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using webapi.Code;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using BankReportingDb.Context;
using Microsoft.EntityFrameworkCore;

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

// Add db
var cfgConn = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
var cfgLib = builder.Configuration.GetSection("CfgLib").Get<CfgLib>();
builder.Services.AddDbContext<BankReporingContext>(dbContxOpt =>
    dbContxOpt.UseLazyLoadingProxies(true)
            .UseSqlServer(cfgConn.DB, opt =>
            {
                if (cfgLib != null)
                {
                    opt.CommandTimeout(cfgLib.DbCmdTimeout);
                    opt.MaxBatchSize(cfgLib.EFBatchSize);
                }
            }));

// Add BL
builder.Services.AddBankReportingLibrary(new LibraryConfig
{
    LibApp = cfgLib
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.UseRequestCulture();

app.Run();
