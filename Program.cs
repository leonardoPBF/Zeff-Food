using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zeff_Food.Data;

//uso de identity
using Microsoft.AspNetCore.Identity;
using Zeff_Food.Models.Entitys;
using Zeff_Food.Models;

//para Emailsender
using Zeff_Food.Service.Interfaces;
using Zeff_Food.Service.classes;

//api
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Configuracion estado de sesion
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Implementacion Identity
    var connectionstring = builder.Configuration.GetConnectionString("PostgresSQLConnection");
    //builder para entidades de negocio
    builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseNpgsql(connectionstring));
    //builder para ASP.identity
    builder.Services.AddDbContext<ApplicationDbContextIdentity>(
        options => options.UseNpgsql(connectionstring));

    //builder para aumentar variables a nuestro ASP.net.user
    builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()  
        .AddEntityFrameworkStores<ApplicationDbContextIdentity>();
//<------>


//Implementacion EmailSender
   
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    // Configurar variables de entorno para sobreescribir las credenciales
    builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
  
    builder.Services.AddTransient<IEmailSender, EmailSender>();


    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
    });
//<------>


// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "API for Zeff Food",
        Contact = new OpenApiContact
        {
            Name = "Zeff Food Support",
            Email = "support@zefffood.com",
            Url = new Uri("https://zefffood.com/contact")
        }
    });
});

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

app.UseCors("AllowAllOrigins");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

    //configuracion de roles
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "User", "Employee" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    //configuracion admin
    using (var scope = app.Services.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();  

        string email = "admin@admin.com";  
        string password = "Test1234,";  

        if(await userManager.FindByEmailAsync(email) == null)  
        {  
            var user = new Usuario();  
            user.UserName = email;  
            user.Email = email ; 
            user.EmailConfirmed = true;             
            await userManager.CreateAsync(user, password);  
            await userManager.AddToRoleAsync(user, "Admin");          
        }
    }


app.Run();
