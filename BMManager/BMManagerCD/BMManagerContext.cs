using Microsoft.EntityFrameworkCore;
using BMManagerLN.SubEncomendas;
using BMManagerLN.SubFuncionarios;
using BMManagerLN.SubMateriais;
using BMManagerLN.SubMontagens;
using BMManagerLN.SubMoveis;
using System.Linq.Expressions;
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

        public DbSet<Etapa_Precisa_Material> Etapa_Precisa_Material { get; set; } = default!;

        public DbSet<Funcionario_Participa_Montagem> Funcionario_Participa_Montagem { get; set; } = default!;

        public DbSet<Encomenda_Precisa_Movel> Encomenda_Precisa_Movel { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Encomenda
            modelBuilder.Entity<Encomenda>().HasKey(m => m.Numero);
            modelBuilder.Entity<Encomenda>().Property(m => m.Numero).ValueGeneratedOnAdd();
            modelBuilder.Entity<Encomenda>().Property(m => m.Data_Prevista).IsRequired(true);
            modelBuilder.Entity<Encomenda>().Property(m => m.Data_Real).IsRequired(false);
            modelBuilder.Entity<Encomenda>().Property(m => m.Concluida).HasDefaultValue(true);

            //Funcionario
            // Configuração da chave primária
            modelBuilder.Entity<Funcionario>().HasKey(f => f.Codigo_Utilizador);
            modelBuilder.Entity<Funcionario>().Property(f => f.Codigo_Utilizador).ValueGeneratedOnAdd();

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
            modelBuilder.Entity<Material>().Property(m => m.Imagem).IsRequired(false); //alterar depois


            //Montagem
            modelBuilder.Entity<Montagem>().HasKey(m => m.Numero);
            modelBuilder.Entity<Montagem>().Property(m => m.Numero).ValueGeneratedOnAdd();
            modelBuilder.Entity<Montagem>().Property(m => m.Data_Inicial).IsRequired(true);
            modelBuilder.Entity<Montagem>().Property(m => m.Data_Final).IsRequired(false);
            modelBuilder.Entity<Montagem>().Property(m => m.Duracao).HasDefaultValue(TimeSpan.Zero).IsRequired(true);
            modelBuilder.Entity<Montagem>().Property(m => m.Estado)
                                                .HasConversion(e => e.ToString(),
                                                               e => (Estado)Enum.Parse(typeof(Estado), e))
                                                .IsRequired();
            modelBuilder.Entity<Montagem>().Property(m => m.Etapa_Concluida).HasDefaultValue(false).IsRequired(true);
            modelBuilder.Entity<Montagem>().Property(m => m.Estado).HasDefaultValue(Estado.Em_Progresso).IsRequired(true);
            modelBuilder.Entity<Montagem>().Property(m => m.Movel).IsRequired(true);
            modelBuilder.Entity<Montagem>().Property(m => m.Etapa).IsRequired(true);
            modelBuilder.Entity<Montagem>().Property(m => m.Encomenda).IsRequired(false);


            //Movel
            modelBuilder.Entity<Movel>().HasKey(m => m.Numero);
            modelBuilder.Entity<Movel>().Property(m => m.Numero).ValueGeneratedOnAdd();
            modelBuilder.Entity<Movel>().Property(m => m.Quantidade).HasDefaultValue(0);
            modelBuilder.Entity<Movel>().Property(m => m.Imagem).IsRequired(false); //alterar depois

            //Etapa
            modelBuilder.Entity<Etapa>().HasKey(e => e.Codigo_Etapa);
            modelBuilder.Entity<Etapa>().Property(e => e.Codigo_Etapa).ValueGeneratedOnAdd();
            modelBuilder.Entity<Etapa>().Property(e => e.Numero).HasDefaultValue(1).IsRequired();
            modelBuilder.Entity<Etapa>().Property(e => e.Proxima_Etapa).IsRequired(false);
            modelBuilder.Entity<Etapa>().Property(e => e.Movel).IsRequired();
            modelBuilder.Entity<Etapa>().Property(m => m.Imagem).IsRequired(false); //alterar depois

            //Etapa_Precisa_Material
            modelBuilder.Entity<Etapa_Precisa_Material>().HasKey(e => new { e.Etapa, e.Material });
            modelBuilder.Entity<Etapa_Precisa_Material>().Property(e => e.Quantidade).HasDefaultValue(0).IsRequired(true);

            //Funcionario_Participa_Montagem
            modelBuilder.Entity<Funcionario_Participa_Montagem>().HasKey(f => new { f.Montagem, f.Funcionario });

            //Encomenda_Precisa_Movel
            modelBuilder.Entity<Encomenda_Precisa_Movel>().HasKey(e => new { e.Encomenda, e.Movel });
            modelBuilder.Entity<Encomenda_Precisa_Movel>().Property(e => e.Quantidade).HasDefaultValue(0).IsRequired(true);
        }

        public async Task<List<T>> ObterDadosPorCondicaoAsync<T>(
            Expression<Func<T, bool>> condicao) where T : class
        {
            var query = this.Set<T>().Where(condicao);

            return await query.ToListAsync();
        }
    }
}
