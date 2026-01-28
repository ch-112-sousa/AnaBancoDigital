using ContaCorrenteAPI.Domain.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICreateContaCorrenteHandler, CreateContaCorrenteHandler>();


var app = builder.Build();



app.UseHttpsRedirection();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
