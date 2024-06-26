using Microsoft.EntityFrameworkCore;
using VMController.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ContextoUsuario>(
    options => options.UseSqlServer("server=COMPUTADOR; DataBase=Usuarios; trusted_connection=true; TrustServerCertificate=True;")
);

builder.Services.AddDbContext<ContextoVM>(
    options => options.UseSqlServer("server=COMPUTADOR; DataBase=VirtualMachines; trusted_connection=true; TrustServerCertificate=True;")
);


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
    pattern: "{controller=Usuarios}/{action=Login}/{id?}");

app.Run();
