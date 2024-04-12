using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zeff_Food.Data;

//uso de identity
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Configuracion estado de sesion
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Implementacion Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


 //Para poder utilizar los modelos en la base de datos
// builder.Services.AddDbContext<ApplicationDbContext>(options =>

//para utilizar base de datos de Postgrest
//     options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQLConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

//para utilizar base de datos de VScode
//options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found."))); 

var connectionstring = builder.Configuration.GetConnectionString("PostgresSQLConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(connectionstring));
builder.Services.AddDbContext<ApplicationDbContextIdentity>(
    options => options.UseNpgsql(connectionstring));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();


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
app.UseAuthentication();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
