using System;
using System.Linq;
using avaliacao_backend.Api.DbContexts;
using avaliacao_backend.Api.Entities;

namespace avaliacao_backend.Api.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository, IDisposable
    {
        private readonly AvaliacaoContext _context;

        public AvaliacaoRepository(AvaliacaoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Alterar(Avaliacao avaliacao)
        {
            if(avaliacao.Codigo == Guid.Empty)
                throw new ArgumentNullException(nameof(avaliacao.Codigo));
            
            var oldAvaliacao = Obter(avaliacao.Codigo);

            if(oldAvaliacao == null)
                throw new ArgumentNullException(nameof(oldAvaliacao));
            
            avaliacao.CodigoAvaliador = oldAvaliacao.CodigoAvaliador;
            avaliacao.CodigoResumo = oldAvaliacao.CodigoResumo;
            
            _context.Avaliacoes.Update(oldAvaliacao).CurrentValues.SetValues(avaliacao);
        }

        public Avaliacao Obter(Guid codigo)
        {
           return _context.Avaliacoes
                    .Where(c => c.Codigo == codigo)
                    .FirstOrDefault();
        }

        public string Codigo(Guid codigoAvaliador, Guid codigoResumo)
        {
            if (codigoAvaliador == Guid.Empty)
                throw new ArgumentNullException(nameof(codigoAvaliador));

            if (codigoResumo == Guid.Empty)
                throw new ArgumentNullException(nameof(codigoResumo));
            
            var retorno = (from c in _context.Avaliacoes
                            where c.CodigoResumo == codigoResumo
                            && c.CodigoAvaliador == codigoAvaliador
                            select c.Codigo).FirstOrDefault();
            
            return retorno.ToString();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool Existe(Guid codigoAvaliador, Guid codigoResumo)
        {
            if (codigoAvaliador == Guid.Empty)
                throw new ArgumentNullException(nameof(codigoAvaliador));

            if (codigoResumo == Guid.Empty)
                throw new ArgumentNullException(nameof(codigoResumo));

            return _context.Avaliacoes.Any(c => c.CodigoAvaliador == codigoAvaliador
                                         && c.CodigoResumo == codigoResumo);
        }

        public void Incluir(Avaliacao avaliacao)
        {
            if(avaliacao.CodigoResumo == Guid.Empty)
                throw new ArgumentNullException(nameof(avaliacao.CodigoResumo));
            
            if(avaliacao.CodigoAvaliador == Guid.Empty)
                throw new ArgumentNullException(nameof(avaliacao.CodigoAvaliador));
            
            avaliacao.Codigo = Guid.NewGuid();

            _context.Avaliacoes.Add(avaliacao);
        }

        public bool Salvar()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}