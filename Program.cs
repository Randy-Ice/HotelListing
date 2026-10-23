using DotNetEnv;
using HotelListing.Data;
using HotelListing.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Odata
builder.Services.AddControllers()
    .AddOData(options =>
    options.Count().Filter().OrderBy().Count().SetMaxTop(100)
    );

//Connection string
Env.Load();
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

//Logging
var logger = new LoggerConfiguration()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Hour)
    .MinimumLevel.Information()
    .CreateLogger();
builder.Logging.AddSerilog(logger);
//builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//Database
builder.Services.AddDbContext<ProjectDB>(options =>
    options.UseSqlServer(connectionString)
);
//Auth
builder.Services.AddDbContext<AuthDB>(options =>
    options.UseSqlServer(connectionString)
);
builder.Services.AddAuthentication();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AuthDB>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandleMiddleware>();
app.MapIdentityApi<IdentityUser>();

app.MapControllers();

app.Run();
