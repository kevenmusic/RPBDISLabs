using MarriageAgency.Data;
using MarriageAgency.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MarriageAgency.Services;
using MarriageAgency.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MarriageAgencyContext>(options =>
    options.UseSqlServer(connectionString));
IServiceCollection services = builder.Services;
services.AddRazorPages().AddRazorRuntimeCompilation();
services.AddDatabaseDeveloperPageExceptionFilter();

services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MarriageAgencyContext>();

services.AddTransient<IServiceService, ServiceService>();
// ���������� �����������
services.AddMemoryCache();
// ���������� ��������� ������
services.AddDistributedMemoryCache();
services.AddSession();

//������������� MVC
services.AddControllersWithViews();
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// ��������� ��������� ������
app.UseSession();

// ��������� ��������� middleware �� ������������� ���� ������ � ���������� ������������� ����
app.UseDbInitializer();

// ��������� ��������� middleware ��� ���������� ����������� � ��������� ������ � ���
app.UseOperatinCache("Services 10");

//�������������
app.UseRouting();

app.UseAuthorization();

// ������������� ������������� ��������� � ������������� 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
