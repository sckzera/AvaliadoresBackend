using Microsoft.EntityFrameworkCore;
using avaliacao_backend.Api.Entities;

namespace avaliacao_backend.Api.DbContexts
{
    public class AvaliacaoContext : DbContext
    {
        public AvaliacaoContext()
        {

        }
     
        public AvaliacaoContext(DbContextOptions<AvaliacaoContext> options)
           : base(options)
        {

        }

        public DbSet<Avaliacao> Avaliacoes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
