using QLRapChieuPhim.Model;
using QLRapChieuPhim.Models;
using QLRapChieuPhim.Reponsitory;
using QLRapChieuPhim.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("QlbanValiContext");
builder.Services.AddDbContext<QlrapChieuPhimContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<IQuocGiaRepository, QuocGiaRepository>();

builder.Services.AddDbContext<QlrapChieuPhimContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<IViTriReponsiory, ViTriReponsitory>();

builder.Services.AddDbContext<QlrapChieuPhimContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<IDangPhimRepository, DangPhimRepository>();

builder.Services.AddDbContext<QlrapChieuPhimContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<IQuocGiaPhimRepository, QuocGiaPhimRepository>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// mới
//builder.Services.AddControllersWithViews();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
