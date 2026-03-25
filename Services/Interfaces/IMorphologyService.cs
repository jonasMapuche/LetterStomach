using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface IMorphologyService
    {
        List<Lesson> GetNoun(List<Sentenca> sentences, List<string> nouns, List<Estoutro> pronouns, List<Preceito> articles, List<Algarismo> numerals);
        List<Lesson> GetNumeralNoun(List<Sentenca> sentences, List<string> nouns, List<Preceito> articles, List<Algarismo> numerals);
        List<Lesson> GetVerb(List<Sentenca> sentences, List<string> models, List<Elocucao> verbs, List<Circunstancia> adverbs);
        List<Lesson> GetAdjectiveAdverb(List<Sentenca> sentences, List<string> adjectives, List<Circunstancia> adverbs);
        List<Lesson> GetAdjective(List<Sentenca> sentences, List<string> adjectives);
        List<Lesson> GetAdjectiveNoun(List<Sentenca> sentences, List<Lesson> adjectives, List<Lesson> nouns, List<Lesson> prepositions);
        List<Lesson> GetNumeral(List<Sentenca> sentences, List<Algarismo> numerals);
        List<Lesson> GetArticle(List<Sentenca> sentences, List<Preceito> articles);
        List<Lesson> GetPreposition(List<Sentenca> sentences, List<Juncao> prepositions);
        List<Lesson> GetPronoun(List<Sentenca> sentences, List<Estoutro> pronouns);
        List<Lesson> GetConjunction(List<Sentenca> sentences, List<Ligacao> conjunctions);

        List<string> GetLessonNoun(Materia lesson, List<Materia> book);
        List<string> GetLessonAdjective(Materia lesson, List<Materia> book);
        List<string> GetLessonVerb(Materia lesson);

        List<Word> GetSuject(string pronoun, string noun, string article, string numeral, string verb, string model);
        List<Word> GetPredicate(string pronoun, string noun, string article, string numeral, string preposition);
    }
}
