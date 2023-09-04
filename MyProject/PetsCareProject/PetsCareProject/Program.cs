using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PetsCareProject.Models;

var builder = WebApplication.CreateBuilder(args);

//Tạo biến kết nối với đường dẫn mặc định ở file Setting
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DbPetsCare>(optionsAction: options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    //
    endpoints.MapControllerRoute(
     name: "Product",
     pattern: "san-pham/",
     new { Controller = "Home", Action = "Product" }
   );
    //
    endpoints.MapControllerRoute(
     name: "Cart",
     pattern: "gio-hang/",
     new { Controller = "Home", Action = "Cart" }
   );
    //
    endpoints.MapControllerRoute(
     name: "Service",
     pattern: "dich-vu/",
     new { Controller = "Home", Action = "Service" }
   );
    //
    endpoints.MapControllerRoute(
     name: "Blog",
     pattern: "bai-viet/",
     new { Controller = "Home", Action = "Blog" }
   );
    //
    endpoints.MapControllerRoute(
     name: "BookingCare",
     pattern: "dat-lich-kham-benh/",
     new { Controller = "Home", Action = "BookingCare" }
   );
    //
    endpoints.MapControllerRoute(
    name: "product-of-type",
    pattern: "product-of-type/",
    defaults: new { controller = "Home", action = "ProductOfType" }
    );
    //
    endpoints.MapControllerRoute(
    name: "tim-kiem-san-pham",
    pattern: "tim-kiem-san-pham/",
    defaults: new { controller = "Home", action = "SearchProduct" }
    );
    //search
    endpoints.MapControllerRoute(
    name: "tim-kiem-bai-viet",
    pattern: "tim-kiem-bai-viet/",
    defaults: new { controller = "Home", action = "SearchPost" }
    );
    //admin
    

});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
