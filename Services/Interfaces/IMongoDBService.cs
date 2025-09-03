using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Services.Interfaces
{
    public interface IMongoDBService
    {
        IMongoCollection<Circunstancia> Circunstancia { get; set; }
        IMongoCollection<Estoutro> Estoutro { get; set; }
        IMongoCollection<Preceito> Preceito { get; set; }
        IMongoCollection<Algarismo> Algarismo { get; set; }
        IMongoCollection<Elocucao> Elocucao { get; set; }
        IMongoCollection<Juncao> Juncao { get; set; }
        IMongoCollection<Materia> Materia { get; set; }
        IMongoCollection<Sentenca> Sentenca { get; set; }
        IMongoCollection<Ligacao> Ligacao { get; set; }
        IMongoCollection<Assistente> Assistente { get; set; }
        
        void Connect();
        void LoadCircustancia();
        void LoadEstoutro();
        void LoadPreceito();
        void LoadAlgarismo();
        void LoadElocucao();
        void LoadJuncao();
        void LoadMateria();
        void LoadSentenca();
        void LoadLigacao();
        void LoadAssistente();
    }
}
