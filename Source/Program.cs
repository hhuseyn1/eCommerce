using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(
       op => op.UseSqlServer(builder.Configuration.GetConnectionString("default")));

var mapConfig = new MapperConfiguration(c =>
{
    c.AddProfile<AutoMapperProfile>();
});

var mapper = mapConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


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
app.UseEndpoints(endpoint =>
{
    endpoint.MapAreaControllerRoute(
        name: "foradminarea",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}"
        );
    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
