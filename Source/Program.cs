using AspNetCore;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using Source.Data;
using Source.Mappings;
using Source.Middlewares;
using Source.Models;
using Source.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(
       op => op.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddIdentity<AppUser,IdentityRole>(options => {
    options.Password.RequiredUniqueChars = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var mapConfig = new MapperConfiguration(c =>
{
    c.AddProfile<AutoMapperProfile>();
});

var mapper = mapConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserManager,UserManager>();

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

app.UseMiddleware<AuthMiddleware>();


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


var container = app.Services.CreateScope();
var userManager = container.ServiceProvider.GetRequiredService<UserManager<AppUser>>(); 
var roleManager = container.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(); 

if(!await roleManager.RoleExistsAsync("Admin"))
{
    var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
    if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
}

var user = await userManager.FindByEmailAsync("admin@admin.com");
if(user is null)
{
    user = new AppUser()
    {
        UserName = "admin",
        Email = "admin@admin.com",
        FullName = "Admin",
        EmailConfirmed = true
    };
    var result = await userManager.CreateAsync(user, "Admin123");
    if(!result.Succeeded) throw new Exception(result.Errors.First().Description);
    result = await userManager.AddToRoleAsync(user, "Admin");
}


app.Run();
