# PROYECTO Zeff-Food

Descripcion: empresa ecommers de comida rapida

### Tecnologia utilizada
* ASP.netcore v.8.0 en Visual Estudio Code. 
* Base de datos postgrest v.16.2.
* pgAdmin v.8.4..

### Extensiones:
-> https://marketplace.visualstudio.com/items?itemName=doggy8088.netcore-extension-pack&ssr=false#review-details
*guia para editar con formatos el archivo md (Readme):https://programminghistorian.org/es/lecciones/introduccion-a-markdown*

# Desarrollo del proyecto

## Implementacion de entidades y creacion base de datos:

### Entidades a usar

- Usuario (Nombre, email, numero celular, año de nacimiento, total gastado, Historial pedidos, Fecha creacion de cuenta)
- Producto (Nombre, descripcion, imagen, precio, stock)
- Inventario (Nombre, Stock, Proveedor, Fecha caducidad, Fecha de compra)
- Carrito ()
- Articulo Carro ()
- Pago ()
- Factura (Fecha de pedidio, lista de productos)

### Comandos git (Inicializacion del proyecto):
```
> git clone https://github.com/leonardoPBF/Zeff-Food.git
//Comando creacion proyecto ASP.net
> dotnet new mvc
> cd Zeff-Food  
```

### Manejo de branches
```
git commit -m "initial" \\genera mensaje del update
git branch -M main \\Creacion rama principal
git branch -M modelos \\Creacion rama secundaria 
```

### Actualizar Branch:
```
git add .
git commit -m "Nueva entidad"
git push origin modelos
```

### NuGet Gallery de la aplicacion 
```
> dotnet add package Microsoft.EntityFrameworkCore
> dotnet add package Microsoft.Extensions.DependencyInjection
> dotnet add package Microsoft.EntityFrameworkCore.Design
> dotnet add package Microsoft.EntityFrameworkCore.Tools
> dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL \\para la base de datos postgrest
> dotnet add package Microsoft.EntityFrameworkCore.Sqlite \\SQLite
```

### Creacion de modelos y conexion base de datos

1. Creacion carpeta Modelos-Entidades(estructuramos los modelos)

2. Creacion carpeta Data, dentro de ella -> ApplicationDbContext

3. Configuracion conectionString en appsettings.json
```
   "ConnectionStrings": {
    "DefaultConnection": "DataSource=zeff_food.db;Cache=Shared", //para base de datos SQLite
    "PostgresSQLConnection": "host=localhost; port=5432; Database=Zeff_Food; Username=postgres; password=******"
  }
```

4. Configuracion program.cs, creacion de builder para utilizar Postgrest
```
    //Para poder utilizar los modelos en la base de datos
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        //opcion 1;
        //para utilizar base de datos de Postgrest
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQLConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));
        
        opcion 2;
        //para utilizar base de datos de VScode-Sqlite
        /* options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found."))); */
    
```

5. Ejecutamos los siguientes comandos para poder generar las migraciones de la base de datos, para luego actualizar la base de datos seleccionada:
```
> dotnet ef migrations add "initial_migration" 
> dotnet ef database update
```
Recomendaciones: Si queremos actualizar alguna variable de alguna entidad lo ideal sera ejecutar el siguiente comando "dotnet ef migrations remove" para poder eliminar nuestra antigua migracion, ademas de ello deberemos ponerle un nuevo nombre a nuestra migracion
```
> dotnet ef migrations remove
> dotnet ef migrations add "nueva_migracion" 
> dotnet ef database update
```

## Implementacion identity
Sistema de autenticación y autorización robusto y flexible.

### NuGet Gallery de identity
```
> dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
> dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
> dotnet add package Microsoft.AspNetCore.Identity.UI
> dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

```

### Codegenerator Identity

1. modificacion program.cs: añadimos un nuevo using y creamos el nuevo servicio.
```
using Microsoft.AspNetCore.Identity;

//Implementacion Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
```

2. modificacion ApplicationDbContext
```
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    //Relacion con la autenticacion de usuarios
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; } = default!;
    }

    //Trabajar con entidades modelo de negocio
    public class ApplicationDbContext2 : DbContext
    {
        public ApplicationDbContext2 (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       
    }
```
3. Ejecucion comando codegenerator: Este nos permitira obtener funcionalidades especificas de ASP.Identity. (elegir dependiendo de las necesidades)
```
//solo para Registro y Login
> dotnet aspnet-codegenerator identity -dc Zeff_Food.Data.ApplicationDbContext --files "Account.Register;Account.Login"

//Para Registro, Login, Logout, ForgotPassword, ResetPassword 
> dotnet aspnet-codegenerator identity -dc Zeff_Food.Data.ApplicationDbContext --files "Account.Register;Account.Login;Account.Logout;Account.ForgotPassword;Account.ResetPassword"
```

### Actualizar migraciones y base de datos
```
//Contexto de base de datos 1
> dotnet ef migrations add "Identity" --context ApplicationDbContext -o "D:\Leonardo\Universidad\ciclo_7\proyecto zeff food\Zeff-Food\Data\Migrations"
> dotnet ef database update --context ApplicationDbContext

//Contexto de base de datos 2
> dotnet ef migrations add "Identity" --context ApplicationDbContextIdentity -o "D:\Leonardo\Universidad\ciclo_7\proyecto zeff food\Zeff-Food\Data\Migrations"

> dotnet ef database update --context ApplicationDbContextIdentity

// para eliminar al actualizaciones de base de datos con especificacion de contexto

> dotnet ef database update 0

```


