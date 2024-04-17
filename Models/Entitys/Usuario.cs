using System;
using System.Collections.Generic;

//Extensiones para el uso de key, column
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Zeff_Food.Models.Entitys
{
    public class Usuario : IdentityUser
    {
        // [Key]       
        // public int Id { get; set; }

        [Column("Nombre")]
        public string? Nombre { get; set; }

        [Column("Fecha_de_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Column("Fecha_creacion_cuenta")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Column("Total_gastado")]
        public decimal TotalGastado { get; set; }
        
        [Column("Historial_pedidos")]
        public List<Factura>? Facturas { get; set; }
    }
}