using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    public interface IMorphologyViewModel
    {
        public List<Lesson> GetNoun(List<Sentenca> sentences, List<string> nouns, List<Estoutro> pronouns, List<Preceito> articles, List<Algarismo> numerals);
        public List<Lesson> GetVerb(List<Sentenca> sentences, List<string> models, List<Elocucao> verbs, List<Circunstancia> adverbs);
        public List<Lesson> GetAdjective(List<Sentenca> sentences, List<string> adjectives, List<Circunstancia> adverbs);
        public List<Lesson> GetAdjectiveNoun(List<Sentenca> sentences, List<string> adjectives, List<Circunstancia> adverbs, List<string> nouns, List<Preceito> articles);
        public List<Lesson> GetNumeral(List<Sentenca> sentences, List<Algarismo> numerals);
        public List<Lesson> GetArticle(List<Sentenca> sentences, List<Preceito> articles);
        public List<Lesson> GetPreposition(List<Sentenca> sentences, List<Juncao> prepositions);
        public List<Lesson> GetPronoun(List<Sentenca> sentences, List<Estoutro> pronouns);

        public List<string> GetLessonNoun(string language, Materia lesson, List<Materia> book);
        public List<string> GetLessonAdjective(Materia lesson, List<Materia> book);
        public List<string> GetLessonVerb(Materia lesson);

        public List<Lesson> Union(List<Lesson> fists, List<Lesson> lasts);

        public List<Word> GetSuject(string pronoun, string noun, string article, string numeral, string verb, string model);
        public List<Word> GetPredicate(string pronoun, string noun, string article, string numeral, string preposition);
    }
}
