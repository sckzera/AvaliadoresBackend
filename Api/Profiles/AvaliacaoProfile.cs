using AutoMapper;

namespace avaliacao_backend.Api.Profiles
{
    public class AvaliacaoProfile : Profile
    {
        public AvaliacaoProfile()
        {
            CreateMap<Models.AvaliacaoInclusao, Entities.Avaliacao>();
            CreateMap<Models.AvaliacaoAlteracao, Entities.Avaliacao>();
        }
    }
}