using FluentValidation;
using FluentValidation.AspNetCore;
<<<<<<< HEAD
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopApp.Apps.AdminApp.Validators.ProductValidators;
using ShopApp.Data;
using ShopApp.Entities;
using ShopApp.Profiles;
using ShopApp.Services.Implementations;
using ShopApp.Services.Interfaces;
using ShopApp.Settings;
using System.Text;

=======
using Microsoft.EntityFrameworkCore;
using ShopApp.Apps.AdminApp.Validators.ProductValidators;
using ShopApp.Data;
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

<<<<<<< HEAD
=======
// Add services to the container.

>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<ProductCreateValidator>();

<<<<<<< HEAD
builder.Services.AddFluentValidationRulesToSwagger();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password = new()
    {
        RequiredLength = 10,
        RequireUppercase = true,
        RequireLowercase = true,
        RequireDigit = true,
        RequireNonAlphanumeric = true
    };

}).AddDefaultTokenProviders().AddEntityFrameworkStores<ShopAppContext>();



builder.Services.AddDbContext<ShopAppContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(opt =>
      opt.AddProfile(new MapperProfile(new HttpContextAccessor()))
);


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
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.Configure<JwtSettings>(config.GetSection("Jwt"));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ShopApp",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

=======
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShopAppContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
<<<<<<< HEAD
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
=======

>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
app.UseAuthorization();

app.MapControllers();

app.Run();
