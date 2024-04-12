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

    }
  
}