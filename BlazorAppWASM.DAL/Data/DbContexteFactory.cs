using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppWASM.DAL.Data
{
    class DbContexteFactory : IDesignTimeDbContextFactory<DbContext>
    {
        public DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();

            // Utilisation de la chaîne de connexion définie directement dans le code
            optionsBuilder.UseSqlServer("Server=RABDY1JN53\\SQLEXPRESS;Database=BiblioDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new DbContext(optionsBuilder.Options);
        }
    }
}
 