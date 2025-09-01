using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    internal interface ISyntaxViewModel
    {
        public string GetOration(List<Word> words);

        public List<Lesson> PeriodSS_V(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_P(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_Pr_P(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_N(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_Pr_N(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_AdjN(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_Pr_AdjN(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_Adj(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_Adj_P(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_Adj_Pr_P(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_Adj_N(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_Adj_Pr_N(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_Adj_AdjN(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
        public List<Lesson> PeriodSS_V_Adj_Pr_AdjN(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun);
    }
}
