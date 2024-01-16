using Microsoft.AspNetCore.Authentication.Cookies;
using TicketTrackingSystem.Services;
using TicketTrackingSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind("MongoDB",new MongoDBSettings());

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBugRepository, BugRepository>();
builder.Services.AddScoped<IBugService, BugService>();
builder.Services.AddSingleton<IMongoDBContext, MongoDBContext>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "bug",
        pattern: "Bugs/{action=Index}/{id?}",
        defaults: new { controller = "Bugs" })
    .RequireAuthorization("RequireQA");

app.MapControllerRoute(
        name: "bugResolution",
        pattern: "Bugs/{action=Resolve}/{id?}",
        defaults: new { controller = "Bugs" })
    .RequireAuthorization("RequireRD");

app.Run();