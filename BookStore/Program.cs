using System;
using System.Configuration;
using BookStore.Models;
using BookStore.Models.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBookStoreRepository<Author> ,AuthorDBRepo>();
builder.Services.AddScoped<IBookStoreRepository<Book> ,BookDBRepo>();
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));

var app = builder.Build();
RunMigrations(app);

void RunMigrations(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();
        db.Database.Migrate();
    }
}

app.UseRouting();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();
