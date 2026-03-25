using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface IModelService
    {
        Task<List<Circunstancia>> LoadAdverb(List<Adverbios> adverb);
        Task<List<Preceito>> LoadArticle(List<Artigos> article);
        Task<List<Estoutro>> LoadPronoun(List<Pronomes> pronoun);
        Task<List<Algarismo>> LoadNumeral(List<Numerais> numeral);
        Task<List<Juncao>> LoadPreposition(List<Preposicoes> preposition);
        Task<List<Materia>> LoadMateria(List<Substantivo> noun, List<Adjetivo> adjective, List<Model> model);
        Task<List<Sentenca>> LoadSentenca(List<Sentencas> sentence);
        Task<List<Ligacao>> LoadLigacao(List<Conjuncoes> conjunction);
        Task<List<Elocucao>> LoadElocucao(List<Verbos> verb);
        Task<List<Assistente>> LoadAssistente(List<Auxiliares> auxiliary);
    }
}
