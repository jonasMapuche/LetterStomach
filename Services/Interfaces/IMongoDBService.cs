using LetterStomach.Data;

namespace LetterStomach.Services.Interfaces
{
    public interface IMongoDBService
    {
        AlgarismoContext AlgarismoContext { get; }
        AssistenteContext AssistenteContext { get; }
        CircunstanciaContext CircunstanciaContex { get; }
        ElocucaoContext ElocucaoContext { get; }
        EstoutroContext EstoutroContext { get; }
        JuncaoContext JuncaoContext { get; }
        LigacaoContext LigacaoContext { get; }
        MaterialContext MaterialContext { get; }
        PreceitoContext PreceitoContext { get; }
        SentencaContext SentencaContext { get; }
    }
}
