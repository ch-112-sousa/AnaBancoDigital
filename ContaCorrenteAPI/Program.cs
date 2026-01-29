using ContaCorrenteAPI.Authentication;
using ContaCorrenteAPI.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string secret = builder.Configuration["JwtSettings:Secret"];
string audience = builder.Configuration["JwtSettings:Audience"];
string issuer = builder.Configuration["JwtSettings:Issuer"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   
    .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = issuer,
           ValidAudience = audience,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
       };

   });

builder.Services.AddAuthorization();


var strConnection = builder.Configuration.GetConnectionString("ContaConnection");
builder.Services.AddTransient<IDbConnection>(provider => new SqlConnection(strConnection));
builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

int expiryMinutes = Convert.ToInt32(builder.Configuration["JwtSettings:ExpiryMinutes"]);
builder.Services.AddScoped<JwtService>(sp => new JwtService(secret, expiryMinutes, audience, issuer));

builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();


app.Run();