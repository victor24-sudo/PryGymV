using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PryGymV.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PryGymVContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PryGymVContext") ?? throw new InvalidOperationException("Connection string 'PryGymVContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Acceso/Index";   //Formalario de Logeo
        option.ExpireTimeSpan = TimeSpan.FromMinutes(5);  //Tiempo de espera 
        option.AccessDeniedPath = "/Home/Privacy";  //Acceso Denegado
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
    pattern: "{controller=Acceso}/{action=Index}/{id?}");

app.Run();
