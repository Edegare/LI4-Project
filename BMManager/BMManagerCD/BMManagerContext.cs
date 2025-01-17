using Microsoft.EntityFrameworkCore;
using BMManagerLN.SubEncomendas;
using BMManagerLN.SubFuncionarios;
using BMManagerLN.SubMateriais;
using BMManagerLN.SubMontagens;
using BMManagerLN.SubMoveis;

namespace BMManager.BMManagerCD
{
    public class BMManagerContext : DbContext
    {
        public BMManagerContext (DbContextOptions<BMManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Encomenda> Encomenda { get; set; } = default!;

        public DbSet<Funcionario> Funcionario { get; set; } = default!;

        public DbSet<Material> Material { get; set; } = default!;

        public DbSet<Montagem> Montagem { get; set; } = default!;

        public DbSet<Movel> Movel { get; set; } = default!;

        public DbSet<Etapa> Etapa { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Encomenda


            //Funcionario
            // Configuração da chave primária
            modelBuilder.Entity<Funcionario>()
                .HasKey(f => f.Codigo_Utilizador);

            modelBuilder.Entity<Funcionario>()
                .Property(f => f.Codigo_Utilizador)
                .ValueGeneratedOnAdd();

            // Configuração do enum Equipa para armazenar como string (ou inteiro)
            modelBuilder.Entity<Funcionario>()
                .Property(f => f.Equipa)
                .HasConversion(
                    e => e.ToString(), // Armazenar como string
                    e => (Equipa)Enum.Parse(typeof(Equipa), e) // Converter de volta para o enum
                )
                .IsRequired();


            //Material
            modelBuilder.Entity<Material>().HasKey(m => m.Numero);
            modelBuilder.Entity<Material>().Property(m => m.Numero).ValueGeneratedOnAdd();
            modelBuilder.Entity<Material>().Property(m => m.Quantidade).HasDefaultValue(0);


            //Montagem


            //Movel


        }
    }
}
