using System;
using System.Collections.Generic;

//Extensiones para el uso de key, column
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace Zeff_Food.Models.Entitys
{
    public class Usuario
    {
        [Key]       
        public int Id { get; set; }

        [Column("Nombre")]
        public string? Nombre { get; set; }

        
        [Column("Ape")]
        public string? Ape { get; set; }
       
        [Column("Contraseña")]
        public string? password { get; set; }

        public string? Email { get; set; }

        [Column("Celular")]
        public string? Celular { get; set; }

        [Column("Fecha_de_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Column("Fecha_creacion_cuenta")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Column("Total_gastado")]
        public decimal TotalGastado { get; set; }

        //public List<> HistorialPedidos { get; set; }
    }
}