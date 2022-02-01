using Microsoft.EntityFrameworkCore;
using JWTAuthentication.WebApi.Settings;
using JWTAuthentication.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JWTAuthentication.WebApi.Contexts;
using JWTAuthentication.WebApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FOApi;


var builder = WebApplication.CreateBuilder(args);


// add services to DI container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

 
    //Get jwt settings
    services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

    //User Manager Service
    services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
    services.AddScoped<IUserService, UserService>();


    //Adding DB Context with MSSQL
    //services.AddDbContext<ApplicationDbContext>(options =>
    //    options.UseSqlServer(
    //        builder.Configuration.GetConnectionString("DefaultConnection"),
    //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

    //services.AddDbContext<FoodorderingContext>(options =>
    // options.UseSqlServer(
    //     builder.Configuration.GetConnectionString("FoodorderingContext"),
    //     b => b.MigrationsAssembly(typeof(FoodorderingContext).Assembly.FullName)));
    var connectionStringDefault = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(connectionStringDefault, ServerVersion.AutoDetect(connectionStringDefault));
    });

    var connectionStringFood = builder.Configuration.GetConnectionString("FoodorderingContext");
    builder.Services.AddDbContext<FoodorderingContext>(options =>
    {
        options.UseMySql(connectionStringFood, ServerVersion.AutoDetect(connectionStringFood));
    });

    //Adding Athentication - JWT
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })

        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                

                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
            };
        });
}
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
{           // userservice
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await ApplicationDbContextSeed.SeedEssentialsAsync(userManager, roleManager);
    }
catch (Exception ex)
{
var logger = services.GetRequiredService<ILogger<Program>>();
logger.LogError(ex, "An error occurred while seeding the database.");
}
}


{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());


}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
