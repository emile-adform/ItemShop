using DbUp;
using ItemShop.Clients;
using ItemShop.Contexts;
using ItemShop.Interfaces;
using ItemShop.Middlewares;
using ItemShop.Repositories;
using ItemShop.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//EF
string dbConnectionString = builder.Configuration.GetConnectionString("PostgreConnection");
builder.Services.AddDbContext<DataContext>(o => o.UseNpgsql(dbConnectionString));

builder.Services.AddTransient<ItemService>();
builder.Services.AddTransient<IEFItemRepository, EFItemRepository>();
builder.Services.AddTransient<JsonPlaceholderClient>();
builder.Services.AddTransient<UserService>();

builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(Program));

//builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(dbConnectionString));

//EnsureDatabase.For.PostgresqlDatabase(dbConnectionString);
//var upgrader =
//    DeployChanges.To.PostgresqlDatabase(dbConnectionString)
//    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
//    .LogToNowhere().Build();
//var result = upgrader.PerformUpgrade();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
