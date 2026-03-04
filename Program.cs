using Proyecto_Grupal.Data;
using Proyecto_Grupal.Repositories;
using Proyecto_Grupal.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Repositorios
builder.Services.AddTransient<TitlesRepository>();
builder.Services.AddTransient<SalariesRepository>();
builder.Services.AddTransient<EmployeesRepository>();
builder.Services.AddTransient<DepartmentsRepository>();
builder.Services.AddTransient<DeptEmpRepository>();
builder.Services.AddTransient<DeptManagerRepository>();

// Servicios
builder.Services.AddTransient<SalaryAuditService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
