using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels
{
    public class SyntaxViewModel : ISyntaxViewModel
    {
        #region ERROR
        private string _error_message;

        public string error_message
        {
            get => _error_message;
            set
            {
                _error_message = value;
            }
        }

        public event EventHandler<string> OnError;
        #endregion

        #region VARIABLE
        protected string VAR_SUBJECT = SettingService.Instance.Suject;
        protected string VAR_PREDICATE = SettingService.Instance.Predicate;
        protected string VAR_PRONOUN = SettingService.Instance.Pronoun;
        protected string VAR_NOUN = SettingService.Instance.Noun;
        protected string VAR_VERB = SettingService.Instance.Verb;
        protected string VAR_PERSONAL = SettingService.Instance.Personal;
        protected string VAR_ADJECTIVE = SettingService.Instance.Adjective;
        protected string VAR_ARTICLE = SettingService.Instance.Article;
        protected string VAR_NUMERAL = SettingService.Instance.Numeral;
        protected string VAR_PREPOSITION = SettingService.Instance.Preposition;
        protected string VAR_POSSESSIVE = SettingService.Instance.Possessive;
        protected string VAR_DEMONSTRATIVE = SettingService.Instance.Demostrtive;
        protected string VAR_ADVERB = SettingService.Instance.Adverb;
        protected string VAR_ADVERB_ADVERB = SettingService.Instance.Adverb_Adverb;
        protected string VAR_ADJECTIVE_NOUN = SettingService.Instance.Adjective_Noun;
        protected string VAR_SINGLE = SettingService.Instance.Single;
        protected string VAR_PLURAL = SettingService.Instance.Plural;

        private IWordEmbeddingService _wordEmbeddingService = new WordEmbeddingService();
        #endregion

        #region FILTER
        private List<Lesson> FilterLesson(List<Lesson> matters, List<string> types)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                for (int quantity = 0; quantity < matters.Count(); quantity++)
                {
                    types.ForEach(item =>
                    {
                        if (matters[quantity].team.Contains(item))
                            lessons.Add(matters[quantity]);
                    });
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region VERIFY
        private bool VerifyVerbSS(List<Word> words, List<Sentenca> sentences, bool noun)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word = string.Empty;
                string verb = string.Empty;
                words.ForEach(item =>
                {
                    if (noun)
                        if ((item.kind == VAR_NOUN) && (item.sentense == VAR_SUBJECT) && (item.team == VAR_NOUN)) word = item.term;
                    else
                        if ((item.kind == VAR_PRONOUN) && (item.sentense == VAR_SUBJECT)) word = item.term;
                    if (item.kind == VAR_VERB) verb = item.term;
                });
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, word, verb);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return false;
            }
        }

        private bool VerifyVerbPS(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word_adjective = string.Empty;
                string word_verb = string.Empty;

                string verb = string.Empty;
                string verb_adverb = string.Empty;
                string verb_adverb_adverb = string.Empty;
                string adjective = string.Empty;
                string adjective_adverb = string.Empty;
                string adjective_adverb_adverb = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.kind == VAR_ADJECTIVE) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adjective = item.term;
                    if ((item.kind == VAR_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adjective_adverb = item.term;
                    if ((item.kind == VAR_ADVERB_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adjective_adverb_adverb = item.term;

                    if (item.kind == VAR_VERB) verb = item.term;
                    if ((item.kind == VAR_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB)) verb_adverb = item.term;
                    if ((item.kind == VAR_ADVERB_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB)) verb_adverb_adverb = item.term;
                });
                if (verb_adverb_adverb != string.Empty) word_verb = verb_adverb_adverb;
                else
                {
                    if (verb_adverb != string.Empty) word_verb = verb_adverb;
                    else
                    {
                        if (verb != string.Empty) word_verb = verb;
                    }
                }
                if (adjective_adverb_adverb != string.Empty) word_adjective = adjective_adverb_adverb;
                else
                {
                    if (adjective_adverb != string.Empty) word_adjective = adjective_adverb;
                    else
                    {
                        if (adjective != string.Empty) word_adjective = adjective;
                    }
                }
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, word_verb, word_adjective);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return false;
            }
        }

        private bool VerifyVerbODAA(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word_noun = string.Empty;
                string word_verb = string.Empty;

                string substantive = string.Empty;
                string article = string.Empty;
                string adjective = string.Empty;
                string verb = string.Empty;
                string adverb = string.Empty;
                string adverb_adverb = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.kind == VAR_NOUN) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN)) substantive = item.term;
                    if ((item.kind == VAR_ARTICLE) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN)) article = item.term;
                    if ((item.kind == VAR_ADJECTIVE) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN)) adjective = item.term;

                    if (item.kind == VAR_VERB) verb = item.term;
                    if ((item.kind == VAR_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB)) adverb = item.term;
                    if ((item.kind == VAR_ADVERB_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB)) adverb_adverb = item.term;
                });
                if (article != string.Empty) word_noun = article;
                else
                {
                    if (adjective != string.Empty) word_noun = adjective;
                    else word_noun = substantive;
                }
                if (adverb_adverb != string.Empty) word_verb = adverb_adverb;
                else
                {
                    if (adverb != string.Empty) word_verb = adverb;
                    else word_verb = verb;
                }
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, word_verb, word_noun);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return false;
            }
        }

        private bool VerifyVerbOD(List<Word> words, List<Sentenca> sentences, bool noun)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word_noun = string.Empty;
                string word_verb = string.Empty;

                string substantive = string.Empty;
                string numeral = string.Empty;
                string article = string.Empty;
                string pronoun = string.Empty;
                string verb = string.Empty;
                string adverb = string.Empty;
                string adverb_adverb = string.Empty;
                words.ForEach(item =>
                {
                    if (noun)
                    {
                        if ((item.kind == VAR_NOUN) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN)) substantive = item.term;
                        if ((item.kind == VAR_NUMERAL) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN)) numeral = item.term;
                        if ((item.kind == VAR_ARTICLE) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN)) article = item.term;
                        if ((item.kind == VAR_PRONOUN) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN)) pronoun = item.term;
                    }
                    else
                        if ((item.kind == VAR_PRONOUN) && (item.sentense == VAR_PREDICATE)) word_noun = item.term;
                    if (item.kind == VAR_VERB) verb = item.term;
                    if ((item.kind == VAR_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB)) adverb = item.term;
                    if ((item.kind == VAR_ADVERB_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB)) adverb_adverb = item.term;
                });
                if (numeral != string.Empty) word_noun = numeral;
                else
                {
                    if (article != string.Empty) word_noun = article;
                    else
                    {
                        if (pronoun != string.Empty) word_noun = pronoun;
                        else
                        {
                            if (noun == true) word_noun = substantive;
                        }
                    }
                }
                if (adverb_adverb != string.Empty) word_verb = adverb_adverb;
                else
                {
                    if (adverb != string.Empty) word_verb = adverb;
                    else
                    {
                        if (verb != string.Empty) word_verb = verb;
                    }
                }
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, word_verb, word_noun);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return false;
            }
        }

        public bool VerifyVerbOI(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word_preposition = string.Empty;
                string word_verb = string.Empty;

                string verb = string.Empty;
                string adverb = string.Empty;
                string adverb_adverb = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.kind == VAR_PREPOSITION) && (item.sentense == VAR_PREDICATE)) word_preposition = item.term;
                    if (item.kind == VAR_VERB) verb = item.term;
                    if ((item.kind == VAR_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB)) adverb = item.term;
                    if ((item.kind == VAR_ADVERB_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB)) adverb_adverb = item.term;
                });
                if (adverb_adverb != string.Empty) word_verb = adverb_adverb;
                else
                {
                    if (adverb != string.Empty) word_verb = adverb;
                    else
                    {
                        if (verb != string.Empty) word_verb = verb;
                    }
                }
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, word_verb, word_preposition);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return false;
            }
        }

        private bool VerifyAdjectiveODAA(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word_noun = string.Empty;
                string word_adjective = string.Empty;

                string substantive = string.Empty;
                string article = string.Empty;
                string adjective_noun = string.Empty;
                string adjective = string.Empty;
                string adverb = string.Empty;
                string adverb_adverb = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.kind == VAR_NOUN) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN)) substantive = item.term;
                    if ((item.kind == VAR_ARTICLE) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN)) article = item.term;
                    if ((item.kind == VAR_ADJECTIVE) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN)) adjective = item.term;

                    if ((item.kind == VAR_ADJECTIVE) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adjective = item.term;
                    if ((item.kind == VAR_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adverb = item.term;
                    if ((item.kind == VAR_ADVERB_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adverb_adverb = item.term;
                });
                if (article != string.Empty) word_noun = article;
                else
                {
                    if (adjective_noun != string.Empty) word_noun = adjective_noun;
                    else word_noun = substantive;
                }
                if (adverb_adverb != string.Empty) word_adjective = adverb_adverb;
                else
                {
                    if (adverb != string.Empty) word_adjective = adverb;
                    else word_adjective = adjective;
                }
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, word_adjective, word_noun);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return false;
            }
        }

        private bool VerifyAdjectiveOD(List<Word> words, List<Sentenca> sentences, bool noun)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word_noun = string.Empty;
                string word_adjective = string.Empty;

                string substantive = string.Empty;
                string numeral = string.Empty;
                string article = string.Empty;
                string pronoun = string.Empty;
                string adjective = string.Empty;
                string adverb = string.Empty;
                string adverb_adverb = string.Empty;
                words.ForEach(item =>
                {
                    if (noun)
                    {
                        if ((item.kind == VAR_NOUN) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN)) substantive = item.term;
                        if ((item.kind == VAR_NUMERAL) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN)) numeral = item.term;
                        if ((item.kind == VAR_ARTICLE) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN)) article = item.term;
                        if ((item.kind == VAR_PRONOUN) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN)) pronoun = item.term;
                    }
                    else
                        if ((item.kind == VAR_PRONOUN) && (item.sentense == VAR_PREDICATE)) word_noun = item.term;
                    if ((item.kind == VAR_ADJECTIVE) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adjective = item.term;
                    if ((item.kind == VAR_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adverb = item.term;
                    if ((item.kind == VAR_ADVERB_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adverb_adverb = item.term;
                });
                if (numeral != string.Empty) word_noun = numeral;
                else
                {
                    if (article != string.Empty) word_noun = article;
                    else
                    {
                        if (pronoun != string.Empty) word_noun = pronoun;
                        else
                        {
                            if (noun == true) word_noun = substantive;
                        }
                    }
                }
                if (adverb_adverb != string.Empty) word_adjective = adverb_adverb;
                else
                {
                    if (adverb != string.Empty) word_adjective = adverb;
                    else
                    {
                        if (adjective != string.Empty) word_adjective = adjective;
                    }
                }
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, word_adjective, word_noun);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return false;
            }
        }

        public bool VerifyAdjectiveOI(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word_preposition = string.Empty;
                string word_adjective = string.Empty;

                string adjective = string.Empty;
                string adverb = string.Empty;
                string adverb_adverb = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.kind == VAR_PREPOSITION) && (item.sentense == VAR_PREDICATE)) word_preposition = item.term;
                    if ((item.kind == VAR_ADJECTIVE) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adverb = item.term;
                    if ((item.kind == VAR_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adverb = item.term;
                    if ((item.kind == VAR_ADVERB_ADVERB) && (item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE)) adverb_adverb = item.term;
                });
                if (adverb_adverb != string.Empty) word_adjective = adverb_adverb;
                else
                {
                    if (adverb != string.Empty) word_adjective = adverb;
                    else
                    {
                        if (adjective != string.Empty) word_adjective = adjective;
                    }
                }
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, word_adjective, word_preposition);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return false;
            }
        }
        #endregion

        #region PUBLIC
        public string GetOration(List<Word> words)
        {
            try
            {
                string pronoun_subject = string.Empty;
                string article_subject = string.Empty;
                string digit_subject = string.Empty;
                string noun_subject = string.Empty;
                string verb = string.Empty;
                string model = string.Empty;
                string pronoun_predicate = string.Empty;
                string article_predicate = string.Empty;
                string digit_predicate = string.Empty;
                string noun_predicate = string.Empty;
                string preposition = string.Empty;

                words.ForEach(word =>
                {
                    if ((word.sentense == VAR_SUBJECT) && (word.kind == VAR_PRONOUN)) pronoun_subject = word.term;
                    if ((word.sentense == VAR_SUBJECT) && (word.kind == VAR_NUMERAL)) digit_subject = word.term;
                    if ((word.sentense == VAR_SUBJECT) && (word.kind == VAR_ARTICLE)) article_subject = word.term;
                    if ((word.sentense == VAR_SUBJECT) && (word.kind == VAR_NOUN)) noun_subject = word.term;
                    if ((word.sentense == VAR_PREDICATE) && (word.kind == VAR_VERB))
                    {
                        verb = word.term;
                        model = word.term;
                    }
                    if ((word.sentense == VAR_PREDICATE) && (word.kind == VAR_PREPOSITION)) preposition = word.term;
                    if ((word.sentense == VAR_PREDICATE) && (word.kind == VAR_PRONOUN)) pronoun_predicate = word.term;
                    if ((word.sentense == VAR_PREDICATE) && (word.kind == VAR_NUMERAL)) digit_predicate = word.term;
                    if ((word.sentense == VAR_PREDICATE) && (word.kind == VAR_ARTICLE)) article_predicate = word.term;
                    if ((word.sentense == VAR_PREDICATE) && (word.kind == VAR_NOUN)) noun_predicate = word.term;
                });

                string term = string.Empty;
                if ((pronoun_subject != string.Empty) && (noun_subject == string.Empty)) term = pronoun_subject + " " + verb;
                if ((noun_subject != string.Empty) && (pronoun_subject == string.Empty) && (digit_subject == string.Empty) && (article_subject == string.Empty)) term = noun_subject + " " + verb;
                if ((noun_subject != string.Empty) && (digit_subject != string.Empty)) term = digit_subject + " " + pronoun_subject + " " + verb;
                if ((noun_subject != string.Empty) && (article_subject != string.Empty)) term = article_subject + " " + pronoun_subject + " " + verb;
                if (preposition != string.Empty) term = term + " " + preposition;
                if ((pronoun_predicate != string.Empty) && (noun_predicate == string.Empty)) term = term + " " + pronoun_predicate;
                if ((pronoun_predicate != string.Empty) && (noun_predicate != string.Empty)) term = term + " " + pronoun_predicate + " " + noun_predicate;
                if ((noun_predicate != string.Empty) && (pronoun_predicate == string.Empty) && (article_predicate == string.Empty) && (digit_predicate == string.Empty)) term = term + " " + noun_predicate;
                if ((noun_predicate != string.Empty) && (article_predicate != string.Empty)) term = term + " " + article_predicate + " " + noun_predicate;
                if ((noun_predicate != string.Empty) && (digit_predicate != string.Empty)) term = term + " " + digit_predicate + " " + noun_predicate;
                return term;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);

                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);

                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_Adj(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_adjective = new List<string>();
                type_adjective.Add(VAR_ADJECTIVE);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, type_adjective);

                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson adjective in adjectives)
                        {
                            List<Word> words1 = new List<Word>();
                            words.ForEach(item =>
                            {
                                words1.Add(item);
                            });
                            adjective.lecture.ForEach(item =>
                            {
                                Word word = new Word();
                                word.term = item.term;
                                word.kind = item.kind;
                                word.sentense = VAR_PREDICATE;
                                word.team = adjective.team;
                                words1.Add(word);
                            });
                            if (!VerifyVerbPS(words1, sentences)) continue;
                            Lesson lesson = new Lesson();
                            lesson.lecture = words1;
                            lessons.Add(lesson);
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_AdjN(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_adjective_noun = new List<string>();
                type_adjective_noun.Add(VAR_ADJECTIVE_NOUN);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);

                List<Lesson> adjectives_nouns = new List<Lesson>();
                adjectives_nouns = FilterLesson(matters, type_adjective_noun);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson adjective_noun in adjectives_nouns)
                        {
                            List<Word> words1 = new List<Word>();
                            words.ForEach(item =>
                            {
                                words1.Add(item);
                            });
                            adjective_noun.lecture.ForEach(item =>
                            {
                                Word word = new Word();
                                word.term = item.term;
                                word.kind = item.kind;
                                word.sentense = VAR_PREDICATE;
                                word.team = adjective_noun.team;
                                words1.Add(word);
                            });
                            if (!VerifyVerbODAA(words1, sentences)) continue;
                            Lesson lesson = new Lesson();
                            lesson.lecture = words1;
                            lessons.Add(lesson);
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_Adj_AdjN(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_adjective = new List<string>();
                type_adjective.Add(VAR_ADJECTIVE);
                List<string> type_adjective_noun = new List<string>();
                type_adjective_noun.Add(VAR_ADJECTIVE_NOUN);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, type_adjective);
                List<Lesson> adjectives_nouns = new List<Lesson>();
                adjectives_nouns = FilterLesson(matters, type_adjective_noun);
                
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson adjective in adjectives)
                        {
                            List<Word> words1 = new List<Word>();
                            words.ForEach(item =>
                            {
                                words1.Add(item);
                            });
                            adjective.lecture.ForEach(item =>
                            {
                                Word word = new Word();
                                word.term = item.term;
                                word.kind = item.kind;
                                word.sentense = VAR_PREDICATE;
                                word.team = adjective.team;
                                words1.Add(word);
                            });
                            if (!VerifyVerbPS(words1, sentences)) continue;
                            foreach (Lesson adjective_noun in adjectives_nouns)
                            {
                                List<Word> words2 = new List<Word>();
                                words1.ForEach(item =>
                                {
                                    words2.Add(item);
                                });
                                adjective_noun.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word.term = item.term;
                                    word.kind = item.kind;
                                    word.sentense = VAR_PREDICATE;
                                    word.team = adjective_noun.team;
                                    words2.Add(word);
                                });
                                if (!((VerifyVerbODAA(words2, sentences)) || (VerifyAdjectiveODAA(words2, sentences)))) continue;
                                Lesson lesson = new Lesson();
                                lesson.lecture = words2;
                                lessons.Add(lesson);
                            }
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_Adj_N(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_adjective = new List<string>();
                type_adjective.Add(VAR_ADJECTIVE);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, type_adjective);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, type_noun);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson adjective in adjectives)
                        {
                            List<Word> words1 = new List<Word>();
                            words.ForEach(item =>
                            {
                                words1.Add(item);
                            });
                            adjective.lecture.ForEach(item =>
                            {
                                Word word = new Word();
                                word.term = item.term;
                                word.kind = item.kind;
                                word.sentense = VAR_PREDICATE;
                                word.team = adjective.team;
                                words1.Add(word);
                            });
                            if (!VerifyVerbPS(words1, sentences)) continue;
                            foreach (Lesson substantive in nouns)
                            {
                                List<Word> words2 = new List<Word>();
                                words1.ForEach(item =>
                                {
                                    words2.Add(item);
                                });
                                substantive.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word.term = item.term;
                                    word.kind = item.kind;
                                    word.sentense = VAR_PREDICATE;
                                    word.team = substantive.team;
                                    words2.Add(word);
                                });
                                if (!((VerifyVerbOD(words2, sentences, true)) || (VerifyAdjectiveOD(words2, sentences, true)))) continue;
                                Lesson lesson = new Lesson();
                                lesson.lecture = words2;
                                lessons.Add(lesson);
                            }
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_Adj_P(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_adjective = new List<string>();
                type_adjective.Add(VAR_ADJECTIVE);
                List<string> type_possessive = new List<string>();
                type_possessive.Add(VAR_POSSESSIVE);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, type_adjective);
                List<Lesson> possessives = new List<Lesson>();
                possessives = FilterLesson(matters, type_possessive);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson adjective in adjectives)
                        {
                            List<Word> words1 = new List<Word>();
                            words.ForEach(item =>
                            {
                                words1.Add(item);
                            });
                            adjective.lecture.ForEach(item =>
                            {
                                Word word = new Word();
                                word.term = item.term;
                                word.kind = item.kind;
                                word.sentense = VAR_PREDICATE;
                                word.team = adjective.team;
                                words1.Add(word);
                            });
                            if (!VerifyVerbPS(words1, sentences)) continue;
                            foreach (Lesson possessive_value in possessives)
                            {
                                List<Word> words2 = new List<Word>();
                                words1.ForEach(item =>
                                {
                                    words2.Add(item);
                                });
                                possessive_value.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word.term = item.term;
                                    word.kind = item.kind;
                                    word.sentense = VAR_PREDICATE;
                                    word.team = possessive_value.team;
                                    words2.Add(word);
                                });
                                if (!((VerifyVerbOD(words2, sentences, false)) || (VerifyAdjectiveOD(words2, sentences, false)))) continue;
                                Lesson lesson = new Lesson();
                                lesson.lecture = words2;
                                lessons.Add(lesson);
                            }
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_Adj_Pr_AdjN(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_adjective = new List<string>();
                type_adjective.Add(VAR_ADJECTIVE);
                List<string> type_adjective_noun = new List<string>();
                type_adjective_noun.Add(VAR_ADJECTIVE_NOUN);
                List<string> type_preposition = new List<string>();
                type_preposition.Add(VAR_PREPOSITION);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, type_adjective);
                List<Lesson> adjectives_nouns = new List<Lesson>();
                adjectives_nouns = FilterLesson(matters, type_adjective_noun);
                List<Lesson> prepositions = new List<Lesson>();
                prepositions = FilterLesson(matters, type_preposition);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson adjective in adjectives)
                        {
                            List<Word> words1 = new List<Word>();
                            words.ForEach(item =>
                            {
                                words1.Add(item);
                            });
                            adjective.lecture.ForEach(item =>
                            {
                                Word word = new Word();
                                word.term = item.term;
                                word.kind = item.kind;
                                word.sentense = VAR_PREDICATE;
                                word.team = adjective.team;
                                words1.Add(word);
                            });
                            if (!VerifyVerbPS(words1, sentences)) continue;
                            foreach (Lesson prepostion in prepositions)
                            {
                                foreach (Lesson adjective_noun in adjectives_nouns)
                                {
                                    List<Word> words2 = new List<Word>();
                                    words1.ForEach(item =>
                                    {
                                        words2.Add(item);
                                    });
                                    adjective_noun.lecture.ForEach(item =>
                                    {
                                        Word word = new Word();
                                        word.term = item.term;
                                        word.kind = item.kind;
                                        word.sentense = VAR_PREDICATE;
                                        word.team = adjective_noun.team;
                                        words2.Add(word);
                                    });
                                    prepostion.lecture.ForEach(item =>
                                    {
                                        Word word = new Word();
                                        word.term = item.term;
                                        word.kind = item.kind;
                                        word.sentense = VAR_PREDICATE;
                                        word.team = prepostion.team;
                                        words2.Add(word);
                                    });
                                    if (!((VerifyVerbOI(words2, sentences)) || (VerifyAdjectiveOI(words2, sentences)))) continue;
                                    Lesson lesson = new Lesson();
                                    lesson.lecture = words2;
                                    lessons.Add(lesson);
                                }
                            }
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_Adj_Pr_N(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_adjective = new List<string>();
                type_adjective.Add(VAR_ADJECTIVE);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_preposition = new List<string>();
                type_preposition.Add(VAR_PREPOSITION);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, type_adjective);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, type_noun);
                List<Lesson> prepositions = new List<Lesson>();
                prepositions = FilterLesson(matters, type_preposition);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson adjective in adjectives)
                        {
                            List<Word> words1 = new List<Word>();
                            words.ForEach(item =>
                            {
                                words1.Add(item);
                            });
                            adjective.lecture.ForEach(item =>
                            {
                                Word word = new Word();
                                word.term = item.term;
                                word.kind = item.kind;
                                word.sentense = VAR_PREDICATE;
                                word.team = adjective.team;
                                words1.Add(word);
                            });
                            if (!VerifyVerbPS(words1, sentences)) continue;
                            foreach (Lesson prepostion in prepositions)
                            {
                                foreach (Lesson substantive in nouns)
                                {
                                    List<Word> words2 = new List<Word>();
                                    words1.ForEach(item =>
                                    {
                                        words2.Add(item);
                                    });
                                    substantive.lecture.ForEach(item =>
                                    {
                                        Word word = new Word();
                                        word.term = item.term;
                                        word.kind = item.kind;
                                        word.sentense = VAR_PREDICATE;
                                        word.team = substantive.team;
                                        words2.Add(word);
                                    });
                                    prepostion.lecture.ForEach(item =>
                                    {
                                        Word word = new Word();
                                        word.term = item.term;
                                        word.kind = item.kind;
                                        word.sentense = VAR_PREDICATE;
                                        word.team = prepostion.team;
                                        words2.Add(word);
                                    });
                                    if (!((VerifyVerbOI(words2, sentences)) || (VerifyAdjectiveOI(words2, sentences)))) continue;
                                    Lesson lesson = new Lesson();
                                    lesson.lecture = words2;
                                    lessons.Add(lesson);
                                }
                            }
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_Adj_Pr_P(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_adjective = new List<string>();
                type_adjective.Add(VAR_ADJECTIVE);
                List<string> type_possessive = new List<string>();
                type_possessive.Add(VAR_POSSESSIVE);
                List<string> type_preposition = new List<string>();
                type_preposition.Add(VAR_PREPOSITION);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, type_adjective);
                List<Lesson> possessives = new List<Lesson>();
                possessives = FilterLesson(matters, type_possessive);
                List<Lesson> prepositions = new List<Lesson>();
                prepositions = FilterLesson(matters, type_preposition);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson adjective in adjectives)
                        {
                            List<Word> words1 = new List<Word>();
                            words.ForEach(item =>
                            {
                                words1.Add(item);
                            });
                            adjective.lecture.ForEach(item =>
                            {
                                Word word = new Word();
                                word.term = item.term;
                                word.kind = item.kind;
                                word.sentense = VAR_PREDICATE;
                                word.team = adjective.team;
                                words1.Add(word);
                            });
                            if (!VerifyVerbPS(words1, sentences)) continue;
                            foreach (Lesson preposition in prepositions)
                            {
                                foreach (Lesson possessive in possessives)
                                {
                                    List<Word> words2 = new List<Word>();
                                    words1.ForEach(item =>
                                    {
                                        words2.Add(item);
                                    });
                                    possessive.lecture.ForEach(item =>
                                    {
                                        Word word = new Word();
                                        word.term = item.term;
                                        word.kind = item.kind;
                                        word.sentense = VAR_PREDICATE;
                                        word.team = possessive.team;
                                        words2.Add(word);
                                    });
                                    preposition.lecture.ForEach(item =>
                                    {
                                        Word word = new Word();
                                        word.term = item.term;
                                        word.kind = item.kind;
                                        word.sentense = VAR_PREDICATE;
                                        word.team = preposition.team;
                                        words2.Add(word);
                                    });
                                    if (!((VerifyVerbOI(words2, sentences)) || (VerifyAdjectiveOI(words2, sentences)))) continue;
                                    Lesson lesson = new Lesson();
                                    lesson.lecture = words2;
                                    lessons.Add(lesson);
                                }
                            }
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_N(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, type_noun);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson substantive in nouns)
                        {
                            List<Word> words1 = new List<Word>();
                            words.ForEach(item =>
                            {
                                words1.Add(item);
                            });
                            substantive.lecture.ForEach(item =>
                            {
                                Word word = new Word();
                                word.term = item.term;
                                word.kind = item.kind;
                                word.sentense = VAR_PREDICATE;
                                word.team = substantive.team;
                                words1.Add(word);
                            });
                            if (!VerifyVerbOD(words1, sentences, true)) continue;
                            Lesson lesson = new Lesson();
                            lesson.lecture = words1;
                            lessons.Add(lesson);
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_P(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_possessive = new List<string>();
                type_verb.Add(VAR_POSSESSIVE);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> possessives = new List<Lesson>();
                possessives = FilterLesson(matters, type_possessive);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson possessive in possessives)
                        {
                            List<Word> words1 = new List<Word>();
                            words.ForEach(item =>
                            {
                                words1.Add(item);
                            });
                            possessive.lecture.ForEach(item =>
                            {
                                Word word = new Word();
                                word.term = item.term;
                                word.kind = item.kind;
                                word.sentense = VAR_PREDICATE;
                                word.team = possessive.team;
                                words1.Add(word);
                            });
                            if (!VerifyVerbOD(words1, sentences, false)) continue;
                            Lesson lesson = new Lesson();
                            lesson.lecture = words1;
                            lessons.Add(lesson);
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_Pr_AdjN(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_adjective_noun = new List<string>();
                type_adjective_noun.Add(VAR_ADJECTIVE_NOUN);
                List<string> type_preposition = new List<string>();
                type_preposition.Add(VAR_PREPOSITION);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> adjectives_nouns = new List<Lesson>();
                adjectives_nouns = FilterLesson(matters, type_adjective_noun);
                List<Lesson> prepositions = new List<Lesson>();
                prepositions = FilterLesson(matters, type_preposition);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson preposition in prepositions)
                        {
                            foreach (Lesson adjective_noun in adjectives_nouns)
                            {
                                List<Word> words1 = new List<Word>();
                                words.ForEach(item =>
                                {
                                    words1.Add(item);
                                });
                                adjective_noun.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word.term = item.term;
                                    word.kind = item.kind;
                                    word.sentense = VAR_PREDICATE;
                                    word.team = adjective_noun.team;
                                    words1.Add(word);
                                });
                                preposition.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word.term = item.term;
                                    word.kind = item.kind;
                                    word.sentense = VAR_PREDICATE;
                                    word.team = preposition.team;
                                    words1.Add(word);
                                });
                                if (!VerifyVerbOI(words1, sentences)) continue;
                                Lesson lesson = new Lesson();
                                lesson.lecture = words1;
                                lessons.Add(lesson);
                            }
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_Pr_N(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_preposition = new List<string>();
                type_preposition.Add(VAR_PREPOSITION);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, type_noun);
                List<Lesson> prepositions = new List<Lesson>();
                prepositions = FilterLesson(lessons, type_preposition);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson preposition in prepositions)
                        {
                            foreach (Lesson substantive in nouns)
                            {
                                List<Word> words1 = new List<Word>();
                                words.ForEach(item =>
                                {
                                    words1.Add(item);
                                });
                                substantive.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word.term = item.term;
                                    word.kind = item.kind;
                                    word.sentense = VAR_PREDICATE;
                                    word.team = substantive.team;
                                    words1.Add(word);
                                });
                                preposition.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word.term = item.term;
                                    word.kind = item.kind;
                                    word.sentense = VAR_PREDICATE;
                                    word.team = preposition.team;
                                    words1.Add(word);
                                });
                                if (!VerifyVerbOI(words1, sentences)) continue;
                                Lesson lesson = new Lesson();
                                lesson.lecture = words1;
                                lessons.Add(lesson);
                            }
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PeriodSS_V_Pr_P(string language, List<Sentenca> sentences, List<Lesson> matters, bool noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                List<string> type_verb = new List<string>();
                type_verb.Add(VAR_VERB);
                List<string> type_noun = new List<string>();
                type_noun.Add(VAR_NOUN);
                List<string> type_pronoun = new List<string>();
                type_pronoun.Add(VAR_PERSONAL);
                type_pronoun.Add(VAR_DEMONSTRATIVE);
                List<string> type_possessive = new List<string>();
                type_verb.Add(VAR_POSSESSIVE);
                List<string> type_preposition = new List<string>();
                type_preposition.Add(VAR_PREPOSITION);

                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, type_verb);
                List<Lesson> subjects = new List<Lesson>();
                if (noun) subjects = FilterLesson(matters, type_noun);
                else subjects = FilterLesson(matters, type_pronoun);
                List<Lesson> possessives = new List<Lesson>();
                possessives = FilterLesson(matters, type_possessive);
                List<Lesson> prepositions = new List<Lesson>();
                prepositions = FilterLesson(matters, type_preposition);
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson subject in subjects)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_PREDICATE;
                            word.team = verb.team;
                            words.Add(word);
                        });
                        subject.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word.term = item.term;
                            word.kind = item.kind;
                            word.sentense = VAR_SUBJECT;
                            word.team = subject.team;
                            words.Add(word);
                        });
                        if (!VerifyVerbSS(words, sentences, noun)) continue;
                        foreach (Lesson preposition in prepositions)
                        {
                            foreach (Lesson possessive in possessives)
                            {
                                List<Word> words1 = new List<Word>();
                                words.ForEach(item =>
                                {
                                    words1.Add(item);
                                });
                                possessive.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word.term = item.term;
                                    word.kind = item.kind;
                                    word.sentense = VAR_PREDICATE;
                                    word.team = possessive.team;
                                    words1.Add(word);
                                });
                                preposition.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word.term = item.term;
                                    word.kind = item.kind;
                                    word.sentense = VAR_PREDICATE;
                                    word.team = preposition.team;
                                    words1.Add(word);
                                });
                                if (!VerifyVerbOI(words1, sentences)) continue;
                                Lesson lesson = new Lesson();
                                lesson.lecture = words1;
                                lessons.Add(lesson);
                            }
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion
    }
}
