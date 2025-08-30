using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    public interface IMorphologyViewModel
    {
        //---Pronoun
        public List<Estoutro> SelectPronoun(string language);
        //---
        public List<Estoutro> GetPronoun(string language);
        public List<Estoutro> GetPronoun(string language, string type);
        //---
        public List<Estoutro> FilterTypePronoun(List<Estoutro> pronouns, List<string> type);
        public List<Estoutro> FilterPronoun(List<Estoutro> pronoun, List<Sentenca> sentence);
        //---
        public List<Estoutro> SetSortPronoun(List<Estoutro> pronoun);
        //---
        public List<Estoutro> MountPronoun(List<string> type, List<Estoutro> pronoun);
        //---
        public List<Lesson> MountMorphologyPronoun(List<Sentenca> sentence, List<Estoutro> pronoun);
        //---
        //---Article
        //---
        public List<Preceito> SelectArticle(string language);
        //---
        public List<Preceito> GetArticle(string language);
        //---
        public List<Preceito> FilterArticle(List<Preceito> article, List<Sentenca> sentence);
        //---
        public HashSet<string> MountArticle(List<Preceito> article);
        //---
        public List<Lesson> MountMorphologyArticle(List<Sentenca> sentence, List<Preceito> article);
        //---
        //---Adverb
        //---
        public List<Circunstancia> SelectAdverb(string language);
        //---
        public List<Circunstancia> GetAdverb(string language);
        //---
        public List<Circunstancia> FilterAdverb(List<Circunstancia> adverb, List<Sentenca> sentence);
        //---
        public List<Lesson> VerifyAdverb(List<Lesson> adverb_adverb, List<Sentenca> sentence);
        //---
        public List<Lesson> MountAdverbAdverb(List<Circunstancia> adverb);
        //---
        //---Digit
        //---
        public List<Algarismo> SelectDigit(string language);
        //---
        public List<Algarismo> GetDigit(string language);
        //---
        public List<Algarismo> FilterDigit(List<Algarismo> digit, List<Sentenca> sentence);
        //---
        public List<Lesson> MountMorphologyDigit(List<Sentenca> sentence, List<Algarismo> digit);
        //---
        //---Preposition
        //---
        public List<Juncao> SelectPreposition(string language);
        //---
        public List<Juncao> GetPreposition(string language);
        //---
        public List<Juncao> FilterPreposition(List<Juncao> preposition, List<Sentenca> sentence);
        //---
        public List<Lesson> MountMorphologyPreposition(List<Sentenca> sentence, List<Juncao> preposition);
        //---
        //---Adjective
        //---
        public List<string> MountAdjective(Materia lesson, List<Materia> book);
        //---
        public List<string> FilterList(List<string> value, List<Sentenca> sentence);
        //---
        public List<Sentenca> VerifyAdjective(List<Sentenca> adjective_adverb, List<Sentenca> sentence);
        //---
        public List<Sentenca> MountAdjectivePronoun(List<string> noun, List<Estoutro> pronoun, List<Preceito> article);
        public List<Sentenca> MountAdjectiveAdverb(List<string> adjective, List<Circunstancia> adverb);
        public List<Sentenca> MountAdjectiveAdverb(List<string> adjective, List<Sentenca> adverb_adverb);
        public List<Sentenca> MountAdjectiveNoun(List<string> noun, List<Sentenca> adjective_adverb, List<Preceito> article);
        //---
        public List<Sentenca> UnionAdjective(List<string> adjective, List<Sentenca> adjective_adverb);
        public List<Sentenca> UnionAdjective(List<Sentenca> adjective_adverb, List<Sentenca> adjective_adverb_adverb);
        //---
        public List<Sentenca> MountMorphologyAdjective(List<Sentenca> sentence, List<string> adjective, List<Circunstancia> adverb);
        public List<Sentenca> MountMorphologyAdjectiveNoun(List<Sentenca> sentence, List<string> adjective, List<Circunstancia> adverb, List<string> noun, List<Preceito> article);
        //---
        //---Noun
        //---
        public List<string> MountNoun(string language, Materia lesson, List<Materia> book);
        //---
        public List<Lesson> UnionNoun(List<Lesson> list_first, List<Lesson> list_second);
        public List<Lesson> UnionNoun(List<string> list_string, List<Lesson> list_second);
        //---
        public List<Lesson> VerifyNoun(List<Lesson> lesson, List<Sentenca> sentence);
        //---
        public List<Lesson> MountNounDigit(List<string> noun, List<Algarismo> digit, List<Preceito> article);
        public List<Lesson> MountNounArticle(List<string> noun, List<Preceito> article);
        public List<Lesson> MountNounPronoun(List<string> noun, List<Estoutro> pronoun, List<Preceito> article);
        //---
        public List<Lesson> MountMorphologyNoun(List<Sentenca> sentence, List<string> noun, List<Estoutro> pronoun, List<Preceito> article, List<Algarismo> digit);
        //---
        //---Verb
        //---
        public List<string> MountModel(Materia lesson);
        public List<Elocucao> MountVerb(string language, Materia lesson);
        //---
        public List<Elocucao> GetModel(string language, string model);
        //---
        public List<Elocucao> SelectVerb(string language);
        //---
        public List<Elocucao> MountVerb(List<string> model, List<Elocucao> verb);
        //---
        public List<Elocucao> FilterVerb(List<Elocucao> verb, List<Sentenca> sentence);
        //---
        public List<Lesson> VerifyVerb(List<Lesson> verb_adverb, List<Sentenca> sentence);
        //---
        public List<Lesson> UnionVerb(List<Elocucao> verb, List<Lesson> verb_adverb);
        public List<Lesson> UnionVerb(List<Lesson> verb_adverb, List<Lesson> verb_adverb_adverb);
        //---
        public List<Lesson> MountVerbAdverb(List<Elocucao> verb, List<Circunstancia> adverb);
        public List<Lesson> MountVerbAdverb(List<Elocucao> verb, List<Lesson> adverb_adverb);
        //---
        public List<Lesson> MountMorphologyVerb(List<Sentenca> sentence, List<string> model, List<Elocucao> verb, List<Circunstancia> adverb);
    }
}
