using ContaCorrenteAPI.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var strConnection = builder.Configuration.GetConnectionString("ContaConnection");

builder.Services.AddScoped<IDbConnection>(provider => new SqlConnection(strConnection));
builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();


builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();