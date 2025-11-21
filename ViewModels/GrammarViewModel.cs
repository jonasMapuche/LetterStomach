using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels
{
    public class GrammarViewModel : IGrammarViewModel
    {
        #region ERROR
        private bool _error_test = false;

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
        private ILetterViewModel _lettersViewModel;
        private IAdverbViewModel _adverbsViewModel;
        private IArticleViewModel _articlesViewModel;
        private IPronounViewModel _pronounsViewModel;
        private INumeralViewModel _numeralsViewModel;
        private IPrepositionViewModel _prepositionsViewModel;
        private IVerbViewModel _verbsViewModel;
        private ISentenceViewModel _sentencesViewModel;
        private IConjunctionViewModel _conjunctionViewModel;

        private ISyntaxViewModel _syntaxViewMode = new SyntaxViewModel();
        private IMorphologyViewModel _morphologyViewModel = new MorphologyViewModel();
        private ISyntaxViewModel syntaxViewModel = new SyntaxViewModel();
        
        private IWordEmbeddingService _wordEmbeddingService = new WordEmbeddingService();

        private List<Lesson> _lesson_english;
        private List<Lesson> _lesson_deutsch;
        private List<Lesson> _lesson_italiano;
        private List<Lesson> _lesson_francais;
        private List<Lesson> _lesson_espanol;

        private List<Circunstancia> _adverb_english = new List<Circunstancia>();
        private List<Circunstancia> _adverb_deutsch = new List<Circunstancia>();
        private List<Circunstancia> _adverb_italiano = new List<Circunstancia>();
        private List<Circunstancia> _adverb_francais = new List<Circunstancia>();
        private List<Circunstancia> _adverb_espanol = new List<Circunstancia>();

        private List<Preceito> _article_english = new List<Preceito>();
        private List<Preceito> _article_deutsch = new List<Preceito>();
        private List<Preceito> _article_italiano = new List<Preceito>();
        private List<Preceito> _article_francais = new List<Preceito>();
        private List<Preceito> _article_espanol = new List<Preceito>();

        private List<Estoutro> _pronoun_english = new List<Estoutro>();
        private List<Estoutro> _pronoun_deutsch = new List<Estoutro>();
        private List<Estoutro> _pronoun_italiano = new List<Estoutro>();
        private List<Estoutro> _pronoun_francais = new List<Estoutro>();
        private List<Estoutro> _pronoun_espanol = new List<Estoutro>();

        private List<Algarismo> _numeral_english = new List<Algarismo>();
        private List<Algarismo> _numeral_deutsch = new List<Algarismo>();
        private List<Algarismo> _numeral_italiano = new List<Algarismo>();
        private List<Algarismo> _numeral_francais = new List<Algarismo>();
        private List<Algarismo> _numeral_espanol = new List<Algarismo>();

        private List<Juncao> _preposition_english = new List<Juncao>();
        private List<Juncao> _preposition_deutsch = new List<Juncao>();
        private List<Juncao> _preposition_italiano = new List<Juncao>();
        private List<Juncao> _preposition_francais = new List<Juncao>();
        private List<Juncao> _preposition_espanol = new List<Juncao>();

        private List<Elocucao> _verb_english = new List<Elocucao>();
        private List<Elocucao> _verb_deutsch = new List<Elocucao>();
        private List<Elocucao> _verb_italiano = new List<Elocucao>();
        private List<Elocucao> _verb_francais = new List<Elocucao>();
        private List<Elocucao> _verb_espanol = new List<Elocucao>();

        private List<Sentenca> _sentence_english = new List<Sentenca>();
        private List<Sentenca> _sentence_deutsch = new List<Sentenca>();
        private List<Sentenca> _sentence_italiano = new List<Sentenca>();
        private List<Sentenca> _sentence_francais = new List<Sentenca>();
        private List<Sentenca> _sentence_espanol = new List<Sentenca>();

        private List<Ligacao> _conjunction_english = new List<Ligacao>();
        private List<Ligacao> _conjunction_deutsch = new List<Ligacao>();
        private List<Ligacao> _conjunction_italiano = new List<Ligacao>();
        private List<Ligacao> _conjunction_francais = new List<Ligacao>();
        private List<Ligacao> _conjunction_espanol = new List<Ligacao>();

        private Language ENGLISH = SettingService.Instance.English;
        private Language DEUTSCH = SettingService.Instance.Deutsch;
        private Language ITALIANO = SettingService.Instance.Italino;
        private Language FRANCAIS = SettingService.Instance.Francais;
        private Language ESPANOL = SettingService.Instance.Espanol;

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
        private string VAR_CONJUNCTION = SettingService.Instance.Conjunction;
        private string VAR_NUMERAL_NOUN = SettingService.Instance.Numeral_Noun;
        private string VAR_ADJECTIVE_ADVERB = SettingService.Instance.Adjective_Adverb;

        private int VAR_ORDER_3 = 3;
        private int VAR_ORDER_4 = 4;
        private int VAR_ORDER_5 = 5;
        private int VAR_ORDER_6 = 6;
        #endregion

        #region INIT
        public void SetGrammar()
        {
            try
            {
                this._adverb_english = GetAdverb(ENGLISH.Lowercase).Distinct().ToList();
                this._adverb_deutsch = GetAdverb(DEUTSCH.Lowercase).Distinct().ToList();
                this._adverb_italiano = GetAdverb(ITALIANO.Lowercase).Distinct().ToList();
                this._adverb_francais = GetAdverb(FRANCAIS.Lowercase).Distinct().ToList();
                this._adverb_espanol = GetAdverb(ESPANOL.Lowercase).Distinct().ToList();

                this._pronoun_english = GetPronoun(ENGLISH.Lowercase).Distinct().ToList();
                this._pronoun_deutsch = GetPronoun(DEUTSCH.Lowercase).Distinct().ToList();
                this._pronoun_italiano = GetPronoun(ITALIANO.Lowercase).Distinct().ToList();
                this._pronoun_francais = GetPronoun(FRANCAIS.Lowercase).Distinct().ToList();
                this._pronoun_espanol = GetPronoun(ESPANOL.Lowercase).Distinct().ToList();

                this._article_english = GetArticle(ENGLISH.Lowercase).Distinct().ToList();
                this._article_deutsch = GetArticle(DEUTSCH.Lowercase).Distinct().ToList();
                this._article_italiano = GetArticle(ITALIANO.Lowercase).Distinct().ToList();
                this._article_francais = GetArticle(FRANCAIS.Lowercase).Distinct().ToList();
                this._article_espanol = GetArticle(ESPANOL.Lowercase).Distinct().ToList();

                this._numeral_english = GetNumeral(ENGLISH.Lowercase).Distinct().ToList();
                this._numeral_deutsch = GetNumeral(DEUTSCH.Lowercase).Distinct().ToList();
                this._numeral_italiano = GetNumeral(ITALIANO.Lowercase).Distinct().ToList();
                this._numeral_francais = GetNumeral(FRANCAIS.Lowercase).Distinct().ToList();
                this._numeral_espanol = GetNumeral(ESPANOL.Lowercase).Distinct().ToList();

                this._preposition_english = GetPreposition(ENGLISH.Lowercase).Distinct().ToList();
                this._preposition_deutsch = GetPreposition(DEUTSCH.Lowercase).Distinct().ToList();
                this._preposition_italiano = GetPreposition(ITALIANO.Lowercase).Distinct().ToList();
                this._preposition_francais = GetPreposition(FRANCAIS.Lowercase).Distinct().ToList();
                this._preposition_espanol = GetPreposition(ESPANOL.Lowercase).Distinct().ToList();

                this._conjunction_english = GetConjunction(ENGLISH.Lowercase).Distinct().ToList();
                this._conjunction_deutsch = GetConjunction(DEUTSCH.Lowercase).Distinct().ToList();
                this._conjunction_italiano = GetConjunction(ITALIANO.Lowercase).Distinct().ToList();
                this._conjunction_francais = GetConjunction(FRANCAIS.Lowercase).Distinct().ToList();
                this._conjunction_espanol = GetConjunction(ESPANOL.Lowercase).Distinct().ToList();

                this._verb_english = GetVerb(ENGLISH.Lowercase).Distinct().ToList();
                this._verb_deutsch = GetVerb(DEUTSCH.Lowercase).Distinct().ToList();
                this._verb_italiano = GetVerb(ITALIANO.Lowercase).Distinct().ToList();
                this._verb_francais = GetVerb(FRANCAIS.Lowercase).Distinct().ToList();
                this._verb_espanol = GetVerb(ESPANOL.Lowercase).Distinct().ToList();

                this._sentence_english = GetSentence(ENGLISH.Lowercase).Distinct().ToList();
                this._sentence_deutsch = GetSentence(DEUTSCH.Lowercase).Distinct().ToList();
                this._sentence_italiano = GetSentence(ITALIANO.Lowercase).Distinct().ToList();
                this._sentence_francais = GetSentence(FRANCAIS.Lowercase).Distinct().ToList();
                this._sentence_espanol = GetSentence(ESPANOL.Lowercase).Distinct().ToList();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void MongoDB()
        {
            try
            {
                this._adverbsViewModel = new MongoDBs.AdverbViewModel();
                this._articlesViewModel = new MongoDBs.ArticleViewModel();
                this._pronounsViewModel = new MongoDBs.PronounViewModel();

                this._numeralsViewModel = new MongoDBs.NumeralViewModel();
                this._prepositionsViewModel = new MongoDBs.PrepositionViewModel();
                this._verbsViewModel = new MongoDBs.VerbViewModel();
                this._lettersViewModel = new MongoDBs.LetterViewModel();
                this._sentencesViewModel = new MongoDBs.SentenceViewModel();
                this._conjunctionViewModel = new MongoDBs.ConjunctionViewModel();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void SQLite()
        {
            try
            {
                this._adverbsViewModel = new SQLites.AdverbViewModel();
                this._articlesViewModel = new SQLites.ArticleViewModel();
                this._pronounsViewModel = new SQLites.PronounViewModel();
                this._numeralsViewModel = new SQLites.NumeralViewModel();
                this._prepositionsViewModel = new SQLites.PrepositionViewModel();
                this._verbsViewModel = new SQLites.VerbViewModel();
                this._lettersViewModel = new SQLites.LetterViewModel();
                this._sentencesViewModel = new SQLites.SentenceViewModel();
                this._conjunctionViewModel = new SQLites.ConjunctionViewModel();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion

        #region GET
        private List<Circunstancia> GetAdverb(string language)
        {
            try
            {
                return this._adverbsViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Preceito> GetArticle(string language)
        {
            try
            {
                return this._articlesViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Estoutro> GetPronoun(string language)
        {
            try
            {
                return this._pronounsViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Algarismo> GetNumeral(string language)
        {
            try
            {
                return this._numeralsViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Juncao> GetPreposition(string language)
        {
            try
            {
                return this._prepositionsViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Elocucao> GetVerb(string language)
        {
            try
            {
                return this._verbsViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Sentenca> GetSentence(string language)
        {
            try
            {
                return this._sentencesViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Ligacao> GetConjunction(string language)
        {
            try
            {
                return this._conjunctionViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Materia> GetLetter(string language)
        {
            try
            {
                return this._lettersViewModel.GetLessonSimple(true, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region SET
        private void SetOration(string language, List<Lesson> lesson_word)
        {
            try
            {
                if (language == ENGLISH.Lowercase) _lesson_english = lesson_word;
                if (language == DEUTSCH.Lowercase) _lesson_deutsch = lesson_word;
                if (language == ITALIANO.Lowercase) _lesson_italiano = lesson_word;
                if (language == FRANCAIS.Lowercase) _lesson_francais = lesson_word;
                if (language == ESPANOL.Lowercase) _lesson_espanol = lesson_word;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region SELECT
        private List<Lesson> SelectOration(string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) return _lesson_english;
                if (language == DEUTSCH.Lowercase) return _lesson_deutsch;
                if (language == ITALIANO.Lowercase) return _lesson_italiano;
                if (language == FRANCAIS.Lowercase) return _lesson_francais;
                if (language == ESPANOL.Lowercase) return _lesson_espanol;
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Sentenca> SelectSentence(string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) return this._sentence_english;
                if (language == DEUTSCH.Lowercase) return this._sentence_deutsch;
                if (language == ITALIANO.Lowercase) return this._sentence_italiano;
                if (language == FRANCAIS.Lowercase) return this._sentence_francais;
                if (language == ESPANOL.Lowercase) return this._sentence_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Juncao> SelectPreposition(string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) return _preposition_english;
                if (language == DEUTSCH.Lowercase) return _preposition_deutsch;
                if (language == ITALIANO.Lowercase) return _preposition_italiano;
                if (language == FRANCAIS.Lowercase) return _preposition_francais;
                if (language == ESPANOL.Lowercase) return _preposition_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Preceito> SelectArticle(string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) return _article_english;
                if (language == DEUTSCH.Lowercase) return _article_deutsch;
                if (language == ITALIANO.Lowercase) return _article_italiano;
                if (language == FRANCAIS.Lowercase) return _article_francais;
                if (language == ESPANOL.Lowercase) return _article_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Algarismo> SelectNumeral(string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) return _numeral_english;
                if (language == DEUTSCH.Lowercase) return _numeral_deutsch;
                if (language == ITALIANO.Lowercase) return _numeral_italiano;
                if (language == FRANCAIS.Lowercase) return _numeral_francais;
                if (language == ESPANOL.Lowercase) return _numeral_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Circunstancia> SelectAdverb(string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) return _adverb_english;
                if (language == DEUTSCH.Lowercase) return _adverb_deutsch;
                if (language == ITALIANO.Lowercase) return _adverb_italiano;
                if (language == FRANCAIS.Lowercase) return _adverb_francais;
                if (language == ESPANOL.Lowercase) return _adverb_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Ligacao> SelectConjunction(string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) return _conjunction_english;
                if (language == DEUTSCH.Lowercase) return _conjunction_deutsch;
                if (language == ITALIANO.Lowercase) return _conjunction_italiano;
                if (language == FRANCAIS.Lowercase) return _conjunction_francais;
                if (language == ESPANOL.Lowercase) return _conjunction_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Elocucao> SelectVerb(string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) return _verb_english;
                if (language == DEUTSCH.Lowercase) return _verb_deutsch;
                if (language == ITALIANO.Lowercase) return _verb_italiano;
                if (language == FRANCAIS.Lowercase) return _verb_francais;
                if (language == ESPANOL.Lowercase) return _verb_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Estoutro> SelectPronoun(string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) return _pronoun_english;
                if (language == DEUTSCH.Lowercase) return _pronoun_deutsch;
                if (language == ITALIANO.Lowercase) return _pronoun_italiano;
                if (language == FRANCAIS.Lowercase) return _pronoun_francais;
                if (language == ESPANOL.Lowercase) return _pronoun_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region PERIOD
        private List<Lesson> OrderLesson(List<Lesson> firsts)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                int order = 0;
                firsts.ForEach(value =>
                {
                    order++;
                    Lesson item = new Lesson();
                    item.order = order;
                    item.team = value.team;
                    item.lecture = value.lecture;
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

        private List<Lesson> OrderLesson(List<Lesson> firsts, List<Lesson> lasts)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                int order = 0;
                firsts.ForEach(value =>
                {
                    order = value.order;
                    lessons.Add(value);
                });
                lasts.ForEach(value =>
                {
                    order++;
                    Lesson item = new Lesson();
                    item.order = order;
                    item.lecture = value.lecture;
                    item.team = value.team;
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

        private List<Lesson> MountOrationSample(List<Sentenca> sentences, List<Lesson> terms)
        {
            try
            {
                List<Lesson> words = new List<Lesson>();
                List<Lesson> sampleSubjectVerb = new List<Lesson>();
                List<Lesson> predicatePredicative = new List<Lesson>();
                List<Lesson> predicateDirectObject = new List<Lesson>();
                List<Lesson> predicateIndirectObject = new List<Lesson>();
                List<Lesson> predicateDirectObjectIndirectObject = new List<Lesson>();
                List<Lesson> predicateDirectObjectPredicative = new List<Lesson>();
                List<Lesson> predicateIndirectObjectPredicative = new List<Lesson>();
                List<Lesson> predicatePredicativeIndirectObject = new List<Lesson>();

                int order_sample = VAR_ORDER_3;
                int order_predicate = VAR_ORDER_4;
                sampleSubjectVerb = this._syntaxViewMode.SampleSubjectVerb(sentences, terms);
                words = sampleSubjectVerb;
                predicatePredicative = this._syntaxViewMode.PredicatePredicative(sentences, terms, sampleSubjectVerb, order_sample);
                words = Union(words, predicatePredicative);
                predicateDirectObject = this._syntaxViewMode.PredicateDirectObject(sentences, terms, sampleSubjectVerb, order_sample);
                words = Union(words, predicateDirectObject);
                predicateIndirectObject = this._syntaxViewMode.PredicateIndirectObject(sentences, terms, predicateDirectObject, order_sample);
                words = Union(words, predicateIndirectObject);
                predicateDirectObjectIndirectObject = this._syntaxViewMode.PredicateDirectObjectIndirectObject(sentences, terms, predicateDirectObject, order_predicate);
                words = Union(words, predicateDirectObjectIndirectObject);
                predicateDirectObjectPredicative = this._syntaxViewMode.PredicateDirectObjectPredicativo(sentences, terms, predicateDirectObject, order_predicate);
                words = Union(words, predicateDirectObjectPredicative);
                predicateIndirectObjectPredicative = this._syntaxViewMode.PredicateIndirectObjectPredicativo(sentences, terms, predicateIndirectObject, order_predicate);
                words = Union(words, predicateIndirectObjectPredicative);
                predicatePredicativeIndirectObject = this._syntaxViewMode.PredicatePredicativoIndirectObject(sentences, terms, predicatePredicative, order_predicate);
                words = Union(words, predicatePredicativeIndirectObject);
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> MountOrationCompound(List<Sentenca> sentences, List<Lesson> terms)
        {
            try
            {
                List<Lesson> words = new List<Lesson>();
                List<Lesson> compoundSubjectVerb = new List<Lesson>();
                List<Lesson> predicatePredicative = new List<Lesson>();
                List<Lesson> predicateDirectObject = new List<Lesson>();
                List<Lesson> predicateIndirectObject = new List<Lesson>();
                List<Lesson> predicateDirectObjectIndirectObject = new List<Lesson>();
                List<Lesson> predicateDirectObjectPredicative = new List<Lesson>();
                List<Lesson> predicateIndirectObjectPredicative = new List<Lesson>();
                List<Lesson> predicatePredicativeIndirectObject = new List<Lesson>();

                int order_sample = VAR_ORDER_5;
                int order_predicate = VAR_ORDER_6;
                compoundSubjectVerb = this._syntaxViewMode.CompoundSubjectVerb(sentences, terms);
                words = compoundSubjectVerb;
                predicatePredicative = this._syntaxViewMode.PredicatePredicative(sentences, terms, compoundSubjectVerb, order_sample);
                words = Union(words, predicatePredicative);
                predicateDirectObject = this._syntaxViewMode.PredicateDirectObject(sentences, terms, compoundSubjectVerb, order_sample);
                words = Union(words, predicateDirectObject);
                predicateIndirectObject = this._syntaxViewMode.PredicateIndirectObject(sentences, terms, compoundSubjectVerb, order_sample);
                words = Union(words, predicateIndirectObject);
                predicateDirectObjectIndirectObject = this._syntaxViewMode.PredicateDirectObjectIndirectObject(sentences, terms, predicateDirectObject, order_predicate);
                words = Union(words, predicateDirectObjectIndirectObject);
                predicateDirectObjectPredicative = this._syntaxViewMode.PredicateDirectObjectPredicativo(sentences, terms, predicateDirectObject, order_predicate);
                words = Union(words, predicateDirectObjectPredicative);
                predicateIndirectObjectPredicative = this._syntaxViewMode.PredicateIndirectObjectPredicativo(sentences, terms, predicateIndirectObject, order_predicate);
                words = Union(words, predicateIndirectObjectPredicative);
                predicatePredicativeIndirectObject = this._syntaxViewMode.PredicatePredicativoIndirectObject(sentences, terms, predicatePredicative, order_predicate);
                words = Union(words, predicatePredicativeIndirectObject);
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region ORATION
        private Word? SubjectBefore(string language, List<Word> words)
        {
            try
            {
                string team = words.First().team;
                Word? noun = words.Find(index => index.kind == VAR_NOUN);
                Word? pronoun = words.Find(index => index.kind == VAR_PRONOUN);
                Word? conjunction = words.Find(index => index.kind == VAR_CONJUNCTION);
                if (team == VAR_CONJUNCTION)
                {
                    return conjunction;
                }
                if (team == VAR_PERSONAL)
                {
                    return pronoun;
                }
                return noun;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private Word? PredicateBefore(string language, List<Word> words)
        {
            try
            {
                string team = words.Find(index => index.team != VAR_PREPOSITION).team;
                Word? noun = words.Find(index => index.kind == VAR_NOUN);
                Word? pronoun = words.Find(index => index.kind == VAR_PRONOUN);
                Word? conjunction = words.Find(index => index.kind == VAR_CONJUNCTION);
                Word? adjective = words.Find(index => index.kind == VAR_ADJECTIVE);
                Word? verb = words.Find(index => index.kind == VAR_VERB);
                Word? adverb = words.Find(index => index.kind == VAR_ADVERB);
                Word? adverb_adverb = words.Find(index => index.kind == VAR_ADVERB_ADVERB);
                if (team == VAR_CONJUNCTION)
                {
                    return conjunction;
                }
                if ((team == VAR_POSSESSIVE) || (team == VAR_DEMONSTRATIVE))
                {
                    return pronoun;
                }
                if (team == VAR_ADJECTIVE_ADVERB)
                {
                    if (adverb_adverb !=  null) 
                        return adverb_adverb;
                    else
                    {
                        if (adverb != null)
                            return adverb;
                        else
                            return adjective;
                    }
                }
                if (team == VAR_VERB)
                {
                    if (adverb_adverb != null)
                        return adverb_adverb;
                    else
                    {
                        if (adverb != null)
                            return adverb;
                        else
                            return verb;
                    }
                }
                return noun;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private Word? SubjectCurrent(string language, List<Word> words)
        {
            try
            {
                string team = words.First().team;
                Word? noun = words.Find(index => index.kind == VAR_NOUN);
                Word? conjunction = words.Find(index => index.kind == VAR_CONJUNCTION);
                Word? article = words.Find(index => index.kind == VAR_ARTICLE);
                Word? numeral = words.Find(index => index.kind == VAR_NUMERAL);
                Word? pronoun = words.Find(index => index.kind == VAR_PRONOUN);
                Word? adjective = words.Find(index => index.kind == VAR_ADJECTIVE_NOUN);
                if (team == VAR_CONJUNCTION)
                {
                    return conjunction;
                }
                if (team == VAR_ADJECTIVE_NOUN)
                {
                    return adjective;
                }
                if (team == VAR_PERSONAL) 
                {
                    return pronoun;
                }
                if ((team == VAR_NOUN) || (team == VAR_NUMERAL_NOUN))
                {
                    if (article != null) return article;
                    if (numeral != null) return numeral;
                    if (pronoun != null) return pronoun;
                }
                return noun;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private Word? PredicateCurrent(string language, List<Word> words)
        {
            try
            {
                Word? team_preposition = words.Find(index => index.team == VAR_PREPOSITION);
                Word? team_different = words.Find(index => index.team != VAR_PREPOSITION);
                string? team = string.Empty;
                if ((team_preposition != null) && (team_different == null)) team = team_preposition.team;
                if ((team_preposition == null) && (team_different != null)) team = team_different.team;
                if ((team_preposition != null) && (team_different != null)) team = team_different.team;
                Word? noun = words.Find(index => index.kind == VAR_NOUN);
                Word? conjunction = words.Find(index => index.kind == VAR_CONJUNCTION);
                Word? article = words.Find(index => index.kind == VAR_ARTICLE);
                Word? numeral = words.Find(index => index.kind == VAR_NUMERAL);
                Word? pronoun = words.Find(index => index.kind == VAR_PRONOUN);
                Word? adjective_noun = words.Find(index => index.kind == VAR_ADJECTIVE_NOUN);
                Word? adjective = words.Find(index => index.kind == VAR_ADJECTIVE);
                Word? verb = words.Find(index => index.kind == VAR_VERB);
                List<Word> preposition = words.FindAll(index => index.kind == VAR_PREPOSITION);
                if (team == VAR_CONJUNCTION)
                {
                    return conjunction;
                }
                if (team == VAR_PREPOSITION)
                {
                    Word? term = preposition.Find(index => index.team == VAR_PREPOSITION);
                    return term;
                }
                if ((team == VAR_DEMONSTRATIVE) || (team == VAR_POSSESSIVE))
                {
                    return pronoun;
                }
                if ((team == VAR_ADJECTIVE_NOUN) || (team == VAR_ADJECTIVE_ADVERB))
                {
                    return adjective;
                }
                if ((team == VAR_NOUN) || (team == VAR_NUMERAL_NOUN))
                {
                    if (article != null) return article;
                    if (numeral != null) return numeral;
                    if (pronoun != null) return pronoun;
                }
                if (team == VAR_VERB)
                {
                    return verb;
                }
                return noun;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> MountSubject(List<Sentenca> sentences, string language, List<Word> matters)
        {
            try
            {
                List<Word> lessons = new List<Word>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                List<Word> filter_matters = new List<Word>();
                filter_matters = matters.FindAll(index => index.sentense == VAR_SUBJECT).ToList();
                List<Word> words = new List<Word>();
                Word? word = new Word();
                List<Word> befores = new List<Word>();
                Word? before = new Word();
                bool last = false;
                int order = filter_matters.OrderBy(index => index.order).Last().order;
                for (int quantity = 1; quantity <= order; quantity++)
                {
                    last = false;
                    words = filter_matters.FindAll(index => index.order == quantity);
                    if (befores.Count == 0)
                    {
                        befores = words;
                        last = true;
                        continue;
                    }
                    word = new Word();
                    word = SubjectCurrent(language, words);
                    before = new Word();
                    before = SubjectBefore(language, befores);
                    bool similarity = false;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, before.term, word.term);
                    if (!similarity) break;
                    foreach (Word item in words)
                    {
                        lessons.Add(item);
                    }
                    befores = words;
                    last = true;
                }
                if (last)
                {
                    foreach (Word item in befores)
                    {
                        lessons.Add(item);
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

        private List<Word> MountPredicate(List<Sentenca> sentences, string language, List<Word> matters)
        {
            try
            {
                List<Word> lessons = new List<Word>();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                List<Word> filter_matters = new List<Word>();
                filter_matters = matters.FindAll(index => index.sentense == VAR_PREDICATE).ToList();
                List<Word> words = new List<Word>();
                Word? word = new Word();
                List<Word> befores = new List<Word>();
                Word? before = new Word();
                bool last = false;
                int first = filter_matters.OrderBy(index => index.order).First().order;
                int order = filter_matters.OrderBy(index => index.order).Last().order;
                for (int quantity = first; quantity <= order; quantity++)
                {
                    last = false;
                    words = filter_matters.FindAll(index => index.order == quantity);
                    if (befores.Count == 0)
                    {
                        befores = words;
                        last = true;
                        continue;
                    }
                    word = new Word();
                    word = PredicateCurrent(language, words);
                    before = new Word();
                    before = PredicateBefore(language, befores);
                    bool similarity = false;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, before.term, word.term);
                    if (!similarity) break;
                    foreach (Word item in befores)
                    {
                        lessons.Add(item);
                    }
                    befores = words;
                    last = true;
                }
                if (last)
                {
                    foreach (Word item in befores)
                    {
                        lessons.Add(item);
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

        private List<Word> MountOration(List<Sentenca> sentences, string language, List<Word> lessons)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = MountSubject(sentences, language, lessons);
                foreach (Word item in terms) 
                {
                    words.Add(item);
                }
                terms = MountPredicate(sentences, language, lessons);
                foreach (Word item in terms)
                {
                    words.Add(item);
                }
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private string? MountExpression(string language, List<Word> words)
        {
            try
            {
                Word? team_preposition = words.Find(index => index.team == VAR_PREPOSITION);
                Word? team_different = words.Find(index => index.team != VAR_PREPOSITION);
                string? team = string.Empty;
                if ((team_preposition != null) && (team_different == null)) team = team_preposition.team;
                if ((team_preposition == null) && (team_different != null)) team = team_different.team;
                if ((team_preposition != null) && (team_different != null)) team = team_different.team;
                Word? noun = words.Find(index => index.kind == VAR_NOUN);
                Word? conjunction = words.Find(index => index.kind == VAR_CONJUNCTION);
                Word? article = words.Find(index => index.kind == VAR_ARTICLE);
                Word? numeral = words.Find(index => index.kind == VAR_NUMERAL);
                Word? pronoun = words.Find(index => index.kind == VAR_PRONOUN);
                Word? adjective = words.Find(index => index.kind == VAR_ADJECTIVE);
                Word? adverb = words.Find(index => index.kind == VAR_ADVERB);
                Word? adverb_adverb = words.Find(index => index.kind == VAR_ADVERB_ADVERB);
                Word? verb = words.Find(index => index.kind == VAR_VERB);
                List<Word> preposition = words.FindAll(index => index.kind == VAR_PREPOSITION);
                string word = string.Empty;
                if (team == VAR_CONJUNCTION)
                {
                    word = conjunction.term;
                }
                if (preposition.Count == 1)
                {
                    if (team == VAR_PREPOSITION)
                    {
                        if (word != string.Empty) word += " "; 
                        word += preposition.First().term;
                    }
                }
                else
                {
                    if (preposition.Count > 1)
                    {
                        if (word != string.Empty) word += " ";
                        word += team_preposition.term;
                    }
                }
                if (team == VAR_ADJECTIVE_NOUN)
                {
                    List<Word> term = preposition.FindAll(index => index.team == team);
                    if (word != string.Empty) word += " ";
                    if (term.Count > 0)
                    {
                        if (adverb_adverb != null)
                            word += adjective.term + " " + adverb.term + " " + adverb_adverb.term + " " + term[0].term + " " + noun.term;
                        else
                        {
                            if (adverb != null) 
                                word += adjective.term + " " + adverb.term + " " + term[0].term + " " + noun.term;
                            else
                                word += adjective.term + " " + term[0].term + " " + noun.term;
                        }
                    }
                    else
                    {
                        if (adverb_adverb != null)
                            word += adjective.term + " " + adverb.term + " " + adverb_adverb.term + " " + noun.term;
                        else
                        {
                            if (adverb != null)
                                word += adjective.term + " " + adverb.term + " " + noun.term;
                            else
                                word += adjective.term + " " + noun.term;
                        }
                    }
                }
                if (team == VAR_ADJECTIVE_ADVERB)
                {
                    if (word != string.Empty) word += " ";
                    if (adverb_adverb != null)
                        word += adjective.term + " " + adverb.term + " " + adverb_adverb.term;
                    else
                    {
                        if (adverb != null)
                            word += adjective.term + " " + adverb.term;
                        else
                            word += adjective.term;
                    }
                }
                if ((team == VAR_DEMONSTRATIVE) || (team == VAR_POSSESSIVE) || (team == VAR_PERSONAL))
                {
                    if (word != string.Empty) word += " ";
                    word += pronoun.term;
                }
                if ((team == VAR_NOUN) || (team == VAR_NUMERAL_NOUN))
                {
                    if (word != string.Empty) word += " ";
                    if (article != null)
                        word += article.term + " " + noun.term;
                    if (numeral != null)
                        word += numeral.term + " " + noun.term;
                    if (pronoun != null)
                        word += pronoun.term + " " + noun.term;
                }
                if (team == VAR_NOUN)
                {
                    if (word != string.Empty) word += " ";
                    word += noun.term;
                }
                if (team == VAR_VERB)
                {
                    if (word != string.Empty) word += " ";
                    if (adverb_adverb != null)
                        word += adjective.term + " " + adverb.term + " " + adverb_adverb.term;
                    else
                    {
                        if (adverb != null)
                            word += adjective.term + " " + adverb.term;
                        else
                            word += verb.term;
                    }
                }
                return word;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public string MountOration(string language, List<Word> words)
        {
            try
            {
                string word = string.Empty;
                if (words.Count == 0) return word;
                int order = words.OrderBy(index => index.order).Last().order;
                for (int quantity = 1; quantity <= order; quantity++)
                {
                    List<Word> terms = new List<Word>();
                    terms = words.FindAll(index => index.order == quantity);
                    word += MountExpression(language, terms);
                    if (quantity < order)
                        word += " ";
                }
                return word;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return string.Empty;
            }
        }
        #endregion

        #region SYNTAX
        private List<Lesson> Union(List<Lesson> fists, List<Lesson> lasts)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                fists.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lesson.team = item.team;
                    lessons.Add(lesson);
                });
                lasts.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lesson.team = item.team;
                    lessons.Add(lesson);
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

        public List<Word> MountSyntax(string language, List<Word> terms, bool reverse)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Lesson> lessons = SelectOration(language).OrderBy(index => index.order).Distinct().ToList();
                List<Sentenca> sentence = SelectSentence(language).Distinct().ToList();
                if (reverse) lessons.Reverse();
                bool next = false;
                int count_foreach = 0;
                foreach (Lesson lesson in lessons)
                {
                    if (!next)
                    {
                        string term = MountOration(language, terms);
                        string word = MountOration(language, lesson.lecture);
                        if (term == word)
                            next = true;
                    }
                    else
                    {
                        words = MountOration(sentence, language, lesson.lecture);
                        if (words != null)
                        {
                            string word = MountOration(language, words);
                            string term = MountOration(language, terms);
                            if (term != word)
                                break;
                        }
                    }
                    count_foreach++;
                    if (lessons.Count == count_foreach)
                    {
                        words = terms;
                        break;
                    }
                }
                if (reverse) lessons.Reverse();
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Word> MountSyntax(string language, Materia lesson, List<Materia> book)
        {
            try
            {
                if (_error_test) throw new InvalidOperationException("Falha na operação!");
                List<Sentenca> sentence = SelectSentence(language).Distinct().ToList();

                List<string> list_noun = this._morphologyViewModel.GetLessonNoun(language, lesson, book);
                List<string> list_adjective = this._morphologyViewModel.GetLessonAdjective(lesson, book);
                List<string> list_model = this._morphologyViewModel.GetLessonVerb(lesson);

                List<Juncao> list_preposition = SelectPreposition(language);
                List<Preceito> list_article = SelectArticle(language);
                List<Algarismo> list_numeral = SelectNumeral(language);
                List<Circunstancia> list_adverb = SelectAdverb(language);
                List<Elocucao> list_verb = SelectVerb(language);
                List<Estoutro> list_pronoun = SelectPronoun(language);
                List<Ligacao> list_conjunction = SelectConjunction(language);

                List<Lesson> mount_noun = this._morphologyViewModel.GetNoun(sentence, list_noun, list_pronoun, list_article, list_numeral);
                List<Lesson> mount_verb = this._morphologyViewModel.GetVerb(sentence, list_model, list_verb, list_adverb);
                List<Lesson> mount_adjective_adverb = this._morphologyViewModel.GetAdjectiveAdverb(sentence, list_adjective, list_adverb);
                List<Lesson> mount_adjective = this._morphologyViewModel.GetAdjective(sentence, list_adjective);
                List<Lesson> mount_preposition = this._morphologyViewModel.GetPreposition(sentence, list_preposition);
                List<Lesson> mount_adjective_noun = this._morphologyViewModel.GetAdjectiveNoun(sentence, mount_adjective, mount_noun, mount_preposition);
                List<Lesson> mount_pronoun = this._morphologyViewModel.GetPronoun(sentence, list_pronoun);
                List<Lesson> mount_conjunction = this._morphologyViewModel.GetConjunction(sentence, list_conjunction);
                List<Lesson> mount_numeral_noun = this._morphologyViewModel.GetNumeralNoun(sentence, list_noun, list_article, list_numeral);
                List <Lesson> mount_numeral = this._morphologyViewModel.GetNumeral(sentence, list_numeral);
                List<Lesson> mount_article = this._morphologyViewModel.GetArticle(sentence, list_article);

                List<Lesson> matters = Union(mount_noun, mount_verb);
                matters = Union(matters, mount_adjective);
                matters = Union(matters, mount_adjective_noun);
                matters = Union(matters, mount_preposition);
                matters = Union(matters, mount_pronoun);
                matters = Union(matters, mount_numeral);
                matters = Union(matters, mount_article);
                matters = Union(matters, mount_conjunction);
                matters = Union(matters, mount_numeral_noun);

                List<Lesson> terms = new List<Lesson>();
                terms = OrderLesson(MountOrationSample(sentence, matters));
                //terms = OrderLesson(terms, MountOrationCompound(sentence, matters));

                SetOration(language, terms);
                List<Word> words = new List<Word>();

                foreach (Lesson phrase in terms)
                {
                    words = MountOration(sentence, language, phrase.lecture);
                    if (words.Count() > 0) break;
                }
                return words;
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
