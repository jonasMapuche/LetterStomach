using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    internal interface ISyntaxViewModel
    {
        //---
        public List<Word> WordSampleOration(string pronoun_subject, string noun_subject, string article_subject, string digit_subject, string verb, string model);
        public List<Word> WordPredicateOration(string pronoun_predicate, string noun_predicate, string article_predicate, string digit_predicate, string preposition);
        //---
        public string MountPhrase(List<Word> word_model);
        //---
        public List<Lesson> MountLessonPronounVerbNoun(string language, List<Lesson> list_noun, List<Elocucao> list_verb, List<Juncao> list_preposition, List<Sentenca> sentence, List<Estoutro> pronoun);
        public List<Lesson> MountLessonNounVerbNoun(string language, List<Lesson> list_noun, List<Elocucao> list_verb, List<Juncao> list_preposition, List<Sentenca> sentence, List<Estoutro> pronoun);
        //---
        public List<Lesson> MountOrder(List<Lesson> list_first, List<Lesson> list_second);
        //---
        public bool VerifyVerbSS(List<Word> list_word, List<Sentenca> sentence, bool noun);
        //---
        public bool VerifyVerbOD(List<Word> list_word, List<Sentenca> sentence, bool noun);
        public bool VerifyVerbOI(List<Word> list_word, List<Sentenca> sentence);
        public bool VerifyVerbODAA(List<Word> list_word, List<Sentenca> sentence);
        //---
        public bool VerifyVerbPS(List<Word> list_word, List<Sentenca> sentence);
        //---
        public bool VerifyAdjectiveOD(List<Word> list_word, List<Sentenca> sentence, bool noun);
        public bool VerifyAdjectiveOI(List<Word> list_word, List<Sentenca> sentence);
        public bool VerifyAdjectiveODAA(List<Word> list_word, List<Sentenca> sentence);
        //---
        /* 
         * - P = pronoun
         * - V = verb
         * - Pr = preposition
         * - Adv = adverb 
         * - N = noun
         * - Adj = adjective
         * - A = article
         * - D = digit
         * - C = conjunction 
         * --
         * Noun (Adjectival Adjunct) + Verb (Adverbial Adjunct)
         * Noun (Adjectival Adjunct) + Verb (Adverbial Adjunct) + Direct Object (Adjectival Adjunct (Adverbial Adjunct))
         * Noun (Adjectival Adjunct) + Verb (Adverbial Adjunct) + Indirect Object (Adjectival Adjunct (Adverbial Adjunct))
         * Noun (Adjectival Adjunct) + Verb (Adverbial Adjunct) + Predicate of the Subject (Adverbial Adjunct)
         * Noun (Adjectival Adjunct) + Verb (Adverbial Adjunct) + Predicate of the Subject (Adverbial Adjunct) + Direct Object (Adjectival Adjunct (Adverbial Adjunct))
         * Noun (Adjectival Adjunct) + Verb (Adverbial Adjunct) + Predicate of the Subject (Adverbial Adjunct) + Indirect Object (Adjectival Adjunct (Adverbial Adjunct))
         */
        //---
        public List<Lesson> PeriodSS_V(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        //---
        public List<Lesson> PeriodSS_V_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        //---
        public List<Lesson> PeriodSS_V_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        //---
        public List<Lesson> PeriodSS_V_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        //---
        public List<Lesson> PeriodSS_V_Adj(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        //---
        public List<Lesson> PeriodSS_V_Adj_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Adj_Pr_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        //---
        public List<Lesson> PeriodSS_V_Adj_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Adj_Pr_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        //---
        public List<Lesson> PeriodSS_V_Adj_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Adj_Pr_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        //---
        /*
         * Noun + Adjectival Adjunct + Verb (Adverbial Adjunct) + Direct Object (Adjectival Adjunct (Adverbial Adjunct)) + Predicate of the Object (Adverbial Adjunct)
         * Noun + Adjectival Adjunct + Verb (Adverbial Adjunct) + Indirect Object (Adjectival Adjunct (Adverbial Adjunct)) + Predicate of the Object (Adverbial Adjunct)
         * Noun + Adjectival Adjunct + Verb (Adverbial Adjunct) + Direct Object (Adjectival Adjunct (Adverbial Adjunct)) + Indirect Object (Adjectival Adjunct (Adverbial Adjunct))
         * Noun + Adjectival Adjunct + Verb (Adverbial Adjunct) + Indirect Object (Adjectival Adjunct (Adverbial Adjunct)) + Direct Object (Adjectival Adjunct (Adverbial Adjunct))
         * -- 
        public List<Lesson> PeriodSS_V_P_Adj(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_N_Adj(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_AdjN_Adj(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
         * --
        public List<Lesson> PeriodSS_V_Pr_P_Adj(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_N_Adj(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_AdjN_Adj(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
         * --
        public List<Lesson> PeriodSS_V_P_Pr_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_P_Pr_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_P_Pr_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_N_Pr_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_N_Pr_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_N_Pr_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_AdjN_Pr_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_AdjN_Pr_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_AdjN_Pr_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
         * --
        public List<Lesson> PeriodSS_V_Pr_P_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_P_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_P_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_N_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_N_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_N_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_AdjN_P(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_AdjN_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        public List<Lesson> PeriodSS_V_Pr_AdjN_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
         */
        /*
         * Noun + Adjectival Adjunct + Verb (Adverbial Adjunct) + Direct Object (Adjectival Adjunct (Adverbial Adjunct)) + Conjuction + Direct Object (Adjectival Adjunct (Adverbial Adjunct))
         * --
         public List<Lesson> PeriodSS_V_N_C_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
         public List<Lesson> PeriodSS_V_N_C_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
         public List<Lesson> PeriodSS_V_AdjN_C_AdjN(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
         public List<Lesson> PeriodSS_V_AdjN_C_N(string language, List<Sentenca> sentence, List<Lesson> period, bool noun);
        */
    }
}
