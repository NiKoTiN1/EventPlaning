using EventPlanning.BusinessLogic.Interfaces;
using EventPlanning.BusinessLogic.Services;
using EventPlanning.DataAccess;
using EventPlanning.DataAccess.Interfaces;
using EventPlanning.DataAccess.Repositories;
using EventPlanning.Domain.Models;
using EventPlanning.ViewModels.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"), options => options.EnableRetryOnFailure()));

builder.Services.AddIdentity<Account, IdentityRole<Guid>>().AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Authentication:ISSUER"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Authentication:AUDIENCE"],
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:KEY"])),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });

builder.Services.AddAutoMapper(typeof(AccountMappingProfile));
builder.Services.AddAutoMapper(typeof(TokenMappingProfile));

builder.Services.AddTransient(typeof(IRefreshTokenRepository), typeof(RefreshTokenRepository));
builder.Services.AddTransient(typeof(IGuestRepository), typeof(GuestRepository));

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

CreateDbIfNotExists(app);

app.Run();


static async void CreateDbIfNotExists(IHost host)
{
    using (IServiceScope scope = host.Services.CreateScope())
    {
        IServiceProvider services = scope.ServiceProvider;
        try
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            IConfiguration configuration = services.GetRequiredService<IConfiguration>();
            await DbInitializer.SeedRoles(roleManager).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}