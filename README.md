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

## Comandos git (Inicializacion del proyecto):
```
> git clone https://github.com/leonardoPBF/Zeff-Food.git
```

## Comando inicial:
```
> dotnet new mvc
> cd Zeff-Food  

//para poder subir

git commit -m "initial"
git branch -M main //Creacion rama principal

git branch -M modelos    
```