using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeff_Food.Models.Entitys
{
    public class Factura
    {
        public int FacturaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal MontoTotal { get; set; }
        public string? Descripcion { get; set; }

        // Propiedad de clave foránea
        public int UsuarioId { get; set; }
        // Propiedad de navegación hacia Usuario
        public Usuario? Usuario { get; set; }

        public List<ItemFactura> Items { get; set; } = new List<ItemFactura>();
    }
}