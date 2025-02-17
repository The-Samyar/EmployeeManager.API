using System.Text;
using EmployeeManager.API.Contexts;
using EmployeeManager.API.Repositories.Implementations;
using EmployeeManager.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//builder.Services.AddDbContext<EmployeeDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("MiladConnection")));
builder.Services.AddDbContext<EmployeeDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"])
                ),
        };
    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Open API V1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
