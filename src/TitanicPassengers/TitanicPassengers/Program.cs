using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.AppDbContext.Roles;
using TitanicPassengers.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddRazorPages();

var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();


builder.Services.AddDbContext<UserAppDbContext>(
                opt => opt.UseNpgsql(config.GetSection("Postgresql").GetConnectionString("UserConnection")));
builder.Services.AddDbContext<AdminAppDbContext>(
                opt => opt.UseNpgsql(config.GetSection("Postgresql").GetConnectionString("AdminConnection")));
builder.Services.AddDbContext<GuestDbContext>(
                opt => opt.UseNpgsql(config.GetSection("Postgresql").GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<AppDbContextFactory>();

builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<ParticipantRepository>();
builder.Services.AddTransient<ParticipantStatusRepository>();
builder.Services.AddTransient<PassengerRepository>();
builder.Services.AddTransient<CloseRelativeRepository>();
builder.Services.AddTransient<LifeboatRepository>();
builder.Services.AddTransient<BodyRepository>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");
builder.Services.AddAuthorization();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();


var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Participants}/{action=GetAllParticipants}");


app.Run();

