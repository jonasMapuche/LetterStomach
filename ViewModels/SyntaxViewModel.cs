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
        private string VAR_SUBJECT = SettingService.Instance.Suject;
        private string VAR_PREDICATE = SettingService.Instance.Predicate;
        private string VAR_PRONOUN = SettingService.Instance.Pronoun;
        private string VAR_NOUN = SettingService.Instance.Noun;
        private string VAR_VERB = SettingService.Instance.Verb;
        private string VAR_PERSONAL = SettingService.Instance.Personal;
        private string VAR_ADJECTIVE = SettingService.Instance.Adjective;
        private string VAR_ARTICLE = SettingService.Instance.Article;
        private string VAR_NUMERAL = SettingService.Instance.Numeral;
        private string VAR_PREPOSITION = SettingService.Instance.Preposition;
        private string VAR_POSSESSIVE = SettingService.Instance.Possessive;
        private string VAR_DEMONSTRATIVE = SettingService.Instance.Demostrtive;
        private string VAR_ADVERB = SettingService.Instance.Adverb;
        private string VAR_ADVERB_ADVERB = SettingService.Instance.Adverb_Adverb;
        private string VAR_ADJECTIVE_NOUN = SettingService.Instance.Adjective_Noun;
        private string VAR_ADJECTIVE_ADVERB = SettingService.Instance.Adjective_Adverb;
        private string VAR_CONJUNCTION = SettingService.Instance.Conjunction;
        private string VAR_SINGLE = SettingService.Instance.Single;
        private string VAR_PLURAL = SettingService.Instance.Plural;
        private string VAR_NUMERAL_NOUN = SettingService.Instance.Numeral_Noun;

        private int VAR_ORDER_1 = 1;
        private int VAR_ORDER_2 = 2;
        private int VAR_ORDER_3 = 3;
        private int VAR_ORDER_4 = 4;

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
        private bool VerifyVerbSampleSubject(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string noun = string.Empty;
                string verb = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_SUBJECT) && (item.team == VAR_NOUN) && (item.kind == VAR_NOUN)) noun = item.term;
                    if ((item.sentense == VAR_SUBJECT) && (item.kind == VAR_PRONOUN)) noun = item.term;
                    if (item.kind == VAR_VERB) verb = item.term;
                });
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun, verb);
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

        private bool VerifyVerbCompoundSubject(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string noun = string.Empty;
                string last = string.Empty;
                string adjunct = string.Empty;
                string conjunction = string.Empty;
                string verb = string.Empty;
                int order_first = VAR_ORDER_1;
                int order_last = VAR_ORDER_3;
                words.ForEach(item =>
                {
                    if (item.order == order_first)
                    {
                        if ((item.sentense == VAR_SUBJECT) && (item.team == VAR_NOUN) && (item.kind == VAR_NOUN)) noun = item.term;
                        if ((item.sentense == VAR_SUBJECT) && (item.kind == VAR_PRONOUN)) noun = item.term;
                    }
                    if (item.order == order_last)
                    {
                        if ((item.sentense == VAR_SUBJECT) && (item.team == VAR_NOUN) && (item.kind == VAR_NOUN)) last = item.term;
                        if ((item.sentense == VAR_SUBJECT) && (item.kind == VAR_PRONOUN)) last = item.term;
                        if ((item.sentense == VAR_SUBJECT) && (item.kind == VAR_NOUN) && 
                            (item.kind == VAR_NUMERAL) || (item.kind == VAR_ARTICLE) || (item.kind == VAR_PRONOUN)) adjunct = item.term;
                    }
                    if ((item.sentense == VAR_SUBJECT) && (item.kind == VAR_CONJUNCTION)) conjunction = item.term;
                    if (item.kind == VAR_VERB) verb = item.term;
                });
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun, conjunction);
                if (similarity)
                {
                    if (adjunct != string.Empty) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, conjunction, adjunct);
                    else similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, conjunction, last);
                }
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, last, verb);
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

        private bool VerifyVerbPredicative(List<Word> words, List<Sentenca> sentences)
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
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE) && (item.kind == VAR_ADJECTIVE)) adjective = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE) && (item.kind == VAR_ADVERB)) adjective_adverb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE) && (item.kind == VAR_ADVERB_ADVERB)) adjective_adverb_adverb = item.term;
                    if (item.kind == VAR_VERB) verb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB) && (item.kind == VAR_ADVERB)) verb_adverb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB) && (item.kind == VAR_ADVERB_ADVERB)) verb_adverb_adverb = item.term;
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

        private bool VerifyVerbAdjectiveNoun(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word_noun = string.Empty;
                string word_verb = string.Empty;
                string noun = string.Empty;
                string adjunct = string.Empty;
                string adjective = string.Empty;
                string verb = string.Empty;
                string adverb = string.Empty;
                string adverb_adverb = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN) && (item.kind == VAR_NOUN)) noun = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN) 
                        && ((item.kind == VAR_ARTICLE) || (item.kind == VAR_NUMERAL) || (item.kind == VAR_PRONOUN))) adjunct = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN) && (item.kind == VAR_ADJECTIVE)) adjective = item.term;
                    if (item.kind == VAR_VERB) verb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB) && (item.kind == VAR_ADVERB)) adverb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB) && (item.kind == VAR_ADVERB_ADVERB)) adverb_adverb = item.term;
                });
                if (adjunct != string.Empty) word_noun = adjunct;
                else
                {
                    if (adjective != string.Empty) word_noun = adjective;
                    else word_noun = noun;
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

        private bool VerifyVerbDirectObject(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word_noun = string.Empty;
                string word_verb = string.Empty;
                string noun = string.Empty;
                string adjunct = string.Empty;
                string verb = string.Empty;
                string adverb = string.Empty;
                string adverb_adverb = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN) && (item.kind == VAR_NOUN)) noun = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN) && 
                        ((item.kind == VAR_NUMERAL) || (item.kind == VAR_ARTICLE) || (item.kind == VAR_PRONOUN))) adjunct = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.kind == VAR_PRONOUN)) noun = item.term;
                    if (item.kind == VAR_VERB) verb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB) && (item.kind == VAR_ADVERB)) adverb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB) && (item.kind == VAR_ADVERB_ADVERB)) adverb_adverb = item.term;
                });
                if (adjunct != string.Empty) 
                    word_noun = adjunct;
                else
                    word_noun = noun;
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

        private bool VerifyVerbIndirectObject(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string word_preposition = string.Empty;
                string word_verb = string.Empty;
                string word_noun = string.Empty;
                string verb = string.Empty;
                string adverb = string.Empty;
                string adverb_adverb = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_PREDICATE) && (item.kind == VAR_PREPOSITION)) word_preposition = item.term;
                    if (item.kind == VAR_VERB) verb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB) && (item.kind == VAR_ADVERB)) adverb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_VERB) && (item.kind == VAR_ADVERB_ADVERB)) adverb_adverb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN)) word_noun = item.term;
                });
                if ((adverb_adverb != string.Empty) && (adverb != string.Empty) && (verb != string.Empty)) word_verb = adverb_adverb;
                if ((adverb_adverb == string.Empty) && (adverb != string.Empty) && (verb != string.Empty)) word_verb = adverb;
                if ((adverb_adverb == string.Empty) && (adverb == string.Empty) && (verb != string.Empty)) word_verb = verb;
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, word_verb, word_preposition);
                if (similarity == false) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, word_noun, word_preposition);
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

        private bool VerifyNumeralConjunctionNoun(List<Word> words, List<Sentenca> sentences, int order_conjunction)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string conjunction = string.Empty;
                string numeral = string.Empty;
                string noun = string.Empty;
                string preposition = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_CONJUNCTION) && (item.kind == VAR_CONJUNCTION) && (item.order == order_conjunction)) conjunction = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_NUMERAL) && (item.kind == VAR_NUMERAL) && (item.order == order_conjunction)) numeral = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_NUMERAL_NOUN) && (item.kind == VAR_NUMERAL) && (item.order == order_conjunction)) noun = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.kind == VAR_PREPOSITION) && (item.order == order_conjunction)) preposition = item.term; 
                });
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, numeral, conjunction);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, conjunction, noun);
                if ((similarity) && (preposition != string.Empty)) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, numeral);
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

        private bool VerifyAdjectiveConjunctionNoun(List<Word> words, List<Sentenca> sentences, int order_conjunction)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string conjunction = string.Empty;
                string adjective = string.Empty;
                string noun = string.Empty;
                string preposition = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_CONJUNCTION) && (item.kind == VAR_CONJUNCTION) && (item.order == order_conjunction)) conjunction = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE) && (item.kind == VAR_ADJECTIVE) && (item.order == order_conjunction)) adjective = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN) && (item.kind == VAR_ADJECTIVE) && (item.order == order_conjunction)) noun = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.kind == VAR_PREPOSITION) && (item.order == order_conjunction)) preposition = item.term;
                });
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, adjective, conjunction);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, conjunction, noun);
                if ((similarity) && (preposition != string.Empty)) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, adjective);
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
        
        private bool VerifyDirectObjectPreposition(List<Word> words, List<Sentenca> sentences, int order_direct_object)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string preposition = string.Empty;
                string noun = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN) && (item.order == order_direct_object)) noun = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_PREPOSITION)) preposition = item.term;
                });
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun, preposition);
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
        
        private bool VerifyIndirectObject(List<Word> words, List<Sentenca> sentences, int order_indirect_object)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string preposition = string.Empty;
                string noun = string.Empty;
                string adjunct = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN) && (item.kind == VAR_NOUN) && (item.order == order_indirect_object)) noun = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.kind == VAR_PRONOUN) && (item.order == order_indirect_object)) noun = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN) && (item.order == order_indirect_object)
                        && ((item.kind == VAR_NUMERAL) || (item.kind == VAR_ARTICLE) || (item.kind == VAR_PRONOUN))) adjunct = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_PREPOSITION) && (item.order == order_indirect_object)) preposition = item.term;
                });
                bool similarity = false;
                if (adjunct != string.Empty) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, adjunct);
                else similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, noun);
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

        private bool VerifyIndirectObjectAdjectiveNoun(List<Word> words, List<Sentenca> sentences, int order_indirect_object)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string adjective = string.Empty;
                string adjunct = string.Empty;
                string preposition = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_PREPOSITION) && (item.order == order_indirect_object)) preposition = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN) && (item.order == order_indirect_object)
                        && ((item.kind == VAR_ARTICLE) || (item.kind == VAR_NUMERAL) || (item.kind == VAR_PRONOUN))) adjunct = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_NOUN) && (item.kind == VAR_ADJECTIVE) && (item.order == order_indirect_object)) adjective = item.term;
                });
                bool similarity = false;
                if (adjunct != string.Empty) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, adjunct);
                else similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, adjective);
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

        private bool VerifyDirectObjectPredicative(List<Word> words, List<Sentenca> sentences, int order_predicative)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string adjective = string.Empty;
                string pronoun = string.Empty;
                string noun = string.Empty;
                int order_object_direct = order_predicative - 1;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_ADVERB) && (item.kind == VAR_ADJECTIVE) && (item.order == order_predicative)) adjective = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_NOUN) && (item.kind == VAR_NOUN) && (item.order == order_object_direct)) noun = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.kind == VAR_PRONOUN) && (item.order == order_object_direct)) pronoun = item.term;
                });
                bool similarity = false;
                if (pronoun != string.Empty) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, pronoun, adjective);
                else similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun, adjective);
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

        private bool VerifyPredicativePreposition(List<Word> words, List<Sentenca> sentences, int order_preposicao)
        {
            try
            {
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string preposition = string.Empty;
                string adjective = string.Empty;
                string adverb = string.Empty;
                int order_predicative = order_preposicao - 1;
                words.ForEach(item =>
                {
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_ADVERB) && (item.kind == VAR_ADJECTIVE) && (item.order == order_predicative)) adjective = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_ADJECTIVE_ADVERB) && (item.kind == VAR_ADVERB) && (item.order == order_predicative)) adverb = item.term;
                    if ((item.sentense == VAR_PREDICATE) && (item.team == VAR_PREPOSITION) && (item.order == order_preposicao)) preposition = item.term;
                });
                bool similarity = false;
                if (adverb != string.Empty) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, adverb, preposition);
                else similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, adjective, preposition);
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

        #region WORD
        private Word Lecture(string term, string kind, string sentence, string team, int order)
        {
            try
            {
                Word word = new Word();
                word.term = term;
                word.kind = kind;
                word.sentense = sentence;
                word.team = team;
                word.order = order;
                return word;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private List<Lesson> Union(List<Lesson> firsts, List<Lesson> lasts)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                firsts.ForEach(item =>
                {
                    lessons.Add(item);
                });
                lasts.ForEach(item =>
                {
                    lessons.Add(item);
                });
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

        #region OBJECT SUBJECT
        private List<Lesson> MountNounVerb(List<Sentenca> sentences, List<Lesson> matters)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind_verb = new List<string>();
                kind_verb.Add(VAR_VERB);
                List<string> kind_noun = new List<string>();
                kind_noun.Add(VAR_NOUN);
                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, kind_verb);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, kind_noun);
                int order_noun = VAR_ORDER_1;
                int order_verb = VAR_ORDER_2;
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson noun in nouns)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, verb.team, order_verb);
                            words.Add(word);
                        });
                        noun.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_SUBJECT, noun.team, order_noun);
                            words.Add(word);
                        });
                        if (!VerifyVerbSampleSubject(words, sentences)) continue;
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

        private List<Lesson> MountPronounVerb(List<Sentenca> sentences, List<Lesson> matters)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind_verb = new List<string>();
                kind_verb.Add(VAR_VERB);
                List<string> kind_pronoun = new List<string>();
                kind_pronoun.Add(VAR_PERSONAL);
                kind_pronoun.Add(VAR_DEMONSTRATIVE);
                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, kind_verb);
                List<Lesson> pronouns = new List<Lesson>();
                pronouns = FilterLesson(matters, kind_pronoun);
                int order_pronoun = VAR_ORDER_1;
                int order_verb = VAR_ORDER_2;
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson pronoun in pronouns)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, verb.team, order_verb);
                            words.Add(word);
                        });
                        pronoun.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_SUBJECT, pronoun.team, order_pronoun);
                            words.Add(word);
                        });
                        if (!VerifyVerbSampleSubject(words, sentences)) continue;
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

        private List<Lesson> MountCompoundVerb(List<Sentenca> sentences, List<Lesson> matters)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind_verb = new List<string>();
                kind_verb.Add(VAR_VERB);
                List<string> kind_noun = new List<string>();
                kind_noun.Add(VAR_NOUN);
                kind_noun.Add(VAR_PERSONAL);
                kind_noun.Add(VAR_ADJECTIVE_NOUN);
                List<string> kind_conjunction = new List<string>();
                kind_conjunction.Add(VAR_CONJUNCTION);
                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, kind_verb);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, kind_noun);
                List<Lesson> conjunctions = new List<Lesson>();
                conjunctions = FilterLesson(matters, kind_conjunction);
                List<Lesson> lasts = new List<Lesson>();
                lasts = FilterLesson(matters, kind_noun);
                int order_noun = VAR_ORDER_1;
                int order_conjunction = VAR_ORDER_2;
                int order_last = VAR_ORDER_3;
                int order_verb = VAR_ORDER_4;
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson conjunction in conjunctions)
                    {
                        foreach (Lesson noun in nouns)
                        {
                            foreach (Lesson last in lasts)
                            {
                                List<Word> words = new List<Word>();
                                verb.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, VAR_PREDICATE, verb.team, order_verb);
                                    words.Add(word);
                                });
                                noun.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, VAR_SUBJECT, noun.team, order_noun);
                                    words.Add(word);
                                });
                                last.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, VAR_SUBJECT, last.team, order_last);
                                    words.Add(word);
                                });
                                conjunction.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, VAR_SUBJECT, conjunction.team, order_conjunction);
                                    words.Add(word);
                                });
                                if (!VerifyVerbCompoundSubject(words, sentences)) continue;
                                Lesson lesson = new Lesson();
                                lesson.lecture = words;
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

        #region OBJECT OBJECT DIRECT
        private List<Lesson> MountVerbNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_NOUN);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, kind);
                if (nouns.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
                    foreach (Lesson noun in nouns)
                    {
                        List<Word> words1 = new List<Word>();
                        words.ForEach(item =>
                        {
                            words1.Add(item);
                        });
                        noun.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, noun.team, order_noun);
                            words1.Add(word);
                        });
                        if (!VerifyVerbDirectObject(words1, sentences)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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

        private List<Lesson> MountVerbPronoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_pronoun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_POSSESSIVE);
                List<Lesson> pronouns = new List<Lesson>();
                pronouns = FilterLesson(matters, kind);
                if (pronouns.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
                    foreach (Lesson pronoun in pronouns)
                    {
                        List<Word> words1 = new List<Word>();
                        words.ForEach(item =>
                        {
                            words1.Add(item);
                        });
                        pronoun.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, pronoun.team, order_pronoun);
                            words1.Add(word);
                        });
                        if (!VerifyVerbDirectObject(words1, sentences)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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

        private List<Lesson> MountVerbAdjectiveNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_adjective_noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_ADJECTIVE_NOUN);
                List<Lesson> adjectives_nouns = new List<Lesson>();
                adjectives_nouns = FilterLesson(matters, kind);
                if (adjectives_nouns.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
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
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, adjective_noun.team, order_adjective_noun);
                            words1.Add(word);
                        });
                        if (!VerifyVerbAdjectiveNoun(words1, sentences)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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
        #endregion

        #region OBJECT PREDICATIVE
        private List<Lesson> MountPredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_predicative)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_ADJECTIVE_ADVERB);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, kind);
                if (adjectives.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
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
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, adjective.team, order_predicative);
                            words1.Add(word);
                        });
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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

        private List<Lesson> MountVerbAdjective(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_adjective)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_ADJECTIVE_ADVERB);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, kind);
                if (adjectives.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
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
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, adjective.team, order_adjective);
                            words1.Add(word);
                        });
                        if (!VerifyVerbPredicative(words1, sentences)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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

        private List<Lesson> MountObjectPredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_predicative)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_ADJECTIVE_ADVERB);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, kind);
                if (adjectives.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
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
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, adjective.team, order_predicative);
                            words1.Add(word);
                        });
                        if (!VerifyDirectObjectPredicative(words1, sentences, order_predicative)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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

        private List<Lesson> MountAdjectivePreposition(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_preposition)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_PREPOSITION);
                List<Lesson> prepositions = new List<Lesson>();
                prepositions = FilterLesson(matters, kind);
                if (prepositions.Count == 0) return lessons;
                int order_direct_object = order_preposition - 1;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
                    foreach (Lesson preposition in prepositions)
                    {
                        List<Word> words1 = new List<Word>();
                        words.ForEach(item =>
                        {
                            words1.Add(item);
                        });
                        preposition.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, preposition.team, order_preposition);
                            words1.Add(word);
                        });
                        if (!VerifyPredicativePreposition(words1, sentences, order_direct_object)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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
        #endregion

        #region OBJECT INDIRECT OBJECT
        private List<Lesson> MountPreposition(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_preposition)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_PREPOSITION);
                List<Lesson> prepositions = new List<Lesson>();
                prepositions = FilterLesson(matters, kind);
                if (prepositions.Count == 0) return lessons;
                int order_direct_object = order_preposition - 1;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
                    foreach (Lesson preposition in prepositions)
                    {
                        List<Word> words1 = new List<Word>();
                        words.ForEach(item =>
                        {
                            words1.Add(item);
                        });
                        preposition.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, preposition.team, order_preposition);
                            words1.Add(word);
                        });
                        if (!VerifyDirectObjectPreposition(words1, sentences, order_direct_object)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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

        private List<Lesson> MountVerbIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_preposition)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_PREPOSITION);
                List<Lesson> prepositions = new List<Lesson>();
                prepositions = FilterLesson(matters, kind);
                if (prepositions.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
                    foreach (Lesson preposition in prepositions)
                    {
                        List<Word> words1 = new List<Word>();
                        words.ForEach(item =>
                        {
                            words1.Add(item);
                        });
                        preposition.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, preposition.team, order_preposition);
                            words1.Add(word);
                        });
                        if (!VerifyVerbIndirectObject(words1, sentences)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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

        private List<Lesson> MountIndirectObjectNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_NOUN);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, kind);
                if (nouns.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
                    foreach (Lesson noun in nouns)
                    {
                        List<Word> words1 = new List<Word>();
                        words.ForEach(item =>
                        {
                            words1.Add(item);
                        });
                        noun.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, noun.team, order_noun);
                            words1.Add(word);
                        });
                        if (!VerifyIndirectObject(words1, sentences, order_noun)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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

        private List<Lesson> MountIndirectObjectPronoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_pronoun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_POSSESSIVE);
                List<Lesson> pronouns = new List<Lesson>();
                pronouns = FilterLesson(matters, kind);
                if (pronouns.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
                    foreach (Lesson pronoun in pronouns)
                    {
                        List<Word> words1 = new List<Word>();
                        words.ForEach(item =>
                        {
                            words1.Add(item);
                        });
                        pronoun.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, pronoun.team, order_pronoun);
                            words1.Add(word);
                        });
                        if (!VerifyIndirectObject(words1, sentences, order_pronoun)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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

        private List<Lesson> MountIndirectObjectAdjectiveNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_adjective_noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_ADJECTIVE_NOUN);
                List<Lesson> adjectives_nouns = new List<Lesson>();
                adjectives_nouns = FilterLesson(matters, kind);
                if (adjectives_nouns.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
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
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, adjective_noun.team, order_adjective_noun);
                            words1.Add(word);
                        });
                        if (!VerifyIndirectObjectAdjectiveNoun(words1, sentences, order_adjective_noun)) continue;
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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
        #endregion

        #region OBJECT CONJUNCTION
        private List<Lesson> MountConjunction(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_conjunction)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(VAR_CONJUNCTION);
                List<Lesson> conjunctions = new List<Lesson>();
                conjunctions = FilterLesson(matters, kind);
                if (conjunctions.Count == 0) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
                    foreach (Lesson conjunction in conjunctions)
                    {
                        List<Word> words1 = new List<Word>();
                        words.ForEach(item =>
                        {
                            words1.Add(item);
                        });
                        conjunction.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, VAR_PREDICATE, conjunction.team, order_conjunction);
                            words1.Add(word);
                        });
                        Lesson lesson = new Lesson();
                        lesson.lecture = words1;
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

        private List<Lesson> MountVerbNumeralConjunctionNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind_noun = new List<string>();
                kind_noun.Add(VAR_NUMERAL_NOUN);
                List<string> kind_numeral = new List<string>();
                kind_numeral.Add(VAR_NUMERAL);
                List<string> kind_conjunction = new List<string>();
                kind_conjunction.Add(VAR_CONJUNCTION);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, kind_noun);
                List<Lesson> numerals = new List<Lesson>();
                numerals = FilterLesson(matters, kind_numeral);
                List<Lesson> conjunctions = new List<Lesson>();
                conjunctions = FilterLesson(matters, kind_conjunction);
                if ((nouns.Count == 0) || (numerals.Count == 0) || (conjunctions.Count == 0)) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
                    foreach (Lesson conjunction in conjunctions)
                    {
                        foreach (Lesson numeral in numerals)
                        {
                            foreach (Lesson noun in nouns)
                            {
                                List<Word> words1 = new List<Word>();
                                words.ForEach(item =>
                                {
                                    words1.Add(item);
                                });
                                noun.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, VAR_PREDICATE, noun.team, order_noun);
                                    words1.Add(word);
                                });
                                numeral.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, VAR_PREDICATE, numeral.team, order_noun);
                                    words1.Add(word);
                                });
                                conjunction.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, VAR_PREDICATE, conjunction.team, order_noun);
                                    words1.Add(word);
                                });
                                if (!VerifyNumeralConjunctionNoun(words1, sentences, order_noun)) continue;
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

        private List<Lesson> MountVerbAdjectiveConjunctionNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_noun)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<string> kind_noun = new List<string>();
                kind_noun.Add(VAR_ADJECTIVE_NOUN);
                List<string> kind_adjective = new List<string>();
                kind_adjective.Add(VAR_ADJECTIVE);
                List<string> kind_conjunction = new List<string>();
                kind_conjunction.Add(VAR_CONJUNCTION);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, kind_noun);
                List<Lesson> adjectives = new List<Lesson>();
                adjectives = FilterLesson(matters, kind_adjective);
                List<Lesson> conjunctions = new List<Lesson>();
                conjunctions = FilterLesson(matters, kind_conjunction);
                if ((nouns.Count == 0) || (adjectives.Count == 0) || (conjunctions.Count == 0)) return lessons;
                foreach (Lesson source in sources)
                {
                    List<Word> words = source.lecture;
                    foreach (Lesson conjunction in conjunctions)
                    {
                        foreach (Lesson adjective in adjectives)
                        {
                            foreach (Lesson noun in nouns)
                            {
                                List<Word> words1 = new List<Word>();
                                words.ForEach(item =>
                                {
                                    words1.Add(item);
                                });
                                noun.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, VAR_PREDICATE, noun.team, order_noun);
                                    words1.Add(word);
                                });
                                adjective.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, VAR_PREDICATE, adjective.team, order_noun);
                                    words1.Add(word);
                                });
                                conjunction.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, VAR_PREDICATE, conjunction.team, order_noun);
                                    words1.Add(word);
                                });
                                if (!VerifyAdjectiveConjunctionNoun(words1, sentences, order_noun)) continue;
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

        #region SAMPLE
        public List<Lesson> SampleSubjectVerb(List<Sentenca> sentences, List<Lesson> matters)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                lessons = MountNounVerb(sentences, matters);
                lessons = Union(lessons, MountPronounVerb(sentences, matters));
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> CompoundSubjectVerb(List<Sentenca> sentences, List<Lesson> matters)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                lessons = MountCompoundVerb(sentences, matters);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PredicateDirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                lessons = MountVerbNoun(sentences, matters, sources, order_init);
                lessons = Union(lessons, MountVerbPronoun(sentences, matters, sources, order_init));
                lessons = Union(lessons, MountVerbAdjectiveNoun(sentences, matters, sources, order_init));
                lessons = Union(lessons, MountVerbNumeralConjunctionNoun(sentences, matters, sources, order_init));
                lessons = Union(lessons, MountVerbAdjectiveConjunctionNoun(sentences, matters, sources, order_init));
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PredicatePredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                lessons = MountVerbAdjective(sentences, matters, sources, order_init);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PredicateIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int init_order)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                lessons = MountVerbIndirectObject(sentences, matters, sources, init_order);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PredicateDirectObjectIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();

                List<Lesson> prepositions = new List<Lesson>();
                prepositions = MountPreposition(sentences, matters, sources, order_init);
                List<Lesson> nouns = new List<Lesson>();
                nouns = MountIndirectObjectNoun(sentences, matters, prepositions, order_init);
                List<Lesson> pronouns = new List<Lesson>();
                pronouns = MountIndirectObjectPronoun(sentences, matters, prepositions, order_init);
                List<Lesson> adjectives_nouns = new List<Lesson>();
                adjectives_nouns = MountIndirectObjectAdjectiveNoun(sentences, matters, prepositions, order_init);
                
                List<Lesson> conjunctios_numerals_nouns = new List<Lesson>();
                conjunctios_numerals_nouns = MountVerbNumeralConjunctionNoun(sentences, matters, prepositions, order_init);
                List<Lesson> conjunctions_adjectives_nouns = new List<Lesson>();
                conjunctions_adjectives_nouns = MountVerbAdjectiveConjunctionNoun(sentences, matters, prepositions, order_init);

                lessons = Union(nouns, pronouns);
                lessons = Union(lessons, adjectives_nouns);
                lessons = Union(lessons, conjunctios_numerals_nouns);
                lessons = Union(lessons, conjunctions_adjectives_nouns);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PredicateDirectObjectPredicativo(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                lessons = MountObjectPredicative(sentences, matters, sources, order_init);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PredicateIndirectObjectPredicativo(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                lessons = MountObjectPredicative(sentences, matters, sources, order_init);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PredicatePredicativoIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<Lesson> prepositions = new List<Lesson>();
                prepositions = MountAdjectivePreposition(sentences, matters, sources, order_init);

                List<Lesson> nouns = new List<Lesson>();
                nouns = MountIndirectObjectNoun(sentences, matters, prepositions, order_init);
                List<Lesson> pronouns = new List<Lesson>();
                pronouns = MountIndirectObjectPronoun(sentences, matters, sources, order_init);
                List<Lesson> adjectives_nouns = new List<Lesson>();
                adjectives_nouns = MountIndirectObjectAdjectiveNoun(sentences, matters, sources, order_init);
                List<Lesson> conjunctions_numerals_nouns = new List<Lesson>();
                conjunctions_numerals_nouns = MountVerbNumeralConjunctionNoun(sentences, matters, sources, order_init);
                List<Lesson> conjunctions_adjectives_nouns = new List<Lesson>();
                conjunctions_adjectives_nouns = MountVerbNumeralConjunctionNoun(sentences, matters, sources, order_init);

                lessons = Union(nouns, pronouns);
                lessons = Union(lessons, adjectives_nouns);
                lessons = Union(lessons, conjunctions_numerals_nouns);
                lessons = Union(lessons, conjunctions_adjectives_nouns);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PredicateDirectObjectDirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                int order_last = order_init + 1;

                List<string> kind_noun = new List<string>();
                kind_noun.Add(VAR_NOUN);
                kind_noun.Add(VAR_ADJECTIVE_NOUN);
                List<Lesson> substantives = new List<Lesson>();
                substantives = FilterLesson(sources, kind_noun);

                List<Lesson> nouns = new List<Lesson>();
                nouns = MountVerbNoun(sentences, matters, substantives, order_last);
                List<Lesson> adjectives_nouns = new List<Lesson>();
                adjectives_nouns = MountVerbAdjectiveNoun(sentences, matters, substantives, order_last);
                List<Lesson> conjunctions_nouns = new List<Lesson>();
                conjunctions_nouns = Union(nouns, adjectives_nouns);
                conjunctions_nouns = MountConjunction(sentences, matters, conjunctions_nouns, order_init);

                List<string> kind_pronoun = new List<string>();
                kind_pronoun.Add(VAR_PRONOUN);
                List<Lesson> surrogates = new List<Lesson>();
                surrogates = FilterLesson(sources, kind_pronoun);

                List<Lesson> pronouns = new List<Lesson>();
                pronouns = MountVerbPronoun(sentences, matters, surrogates, order_last);
                List<Lesson> conjunctions_pronouns = new List<Lesson>();
                conjunctions_pronouns = MountConjunction(sentences, matters, conjunctions_pronouns, order_init);

                lessons = Union(conjunctions_nouns, conjunctions_pronouns);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PredicateIndirectObjectIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                int order_last = order_init + 1;

                List<string> kind_noun = new List<string>();
                kind_noun.Add(VAR_NOUN);
                kind_noun.Add(VAR_ADJECTIVE_NOUN);
                List<Lesson> substantives = new List<Lesson>();
                substantives = FilterLesson(sources, kind_noun);

                List<Lesson> nouns = new List<Lesson>();
                nouns = MountVerbNoun(sentences, matters, substantives, order_last);
                List<Lesson> adjectives_nouns = new List<Lesson>();
                adjectives_nouns = MountVerbAdjectiveNoun(sentences, matters, substantives, order_last);
                List<Lesson> conjunctions_nouns = new List<Lesson>();
                conjunctions_nouns = Union(nouns, adjectives_nouns);
                conjunctions_nouns = MountPreposition(sentences, matters, conjunctions_nouns, order_last);
                conjunctions_nouns = MountConjunction(sentences, matters, conjunctions_nouns, order_init);

                List<string> kind_pronoun = new List<string>();
                kind_pronoun.Add(VAR_PRONOUN);
                List<Lesson> surrogates = new List<Lesson>();
                surrogates = FilterLesson(sources, kind_pronoun);

                List<Lesson> pronouns = new List<Lesson>();
                pronouns = MountVerbPronoun(sentences, matters, surrogates, order_last);
                List<Lesson> conjunctions_pronouns = new List<Lesson>();
                conjunctions_pronouns = MountPreposition(sentences, matters, conjunctions_pronouns, order_last);
                conjunctions_pronouns = MountConjunction(sentences, matters, conjunctions_pronouns, order_init);

                lessons = Union(conjunctions_nouns, conjunctions_pronouns);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> PredicatePredicativePredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                int order_last = order_init + 1;
                lessons = MountPredicative(sentences, matters, sources, order_last);
                lessons = MountConjunction(sentences, matters, lessons, order_init);
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
