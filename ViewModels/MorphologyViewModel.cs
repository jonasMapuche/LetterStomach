using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels
{
    public class MorphologyViewModel : IMorphologyViewModel
    {
        public List<Circunstancia> FilterAdverb(List<Circunstancia> adverb, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public List<Preceito> FilterArticle(List<Preceito> article, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public List<Algarismo> FilterDigit(List<Algarismo> digit, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public List<string> FilterList(List<string> value, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public List<Juncao> FilterPreposition(List<Juncao> preposition, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public List<Estoutro> FilterPronoun(List<Estoutro> pronoun, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public List<Estoutro> FilterTypePronoun(List<Estoutro> pronouns, List<string> type)
        {
            throw new NotImplementedException();
        }

        public List<Elocucao> FilterVerb(List<Elocucao> verb, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public List<Circunstancia> GetAdverb(string language)
        {
            throw new NotImplementedException();
        }

        public List<Preceito> GetArticle(string language)
        {
            throw new NotImplementedException();
        }

        public List<Algarismo> GetDigit(string language)
        {
            throw new NotImplementedException();
        }

        public List<Elocucao> GetModel(string language, string model)
        {
            throw new NotImplementedException();
        }

        public List<Juncao> GetPreposition(string language)
        {
            throw new NotImplementedException();
        }

        public List<Estoutro> GetPronoun(string language)
        {
            throw new NotImplementedException();
        }

        public List<Estoutro> GetPronoun(string language, string type)
        {
            throw new NotImplementedException();
        }

        public List<string> MountAdjective(Materia lesson, List<Materia> book)
        {
            throw new NotImplementedException();
        }

        public List<Sentenca> MountAdjectiveAdverb(List<string> adjective, List<Circunstancia> adverb)
        {
            throw new NotImplementedException();
        }

        public List<Sentenca> MountAdjectiveAdverb(List<string> adjective, List<Sentenca> adverb_adverb)
        {
            throw new NotImplementedException();
        }

        public List<Sentenca> MountAdjectiveNoun(List<string> noun, List<Sentenca> adjective_adverb, List<Preceito> article)
        {
            throw new NotImplementedException();
        }

        public List<Sentenca> MountAdjectivePronoun(List<string> noun, List<Estoutro> pronoun, List<Preceito> article)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountAdverbAdverb(List<Circunstancia> adverb)
        {
            throw new NotImplementedException();
        }

        public HashSet<string> MountArticle(List<Preceito> article)
        {
            throw new NotImplementedException();
        }

        public List<string> MountModel(Materia lesson)
        {
            throw new NotImplementedException();
        }

        public List<Sentenca> MountMorphologyAdjective(List<Sentenca> sentence, List<string> adjective, List<Circunstancia> adverb)
        {
            throw new NotImplementedException();
        }

        public List<Sentenca> MountMorphologyAdjectiveNoun(List<Sentenca> sentence, List<string> adjective, List<Circunstancia> adverb, List<string> noun, List<Preceito> article)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountMorphologyArticle(List<Sentenca> sentence, List<Preceito> article)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountMorphologyDigit(List<Sentenca> sentence, List<Algarismo> digit)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountMorphologyNoun(List<Sentenca> sentence, List<string> noun, List<Estoutro> pronoun, List<Preceito> article, List<Algarismo> digit)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountMorphologyPreposition(List<Sentenca> sentence, List<Juncao> preposition)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountMorphologyPronoun(List<Sentenca> sentence, List<Estoutro> pronoun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountMorphologyVerb(List<Sentenca> sentence, List<string> model, List<Elocucao> verb, List<Circunstancia> adverb)
        {
            throw new NotImplementedException();
        }

        public List<string> MountNoun(string language, Materia lesson, List<Materia> book)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountNounArticle(List<string> noun, List<Preceito> article)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountNounDigit(List<string> noun, List<Algarismo> digit, List<Preceito> article)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountNounPronoun(List<string> noun, List<Estoutro> pronoun, List<Preceito> article)
        {
            throw new NotImplementedException();
        }

        public List<Estoutro> MountPronoun(List<string> type, List<Estoutro> pronoun)
        {
            throw new NotImplementedException();
        }

        public List<Elocucao> MountVerb(string language, Materia lesson)
        {
            throw new NotImplementedException();
        }

        public List<Elocucao> MountVerb(List<string> model, List<Elocucao> verb)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountVerbAdverb(List<Elocucao> verb, List<Circunstancia> adverb)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountVerbAdverb(List<Elocucao> verb, List<Lesson> adverb_adverb)
        {
            throw new NotImplementedException();
        }

        public List<Circunstancia> SelectAdverb(string language)
        {
            throw new NotImplementedException();
        }

        public List<Preceito> SelectArticle(string language)
        {
            throw new NotImplementedException();
        }

        public List<Algarismo> SelectDigit(string language)
        {
            throw new NotImplementedException();
        }

        public List<Juncao> SelectPreposition(string language)
        {
            throw new NotImplementedException();
        }

        public List<Estoutro> SelectPronoun(string language)
        {
            throw new NotImplementedException();
        }

        public List<Elocucao> SelectVerb(string language)
        {
            throw new NotImplementedException();
        }

        public List<Estoutro> SetSortPronoun(List<Estoutro> pronoun)
        {
            throw new NotImplementedException();
        }

        public List<Sentenca> UnionAdjective(List<string> adjective, List<Sentenca> adjective_adverb)
        {
            throw new NotImplementedException();
        }

        public List<Sentenca> UnionAdjective(List<Sentenca> adjective_adverb, List<Sentenca> adjective_adverb_adverb)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> UnionNoun(List<Lesson> list_first, List<Lesson> list_second)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> UnionNoun(List<string> list_string, List<Lesson> list_second)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> UnionVerb(List<Elocucao> verb, List<Lesson> verb_adverb)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> UnionVerb(List<Lesson> verb_adverb, List<Lesson> verb_adverb_adverb)
        {
            throw new NotImplementedException();
        }

        public List<Sentenca> VerifyAdjective(List<Sentenca> adjective_adverb, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> VerifyAdverb(List<Lesson> adverb_adverb, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> VerifyNoun(List<Lesson> lesson, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> VerifyVerb(List<Lesson> verb_adverb, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }
    }
}
