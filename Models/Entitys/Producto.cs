using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeff_Food.Models.Entitys
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
    }
}