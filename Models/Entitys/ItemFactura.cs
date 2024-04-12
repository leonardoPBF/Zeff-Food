using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeff_Food.Models.Entitys
{
    public class ItemFactura
    {
        public int ItemFacturaId { get; set; }
        public int FacturaId { get; set; }
        public Factura? Factura { get; set; }
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }        
        public decimal Subtotal { get { return Cantidad * Producto.Precio; } }
    }
}