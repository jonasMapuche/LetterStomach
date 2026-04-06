using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface IWordEmbeddingService
    {
        Dictionary<(string, string), int> Word2Vec(List<Sentenca> sentences);
        HashSet<string> Vocabulary(List<Sentenca> sentences);
        bool Similarity(Dictionary<(string, string), int> word_2_vec, HashSet<string> vocabulary, string? target, string? target1);
        string RemoveAccent(string input);
        List<Tutorial> EncodeLesson(List<Lesson> lesson, HashSet<string> vocabulary);
        byte[] Encode(string kind, HashSet<string> briefs);
        byte[] Encode(int kind, HashSet<int> briefs);
        byte[] HashSHA256(string texto);
        byte[] HashSHA256(int value);
        bool Similarity(Dictionary<(byte[], byte[]), int> word_2_vec, byte[]? target, byte[]? target1);
        Dictionary<(byte[], byte[]), int> Word2VecSHA256(List<Sentenca> sentences, HashSet<string> vocabulary);
        List<Lesson> DecodeLesson(List<Tutorial> tutorials, HashSet<string> vocabulary);
    }
}
