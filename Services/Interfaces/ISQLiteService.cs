using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface ISQLiteService
    {
        List<Circunstancia> Circunstancia { get; set; }
        List<Estoutro> Estoutro { get; set; }
        List<Preceito> Preceito { get; set; }
        List<Algarismo> Algarismo { get; set; }
        List<Juncao> Juncao { get; set; }
        List<Materia> Materia { get; set; }
        List<Elocucao> Elocucao { get; set; }
        List<Sentenca> Sentenca { get; set; }
        List<Ligacao> Ligacao { get; set; }
        List<Assistente> Assistente { get; set; }

        void Connect();
        Task<int> DeleteAll();
        Task<int> Delete(int select_table);
        Task CreateAll();
        Task Create(int select_table);
        Task InsertAdverb();
        Task InsertPronoun();
        Task InsertArticle();
        Task InsertNumeral();
        Task InsertPreposition();
        Task InsertNoun();
        Task InsertAdjective();
        Task InsertVerb();
        Task InsertSentence();
        Task InsertConjunction();
        Task InsertAuxiliary();
        Task LoadAdverb();
        Task LoadPronoun();
        Task LoadArticle();
        Task LoadNumeral();
        Task LoadPreposition();
        Task LoadLetter();
        Task LoadVerb();
        Task LoadSentence();
        Task LoadConjunction();
        Task LoadAuxiliary();
    }
}
