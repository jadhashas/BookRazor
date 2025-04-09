using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorAppWASM.DAL.Entities;

namespace BlazorAppWASM.DAL.Data
{
    public class DbContexte : DbContext
    {
        public DbContexte(DbContextOptions<DbContexte> options) : base(options)
        {
        }
        public DbSet<Livre> Livres { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de l'index unique sur l'email
            modelBuilder.Entity<Utilisateur>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Utilisateur>()
    .HasIndex(u => u.NomUtilisateur)
    .IsUnique();
        }
    }
    
}

