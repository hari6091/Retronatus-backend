using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using retronatus_backend.Model;

namespace retronatus_backend.Context
{
    public class RetronatusContext : DbContext
    {
        public RetronatusContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Categoria>? Categoria { get; set; }
        public DbSet<Comentario>? Comentario { get; set; }
        public DbSet<Local>? Local { get; set; }
        public DbSet<Publicacao>? Publicacao { get; set; }
        public DbSet<Usuario>? Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.LowercaseRelationalTableAndPropertyNames();
        }
    }
}

static class DataContextExtensions
{
    public static void LowercaseRelationalTableAndPropertyNames(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName()?.ToLowerInvariant() ?? entity.GetTableName());
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(
                    property.GetColumnName()?.ToLowerInvariant() ?? property.GetColumnName()
                );
            }
        }
    }
}
