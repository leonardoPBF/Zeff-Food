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
}