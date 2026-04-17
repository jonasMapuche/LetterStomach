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
        private string _adnominal_adjunct;
        private string _adverbial_verb;
        private string _adverbial_adjective;

        private HashSet<string> _morphology;
        private HashSet<string> _syntax;
        private HashSet<int> _order;

        private int _order_1 = 1;
        private int _order_2 = 2;
        private int _order_3 = 3;
        private int _order_4 = 4;

        private IWordEmbeddingService _wordEmbeddingService;
        #endregion

        #region CONSTRUCTOR
        public SyntaxService(WordEmbeddingService wordEmbeddingService)
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
                this._morphology = SettingService.Instance.Morphology;
                this._syntax = SettingService.Instance.Syntax;
                this._order = SettingService.Instance.Order;
                this._adnominal_adjunct = SettingService.Instance.Adnominal_Adjunct;
                this._adverbial_verb = SettingService.Instance.Adverbial_Verb;
                this._adverbial_adjective = SettingService.Instance.Adverbial_Adjective;

                this._wordEmbeddingService = wordEmbeddingService;
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

        private List<Tutorial> FilterLesson(List<Tutorial> tutorials, List<byte[]> kinds)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter lesson \"Syntax\" view model failed!");

                List<Tutorial> seminars = new List<Tutorial>();
                for (int quantity = 0; quantity < tutorials.Count(); quantity++)
                {
                    foreach (byte[] item in kinds)
                    {
                        if (tutorials[quantity].team.AsSpan().SequenceEqual(item))
                            seminars.Add(tutorials[quantity]);
                    };
                }
                return seminars;
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
                    if ((item.sentence == this._subject) && (item.team == this._noun) && (item.kind == this._noun)) noun = item.term;
                    if ((item.sentence == this._subject) && (item.kind == this._pronoun)) noun = item.term;
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

        private bool VerifyVerbSampleSubject(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb sample subject \"Syntax\" service failed!");

                byte[] syntax_subject = this._wordEmbeddingService.Encode(this._subject, this._syntax);
                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);
                byte[] morphology_verb = this._wordEmbeddingService.Encode(this._verb, this._morphology);

                byte[]? noun = null;
                byte[]? verb = null;

                foreach (Instruction item in words)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject)) 
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun = item.term;
                    
                    if (item.kind.AsSpan().SequenceEqual(morphology_verb)) verb = item.term;
                };

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun, verb);
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
                        if ((item.sentence == this._subject) && (item.team == this._noun) && (item.kind == this._noun)) noun = item.term;
                        if ((item.sentence == this._subject) && (item.kind == this._pronoun)) noun = item.term;
                    }
                    if (item.order == order_last)
                    {
                        if ((item.sentence == this._subject) && (item.team == this._noun) && (item.kind == this._noun)) last = item.term;
                        if ((item.sentence == this._subject) && (item.kind == this._pronoun)) last = item.term;
                        if ((item.sentence == this._subject) && (item.kind == this._noun) && 
                            (item.kind == this._numeral) || (item.kind == this._article) || (item.kind == this._pronoun)) adjunct = item.term;
                    }
                    if ((item.sentence == this._subject) && (item.kind == this._conjunction)) conjunction = item.term;
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

        private bool VerifyVerbCompoundSubject(List<Instruction> words, List<Instruction> firsts, List<Instruction> lasts, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb compound subject \"Syntax\" service failed!");

                byte[] syntax_subject = this._wordEmbeddingService.Encode(this._subject, this._syntax);
                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);

                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);
                byte[] morphology_adjective = this._wordEmbeddingService.Encode(this._adjective, this._morphology);
                byte[] morphology_article = this._wordEmbeddingService.Encode(this._article, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);

                byte[] morphology_adverbial_verb = this._wordEmbeddingService.Encode(this._adverbial_verb, this._morphology);
                byte[] morphology_verb = this._wordEmbeddingService.Encode(this._verb, this._morphology);
                byte[] morphology_adverb = this._wordEmbeddingService.Encode(this._adverb, this._morphology);
                byte[] morphology_adverb_adverb = this._wordEmbeddingService.Encode(this._adverb_adverb, this._morphology);

                byte[] morphology_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);

                byte[]? conjunction = null;

                byte[]? noun = null;
                byte[]? adjective = null;
                byte[]? article = null;
                byte[]? numeral = null;
                byte[]? pronoun = null;

                byte[]? verb = null;
                byte[]? verb_adverb = null;
                byte[]? verb_adverb_adverb = null;

                byte[]? noun2 = null;
                byte[]? adjective2 = null;
                byte[]? article2 = null;
                byte[]? numeral2 = null;
                byte[]? pronoun2 = null;

                byte[]? adnominal = null;
                byte[]? adnominal2 = null;
                byte[]? adnominal2_last = null;
                byte[]? adverbial = null;

                foreach (Instruction item in words)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_conjunction))
                        && (item.kind.AsSpan().SequenceEqual(morphology_conjunction))) conjunction = item.term;

                    if (item.kind.AsSpan().SequenceEqual(morphology_verb)) verb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) verb_adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) verb_adverb_adverb = item.term;
                }

                foreach (Instruction item in firsts)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun = item.term;
                }

                foreach (Instruction item in lasts)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun2 = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_subject))
                        && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun2 = item.term;
                }

                if (noun != null)
                    adnominal = noun;
                if ((noun != null) && (adjective != null))
                    adnominal = noun;
                if ((noun != null) && (article != null))
                    adnominal = noun;
                if ((noun != null) && (numeral != null))
                    adnominal = noun;
                if ((noun != null) && (pronoun != null))
                    adnominal = noun;

                if (noun2 != null)
                {
                    adnominal2 = noun2;
                    adnominal2_last = noun2;
                }
                if ((noun2 != null) && (adjective2 != null))
                {
                    adnominal2 = adjective2;
                    adnominal2_last = noun;
                }
                if ((noun2 != null) && (article2 != null))
                {
                    adnominal2 = article2;
                    adnominal2_last = noun;
                }
                if ((noun2 != null) && (numeral2 != null))
                {
                    adnominal2 = numeral2;
                    adnominal2_last = noun;
                }
                if ((noun2 != null) && (pronoun2 != null))
                {
                    adnominal2 = pronoun2;
                    adnominal2_last = noun;
                }

                if (verb != null)
                    adverbial = verb;
                if ((verb_adverb != null) && (verb != null))
                    adverbial = verb;
                if ((verb_adverb_adverb != null) && (verb_adverb != null) && (verb != null))
                    adverbial = verb;

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, adnominal, conjunction);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, conjunction, adnominal2);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, adnominal2_last, adverbial);
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
                    if ((item.sentence == this._predicate) && (item.team == this._adjective) && (item.kind == this._adjective)) adjective = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._adjective) && (item.kind == this._adverb)) adjective_adverb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._adjective) && (item.kind == this._adverb_adverb)) adjective_adverb_adverb = item.term;
                    if (item.kind == this._verb) verb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._verb) && (item.kind == this._adverb)) verb_adverb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._verb) && (item.kind == this._adverb_adverb)) verb_adverb_adverb = item.term;
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

        private bool VerifyVerbPredicative(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb predicative \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adverbial_verb = this._wordEmbeddingService.Encode(this._adverbial_verb, this._syntax);
                byte[] morphology_adverbial_adjective = this._wordEmbeddingService.Encode(this._adverbial_adjective, this._syntax);
                byte[] morphology_adjective = this._wordEmbeddingService.Encode(this._adjective, this._morphology);
                byte[] morphology_adverb = this._wordEmbeddingService.Encode(this._adverb, this._morphology);
                byte[] morphology_adverb_adverb = this._wordEmbeddingService.Encode(this._adverb_adverb, this._morphology);
                byte[] morphology_verb = this._wordEmbeddingService.Encode(this._verb, this._morphology);

                byte[]? adjective = null;
                byte[]? adjective_adverb = null;
                byte[]? adjective_adverb_adverb = null;
                byte[]? verb = null;
                byte[]? verb_adverb = null;
                byte[]? verb_adverb_adverb = null;

                byte[]? predicative = null;
                byte[]? adverbial = null;

                foreach (Instruction item in words)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_adjective))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_adjective))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) adjective_adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_adjective))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) adjective_adverb_adverb = item.term;

                    if (item.kind.AsSpan().SequenceEqual(morphology_verb)) verb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) verb_adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) verb_adverb_adverb = item.term;
                }

                if (verb != null)
                    adverbial = verb;
                if ((verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb;
                if ((verb_adverb_adverb != null) && (verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb_adverb;
                
                if (adjective != null)
                    predicative = adjective;
                if ((adjective_adverb != null) && (adjective != null))
                    predicative = adjective;
                if ((adjective_adverb_adverb != null) && (adjective_adverb != null) && (adjective != null))
                    predicative = adjective;

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, adverbial, predicative);
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
                    if ((item.sentence == this._predicate) && (item.team == this._adjective_noun) && (item.kind == this._noun)) noun = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._adjective_noun) 
                        && ((item.kind == this._article) || (item.kind == this._numeral) || (item.kind == this._pronoun))) adjunct = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._adjective_noun) && (item.kind == this._adjective)) adjective = item.term;
                    if (item.kind == this._verb) verb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._verb) && (item.kind == this._adverb)) adverb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._verb) && (item.kind == _adverb_adverb)) adverb_adverb = item.term;
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

        private bool VerifyVerbAdjectiveNoun(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb adjective noun \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_adverbial_verb = this._wordEmbeddingService.Encode(this._adverbial_verb, this._morphology);

                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                byte[] morphology_article = this._wordEmbeddingService.Encode(this._article, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);
                byte[] morphology_adjective = this._wordEmbeddingService.Encode(this._adjective, this._morphology);

                byte[] morphology_verb = this._wordEmbeddingService.Encode(this._verb, this._morphology);
                byte[] morphology_adverb = this._wordEmbeddingService.Encode(this._adverb, this._morphology);
                byte[] morphology_adverb_adverb = this._wordEmbeddingService.Encode(this._adverb_adverb, this._morphology);

                byte[]? noun = null;
                byte[]? article = null;
                byte[]? numeral = null;
                byte[]? pronoun = null;
                byte[]? adjective = null;

                byte[]? verb = null;
                byte[]? verb_adverb = null;
                byte[]? verb_adverb_adverb = null;

                byte[]? adverbial = null;
                byte[]? adnominal = null;

                foreach (Instruction item in words)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective = item.term;

                    if (item.kind.AsSpan().SequenceEqual(morphology_verb)) verb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) verb_adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) verb_adverb_adverb = item.term;
                };

                if (verb != null)
                    adverbial = verb;
                if ((verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb;
                if ((verb_adverb_adverb != null) && (verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb_adverb;

                if ((noun != null) && (adjective != null))
                    adnominal = adjective;
                if ((noun != null) && (article != null) && (adjective != null))
                    adnominal = article;
                if ((noun != null) && (numeral != null) && (adjective != null))
                    adnominal = numeral;
                if ((noun != null) && (pronoun != null) && (adjective != null))
                    adnominal = pronoun;

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, adverbial, adnominal);
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
                    if ((item.sentence == this._predicate) && (item.team == this._noun) && (item.kind == this._noun)) noun = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._noun) && 
                        ((item.kind == this._numeral) || (item.kind == this._article) || (item.kind == this._pronoun))) adjunct = item.term;
                    if ((item.sentence == this._predicate) && (item.kind == this._pronoun)) noun = item.term;
                    if (item.kind == this._verb) verb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._verb) && (item.kind == this._adverb)) adverb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._verb) && (item.kind == _adverb_adverb)) adverb_adverb = item.term;
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

        private bool VerifyVerbDirectObject(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb direct object \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                byte[] morphology_article = this._wordEmbeddingService.Encode(this._article, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);
                byte[] morphology_adjective = this._wordEmbeddingService.Encode(this._adjective, this._morphology);

                byte[] morphology_adverbial_verb = this._wordEmbeddingService.Encode(this._adverbial_verb, this._morphology);
                byte[] morphology_verb = this._wordEmbeddingService.Encode(this._verb, this._morphology);
                byte[] morphology_adverb = this._wordEmbeddingService.Encode(this._adverb, this._morphology);
                byte[] morphology_adverb_adverb = this._wordEmbeddingService.Encode(this._adverb_adverb, this._morphology);

                byte[] morphology_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);

                byte[]? noun = null;
                byte[]? numeral = null;
                byte[]? article = null;
                byte[]? pronoun = null;
                byte[]? adjective = null;
                byte[]? verb = null;
                byte[]? verb_adverb = null;
                byte[]? verb_adverb_adverb = null;
                byte[]? preposition = null;

                byte[]? adnominal = null;
                byte[]? adverbial = null;

                foreach (Instruction item in words)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun = item.term;
                    
                    if (item.kind.AsSpan().SequenceEqual(morphology_verb)) verb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) verb_adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) verb_adverb_adverb = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.kind.AsSpan().SequenceEqual(morphology_preposition))) preposition = item.term;
                };

                if (verb != null)
                    adverbial = verb;
                if ((verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb;
                if ((verb_adverb_adverb != null) && (verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb_adverb;

                if (noun != null)
                    adnominal = noun;
                if ((adjective != null) && (noun != null))
                    adnominal = adjective;
                if ((numeral != null) && (noun != null))
                    adnominal = numeral;
                if ((article != null) && (noun != null))
                    adnominal = article;
                if ((pronoun != null) && (noun != null))
                    adnominal = pronoun;

                bool similarity = false;
                if (preposition == null) similarity = this._wordEmbeddingService.Similarity(word_2_vec, adverbial, adnominal);
                if (preposition != null) similarity = this._wordEmbeddingService.Similarity(word_2_vec, preposition, adnominal);
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
                    if ((item.sentence == this._predicate) && (item.kind == this._preposition)) word_preposition = item.term;
                    if (item.kind == this._verb) verb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._verb) && (item.kind == this._adverb)) adverb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._verb) && (item.kind == _adverb_adverb)) adverb_adverb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._noun)) word_noun = item.term;
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

        private bool VerifyVerbIndirectObject(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb indirect object \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);

                byte[] morphology_adverbial_verb = this._wordEmbeddingService.Encode(this._adverbial_verb, this._morphology);
                byte[] morphology_verb = this._wordEmbeddingService.Encode(this._verb, this._morphology);
                byte[] morphology_adverb = this._wordEmbeddingService.Encode(this._adverb, this._morphology);
                byte[] morphology_adverb_adverb = this._wordEmbeddingService.Encode(this._adverb_adverb, this._morphology);

                byte[]? preposition = null;
                byte[]? verb = null;
                byte[]? verb_adverb = null;
                byte[]? verb_adverb_adverb = null;

                byte[]? adverbial = null;

                foreach (Instruction item in words)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.kind.AsSpan().SequenceEqual(morphology_preposition))) preposition = item.term;
                    
                    if (item.kind.AsSpan().SequenceEqual(morphology_verb)) verb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) verb_adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) verb_adverb_adverb = item.term;
                };

                if (verb != null)
                    adverbial = verb;
                if ((verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb;
                if ((verb_adverb_adverb != null) && (verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb_adverb;

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, adverbial, preposition);
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
                    if ((item.sentence == this._predicate) && (item.team == this._conjunction) && (item.kind == this._conjunction) && (item.order == order_conjunction)) conjunction = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._numeral) && (item.kind == this._numeral) && (item.order == order_conjunction)) numeral = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._numeral_noun) && (item.kind == this._numeral) && (item.order == order_conjunction)) noun = item.term;
                    if ((item.sentence == this._predicate) && (item.kind == this._preposition) && (item.order == order_conjunction)) preposition = item.term; 
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

        private bool  VerifyVerbNumeralConjunctionNoun(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify numeral conjunction noun \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);

                byte[] morphology_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);
                byte[] morphology_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);

                byte[] morphology_adverbial_verb = this._wordEmbeddingService.Encode(this._adverbial_verb, this._morphology);
                byte[] morphology_verb = this._wordEmbeddingService.Encode(this._verb, this._morphology);
                byte[] morphology_adverb = this._wordEmbeddingService.Encode(this._adverb, this._morphology);
                byte[] morphology_adverb_adverb = this._wordEmbeddingService.Encode(this._adverb_adverb, this._morphology);

                byte[]? numeral = null;
                byte[]? numeral_noun = null;

                byte[]? verb = null;
                byte[]? verb_adverb = null;
                byte[]? verb_adverb_adverb = null;

                byte[]? conjunction = null;
                byte[]? preposition = null;
                byte[]? adverbial = null;

                foreach (Instruction item in words)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_conjunction))
                        && (item.kind.AsSpan().SequenceEqual(morphology_conjunction))) conjunction = item.term;
                    
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_numeral))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral = item.term;
                    
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral_noun = item.term;

                    if (item.kind.AsSpan().SequenceEqual(morphology_verb)) verb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) verb_adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) verb_adverb_adverb = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.kind.AsSpan().SequenceEqual(morphology_preposition))) preposition = item.term;
                };

                if (verb != null)
                    adverbial = verb;
                if ((verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb;
                if ((verb_adverb_adverb != null) && (verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb_adverb;

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, numeral, conjunction);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, conjunction, numeral_noun);
                if ((similarity) && (preposition == null)) similarity = this._wordEmbeddingService.Similarity(word_2_vec, adverbial, numeral);
                if ((similarity) && (preposition != null)) similarity = this._wordEmbeddingService.Similarity(word_2_vec, preposition, numeral);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex) 
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyPrepositionNumeralConjunctionNoun(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec, byte[] order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify numeral conjunction noun \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);

                byte[] morphology_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);
                byte[] morphology_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);

                byte[]? numeral = null;
                byte[]? numeral_noun = null;

                byte[]? conjunction = null;
                byte[]? preposition = null;

                foreach (Instruction item in words)
                {
                    if (!item.order.AsSpan().SequenceEqual(order)) continue;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_conjunction))
                        && (item.kind.AsSpan().SequenceEqual(morphology_conjunction))) conjunction = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_numeral))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral_noun = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.kind.AsSpan().SequenceEqual(morphology_preposition))) preposition = item.term;
                }

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, preposition, numeral);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, numeral, conjunction);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, conjunction, numeral_noun);
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
                    if ((item.sentence == this._predicate) && (item.team == this._conjunction) && (item.kind == this._conjunction) && (item.order == order_conjunction)) conjunction = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._adjective) && (item.kind == this._adjective) && (item.order == order_conjunction)) adjective = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._adjective_noun) && (item.kind == this._adjective) && (item.order == order_conjunction)) noun = item.term;
                    if ((item.sentence == this._predicate) && (item.kind == this._preposition) && (item.order == order_conjunction)) preposition = item.term;
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

        private bool VerifyVerbAdjectiveConjunctionNoun(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify adjective conjunction noun \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_article = this._wordEmbeddingService.Encode(this._article, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);
                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);

                byte[] morphology_adverbial_adjective = this._wordEmbeddingService.Encode(this._adverbial_adjective, this._morphology);
                byte[] morphology_adjective = this._wordEmbeddingService.Encode(this._adjective, this._morphology);
                byte[] morphology_adverb = this._wordEmbeddingService.Encode(this._adverb, this._morphology);
                byte[] morphology_adverb_adverb = this._wordEmbeddingService.Encode(this._adverb_adverb, this._morphology);

                byte[] morphology_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                byte[] morphology_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);

                byte[] morphology_adverbial_verb = this._wordEmbeddingService.Encode(this._adverbial_verb, this._morphology);
                byte[] morphology_verb = this._wordEmbeddingService.Encode(this._verb, this._morphology);

                byte[]? conjunction = null;
                byte[]? preposition = null;

                byte[]? adjective = null;
                byte[]? adverb = null;
                byte[]? adverb_adverb = null;

                byte[]? noun = null;
                byte[]? adjective_noun = null;
                byte[]? article = null;
                byte[]? numeral = null;
                byte[]? pronoun = null;

                byte[]? verb = null;
                byte[]? verb_adverb = null;
                byte[]? verb_adverb_adverb = null;

                byte[]? adnominal = null;
                byte[]? adverbial_verb = null;
                byte[]? adverbial_adjective = null;

                foreach (Instruction item in words)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_conjunction))
                        && (item.kind.AsSpan().SequenceEqual(morphology_conjunction))) conjunction = item.term;
                    
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adjective))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adjective))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adjective))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) adverb_adverb = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective_noun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                    if (item.kind.AsSpan().SequenceEqual(morphology_verb)) verb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) verb_adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) verb_adverb_adverb = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.kind.AsSpan().SequenceEqual(morphology_preposition))) preposition = item.term;
                };

                if ((noun != null) && (adjective_noun != null))
                    adnominal = adjective_noun;
                if ((noun != null) && (article != null))
                    adnominal = article;
                if ((noun != null) && (numeral != null))
                    adnominal = numeral;
                if ((noun != null) && (pronoun != null))
                    adnominal = pronoun;

                if (adjective != null)
                    adverbial_adjective = adjective;
                if ((adjective != null) && (adverb != null))
                    adverbial_adjective = adverb;
                if ((adjective != null) && (adverb != null) && (adverb_adverb != null))
                    adverbial_adjective = adverb_adverb;

                if (verb != null)
                    adverbial_verb = verb;
                if ((verb_adverb != null) && (verb != null))
                    adverbial_verb = verb_adverb;
                if ((verb_adverb_adverb != null) && (verb_adverb != null) && (verb != null))
                    adverbial_verb = verb_adverb_adverb;

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, adverbial_adjective, conjunction);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, conjunction, adnominal);
                if ((similarity) && (preposition == null)) similarity = this._wordEmbeddingService.Similarity(word_2_vec, adverbial_verb, adjective);
                if ((similarity) && (preposition != null)) similarity = this._wordEmbeddingService.Similarity(word_2_vec, preposition, adjective);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyPrepositionAdjectiveConjunctionNoun(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec, byte[] order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify adjective conjunction noun \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_article = this._wordEmbeddingService.Encode(this._article, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);
                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);

                byte[] morphology_adverbial_adjective = this._wordEmbeddingService.Encode(this._adverbial_adjective, this._morphology);
                byte[] morphology_adjective = this._wordEmbeddingService.Encode(this._adjective, this._morphology);
                byte[] morphology_adverb = this._wordEmbeddingService.Encode(this._adverb, this._morphology);
                byte[] morphology_adverb_adverb = this._wordEmbeddingService.Encode(this._adverb_adverb, this._morphology);

                byte[] morphology_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                byte[] morphology_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);

                byte[]? conjunction = null;
                byte[]? preposition = null;

                byte[]? adjective = null;
                byte[]? adverb = null;
                byte[]? adverb_adverb = null;

                byte[]? noun = null;
                byte[]? adjective_noun = null;
                byte[]? article = null;
                byte[]? numeral = null;
                byte[]? pronoun = null;

                byte[]? adnominal = null;
                byte[]? adverbial_adjective = null;

                foreach (Instruction item in words)
                {
                    if (!item.order.AsSpan().SequenceEqual(order)) continue;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_conjunction))
                        && (item.kind.AsSpan().SequenceEqual(morphology_conjunction))) conjunction = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adjective))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adjective))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adjective))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) adverb_adverb = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective_noun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.kind.AsSpan().SequenceEqual(morphology_preposition))) preposition = item.term;
                }

                if ((noun != null) && (adjective_noun != null))
                    adnominal = adjective_noun;
                if ((noun != null) && (article != null))
                    adnominal = article;
                if ((noun != null) && (numeral != null))
                    adnominal = numeral;
                if ((noun != null) && (pronoun != null))
                    adnominal = pronoun;

                if (adjective != null)
                    adverbial_adjective = adjective;
                if ((adjective != null) && (adverb != null))
                    adverbial_adjective = adverb;
                if ((adjective != null) && (adverb != null) && (adverb_adverb != null))
                    adverbial_adjective = adverb_adverb;

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, preposition, adverbial_adjective);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, adverbial_adjective, conjunction);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, conjunction, adnominal);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyVerbNounConjunctionNoun(List<Instruction> words, List<Instruction> firsts, List<Instruction> lasts, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify noun conjunction noun \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);

                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);
                byte[] morphology_adjective = this._wordEmbeddingService.Encode(this._adjective, this._morphology);
                byte[] morphology_article = this._wordEmbeddingService.Encode(this._article, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);

                byte[] morphology_adverbial_verb = this._wordEmbeddingService.Encode(this._adverbial_verb, this._morphology);
                byte[] morphology_verb = this._wordEmbeddingService.Encode(this._verb, this._morphology);
                byte[] morphology_adverb = this._wordEmbeddingService.Encode(this._adverb, this._morphology);
                byte[] morphology_adverb_adverb = this._wordEmbeddingService.Encode(this._adverb_adverb, this._morphology);

                byte[] morphology_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                byte[] morphology_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);

                byte[]? conjunction = null;
                byte[]? preposition = null;

                byte[]? noun = null;
                byte[]? adjective = null;
                byte[]? article = null;
                byte[]? numeral = null;
                byte[]? pronoun = null;

                byte[]? verb = null;
                byte[]? verb_adverb = null;
                byte[]? verb_adverb_adverb = null;

                byte[]? noun2 = null;
                byte[]? adjective2 = null;
                byte[]? article2 = null;
                byte[]? numeral2 = null;
                byte[]? pronoun2 = null;

                byte[]? adnominal = null;
                byte[]? adnominal_first = null;
                byte[]? adnominal2 = null;
                byte[]? adverbial = null;

                foreach (Instruction item in words)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_conjunction))
                        && (item.kind.AsSpan().SequenceEqual(morphology_conjunction))) conjunction = item.term;
                    
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.kind.AsSpan().SequenceEqual(morphology_preposition))) preposition = item.term;

                    if (item.kind.AsSpan().SequenceEqual(morphology_verb)) verb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb))) verb_adverb = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_verb))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adverb_adverb))) verb_adverb_adverb = item.term;
                }

                foreach (Instruction item in firsts)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun = item.term;
                }

                foreach (Instruction item in lasts)
                {
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun2 = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun2 = item.term;
                }

                if (noun != null)
                {
                    adnominal = noun;
                    adnominal_first = noun;
                }
                if ((noun != null) && (adjective != null))
                {
                    adnominal = noun;
                    adnominal_first = adjective;
                }
                if ((noun != null) && (article != null))
                {
                    adnominal = noun;
                    adnominal_first = article;
                }
                if ((noun != null) && (numeral != null))
                {
                    adnominal = noun;
                    adnominal_first = numeral;
                }
                if ((noun != null) && (pronoun != null))
                {
                    adnominal = noun;
                    adnominal_first = pronoun;
                }

                if (noun2 != null)
                    adnominal2 = noun2;
                if ((noun2 != null) && (adjective2 != null))
                    adnominal2 = adjective2;
                if ((noun2 != null) && (article2 != null))
                    adnominal2 = article2;
                if ((noun2 != null) && (numeral2 != null))
                    adnominal2 = numeral2;
                if ((noun2 != null) && (pronoun2 != null))
                    adnominal2 = pronoun2;

                if (verb != null)
                    adverbial = verb;
                if ((verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb;
                if ((verb_adverb_adverb != null) && (verb_adverb != null) && (verb != null))
                    adverbial = verb_adverb_adverb;

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, adnominal, conjunction);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, conjunction, adnominal2);
                if ((similarity) && (preposition == null)) similarity = this._wordEmbeddingService.Similarity(word_2_vec, adverbial, adnominal_first);
                if ((similarity) && (preposition != null)) similarity = this._wordEmbeddingService.Similarity(word_2_vec, preposition, adnominal_first);
                if (similarity) return true;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private bool VerifyPrepositionNounConjunctionNoun(List<Instruction> words, List<Instruction> firsts, List<Instruction> lasts, Dictionary<(byte[], byte[]), int> word_2_vec, byte[] order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify noun conjunction noun \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);

                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);
                byte[] morphology_adjective = this._wordEmbeddingService.Encode(this._adjective, this._morphology);
                byte[] morphology_article = this._wordEmbeddingService.Encode(this._article, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);

                byte[] morphology_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                byte[] morphology_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);

                byte[]? conjunction = null;
                byte[]? preposition = null;

                byte[]? noun = null;
                byte[]? adjective = null;
                byte[]? article = null;
                byte[]? numeral = null;
                byte[]? pronoun = null;

                byte[]? noun2 = null;
                byte[]? adjective2 = null;
                byte[]? article2 = null;
                byte[]? numeral2 = null;
                byte[]? pronoun2 = null;

                byte[]? adnominal = null;
                byte[]? adnominal_first = null;
                byte[]? adnominal2 = null;

                foreach (Instruction item in words)
                {
                    if (!item.order.AsSpan().SequenceEqual(order)) continue;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_conjunction))
                        && (item.kind.AsSpan().SequenceEqual(morphology_conjunction))) conjunction = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.kind.AsSpan().SequenceEqual(morphology_preposition))) preposition = item.term;
                }

                foreach (Instruction item in firsts)
                {
                    if (!item.order.AsSpan().SequenceEqual(order)) continue;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun = item.term;
                }

                foreach (Instruction item in lasts)
                {
                    if (!item.order.AsSpan().SequenceEqual(order)) continue;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun2 = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun2 = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun2 = item.term;
                }

                if (noun != null)
                {
                    adnominal = noun;
                    adnominal_first = noun;
                }
                if ((noun != null) && (adjective != null))
                {
                    adnominal = noun;
                    adnominal_first = adjective;
                }
                if ((noun != null) && (article != null))
                {
                    adnominal = noun;
                    adnominal_first = article;
                }
                if ((noun != null) && (numeral != null))
                {
                    adnominal = noun;
                    adnominal_first = numeral;
                }
                if ((noun != null) && (pronoun != null))
                {
                    adnominal = noun;
                    adnominal_first = pronoun;
                }

                if (noun2 != null)
                    adnominal2 = noun2;
                if ((noun2 != null) && (adjective2 != null))
                    adnominal2 = adjective2;
                if ((noun2 != null) && (article2 != null))
                    adnominal2 = article2;
                if ((noun2 != null) && (numeral2 != null))
                    adnominal2 = numeral2;
                if ((noun2 != null) && (pronoun2 != null))
                    adnominal2 = pronoun2;

                bool similarity = false;
                similarity = this._wordEmbeddingService.Similarity(word_2_vec, preposition, adnominal_first);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, adnominal, conjunction);
                if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, conjunction, adnominal2);
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
                    if ((item.sentence == this._predicate) && (item.team == this._noun) && (item.order == order_direct_object)) noun = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._preposition)) preposition = item.term;
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

        private bool VerifyDirectObjectPreposition(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec, byte[] order_direct_object, byte[] order_preposition)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify direct object preposition \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);

                byte[]? preposition = null;

                foreach (Instruction item in words)
                {
                    if (!item.order.AsSpan().SequenceEqual(order_preposition)) continue;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_preposition))
                        && (item.kind.AsSpan().SequenceEqual(morphology_preposition))) preposition = item.term;
                }

                byte[] morphology_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_adverbial_adjective = this._wordEmbeddingService.Encode(this._adverbial_adjective, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                byte[] morphology_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);

                List<Instruction> compound_conjunction = words.FindAll(index => index.team.SequenceEqual(morphology_conjunction)
                    && index.sentence.SequenceEqual(morphology_predicate));
                List<Instruction> compound_adnominal = words.FindAll(index => index.team.SequenceEqual(morphology_adnominal_adjunct)
                    && index.sentence.SequenceEqual(morphology_predicate));
                List<Instruction> compound_numeral = words.FindAll(index => index.team.SequenceEqual(morphology_numeral)
                    && index.sentence.SequenceEqual(morphology_predicate));
                List<Instruction> compound_adjective = words.FindAll(index => index.team.SequenceEqual(morphology_adverbial_adjective)
                    && index.sentence.SequenceEqual(morphology_predicate));
                List<Instruction> compound_pronoun = words.FindAll(index => (!index.team.SequenceEqual(morphology_pronoun))
                    && (index.kind.SequenceEqual(morphology_pronoun))
                    && index.sentence.SequenceEqual(morphology_predicate));

                byte[]? noun = null;
                byte[]? noun2 = null;
                byte[]? pronoun = null;
                byte[]? pronoun2 = null;

                bool similarity = false;
                if (((compound_conjunction.Count > 0) && (compound_numeral.Count > 0))
                    || ((compound_conjunction.Count > 0) && (compound_adjective.Count > 0)))
                {
                    foreach (Instruction item in words)
                    {
                        if (!item.order.AsSpan().SequenceEqual(order_direct_object)) continue;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;
                    }
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun, preposition);
                    if (similarity) 
                        return true;
                }
                if ((compound_conjunction.Count > 0) && (compound_pronoun.Count > 1))
                {
                    int index = 0;
                    foreach (Instruction item in words)
                    {
                        if (!item.order.AsSpan().SequenceEqual(order_direct_object)) continue;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_pronoun)))
                        {
                            if (index == 0) pronoun = item.term;
                            if (index == 1) pronoun2 = item.term;
                        }
                        index++;
                    }
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, pronoun, preposition);
                    if (similarity) 
                        return true;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, pronoun2, preposition);
                    if (similarity) 
                        return true;
                }
                if ((compound_conjunction.Count > 0) && (compound_adnominal.Count > 1))
                {
                    int index = 0;
                    foreach (Instruction item in words)
                    {
                        if (!item.order.AsSpan().SequenceEqual(order_direct_object)) continue;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_noun)))
                        {
                            if (index == 0) noun = item.term;
                            if (index == 1) noun2 = item.term;
                        }
                        index++;
                    }
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun, preposition);
                    if (similarity) 
                        return true;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun2, preposition);
                    if (similarity) 
                        return true;
                }
                if ((compound_conjunction.Count > 0) 
                    && (compound_adnominal.Count > 0) 
                    && (compound_pronoun.Count > 0))
                {
                    foreach (Instruction item in words)
                    {
                        if (!item.order.AsSpan().SequenceEqual(order_direct_object)) continue;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun = item.term;
                    }
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun, preposition);
                    if (similarity) 
                        return true;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, pronoun, preposition);
                    if (similarity) 
                        return true;
                }
                if (compound_conjunction.Count == 0)
                {
                    foreach (Instruction item in words)
                    {
                        if (!item.order.AsSpan().SequenceEqual(order_direct_object)) continue;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun = item.term;
                    }
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun, preposition);
                    if (similarity) 
                        return true;
                }
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
                    if ((item.sentence == this._predicate) && (item.team == this._noun) && (item.kind == this._noun) && (item.order == order_indirect_object)) noun = item.term;
                    if ((item.sentence == this._predicate) && (item.kind == this._pronoun) && (item.order == order_indirect_object)) noun = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._noun) && (item.order == order_indirect_object)
                        && ((item.kind == this._numeral) || (item.kind == this._article) || (item.kind == this._pronoun))) adjunct = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._preposition) && (item.order == order_indirect_object)) preposition = item.term;
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

        private bool VerifyIndirectObject(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec, byte[] order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify indirect object \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_article = this._wordEmbeddingService.Encode(this._article, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);
                byte[] morphology_adjective = this._wordEmbeddingService.Encode(this._adjective, this._morphology);
                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);

                byte[] morphology_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);

                byte[]? noun = null;
                byte[]? adjective = null;
                byte[]? article = null;
                byte[]? numeral = null;
                byte[]? pronoun = null;

                byte[]? preposition = null;
                byte[]? adnominal = null;

                foreach (Instruction item in words)
                {
                    if (!item.order.AsSpan().SequenceEqual(order)) continue;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_article))) article = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_numeral))) numeral = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun = item.term;
                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                        && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun = item.term;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.kind.AsSpan().SequenceEqual(morphology_preposition))) preposition = item.term;
                }

                if (noun != null)
                    adnominal = noun;
                if ((noun != null) && (adjective != null))
                    adnominal = adjective;
                if ((noun != null) && (article != null))
                    adnominal = article;
                if ((noun != null) && (numeral != null))
                    adnominal = numeral;
                if ((noun != null) && (pronoun != null))
                    adnominal = pronoun;

                bool similarity = false;

                similarity = this._wordEmbeddingService.Similarity(word_2_vec, preposition, adnominal);
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
                    if ((item.sentence == this._predicate) && (item.team == this._preposition) && (item.order == order_indirect_object)) preposition = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._adjective_noun) && (item.order == order_indirect_object)
                        && ((item.kind == this._article) || (item.kind == this._numeral) || (item.kind == this._pronoun))) adjunct = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._adjective_noun) && (item.kind == this._adjective) && (item.order == order_indirect_object)) adjective = item.term;
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
                    if ((item.sentence == this._predicate) && (item.team == this._adjective_adverb) && (item.kind == this._adjective) && (item.order == order_predicative)) adjective = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._noun) && (item.kind == this._noun) && (item.order == order_object_direct)) noun = item.term;
                    if ((item.sentence == this._predicate) && (item.kind == this._pronoun) && (item.order == order_object_direct)) pronoun = item.term;
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

        private bool VerifyDirectObjectPredicative(List<Instruction> words, Dictionary<(byte[], byte[]), int> word_2_vec, byte[] order_direct_object, byte[] order_predicative)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify direct object predicative \"Syntax\" service failed!");

                byte[] syntax_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adverbial_adjective = this._wordEmbeddingService.Encode(this._adverbial_adjective, this._morphology);
                byte[] morphology_adjective = this._wordEmbeddingService.Encode(this._adjective, this._morphology);

                byte[]? adjective = null;

                foreach (Instruction item in words)
                {
                    if (!item.order.AsSpan().SequenceEqual(order_predicative)) continue;

                    if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                        && (item.team.AsSpan().SequenceEqual(morphology_adverbial_adjective))
                        && (item.kind.AsSpan().SequenceEqual(morphology_adjective))) adjective = item.term;
                }

                byte[] morphology_predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] morphology_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                byte[] morphology_pronoun = this._wordEmbeddingService.Encode(this._pronoun, this._morphology);
                byte[] morphology_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                byte[] morphology_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                byte[] morphology_noun = this._wordEmbeddingService.Encode(this._noun, this._morphology);

                List<Instruction> compound_conjunction = words.FindAll(index => index.team.SequenceEqual(morphology_conjunction)
                    && index.sentence.SequenceEqual(morphology_predicate));
                List<Instruction> compound_adnominal = words.FindAll(index => index.team.SequenceEqual(morphology_adnominal_adjunct)
                    && index.sentence.SequenceEqual(morphology_predicate));
                List<Instruction> compound_numeral = words.FindAll(index => index.team.SequenceEqual(morphology_numeral)
                    && index.sentence.SequenceEqual(morphology_predicate));
                List<Instruction> compound_adjective = words.FindAll(index => index.team.SequenceEqual(morphology_adverbial_adjective)
                    && index.sentence.SequenceEqual(morphology_predicate));
                List<Instruction> compound_pronoun = words.FindAll(index => (!index.team.SequenceEqual(morphology_pronoun))
                    && (index.kind.SequenceEqual(morphology_pronoun))
                    && index.sentence.SequenceEqual(morphology_predicate));

                byte[]? noun = null;
                byte[]? noun2 = null;
                byte[]? pronoun = null;
                byte[]? pronoun2 = null;

                bool similarity = false;
                if (((compound_conjunction.Count > 0) && (compound_numeral.Count > 0))
                    || ((compound_conjunction.Count > 0) && (compound_adjective.Count > 0)))
                {
                    foreach (Instruction item in words)
                    {
                        if (!item.order.AsSpan().SequenceEqual(order_direct_object)) continue;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;
                    }
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun, adjective);
                    if (similarity) return true;
                }
                if ((compound_conjunction.Count > 0) && (compound_pronoun.Count > 1))
                {
                    int index = 0;
                    foreach (Instruction item in words)
                    {
                        if (!item.order.AsSpan().SequenceEqual(order_direct_object)) continue;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_pronoun)))
                        {
                            if (index == 0) pronoun = item.term;
                            if (index == 1) pronoun2 = item.term;
                        }
                        index++;
                    }
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, pronoun, adjective);
                    if (similarity) return true;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, pronoun2, adjective);
                    if (similarity) return true;
                }
                if ((compound_conjunction.Count > 0) && (compound_adnominal.Count > 1))
                {
                    int index = 0;
                    foreach (Instruction item in words)
                    {
                        if (!item.order.AsSpan().SequenceEqual(order_direct_object)) continue;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_noun)))
                        {
                            if (index == 0) noun = item.term;
                            if (index == 1) noun2 = item.term;
                        }
                        index++;
                    } 
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun, adjective);
                    if (similarity) return true;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun2, adjective);
                    if (similarity) return true;
                }
                if ((compound_conjunction.Count > 0) && (compound_adnominal.Count > 0) && (compound_pronoun.Count > 0))
                {
                    foreach (Instruction item in words)
                    {
                        if (!item.order.AsSpan().SequenceEqual(order_direct_object)) continue;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) pronoun = item.term;
                    }
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun, adjective);
                    if (similarity) return true;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, pronoun, adjective);
                    if (similarity) return true;
                }
                if (compound_conjunction.Count == 0)
                {
                    foreach (Instruction item in words)
                    {
                        if (!item.order.AsSpan().SequenceEqual(order_direct_object)) continue;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_noun))) noun = item.term;

                        if ((item.sentence.AsSpan().SequenceEqual(syntax_predicate))
                            && (!item.team.AsSpan().SequenceEqual(morphology_adnominal_adjunct))
                            && (item.kind.AsSpan().SequenceEqual(morphology_pronoun))) noun = item.term;
                    }
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, noun, adjective);
                    if (similarity) return true;
                }
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
                    if ((item.sentence == this._predicate) && (item.team == this._adjective_adverb) && (item.kind == this._adjective) && (item.order == order_predicative)) adjective = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._adjective_adverb) && (item.kind == this._adverb) && (item.order == order_predicative)) adverb = item.term;
                    if ((item.sentence == this._predicate) && (item.team == this._preposition) && (item.order == order_preposicao)) preposition = item.term;
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
                word.sentence = sentence;
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

        private Instruction Lecture(byte[] term, byte[] kind, byte[] sentence, byte[] team, byte[] order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation lecture \"Syntax\" view model failed!");

                Instruction word = new Instruction();
                word.term = term;
                word.kind = kind;
                word.sentence = sentence;
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

        private List<Tutorial> Union(List<Tutorial> firsts, List<Tutorial> lasts)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation union \"Syntax\" service failed!");

                List<Tutorial> tutorials = new List<Tutorial>();
                firsts.ForEach(item => tutorials.Add(item));
                lasts.ForEach(item => tutorials.Add(item));
                return tutorials;
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

        private List<Tutorial> MountNounVerb(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount noun verb \"Syntax\" view model failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adverbial_verb = new List<byte[]>();
                byte[] sha_adverbial_verb = this._wordEmbeddingService.Encode(this._adverbial_verb, this._morphology);
                kind_adverbial_verb.Add(sha_adverbial_verb);
                List<Tutorial> adverbials_verbs = new List<Tutorial>();
                adverbials_verbs = FilterLesson(tutorials, kind_adverbial_verb);

                List<byte[]> kind_adnominal_adjunct = new List<byte[]>();
                byte[] sha_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                kind_adnominal_adjunct.Add(sha_adnominal_adjunct);
                byte[] sha_personal = this._wordEmbeddingService.Encode(this._personal, this._morphology);
                kind_adnominal_adjunct.Add(sha_personal);
                byte[] sha_demonstrative = this._wordEmbeddingService.Encode(this._demonstrative, this._morphology);
                kind_adnominal_adjunct.Add(sha_demonstrative);
                List<Tutorial> adnominals_adjuncts = new List<Tutorial>();
                adnominals_adjuncts = FilterLesson(tutorials, kind_adnominal_adjunct);

                byte[] subject = this._wordEmbeddingService.Encode(this._subject, this._syntax);
                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_noun = this._wordEmbeddingService.Encode(this._order_1, this._order);
                byte[] order_verb = this._wordEmbeddingService.Encode(this._order_2, this._order);

                foreach (Tutorial adverbial_verb in adverbials_verbs)
                {
                    foreach (Tutorial adnominal_adjunct in adnominals_adjuncts)
                    {
                        List<Instruction> words = new List<Instruction>();
                        foreach (Instruction item in adverbial_verb.lecture)
                        {
                            Instruction word = new Instruction();
                            word = Lecture(item.term, item.kind, predicate, adverbial_verb.team, order_verb);
                            words.Add(word);
                        };
                        foreach (Instruction item in adnominal_adjunct.lecture)
                        {
                            Instruction word = new Instruction();
                            word = Lecture(item.term, item.kind, subject, adnominal_adjunct.team, order_noun);
                            words.Add(word);
                        };
                        if (!VerifyVerbSampleSubject(words, word_2_vec)) continue;
                        Tutorial seminar = new Tutorial();
                        seminar.lecture = words;
                        seminars.Add(seminar);
                    }
                }
                return seminars;
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

        private List<Tutorial> MountCompoundVerb(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount compound verb \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adnominal_adjunct = new List<byte[]>();
                byte[] sha_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                kind_adnominal_adjunct.Add(sha_adnominal_adjunct);
                byte[] sha_personal = this._wordEmbeddingService.Encode(this._personal, this._morphology);
                kind_adnominal_adjunct.Add(sha_personal);
                byte[] sha_demonstrative = this._wordEmbeddingService.Encode(this._demonstrative, this._morphology);
                kind_adnominal_adjunct.Add(sha_demonstrative);
                List<Tutorial> adnominals_firsts = new List<Tutorial>();
                adnominals_firsts = FilterLesson(tutorials, kind_adnominal_adjunct);

                List<Tutorial> adnominals_lasts = new List<Tutorial>();
                adnominals_lasts = FilterLesson(tutorials, kind_adnominal_adjunct);

                List<byte[]> kind_adverbial_verb = new List<byte[]>();
                byte[] sha_adverbial_verb = this._wordEmbeddingService.Encode(this._adverbial_verb, this._morphology);
                kind_adverbial_verb.Add(sha_adverbial_verb);
                List<Tutorial> adverbials_verbs = new List<Tutorial>();
                adverbials_verbs = FilterLesson(tutorials, kind_adverbial_verb);

                List<byte[]> kind_conjunction = new List<byte[]>();
                byte[] sha_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                kind_conjunction.Add(sha_conjunction);
                List<Tutorial> conjunctions = new List<Tutorial>();
                conjunctions = FilterLesson(tutorials, kind_conjunction);

                byte[] subject = this._wordEmbeddingService.Encode(this._subject, this._syntax);
                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_noun = this._wordEmbeddingService.Encode(this._order_1, this._order);
                byte[] order_verb = this._wordEmbeddingService.Encode(this._order_2, this._order);

                foreach (Tutorial adverbial_verb in adverbials_verbs)
                {
                    foreach (Tutorial conjunction in conjunctions)
                    {
                        foreach (Tutorial adnominal_first in adnominals_firsts)
                        {
                            foreach (Tutorial adnominal_last in adnominals_lasts)
                            {
                                List<Instruction> words = new List<Instruction>();
                                List<Instruction> firsts = new List<Instruction>();
                                List<Instruction> lasts = new List<Instruction>();
                                words.ForEach(item => words.Add(item));
                                foreach (Instruction item in adverbial_verb.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adverbial_verb.team, order_verb);
                                    words.Add(word);
                                }
                                foreach (Instruction item in adnominal_last.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, subject, adnominal_last.team, order_noun);
                                    words.Add(word);
                                    lasts.Add(word);
                                }
                                foreach (Instruction item in adnominal_first.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, subject, adnominal_first.team, order_noun);
                                    words.Add(word);
                                    firsts.Add(word);
                                }
                                foreach (Instruction item in conjunction.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, subject, conjunction.team, order_noun);
                                    words.Add(word);
                                }
                                if (!VerifyVerbCompoundSubject(words, firsts, lasts, word_2_vec)) continue;
                                Tutorial seminar = new Tutorial();
                                seminar.lecture = words;
                                seminars.Add(seminar);
                            }
                        }
                    }
                }
                return seminars;
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

        private List<Tutorial> MountVerbNoun(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb noun \"Syntax\" service failed!");
               
                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adnominal_adjunct = new List<byte[]>();
                byte[] sha_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                kind_adnominal_adjunct.Add(sha_adnominal_adjunct);
                byte[] sha_possessive = this._wordEmbeddingService.Encode(this._possessive, this._morphology);
                kind_adnominal_adjunct.Add(sha_possessive);
                byte[] sha_pessoal = this._wordEmbeddingService.Encode(this._personal, this._morphology);
                kind_adnominal_adjunct.Add(sha_pessoal);
                List<Tutorial> adnominals_adjunts = new List<Tutorial>();
                adnominals_adjunts = FilterLesson(tutorials, kind_adnominal_adjunct);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_noun = this._wordEmbeddingService.Encode(order, this._order);

                if (adnominals_adjunts.Count == 0) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial adnominal_adjunt in adnominals_adjunts)
                    {
                        List<Instruction> words1 = new List<Instruction>();
                        words.ForEach(item => words1.Add(item));
                        foreach (Instruction item in adnominal_adjunt.lecture)
                        {
                            Instruction word = new Instruction();
                            word = Lecture(item.term, item.kind, predicate, adnominal_adjunt.team, order_noun);
                            words1.Add(word);
                        };
                        if (!VerifyVerbDirectObject(words1, word_2_vec)) continue;
                        Tutorial seminar = new Tutorial();
                        seminar.lecture = words1;
                        seminars.Add(seminar);
                    }
                }
                return seminars;
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

        private List<Tutorial> MountVerbAdjectiveNoun(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb adjective noun \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adjective_noun = new List<byte[]>();
                byte[] ssh_adjective_noun = this._wordEmbeddingService.Encode(this._adjective_noun, this._morphology);
                kind_adjective_noun.Add(ssh_adjective_noun);
                List<Tutorial> adjectives_nouns = new List<Tutorial>();
                adjectives_nouns = FilterLesson(tutorials, kind_adjective_noun);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_adjective_noun = this._wordEmbeddingService.Encode(order, this._order);

                if (adjectives_nouns.Count == 0) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial adjective_noun in adjectives_nouns)
                    {
                        List<Instruction> words1 = new List<Instruction>();
                        words.ForEach(item => words1.Add(item));

                        foreach (Instruction item in adjective_noun.lecture)
                        {
                            Instruction word = new Instruction();
                            word = Lecture(item.term, item.kind, predicate, adjective_noun.team, order_adjective_noun);
                            words1.Add(word);
                        };
                        if (!VerifyVerbAdjectiveNoun(words1, word_2_vec)) continue;
                        Tutorial seminar = new Tutorial();
                        seminar.lecture = words1;
                        seminars.Add(seminar);
                    }
                }
                return seminars;
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

        private List<Tutorial> MountVerbAdjective(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb adjective \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adverbial_adjective = new List<byte[]>();
                byte[] sha_adverbial_adjective = this._wordEmbeddingService.Encode(this._adverbial_adjective, this._morphology);
                kind_adverbial_adjective.Add(sha_adverbial_adjective);
                List<Tutorial> adverbials_adjectives = new List<Tutorial>();
                adverbials_adjectives = FilterLesson(tutorials, kind_adverbial_adjective);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_adjective = this._wordEmbeddingService.Encode(order, this._order);

                if (adverbials_adjectives.Count == 0) return seminars;
                foreach (Tutorial source in tutorials)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial adverbial_adjective in adverbials_adjectives)
                    {
                        List<Instruction> words1 = new List<Instruction>();
                        words.ForEach(item => words1.Add(item));
                        foreach (Instruction item in adverbial_adjective.lecture)
                        {
                            Instruction word = new Instruction();
                            word = Lecture(item.term, item.kind, predicate, adverbial_adjective.team, order_adjective);
                            words1.Add(word);
                        };
                        if (!VerifyVerbPredicative(words1, word_2_vec)) continue;
                        Tutorial tutorial = new Tutorial();
                        tutorial.lecture = words1;
                        tutorials.Add(tutorial);
                    }
                }
                return tutorials;
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

        private List<Tutorial> MountObjectPredicative(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_sample, int order_predicative)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount object predicative \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adverbial_adjective = new List<byte[]>();
                byte[] sha_adverbial_adjective = this._wordEmbeddingService.Encode(this._adverbial_adjective, this._morphology);
                kind_adverbial_adjective.Add(sha_adverbial_adjective);
                List<Tutorial> adverbials_adjectives = new List<Tutorial>();
                adverbials_adjectives = FilterLesson(tutorials, kind_adverbial_adjective);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_object = this._wordEmbeddingService.Encode(order_sample, this._order);
                byte[] order_predicate = this._wordEmbeddingService.Encode(order_predicative, this._order);

                if (adverbials_adjectives.Count == 0) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial adverbial_adjective in adverbials_adjectives)
                    {
                        List<Instruction> words1 = new List<Instruction>();
                        words.ForEach(item => words1.Add(item));

                        foreach (Instruction item in adverbial_adjective.lecture)
                        {
                            Instruction word = new Instruction();
                            word = Lecture(item.term, item.kind, predicate, adverbial_adjective.team, order_predicate);
                            words1.Add(word);
                        };
                        if (!VerifyDirectObjectPredicative(words1, word_2_vec, order_object, order_predicate)) continue;
                        Tutorial seminar = new Tutorial();
                        seminar.lecture = words1;
                        seminars.Add(seminar);
                    }
                }
                return seminars;
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
        private List<Lesson> MountDirectObjectPreposition(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_preposition)
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

        private List<Tutorial> MountDirectObjectPreposition(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_direct_object, int order_indirect_object)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount preposition indirect object \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_preposition = new List<byte[]>();
                byte[] sha_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);
                kind_preposition.Add(sha_preposition);
                List<Tutorial> prepositions = new List<Tutorial>();
                prepositions = FilterLesson(tutorials, kind_preposition);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order = this._wordEmbeddingService.Encode(order_direct_object, this._order);
                byte[] order_preposition = this._wordEmbeddingService.Encode(order_indirect_object, this._order);

                if (prepositions.Count == 0) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial preposition in prepositions)
                    {
                        List<Instruction> words1 = new List<Instruction>();
                        words.ForEach(item => words1.Add(item));
                        foreach (Instruction item in preposition.lecture)
                        {
                            Instruction word = new Instruction();
                            word = Lecture(item.term, item.kind, predicate, preposition.team, order_preposition);
                            words1.Add(word);
                        };
                        if (!VerifyDirectObjectPreposition(words1, word_2_vec, order, order_preposition)) continue;
                        Tutorial seminar = new Tutorial();
                        seminar.lecture = words1;
                        seminars.Add(seminar);
                    }
                }
                return seminars;
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

        private List<Tutorial> MountVerbIndirectObject(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb indirect object \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_preposition = new List<byte[]>();
                byte[] sha_preposition = this._wordEmbeddingService.Encode(this._preposition, this._morphology);
                kind_preposition.Add(sha_preposition);
                List<Tutorial> prepositions = new List<Tutorial>();
                prepositions = FilterLesson(tutorials, kind_preposition);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_preposition = this._wordEmbeddingService.Encode(order, this._order);

                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial preposition in prepositions)
                    {
                        List<Instruction> words1 = new List<Instruction>();
                        words.ForEach(item => words1.Add(item));

                        foreach (Instruction item in preposition.lecture)
                        {
                            Instruction word = new Instruction();
                            word = Lecture(item.term, item.kind, predicate, preposition.team, order_preposition);
                            words1.Add(word);
                        };
                        if (!VerifyVerbIndirectObject(words1, word_2_vec)) continue;
                        Tutorial seminar = new Tutorial();
                        seminar.lecture = words1;
                        seminars.Add(seminar);
                    }
                }
                return seminars;
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

        private List<Tutorial> MountDirectObjectIndirectObject(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount indirect object noun \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adnominal_adjunct = new List<byte[]>();
                byte[] sha_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                kind_adnominal_adjunct.Add(sha_adnominal_adjunct);
                byte[] sha_possessive = this._wordEmbeddingService.Encode(this._possessive, this._morphology);
                kind_adnominal_adjunct.Add(sha_possessive);
                byte[] sha_pessoal = this._wordEmbeddingService.Encode(this._personal, this._morphology);
                kind_adnominal_adjunct.Add(sha_pessoal);
                List<Tutorial> adnominals_adjuncts = new List<Tutorial>();
                adnominals_adjuncts = FilterLesson(tutorials, kind_adnominal_adjunct);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_indirect_object = this._wordEmbeddingService.Encode(order, this._order);

                if (adnominals_adjuncts.Count == 0) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial adnominal_adjunct in adnominals_adjuncts)
                    {
                        List<Instruction> words1 = new List<Instruction>();
                        words.ForEach(item => words1.Add(item));

                        foreach (Instruction item in adnominal_adjunct.lecture)
                        {
                            Instruction word = new Instruction();
                            word = Lecture(item.term, item.kind, predicate, adnominal_adjunct.team, order_indirect_object);
                            words1.Add(word);
                        };
                        if (!VerifyIndirectObject(words1, word_2_vec, order_indirect_object)) continue;
                        Tutorial seminar = new Tutorial();
                        seminar.lecture = words1;
                        seminars.Add(seminar);
                    }
                }
                return seminars;
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

        private List<Tutorial> MountVerbNumeralConjunctionNoun(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb numeral conjunction noun \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adnominal_adjunct = new List<byte[]>();
                byte[] sha_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                kind_adnominal_adjunct.Add(sha_adnominal_adjunct);
                List<Tutorial> adnominals_adjuncts = new List<Tutorial>();
                adnominals_adjuncts = FilterLesson(tutorials, kind_adnominal_adjunct);

                List<byte[]> kind_numeral = new List<byte[]>();
                byte[] sha_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                kind_numeral.Add(sha_numeral);
                List<Tutorial> numerals = new List<Tutorial>();
                numerals = FilterLesson(tutorials, kind_numeral);

                List<byte[]> kind_conjunction = new List<byte[]>();
                byte[] sha_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                kind_conjunction.Add(sha_conjunction);
                List<Tutorial> conjunctions = new List<Tutorial>();
                conjunctions = FilterLesson(tutorials, kind_conjunction);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_compound = this._wordEmbeddingService.Encode(order, this._order);

                if ((adnominals_adjuncts.Count == 0) || (numerals.Count == 0) || (conjunctions.Count == 0)) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial conjunction in conjunctions)
                    {
                        foreach (Tutorial numeral in numerals)
                        {
                            foreach (Tutorial adnominal_adjunct in adnominals_adjuncts)
                            {
                                List<Instruction> words1 = new List<Instruction>();
                                words.ForEach(item => words1.Add(item));
                                foreach (Instruction item in adnominal_adjunct.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adnominal_adjunct.team, order_compound);
                                    words1.Add(word);
                                };
                                foreach (Instruction item in numeral.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, numeral.team, order_compound);
                                    words1.Add(word);
                                };
                                foreach (Instruction item in conjunction.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, conjunction.team, order_compound);
                                    words1.Add(word);
                                };
                                if (!VerifyVerbNumeralConjunctionNoun(words1, word_2_vec)) continue;
                                Tutorial seminar = new Tutorial();
                                seminar.lecture = words1;
                                seminars.Add(seminar);
                            }
                        }
                    }
                }
                return seminars;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Tutorial> MountPrepositionNumeralConjunctionNoun(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb numeral conjunction noun \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adnominal_adjunct = new List<byte[]>();
                byte[] sha_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                kind_adnominal_adjunct.Add(sha_adnominal_adjunct);
                List<Tutorial> adnominals_adjuncts = new List<Tutorial>();
                adnominals_adjuncts = FilterLesson(tutorials, kind_adnominal_adjunct);

                List<byte[]> kind_numeral = new List<byte[]>();
                byte[] sha_numeral = this._wordEmbeddingService.Encode(this._numeral, this._morphology);
                kind_numeral.Add(sha_numeral);
                List<Tutorial> numerals = new List<Tutorial>();
                numerals = FilterLesson(tutorials, kind_numeral);

                List<byte[]> kind_conjunction = new List<byte[]>();
                byte[] sha_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                kind_conjunction.Add(sha_conjunction);
                List<Tutorial> conjunctions = new List<Tutorial>();
                conjunctions = FilterLesson(tutorials, kind_conjunction);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_compound = this._wordEmbeddingService.Encode(order, this._order);

                if ((adnominals_adjuncts.Count == 0) || (numerals.Count == 0) || (conjunctions.Count == 0)) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial conjunction in conjunctions)
                    {
                        foreach (Tutorial numeral in numerals)
                        {
                            foreach (Tutorial adnominal_adjunct in adnominals_adjuncts)
                            {
                                List<Instruction> words1 = new List<Instruction>();
                                words.ForEach(item => words1.Add(item));
                                foreach (Instruction item in adnominal_adjunct.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adnominal_adjunct.team, order_compound);
                                    words1.Add(word);
                                }
                                ;
                                foreach (Instruction item in numeral.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, numeral.team, order_compound);
                                    words1.Add(word);
                                }
                                foreach (Instruction item in conjunction.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, conjunction.team, order_compound);
                                    words1.Add(word);
                                }
                                if (!VerifyPrepositionNumeralConjunctionNoun(words1, word_2_vec, order_compound)) continue;
                                Tutorial seminar = new Tutorial();
                                seminar.lecture = words1;
                                seminars.Add(seminar);
                            }
                        }
                    }
                }
                return seminars;
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

        private List<Tutorial> MountVerbAdjectiveConjunctionNoun(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb adjective conjunction noun \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adnominal_adjunct = new List<byte[]>();
                byte[] sha_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                kind_adnominal_adjunct.Add(sha_adnominal_adjunct);
                List<Tutorial> adnominals_adjuncts = new List<Tutorial>();
                adnominals_adjuncts = FilterLesson(tutorials, kind_adnominal_adjunct);

                List<byte[]> kind_adverbial_adjective = new List<byte[]>();
                byte[] sha_adverbial_adjective = this._wordEmbeddingService.Encode(this._adverbial_adjective, this._morphology);
                kind_adverbial_adjective.Add(sha_adverbial_adjective);
                List<Tutorial> adverbials_adjectives = new List<Tutorial>();
                adverbials_adjectives = FilterLesson(tutorials, kind_adverbial_adjective);

                List<byte[]> kind_conjunction = new List<byte[]>();
                byte[] sha_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                kind_conjunction.Add(sha_conjunction);
                List<Tutorial> conjunctions = new List<Tutorial>();
                conjunctions = FilterLesson(tutorials, kind_conjunction);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_noun = this._wordEmbeddingService.Encode(order, this._order);

                if ((adnominals_adjuncts.Count == 0) || (adverbials_adjectives.Count == 0) || (conjunctions.Count == 0)) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial conjunction in conjunctions)
                    {
                        foreach (Tutorial adverbial_adjective in adverbials_adjectives)
                        {
                            foreach (Tutorial adnominal_adjunct in adnominals_adjuncts)
                            {
                                List<Instruction> words1 = new List<Instruction>();
                                words.ForEach(item => words1.Add(item));
                                foreach (Instruction item in adnominal_adjunct.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adnominal_adjunct.team, order_noun);
                                    words1.Add(word);
                                };
                                foreach (Instruction item in adverbial_adjective.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adverbial_adjective.team, order_noun);
                                    words1.Add(word);
                                };
                                foreach (Instruction item in conjunction.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, conjunction.team, order_noun);
                                    words1.Add(word);
                                };
                                if (!VerifyVerbAdjectiveConjunctionNoun(words1, word_2_vec)) continue;
                                Tutorial seminar = new Tutorial();
                                seminar.lecture = words1;
                                seminars.Add(seminar);
                            }
                        }
                    }
                }
                return seminars;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Tutorial> MountPrepositionAdjectiveConjunctionNoun(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb adjective conjunction noun \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adnominal_adjunct = new List<byte[]>();
                byte[] sha_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                kind_adnominal_adjunct.Add(sha_adnominal_adjunct);
                List<Tutorial> adnominals_adjuncts = new List<Tutorial>();
                adnominals_adjuncts = FilterLesson(tutorials, kind_adnominal_adjunct);

                List<byte[]> kind_adverbial_adjective = new List<byte[]>();
                byte[] sha_adverbial_adjective = this._wordEmbeddingService.Encode(this._adverbial_adjective, this._morphology);
                kind_adverbial_adjective.Add(sha_adverbial_adjective);
                List<Tutorial> adverbials_adjectives = new List<Tutorial>();
                adverbials_adjectives = FilterLesson(tutorials, kind_adverbial_adjective);

                List<byte[]> kind_conjunction = new List<byte[]>();
                byte[] sha_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                kind_conjunction.Add(sha_conjunction);
                List<Tutorial> conjunctions = new List<Tutorial>();
                conjunctions = FilterLesson(tutorials, kind_conjunction);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_compound = this._wordEmbeddingService.Encode(order, this._order);

                if ((adnominals_adjuncts.Count == 0) || (adverbials_adjectives.Count == 0) || (conjunctions.Count == 0)) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial conjunction in conjunctions)
                    {
                        foreach (Tutorial adverbial_adjective in adverbials_adjectives)
                        {
                            foreach (Tutorial adnominal_adjunct in adnominals_adjuncts)
                            {
                                List<Instruction> words1 = new List<Instruction>();
                                words.ForEach(item => words1.Add(item));
                                foreach (Instruction item in adnominal_adjunct.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adnominal_adjunct.team, order_compound);
                                    words1.Add(word);
                                }
                                ;
                                foreach (Instruction item in adverbial_adjective.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adverbial_adjective.team, order_compound);
                                    words1.Add(word);
                                }
                                ;
                                foreach (Instruction item in conjunction.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, conjunction.team, order_compound);
                                    words1.Add(word);
                                }
                                ;
                                if (!VerifyPrepositionAdjectiveConjunctionNoun(words1, word_2_vec, order_compound)) continue;
                                Tutorial seminar = new Tutorial();
                                seminar.lecture = words1;
                                seminars.Add(seminar);
                            }
                        }
                    }
                }
                return seminars;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Tutorial> MountVerbNounConjunctionNoun(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb noun conjunction noun \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adnominal_adjunct = new List<byte[]>();
                byte[] sha_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                kind_adnominal_adjunct.Add(sha_adnominal_adjunct);
                byte[] sha_possessive = this._wordEmbeddingService.Encode(this._possessive, this._morphology);
                kind_adnominal_adjunct.Add(sha_possessive);
                byte[] sha_pessoal = this._wordEmbeddingService.Encode(this._personal, this._morphology);
                kind_adnominal_adjunct.Add(sha_pessoal);
                List<Tutorial> adnominals_firsts = new List<Tutorial>();
                adnominals_firsts = FilterLesson(tutorials, kind_adnominal_adjunct);

                List<Tutorial> adnominals_lasts = new List<Tutorial>();
                adnominals_lasts = FilterLesson(tutorials, kind_adnominal_adjunct);

                List<byte[]> kind_conjunction = new List<byte[]>();
                byte[] sha_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                kind_conjunction.Add(sha_conjunction);
                List<Tutorial> conjunctions = new List<Tutorial>();
                conjunctions = FilterLesson(tutorials, kind_conjunction);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_compound = this._wordEmbeddingService.Encode(order, this._order);

                if ((adnominals_firsts.Count == 0) || (adnominals_lasts.Count == 0) || (conjunctions.Count == 0)) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial conjunction in conjunctions)
                    {
                        foreach (Tutorial adnominal_first in adnominals_firsts)
                        {
                            foreach (Tutorial adnominal_last in adnominals_lasts)
                            {
                                List<Instruction> words1 = new List<Instruction>();
                                List<Instruction> firsts = new List<Instruction>();
                                List<Instruction> lasts = new List<Instruction>();
                                words.ForEach(item => words1.Add(item));
                                foreach (Instruction item in adnominal_first.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adnominal_first.team, order_compound);
                                    words1.Add(word);
                                    firsts.Add(word);
                                }
                                foreach (Instruction item in adnominal_last.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adnominal_last.team, order_compound);
                                    words1.Add(word);
                                    lasts.Add(word);
                                }
                                foreach (Instruction item in conjunction.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, conjunction.team, order_compound);
                                    words1.Add(word);
                                }
                                if (!VerifyVerbNounConjunctionNoun(words1, firsts, lasts, word_2_vec)) continue;
                                Tutorial seminar = new Tutorial();
                                seminar.lecture = words1;
                                seminars.Add(seminar);
                            }
                        }
                    }
                }
                return seminars;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Tutorial> MountPrepositionNounConjunctionNoun(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb noun conjunction noun \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<byte[]> kind_adnominal_adjunct = new List<byte[]>();
                byte[] sha_adnominal_adjunct = this._wordEmbeddingService.Encode(this._adnominal_adjunct, this._morphology);
                kind_adnominal_adjunct.Add(sha_adnominal_adjunct);
                byte[] sha_possessive = this._wordEmbeddingService.Encode(this._possessive, this._morphology);
                kind_adnominal_adjunct.Add(sha_possessive);
                byte[] sha_pessoal = this._wordEmbeddingService.Encode(this._personal, this._morphology);
                kind_adnominal_adjunct.Add(sha_pessoal);
                List<Tutorial> adnominals_firsts = new List<Tutorial>();
                adnominals_firsts = FilterLesson(tutorials, kind_adnominal_adjunct);

                List<Tutorial> adnominals_lasts = new List<Tutorial>();
                adnominals_lasts = FilterLesson(tutorials, kind_adnominal_adjunct);

                List<byte[]> kind_conjunction = new List<byte[]>();
                byte[] sha_conjunction = this._wordEmbeddingService.Encode(this._conjunction, this._morphology);
                kind_conjunction.Add(sha_conjunction);
                List<Tutorial> conjunctions = new List<Tutorial>();
                conjunctions = FilterLesson(tutorials, kind_conjunction);

                byte[] predicate = this._wordEmbeddingService.Encode(this._predicate, this._syntax);
                byte[] order_compound = this._wordEmbeddingService.Encode(order, this._order);

                if ((adnominals_firsts.Count == 0) || (adnominals_lasts.Count == 0) || (conjunctions.Count == 0)) return seminars;
                foreach (Tutorial source in sources)
                {
                    List<Instruction> words = source.lecture;
                    foreach (Tutorial conjunction in conjunctions)
                    {
                        foreach (Tutorial adnominal_first in adnominals_firsts)
                        {
                            foreach (Tutorial adnominal_last in adnominals_lasts)
                            {
                                List<Instruction> words1 = new List<Instruction>();
                                List<Instruction> firsts = new List<Instruction>();
                                List<Instruction> lasts = new List<Instruction>();
                                words.ForEach(item => words1.Add(item));
                                foreach (Instruction item in adnominal_first.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adnominal_first.team, order_compound);
                                    words1.Add(word);
                                    firsts.Add(word);
                                }
                                foreach (Instruction item in adnominal_last.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, adnominal_last.team, order_compound);
                                    words1.Add(word);
                                    lasts.Add(word);
                                }
                                foreach (Instruction item in conjunction.lecture)
                                {
                                    Instruction word = new Instruction();
                                    word = Lecture(item.term, item.kind, predicate, conjunction.team, order_compound);
                                    words1.Add(word);
                                }
                                ;
                                if (!VerifyPrepositionNounConjunctionNoun(words1, firsts, lasts, word_2_vec, order_compound)) continue;
                                Tutorial seminar = new Tutorial();
                                seminar.lecture = words1;
                                seminars.Add(seminar);
                            }
                        }
                    }
                }
                return seminars;
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

        public List<Tutorial> SampleSubjectVerb(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation sample subject verb \"Syntax\" view model failed!");

                List<Tutorial> seminars = new List<Tutorial>();
                seminars = MountNounVerb(tutorials, word_2_vec);
                return seminars;
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

        public List<Tutorial> CompoundSubjectVerb(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation compound subject verb \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();
                seminars = MountCompoundVerb(tutorials, word_2_vec);
                return seminars;
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

        public List<Tutorial> PredicateDirectObject(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate direct object \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();
                seminars = MountVerbNoun(tutorials, word_2_vec, sources, order_init);
                seminars = Union(seminars, MountVerbNumeralConjunctionNoun(tutorials, word_2_vec, sources, order_init));
                seminars = Union(seminars, MountVerbAdjectiveConjunctionNoun(tutorials, word_2_vec, sources, order_init));
                seminars = Union(seminars, MountVerbNounConjunctionNoun(tutorials, word_2_vec, sources, order_init));
                
                return seminars;
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

        public List<Tutorial> PredicatePredicative(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate predicative \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();
                seminars = MountVerbAdjective(tutorials, word_2_vec, sources, order_init);
                return seminars;
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

        public List<Tutorial> PredicateIndirectObject(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_init)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate indirect object \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();
                seminars = MountVerbIndirectObject(tutorials, word_2_vec, sources, order_init);
                seminars = Union(seminars, MountVerbNoun(tutorials, word_2_vec, seminars, order_init));
                seminars = Union(seminars, MountVerbNumeralConjunctionNoun(tutorials, word_2_vec, seminars, order_init));
                seminars = Union(seminars, MountVerbAdjectiveConjunctionNoun(tutorials, word_2_vec, seminars, order_init));
                seminars = Union(seminars, MountVerbNounConjunctionNoun(tutorials, word_2_vec, seminars, order_init));
                return seminars;
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
                prepositions = MountDirectObjectPreposition(sentences, matters, sources, order_init);
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

        public List<Tutorial> PredicateDirectObjectIndirectObject(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_direct_object, int order_indirect_object)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate direct object indirect object \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                List<Tutorial> prepositions = new List<Tutorial>();
                prepositions = MountDirectObjectPreposition(tutorials, word_2_vec, sources, order_direct_object, order_indirect_object);

                List<Tutorial> adnominals_adjunts = new List<Tutorial>();
                adnominals_adjunts = MountDirectObjectIndirectObject(tutorials, word_2_vec, prepositions, order_indirect_object);

                List<Tutorial> numerals_nouns = new List<Tutorial>();
                numerals_nouns = MountPrepositionNumeralConjunctionNoun(tutorials, word_2_vec, prepositions, order_indirect_object);

                List<Tutorial> adjectives_nouns = new List<Tutorial>();
                adjectives_nouns = MountPrepositionAdjectiveConjunctionNoun(tutorials, word_2_vec, prepositions, order_indirect_object);

                List<Tutorial> nouns_nouns = new List<Tutorial>();
                nouns_nouns = MountPrepositionNounConjunctionNoun(tutorials, word_2_vec, prepositions, order_indirect_object);

                seminars = Union(adnominals_adjunts, numerals_nouns);
                seminars = Union(seminars, adjectives_nouns);
                seminars = Union(seminars, nouns_nouns);

                return seminars;
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

        public List<Tutorial> PredicateDirectObjectPredicative(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_sample, int order_predicative)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate indirect object predicative \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();
                
                seminars = MountObjectPredicative(tutorials, word_2_vec, sources, order_sample, order_predicative);
                return seminars;
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

        public List<Tutorial> PredicateIndirectObjectPredicative(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_sample, int order_predicative)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate indirect object predicative \"Syntax\" service failed!");

                List<Tutorial> seminars = new List<Tutorial>();

                seminars = MountObjectPredicative(tutorials, word_2_vec, sources, order_sample, order_predicative);
                return seminars;
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
                conjunctions_nouns = MountDirectObjectPreposition(sentences, matters, conjunctions_nouns, order_last);
                conjunctions_nouns = MountConjunction(sentences, matters, conjunctions_nouns, order_init);

                List<string> kind_pronoun = new List<string>();
                kind_pronoun.Add(this._pronoun);
                List<Lesson> surrogates = new List<Lesson>();
                surrogates = FilterLesson(sources, kind_pronoun);

                List<Lesson> pronouns = new List<Lesson>();
                pronouns = MountVerbPronoun(sentences, matters, surrogates, order_last);
                List<Lesson> conjunctions_pronouns = new List<Lesson>();
                conjunctions_pronouns = MountDirectObjectPreposition(sentences, matters, conjunctions_pronouns, order_last);
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
