using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zeff_Food.Models.Entitys;

namespace Zeff_Food.Data
{

    //Relacion con la autenticacion de usuarios
    public class ApplicationDbContextIdentity : IdentityDbContext<Usuario>
    {
        public ApplicationDbContextIdentity  (DbContextOptions<ApplicationDbContextIdentity> options)
            : base(options)
        {
        }

      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //configuracion para poder manejar horario UTC
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("PostgresSQLConnection");
             optionsBuilder.UseNpgsql(connectionString, o => o.UseNodaTime());
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
  
}