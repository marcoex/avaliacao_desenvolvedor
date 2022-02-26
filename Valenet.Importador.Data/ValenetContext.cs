using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Valenet.Importador.Data.Model;

#nullable disable

namespace Valenet.Importador.Data
{
    public partial class ValenetContext : DbContext
    {
        public ValenetContext()
        {
        }

        public ValenetContext(DbContextOptions<ValenetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pedido> Pedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("Pedido");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Comprador)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Fornecedor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrecoUnitario).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
