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

        public DbSet<Funcionario> Funcionario { get; set; } = default!;

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


            //Montagem


            //Movel


        }
    }
}
