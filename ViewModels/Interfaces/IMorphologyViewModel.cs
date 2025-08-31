using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    public interface IMorphologyViewModel
    {
        public List<Lesson> GetNoun(List<Sentenca> sentence, List<string> noun, List<Estoutro> pronoun, List<Preceito> article, List<Algarismo> digit);
        public List<Lesson> GetVerb(List<Sentenca> sentence, List<string> model, List<Elocucao> verb, List<Circunstancia> adverb);
        public List<Lesson> GetAdjective(List<Sentenca> sentences, List<string> adjectives, List<Circunstancia> adverbs);
        public List<Lesson> GetAdjectiveNoun(List<Sentenca> sentences, List<string> adjectives, List<Circunstancia> adverbs, List<string> nouns, List<Preceito> articles);
        public List<Lesson> GetNumeral(List<Sentenca> sentence, List<Algarismo> digit);
        public List<Lesson> GetArticle(List<Sentenca> sentence, List<Preceito> article);
        public List<Lesson> GetPreposition(List<Sentenca> sentence, List<Juncao> preposition);
        public List<Lesson> GetPronoun(List<Sentenca> sentence, List<Estoutro> pronoun);

        public List<string> GetLessonNoun(string language, Materia lesson, List<Materia> book);
        public List<string> GetLessonAdjective(Materia lesson, List<Materia> book);
        public List<string> GetLessonVerb(Materia lesson);

        public List<Lesson> Union(List<Lesson> list_fist, List<Lesson> list_last);

        public List<Word> GetSuject(string pronoun_subject, string noun_subject, string article_subject, string digit_subject, string verb, string model);
        public List<Word> GetPredicate(string pronoun_predicate, string noun_predicate, string article_predicate, string digit_predicate, string preposition);
    }
}
