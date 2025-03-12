using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCAPP.Business.Services;
using MVCAPP.DataAccess;
using MVCAPP.DataAccess.Repositories;
using MVCAPP.Domain.Models.Abstractions.Books;
using MVCAPP.Domain.Models.Abstractions.Writers;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.Infrastructure.Abstractions;
using MVCAPP.Infrastructure.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region DbContext

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options
        .UseNpgsql(builder.Configuration.GetConnectionString(nameof(ApplicationDbContext)+"POSTGRES"))
        .UseLazyLoadingProxies();
});

#endregion

#region Repositories

builder.Services.AddScoped<IBooksRepository, BooksRepository>();

#endregion

#region Services

builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IFileManager, FileManager>();

#endregion

#region Identity

builder.Services.AddIdentity<ApiUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
});

#endregion

WebApplication app = builder.Build();

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
    name: "areas",
    pattern: "{area:exists}/{controller=Books}/{action=Index}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}");


app.Run();