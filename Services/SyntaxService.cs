using LetterStomach.Models;
using LetterStomach.Services.Interfaces;

namespace LetterStomach.Services
{
    public class SyntaxService : ISyntaxService
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string? _error_message;

        public string? error_message
        {
            get => this._error_message;
            set
            {
                this._error_message = value;
            }
        }

        public event EventHandler<string>? OnError;
        #endregion

        #region VARIABLE
        private string _subject;
        private string _predicate;
        private string _pronoun;
        private string _noun;
        private string _verb;
        private string _personal;
        private string _adjective;
        private string _article;
        private string _numeral;
        private string _preposition;
        private string _possessive;
        private string _demonstrative;
        private string _adverb;
        private string _adverb_adverb;
        private string _adjective_noun;
        private string _adjective_adverb;
        private string _conjunction;
        private string _numeral_noun;

        private int _order_1 = 1;
        private int _order_2 = 2;
        private int _order_3 = 3;
        private int _order_4 = 4;

        private IWordEmbeddingService _wordEmbeddingService;
        #endregion

        #region CONSTRUCTOR
        public SyntaxService()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Syntax\" service failed!");

                this._subject = SettingService.Instance.Suject;
                this._predicate = SettingService.Instance.Predicate;
                this._pronoun = SettingService.Instance.Pronoun;
                this._noun = SettingService.Instance.Noun;
                this._verb = SettingService.Instance.Verb;
                this._personal = SettingService.Instance.Personal;
                this._adjective = SettingService.Instance.Adjective;
                this._article = SettingService.Instance.Article;
                this._numeral = SettingService.Instance.Numeral;
                this._preposition = SettingService.Instance.Preposition;
                this._possessive = SettingService.Instance.Possessive;
                this._demonstrative = SettingService.Instance.Demostrtive;
                this._adverb = SettingService.Instance.Adverb;
                this._adverb_adverb = SettingService.Instance.Adverb_Adverb;
                this._adjective_noun = SettingService.Instance.Adjective_Noun;
                this._adjective_adverb = SettingService.Instance.Adjective_Adverb;
                this._conjunction = SettingService.Instance.Conjunction;
                this._numeral_noun = SettingService.Instance.Numeral_Noun;

                this._wordEmbeddingService = new WordEmbeddingService();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<Lesson> FilterLesson(List<Lesson> matters, List<string> types)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter list \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                for (int quantity = 0; quantity < matters.Count(); quantity++)
                {
                    types.ForEach(item =>
                    {
                        if (matters[quantity].team == item)
                            lessons.Add(matters[quantity]);
                    });
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region VERIFY
        private bool VerifyVerbSampleSubject(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb sample subject \"Syntax\" service failed!");

                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string noun = string.Empty;
                string verb = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == this._subject) && (item.team == this._noun) && (item.kind == this._noun)) noun = item.term;
                    if ((item.sentense == this._subject) && (item.kind == this._pronoun)) noun = item.term;
                    if (item.kind == this._verb) verb = item.term;
                });
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun, verb);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyVerbCompoundSubject(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb compound subject \"Syntax\" service failed!");

                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string noun = string.Empty;
                string last = string.Empty;
                string adjunct = string.Empty;
                string conjunction = string.Empty;
                string verb = string.Empty;
                int order_first = this._order_1;
                int order_last = this._order_3;
                words.ForEach(item =>
                {
                    if (item.order == order_first)
                    {
                        if ((item.sentense == this._subject) && (item.team == this._noun) && (item.kind == this._noun)) noun = item.term;
                        if ((item.sentense == this._subject) && (item.kind == this._pronoun)) noun = item.term;
                    }
                    if (item.order == order_last)
                    {
                        if ((item.sentense == this._subject) && (item.team == this._noun) && (item.kind == this._noun)) last = item.term;
                        if ((item.sentense == this._subject) && (item.kind == this._pronoun)) last = item.term;
                        if ((item.sentense == this._subject) && (item.kind == this._noun) && 
                            (item.kind == this._numeral) || (item.kind == this._article) || (item.kind == this._pronoun)) adjunct = item.term;
                    }
                    if ((item.sentense == this._subject) && (item.kind == this._conjunction)) conjunction = item.term;
                    if (item.kind == this._verb) verb = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyVerbPredicative(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb predicative \"Syntax\" service failed!");

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
                    if ((item.sentense == this._predicate) && (item.team == this._adjective) && (item.kind == this._adjective)) adjective = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._adjective) && (item.kind == this._adverb)) adjective_adverb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._adjective) && (item.kind == _adverb_adverb)) adjective_adverb_adverb = item.term;
                    if (item.kind == this._verb) verb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._verb) && (item.kind == this._adverb)) verb_adverb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._verb) && (item.kind == _adverb_adverb)) verb_adverb_adverb = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyVerbAdjectiveNoun(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb adjective noun \"Syntax\" service failed!");

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
                    if ((item.sentense == this._predicate) && (item.team == this._adjective_noun) && (item.kind == this._noun)) noun = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._adjective_noun) 
                        && ((item.kind == this._article) || (item.kind == this._numeral) || (item.kind == this._pronoun))) adjunct = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._adjective_noun) && (item.kind == this._adjective)) adjective = item.term;
                    if (item.kind == this._verb) verb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._verb) && (item.kind == this._adverb)) adverb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._verb) && (item.kind == _adverb_adverb)) adverb_adverb = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyVerbDirectObject(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb direct object \"Syntax\" service failed!");

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
                    if ((item.sentense == this._predicate) && (item.team == this._noun) && (item.kind == this._noun)) noun = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._noun) && 
                        ((item.kind == this._numeral) || (item.kind == this._article) || (item.kind == this._pronoun))) adjunct = item.term;
                    if ((item.sentense == this._predicate) && (item.kind == this._pronoun)) noun = item.term;
                    if (item.kind == this._verb) verb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._verb) && (item.kind == this._adverb)) adverb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._verb) && (item.kind == _adverb_adverb)) adverb_adverb = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyVerbIndirectObject(List<Word> words, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb indirect object \"Syntax\" service failed!");

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
                    if ((item.sentense == this._predicate) && (item.kind == this._preposition)) word_preposition = item.term;
                    if (item.kind == this._verb) verb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._verb) && (item.kind == this._adverb)) adverb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._verb) && (item.kind == _adverb_adverb)) adverb_adverb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._noun)) word_noun = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyNumeralConjunctionNoun(List<Word> words, List<Sentenca> sentences, int order_conjunction)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify numeral conjunction noun \"Syntax\" service failed!");

                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string conjunction = string.Empty;
                string numeral = string.Empty;
                string noun = string.Empty;
                string preposition = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == this._predicate) && (item.team == this._conjunction) && (item.kind == this._conjunction) && (item.order == order_conjunction)) conjunction = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._numeral) && (item.kind == this._numeral) && (item.order == order_conjunction)) numeral = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._numeral_noun) && (item.kind == this._numeral) && (item.order == order_conjunction)) noun = item.term;
                    if ((item.sentense == this._predicate) && (item.kind == this._preposition) && (item.order == order_conjunction)) preposition = item.term; 
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyAdjectiveConjunctionNoun(List<Word> words, List<Sentenca> sentences, int order_conjunction)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify adjective conjunction noun \"Syntax\" service failed!");

                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string conjunction = string.Empty;
                string adjective = string.Empty;
                string noun = string.Empty;
                string preposition = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == this._predicate) && (item.team == this._conjunction) && (item.kind == this._conjunction) && (item.order == order_conjunction)) conjunction = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._adjective) && (item.kind == this._adjective) && (item.order == order_conjunction)) adjective = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._adjective_noun) && (item.kind == this._adjective) && (item.order == order_conjunction)) noun = item.term;
                    if ((item.sentense == this._predicate) && (item.kind == this._preposition) && (item.order == order_conjunction)) preposition = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        
        private bool VerifyDirectObjectPreposition(List<Word> words, List<Sentenca> sentences, int order_direct_object)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify direct object preposition \"Syntax\" service failed!");

                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string preposition = string.Empty;
                string noun = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == this._predicate) && (item.team == this._noun) && (item.order == order_direct_object)) noun = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._preposition)) preposition = item.term;
                });
                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun, preposition);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        
        private bool VerifyIndirectObject(List<Word> words, List<Sentenca> sentences, int order_indirect_object)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify indirect object \"Syntax\" service failed!");

                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string preposition = string.Empty;
                string noun = string.Empty;
                string adjunct = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == this._predicate) && (item.team == this._noun) && (item.kind == this._noun) && (item.order == order_indirect_object)) noun = item.term;
                    if ((item.sentense == this._predicate) && (item.kind == this._pronoun) && (item.order == order_indirect_object)) noun = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._noun) && (item.order == order_indirect_object)
                        && ((item.kind == this._numeral) || (item.kind == this._article) || (item.kind == this._pronoun))) adjunct = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._preposition) && (item.order == order_indirect_object)) preposition = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyIndirectObjectAdjectiveNoun(List<Word> words, List<Sentenca> sentences, int order_indirect_object)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify indirect object adjective noun \"Syntax\" service failed!");

                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string adjective = string.Empty;
                string adjunct = string.Empty;
                string preposition = string.Empty;
                words.ForEach(item =>
                {
                    if ((item.sentense == this._predicate) && (item.team == this._preposition) && (item.order == order_indirect_object)) preposition = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._adjective_noun) && (item.order == order_indirect_object)
                        && ((item.kind == this._article) || (item.kind == this._numeral) || (item.kind == this._pronoun))) adjunct = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._adjective_noun) && (item.kind == this._adjective) && (item.order == order_indirect_object)) adjective = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyDirectObjectPredicative(List<Word> words, List<Sentenca> sentences, int order_predicative)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify direct object predicative \"Syntax\" service failed!");

                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string adjective = string.Empty;
                string pronoun = string.Empty;
                string noun = string.Empty;
                int order_object_direct = order_predicative - 1;
                words.ForEach(item =>
                {
                    if ((item.sentense == this._predicate) && (item.team == this._adjective_adverb) && (item.kind == this._adjective) && (item.order == order_predicative)) adjective = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._noun) && (item.kind == this._noun) && (item.order == order_object_direct)) noun = item.term;
                    if ((item.sentense == this._predicate) && (item.kind == this._pronoun) && (item.order == order_object_direct)) pronoun = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyPredicativePreposition(List<Word> words, List<Sentenca> sentences, int order_preposicao)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify predicative preposition \"Syntax\" service failed!");

                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                string preposition = string.Empty;
                string adjective = string.Empty;
                string adverb = string.Empty;
                int order_predicative = order_preposicao - 1;
                words.ForEach(item =>
                {
                    if ((item.sentense == this._predicate) && (item.team == this._adjective_adverb) && (item.kind == this._adjective) && (item.order == order_predicative)) adjective = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._adjective_adverb) && (item.kind == this._adverb) && (item.order == order_predicative)) adverb = item.term;
                    if ((item.sentense == this._predicate) && (item.team == this._preposition) && (item.order == order_preposicao)) preposition = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region WORD
        private Word Lecture(string term, string kind, string sentence, string team, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation lecture \"Syntax\" service failed!");

                Word word = new Word();
                word.term = term;
                word.kind = kind;
                word.sentense = sentence;
                word.team = team;
                word.order = order;
                return word;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> Union(List<Lesson> firsts, List<Lesson> lasts)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation union \"Syntax\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region SUBJECT
        private List<Lesson> MountNounVerb(List<Sentenca> sentences, List<Lesson> matters)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation nount noun verb \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind_verb = new List<string>();
                kind_verb.Add(this._verb);
                List<string> kind_noun = new List<string>();
                kind_noun.Add(this._noun);
                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, kind_verb);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, kind_noun);
                int order_noun = this._order_1;
                int order_verb = this._order_2;
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson noun in nouns)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, this._predicate, verb.team, order_verb);
                            words.Add(word);
                        });
                        noun.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, this._subject, noun.team, order_noun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountPronounVerb(List<Sentenca> sentences, List<Lesson> matters)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount pronoun verb \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind_verb = new List<string>();
                kind_verb.Add(this._verb);
                List<string> kind_pronoun = new List<string>();
                kind_pronoun.Add(this._personal);
                kind_pronoun.Add(this._demonstrative);
                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, kind_verb);
                List<Lesson> pronouns = new List<Lesson>();
                pronouns = FilterLesson(matters, kind_pronoun);
                int order_pronoun = this._order_1;
                int order_verb = this._order_2;
                foreach (Lesson verb in verbs)
                {
                    foreach (Lesson pronoun in pronouns)
                    {
                        List<Word> words = new List<Word>();
                        verb.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, this._predicate, verb.team, order_verb);
                            words.Add(word);
                        });
                        pronoun.lecture.ForEach(item =>
                        {
                            Word word = new Word();
                            word = Lecture(item.term, item.kind, this._subject, pronoun.team, order_pronoun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountCompoundVerb(List<Sentenca> sentences, List<Lesson> matters)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount compound verb \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind_verb = new List<string>();
                kind_verb.Add(this._verb);
                List<string> kind_noun = new List<string>();
                kind_noun.Add(this._noun);
                kind_noun.Add(this._personal);
                kind_noun.Add(this._adjective_noun);
                List<string> kind_conjunction = new List<string>();
                kind_conjunction.Add(this._conjunction);
                List<Lesson> verbs = new List<Lesson>();
                verbs = FilterLesson(matters, kind_verb);
                List<Lesson> nouns = new List<Lesson>();
                nouns = FilterLesson(matters, kind_noun);
                List<Lesson> conjunctions = new List<Lesson>();
                conjunctions = FilterLesson(matters, kind_conjunction);
                List<Lesson> lasts = new List<Lesson>();
                lasts = FilterLesson(matters, kind_noun);
                int order_noun = this._order_1;
                int order_conjunction = this._order_2;
                int order_last = this._order_3;
                int order_verb = this._order_4;
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
                                    word = Lecture(item.term, item.kind, this._predicate, verb.team, order_verb);
                                    words.Add(word);
                                });
                                noun.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, this._subject, noun.team, order_noun);
                                    words.Add(word);
                                });
                                last.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, this._subject, last.team, order_last);
                                    words.Add(word);
                                });
                                conjunction.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, this._subject, conjunction.team, order_conjunction);
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region DIRECT OBJECT
        private List<Lesson> MountVerbNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_noun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb noun \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._noun);
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
                            word = Lecture(item.term, item.kind, this._predicate, noun.team, order_noun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountVerbPronoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_pronoun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb pronoun \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._possessive);
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
                            word = Lecture(item.term, item.kind, this._predicate, pronoun.team, order_pronoun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountVerbAdjectiveNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_adjective_noun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb adjective noun \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._adjective_noun);
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
                            word = Lecture(item.term, item.kind, this._predicate, adjective_noun.team, order_adjective_noun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region PREDICATIVE
        private List<Lesson> MountPredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_predicative)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount predicative \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._adjective_adverb);
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
                            word = Lecture(item.term, item.kind, this._predicate, adjective.team, order_predicative);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountVerbAdjective(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_adjective)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb adjective \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._adjective_adverb);
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
                            word = Lecture(item.term, item.kind, this._predicate, adjective.team, order_adjective);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountObjectPredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_predicative)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount object predicative \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._adjective_adverb);
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
                            word = Lecture(item.term, item.kind, this._predicate, adjective.team, order_predicative);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountAdjectivePreposition(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_preposition)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount adjective preposition \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._preposition);
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
                            word = Lecture(item.term, item.kind, this._predicate, preposition.team, order_preposition);
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region INDIRECT OBJECT
        private List<Lesson> MountPreposition(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_preposition)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount preposition \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._preposition);
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
                            word = Lecture(item.term, item.kind, this._predicate, preposition.team, order_preposition);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountVerbIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_preposition)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb indirect object \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._preposition);
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
                            word = Lecture(item.term, item.kind, this._predicate, preposition.team, order_preposition);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountIndirectObjectNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_noun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount indirect object noun \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._noun);
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
                            word = Lecture(item.term, item.kind, this._predicate, noun.team, order_noun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountIndirectObjectPronoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_pronoun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount indirect object pronoun \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._possessive);
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
                            word = Lecture(item.term, item.kind, this._predicate, pronoun.team, order_pronoun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountIndirectObjectAdjectiveNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_adjective_noun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount indirect object adjective noun \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._adjective_noun);
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
                            word = Lecture(item.term, item.kind, this._predicate, adjective_noun.team, order_adjective_noun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region CONJUNCTION
        private List<Lesson> MountConjunction(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_conjunction)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount conjunction \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind = new List<string>();
                kind.Add(this._conjunction);
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
                            word = Lecture(item.term, item.kind, this._predicate, conjunction.team, order_conjunction);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountVerbNumeralConjunctionNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_noun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb numeral conjunction noun \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind_noun = new List<string>();
                kind_noun.Add(this._numeral_noun);
                List<string> kind_numeral = new List<string>();
                kind_numeral.Add(this._numeral);
                List<string> kind_conjunction = new List<string>();
                kind_conjunction.Add(this._conjunction);
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
                                    word = Lecture(item.term, item.kind, this._predicate, noun.team, order_noun);
                                    words1.Add(word);
                                });
                                numeral.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, this._predicate, numeral.team, order_noun);
                                    words1.Add(word);
                                });
                                conjunction.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, this._predicate, conjunction.team, order_noun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountVerbAdjectiveConjunctionNoun(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_noun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb adjective conjunction noun \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                List<string> kind_noun = new List<string>();
                kind_noun.Add(this._adjective_noun);
                List<string> kind_adjective = new List<string>();
                kind_adjective.Add(this._adjective);
                List<string> kind_conjunction = new List<string>();
                kind_conjunction.Add(this._conjunction);
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
                                    word = Lecture(item.term, item.kind, this._predicate, noun.team, order_noun);
                                    words1.Add(word);
                                });
                                adjective.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, this._predicate, adjective.team, order_noun);
                                    words1.Add(word);
                                });
                                conjunction.lecture.ForEach(item =>
                                {
                                    Word word = new Word();
                                    word = Lecture(item.term, item.kind, this._predicate, conjunction.team, order_noun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region SAMPLE
        public List<Lesson> SampleSubjectVerb(List<Sentenca> sentences, List<Lesson> matters)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation sample subject verb \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                lessons = MountNounVerb(sentences, matters);
                lessons = Union(lessons, MountPronounVerb(sentences, matters));
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> CompoundSubjectVerb(List<Sentenca> sentences, List<Lesson> matters)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation compound subject verb \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                lessons = MountCompoundVerb(sentences, matters);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> PredicateDirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate direct object \"Syntax\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> PredicatePredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate predicative \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                lessons = MountVerbAdjective(sentences, matters, sources, order_init);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> PredicateIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int init_order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate indirect object \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                lessons = MountVerbIndirectObject(sentences, matters, sources, init_order);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> PredicateDirectObjectIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate direct object indirect object \"Syntax\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> PredicateDirectObjectPredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate indirect object predicative \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                lessons = MountObjectPredicative(sentences, matters, sources, order_init);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> PredicateIndirectObjectPredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate indirect object predicative \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                lessons = MountObjectPredicative(sentences, matters, sources, order_init);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> PredicatePredicativeIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate predicative indirect object \"Syntax\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> PredicateDirectObjectDirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate direct object direct object \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                int order_last = order_init + 1;

                List<string> kind_noun = new List<string>();
                kind_noun.Add(this._noun);
                kind_noun.Add(this._adjective_noun);
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
                kind_pronoun.Add(this._pronoun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> PredicateIndirectObjectIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate indirect object indirect object \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                int order_last = order_init + 1;

                List<string> kind_noun = new List<string>();
                kind_noun.Add(this._noun);
                kind_noun.Add(this._adjective_noun);
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
                kind_pronoun.Add(this._pronoun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> PredicatePredicativePredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate predicative predicative \"Syntax\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                int order_last = order_init + 1;
                lessons = MountPredicative(sentences, matters, sources, order_last);
                lessons = MountConjunction(sentences, matters, lessons, order_init);
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion
    }
}
