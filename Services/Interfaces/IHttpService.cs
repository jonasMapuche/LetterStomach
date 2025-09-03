using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface IHttpService
    {
        Task<string> HttpPost(StreamContent message, string file_name);
        Task<List<Adverbios>> HttpAdverb();
        Task<List<Pronomes>> HttpPronoun();
        Task<List<Artigos>> HttpArticle();
        Task<List<Numerais>> HttpNumeral();
        Task<List<Preposicoes>> HttpPreposition();
        Task<List<Substantivo>> HttpNoun();
        Task<List<Adjetivo>> HttpAdjective();
        Task<List<Verbos>> HttpVerb();
        Task<List<Sentencas>> HttpSentence();
        Task<List<Conjuncoes>> HttpConjunction();
        Task<List<Auxiliares>> HttpAuxiliary();
    }
}
