using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface IWordEmbeddingService
    {
        Dictionary<(string, string), int> Word2Vec(List<Sentenca> sentences);
        HashSet<string> Vocabulary(List<Sentenca> sentences);
        bool Similarity(Dictionary<(string, string), int> word_2_vec, HashSet<string> vocabulary, string target, string target1);
        string RemoveAccent(string input);
    }
}
