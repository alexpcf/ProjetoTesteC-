using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prova_.net6.Models;
using System.IO;

namespace Prova_.net6.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Especificar tipo de armazenamento para as propriedades decimais
            modelBuilder.Entity<Banco>()
                .Property(b => b.PercentualJuros)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Boleto>()
                .Property(b => b.Valor)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Configurar opções usando a string de conexão do appsettings.json
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Boleto> Boletos { get; set; }
        public DbSet<Banco> Bancos { get; set; }
    }
}
