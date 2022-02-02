using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DesafioApiRest.Model
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options):base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Estoque> Estoques { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasMany(b => b.Lojas)
                .WithMany(c => c.Produtos)
                .UsingEntity<Estoque>(
                   j => j
                        .HasOne(lj => lj.Loja)
                        .WithMany(est => est.Estoques)
                        .HasForeignKey(lj => lj.loja_id),
                     j => j
                        .HasOne(prd => prd.Produto)
                        .WithMany(est => est.Estoques)
                        .HasForeignKey(prd => prd.produto_id),
                     j =>
                    {
                        j.HasKey(t => new { t.produto_id, t.loja_id });
                    });
        }
    }

   
}
