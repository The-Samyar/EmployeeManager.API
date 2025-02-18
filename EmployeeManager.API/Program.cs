using System.Text;
using EmployeeManager.API.Contexts;
using EmployeeManager.API.Repositories.Implementations;
using EmployeeManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<EmployeeDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MiladConnection")));
//builder.Services.AddDbContext<EmployeeDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var secrectKey = builder.Configuration.GetSection("Authentication:SecretForKey");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "your_issuer",
        ValidAudience = "your_audience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrectKey.Value)),
    };
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users Management", Version = "v1", Description = "Users Management Rest Api Services - 2024" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
   {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
   });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UsersManagement v1");
    });

}


app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
