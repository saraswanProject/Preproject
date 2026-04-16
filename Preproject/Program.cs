using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Preproject.Helpers;
using System.Collections.Generic;
using System.Text;
using TransactionRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔐 Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var keyString = jwtSettings["Key"] ?? throw new Exception("JWT Key is missing");
var key = Encoding.UTF8.GetBytes(keyString);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// ✅ Add Authorization
builder.Services.AddAuthorization();

// Your existing services
builder.Services.AddScoped<ITransactionRepository>(s =>
    new TransactionRepo(
        builder.Configuration.GetValue<string>("TrnRepoDllToken"),
        builder.Configuration.GetConnectionString("DefaultConnection"),
        builder.Configuration.GetValue<string>("TOKENID_ENABLED")));

builder.Services.AddScoped<IDbHelperService, DbHelperService>();
builder.Services.AddTransient<IMaintainenceService, MaintainenceService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();
app.UsePathBase("/utility_api");

// 🛠️ CHANGED: Configure Swagger to prepend the Nginx reverse proxy path
app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        swaggerDoc.Servers = new List<OpenApiServer>
        {
            new OpenApiServer { Url = "/utility_api" }
        };
    });
});

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

if (!app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c =>
    {
        // Use the relative path trick here:
        c.SwaggerEndpoint("./v1/swagger.json", "TOP UP V1");
    });
}
else
{
    app.UseSwaggerUI(c =>
    {
        // And here:
        c.SwaggerEndpoint("./v1/swagger.json", "TOP UP V1");
    });
}
app.UseHttpsRedirection();

// 🔐 Order matters
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();