# PROYECTO Zeff-Food

Descripcion: empresa ecommers de comida rapida

## Tecnologia utilizada
* ASP.netcore v.8.0 en Visual Estudio Code. 
* Base de datos postgrest v.16.2.
* pgAdmin v.8.4..

## Manejo de entidades:

- Usuario (Nombre, email, numero celular, año de nacimiento, total gastado, Historial pedidos, Fecha creacion de cuenta)
- Producto (Nombre, descripcion, imagen, precio, stock)
- Inventario (Nombre, Stock, Proveedor, Fecha caducidad, Fecha de compra)
- Carrito ()
- Articulo Carro ()
- Pago ()
- Factura (Fecha de pedidio, lista de productos)

*guia para editar con formatos el archivo md (Readme):https://programminghistorian.org/es/lecciones/introduccion-a-markdown*

# DESARROLLO PROYECTO

## Comandos git (Inicializacion del proyecto):
```
> git clone https://github.com/leonardoPBF/Zeff-Food.git

//Comando creacion proyecto ASP.net
> dotnet new mvc
> cd Zeff-Food  
```

## Para poder subir a la rama principal
```
git commit -m "initial" \\genera mensaje del update
git branch -M main \\Creacion rama principal

git branch -M modelos \\Creacion rama secundaria 
```

## Actualizar Branch:
```
git add .
git commit -m "Nueva entidad"
git push origin modelos
```

## NuGet Gallery de la aplicacion 
```
> dotnet add package Microsoft.EntityFrameworkCore
> dotnet add package Microsoft.Extensions.DependencyInjection
> dotnet add package Microsoft.EntityFrameworkCore.Design
> dotnet add package Microsoft.EntityFrameworkCore.Tools
> dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL \\para la base de datos postgrest
> dotnet add package Microsoft.EntityFrameworkCore.Sqlite \\SQLite
```

1. Creacion carpeta Modelos-Entidades(mapeamos los modelos)
2. Creacion carpeta Data -> ApplicationDbContext
3. Configuracion conectionString en appsettings.json
```
   "ConnectionStrings": {
    "DefaultConnection": "DataSource=zeff_food.db;Cache=Shared", //para base de datos SQLite
    "PostgresSQLConnection": "host=localhost; port=5432; Database=Zeff_Food; Username=postgres; password=Zx123Leo"
  }
```
4. Configuracion program
```
    //Para poder utilizar los modelos en la base de datos
    builder.Services.AddDbContext<ApplicationDbContext>(options =>

    //para utilizar base de datos de Postgrest
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQLConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));
    
    //para utilizar base de datos de VScode
    /* options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found."))); */
    
```

5. Ejecutamos los siguientes comandos para poder generar las migraciones de la base de datos, para luego actualizarla:
```
> dotnet ef migrations add "initial_migration" 
> dotnet ef database update
```