using System;
using avaliacao_backend.Api.Entities;

namespace avaliacao_backend.Api.Repositories
{
    public interface IAvaliacaoRepository
    {
        void Incluir(Avaliacao avaliacao);
        bool Salvar();
        bool Existe(Guid codigoAvaliador, Guid codigoResumo);
        string Codigo(Guid codigoAvaliador, Guid codigoResumo);
        void Alterar(Avaliacao avaliacao);
        Avaliacao Obter(Guid codigo);
    }
}