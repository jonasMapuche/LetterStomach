using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels
{
    public class SyntaxViewModel : ISyntaxViewModel
    {
        public List<Lesson> MountLessonNounVerbNoun(string language, List<Lesson> list_noun, List<Elocucao> list_verb, List<Juncao> list_preposition, List<Sentenca> sentence, List<Estoutro> pronoun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountLessonPronounVerbNoun(string language, List<Lesson> list_noun, List<Elocucao> list_verb, List<Juncao> list_preposition, List<Sentenca> sentence, List<Estoutro> pronoun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> MountOrder(List<Lesson> list_first, List<Lesson> list_second)
        {
            throw new NotImplementedException();
        }

        public string MountPhrase(List<Word> word_model)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_Adj(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_Adj_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_Adj_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_Adj_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_Adj_Pr_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_Adj_Pr_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_Adj_Pr_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_Pr_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_Pr_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> PeriodSS_V_Pr_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun)
        {
            throw new NotImplementedException();
        }

        public bool VerifyAdjectiveOD(List<Word> list_word, List<Sentenca> sentence, bool noun)
        {
            throw new NotImplementedException();
        }

        public bool VerifyAdjectiveODAA(List<Word> list_word, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public bool VerifyAdjectiveOI(List<Word> list_word, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public bool VerifyVerbOD(List<Word> list_word, List<Sentenca> sentence, bool noun)
        {
            throw new NotImplementedException();
        }

        public bool VerifyVerbODAA(List<Word> list_word, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public bool VerifyVerbOI(List<Word> list_word, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public bool VerifyVerbPS(List<Word> list_word, List<Sentenca> sentence)
        {
            throw new NotImplementedException();
        }

        public bool VerifyVerbSS(List<Word> list_word, List<Sentenca> sentence, bool noun)
        {
            throw new NotImplementedException();
        }

        public List<Word> WordPredicateOration(string pronoun_predicate, string noun_predicate, string article_predicate, string digit_predicate, string preposition)
        {
            throw new NotImplementedException();
        }

        public List<Word> WordSampleOration(string pronoun_subject, string noun_subject, string article_subject, string digit_subject, string verb, string model)
        {
            throw new NotImplementedException();
        }
    }
}
