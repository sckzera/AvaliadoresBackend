using System;
using System.ComponentModel.DataAnnotations;
using avaliacao_backend.Api.Enums;

namespace avaliacao_backend.Api.Models
{
    public class AvaliacaoInclusao
    {
        #region Resumo
        [Required]
        public float Relevancia { get; set; }

        [Required]
        public float Adequacao { get; set; }

        [Required]
        public float Coerencia { get; set; }

        [Required]
        public float ApresentacaoAdequada { get; set; }

        [Required]
        public float AvaliacaoResumo { get; set; }

        #endregion

        #region Apresentação

        [Required]
        public float Qualidade { get; set; }

        [Required]
        public float ApresentacaoOral { get; set; }

        [Required]
        public float ContribuicaoDesenvolvimento { get; set; }

        [Required]
        public float ContribuicaoComunidade { get; set; }

        [Required]
        public float AvaliacaoGeral { get; set; }

        #endregion

        [Required]
        public float SomaDasNotas { get; set; }

        [Required]
        public EnumPremioTeixeira PremioTeixeira { get; set; }
    }
}