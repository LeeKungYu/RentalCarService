using Application.RentalCar;
using Application.RentalCar.Repository;
using Infrastructure.RentalCar;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration.GetSection("AppSettings"); // 也可以用var 但可讀性相對低

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(cookieOption =>
    {
        //cookieOption.LoginPath = "/Account/Login"; //不要寫死 讓程式去讀appsettings.json        
        cookieOption.LoginPath = configuration.GetValue<string>("LoginPage");
        cookieOption.ExpireTimeSpan = TimeSpan.FromMinutes(configuration.GetValue<int>("TimeoutMinutes"));
    });

builder.Services.AddDbContext<SaleCarDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RentalCarConn"));
});

//越新的放越後面 但要注意介面與Repository的擺放位置以及Services的執行順序。

builder.Services.AddScoped<IQueryRentalCarUseCase, RentalCarRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<RentalCarServices>();
builder.Services.AddScoped<AccountServices>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
