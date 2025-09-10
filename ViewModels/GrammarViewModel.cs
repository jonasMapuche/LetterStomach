using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels
{
    public class GrammarViewModel : IGrammarViewModel
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
                this._adverb_english = GetAdverb(ENGLISH.Name).Distinct().ToList();
                this._adverb_deutsch = GetAdverb(DEUTSCH.Name).Distinct().ToList();
                this._adverb_italiano = GetAdverb(ITALIANO.Name).Distinct().ToList();
                this._adverb_francais = GetAdverb(FRANCAIS.Name).Distinct().ToList();
                this._adverb_espanol = GetAdverb(ESPANOL.Name).Distinct().ToList();

                this._pronoun_english = GetPronoun(ENGLISH.Name).Distinct().ToList();
                this._pronoun_deutsch = GetPronoun(DEUTSCH.Name).Distinct().ToList();
                this._pronoun_italiano = GetPronoun(ITALIANO.Name).Distinct().ToList();
                this._pronoun_francais = GetPronoun(FRANCAIS.Name).Distinct().ToList();
                this._pronoun_espanol = GetPronoun(ESPANOL.Name).Distinct().ToList();

                this._article_english = GetArticle(ENGLISH.Name).Distinct().ToList();
                this._article_deutsch = GetArticle(DEUTSCH.Name).Distinct().ToList();
                this._article_italiano = GetArticle(ITALIANO.Name).Distinct().ToList();
                this._article_francais = GetArticle(FRANCAIS.Name).Distinct().ToList();
                this._article_espanol = GetArticle(ESPANOL.Name).Distinct().ToList();

                this._numeral_english = GetNumeral(ENGLISH.Name).Distinct().ToList();
                this._numeral_deutsch = GetNumeral(DEUTSCH.Name).Distinct().ToList();
                this._numeral_italiano = GetNumeral(ITALIANO.Name).Distinct().ToList();
                this._numeral_francais = GetNumeral(FRANCAIS.Name).Distinct().ToList();
                this._numeral_espanol = GetNumeral(ESPANOL.Name).Distinct().ToList();

                this._preposition_english = GetPreposition(ENGLISH.Name).Distinct().ToList();
                this._preposition_deutsch = GetPreposition(DEUTSCH.Name).Distinct().ToList();
                this._preposition_italiano = GetPreposition(ITALIANO.Name).Distinct().ToList();
                this._preposition_francais = GetPreposition(FRANCAIS.Name).Distinct().ToList();
                this._preposition_espanol = GetPreposition(ESPANOL.Name).Distinct().ToList();

                this._conjunction_english = GetConjunction(ENGLISH.Name).Distinct().ToList();
                this._conjunction_deutsch = GetConjunction(DEUTSCH.Name).Distinct().ToList();
                this._conjunction_italiano = GetConjunction(ITALIANO.Name).Distinct().ToList();
                this._conjunction_francais = GetConjunction(FRANCAIS.Name).Distinct().ToList();
                this._conjunction_espanol = GetConjunction(ESPANOL.Name).Distinct().ToList();

                this._verb_english = GetVerb(ENGLISH.Name).Distinct().ToList();
                this._verb_deutsch = GetVerb(DEUTSCH.Name).Distinct().ToList();
                this._verb_italiano = GetVerb(ITALIANO.Name).Distinct().ToList();
                this._verb_francais = GetVerb(FRANCAIS.Name).Distinct().ToList();
                this._verb_espanol = GetVerb(ESPANOL.Name).Distinct().ToList();

                this._sentence_english = GetSentence(ENGLISH.Name).Distinct().ToList();
                this._sentence_deutsch = GetSentence(DEUTSCH.Name).Distinct().ToList();
                this._sentence_italiano = GetSentence(ITALIANO.Name).Distinct().ToList();
                this._sentence_francais = GetSentence(FRANCAIS.Name).Distinct().ToList();
                this._sentence_espanol = GetSentence(ESPANOL.Name).Distinct().ToList();
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

        public string GetSyntax(List<Word> oration)
        {
            try
            {
                return this._syntaxViewMode.GetOration(oration);
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
                if (language == ENGLISH.Name) _lesson_english = lesson_word;
                if (language == DEUTSCH.Name) _lesson_deutsch = lesson_word;
                if (language == ITALIANO.Name) _lesson_italiano = lesson_word;
                if (language == FRANCAIS.Name) _lesson_francais = lesson_word;
                if (language == ESPANOL.Name) _lesson_espanol = lesson_word;
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
                if (language == ENGLISH.Name) return _lesson_english;
                if (language == DEUTSCH.Name) return _lesson_deutsch;
                if (language == ITALIANO.Name) return _lesson_italiano;
                if (language == FRANCAIS.Name) return _lesson_francais;
                if (language == ESPANOL.Name) return _lesson_espanol;
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
                if (language == ENGLISH.Name) return this._sentence_english;
                if (language == DEUTSCH.Name) return this._sentence_deutsch;
                if (language == ITALIANO.Name) return this._sentence_italiano;
                if (language == FRANCAIS.Name) return this._sentence_francais;
                if (language == ESPANOL.Name) return this._sentence_espanol;
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
                if (language == ENGLISH.Name) return _preposition_english;
                if (language == DEUTSCH.Name) return _preposition_deutsch;
                if (language == ITALIANO.Name) return _preposition_italiano;
                if (language == FRANCAIS.Name) return _preposition_francais;
                if (language == ESPANOL.Name) return _preposition_espanol;
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
                if (language == ENGLISH.Name) return _article_english;
                if (language == DEUTSCH.Name) return _article_deutsch;
                if (language == ITALIANO.Name) return _article_italiano;
                if (language == FRANCAIS.Name) return _article_francais;
                if (language == ESPANOL.Name) return _article_espanol;
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
                if (language == ENGLISH.Name) return _numeral_english;
                if (language == DEUTSCH.Name) return _numeral_deutsch;
                if (language == ITALIANO.Name) return _numeral_italiano;
                if (language == FRANCAIS.Name) return _numeral_francais;
                if (language == ESPANOL.Name) return _numeral_espanol;
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
                if (language == ENGLISH.Name) return _adverb_english;
                if (language == DEUTSCH.Name) return _adverb_deutsch;
                if (language == ITALIANO.Name) return _adverb_italiano;
                if (language == FRANCAIS.Name) return _adverb_francais;
                if (language == ESPANOL.Name) return _adverb_espanol;
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
                if (language == ENGLISH.Name) return _conjunction_english;
                if (language == DEUTSCH.Name) return _conjunction_deutsch;
                if (language == ITALIANO.Name) return _conjunction_italiano;
                if (language == FRANCAIS.Name) return _conjunction_francais;
                if (language == ESPANOL.Name) return _conjunction_espanol;
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
                if (language == ENGLISH.Name) return _verb_english;
                if (language == DEUTSCH.Name) return _verb_deutsch;
                if (language == ITALIANO.Name) return _verb_italiano;
                if (language == FRANCAIS.Name) return _verb_francais;
                if (language == ESPANOL.Name) return _verb_espanol;
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
                if (language == ENGLISH.Name) return _pronoun_english;
                if (language == DEUTSCH.Name) return _pronoun_deutsch;
                if (language == ITALIANO.Name) return _pronoun_italiano;
                if (language == FRANCAIS.Name) return _pronoun_francais;
                if (language == ESPANOL.Name) return _pronoun_espanol;
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

        #region SYNTAX
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
                predicateIndirectObjectPredicative = this._syntaxViewMode.PredicateIndirectObjectPredicativo(sentences, terms, predicateDirectObject, order_predicate);
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
                predicateIndirectObjectPredicative = this._syntaxViewMode.PredicateIndirectObjectPredicativo(sentences, terms, predicateDirectObject, order_predicate);
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

        #region MORPHOLOGY
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

        private List<Word> Authenticate(string language, List<Word> lessons)
        {
            try
            {
                List<Word> new_word = new List<Word>();
                List<Sentenca> sentences = SelectSentence(language).Distinct().ToList();
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);

                string pronoun_subject = string.Empty;
                string pronoun_predicate = string.Empty;
                string article_subject = string.Empty;
                string article_predicate = string.Empty;
                string digit_subject = string.Empty;
                string digit_predicate = string.Empty;
                string noun_subject = string.Empty;
                string noun_predicate = string.Empty;
                string verb = string.Empty;
                string model = string.Empty;
                string preposition = string.Empty;
                foreach (Word word in lessons)
                {
                    if ((word.kind == VAR_PRONOUN) && (word.sentense == VAR_SUBJECT)) pronoun_subject = word.term;
                    if ((word.kind == VAR_ARTICLE) && (word.sentense == VAR_SUBJECT)) article_subject = word.term;
                    if ((word.kind == VAR_NUMERAL) && (word.sentense == VAR_SUBJECT)) digit_subject = word.term;
                    if ((word.kind == VAR_NOUN) && (word.sentense == VAR_SUBJECT)) noun_subject = word.term;
                    if (word.kind == VAR_VERB)
                    {
                        verb = word.term;
                        model = word.model;
                    }
                    if ((word.kind == VAR_PREPOSITION) && (word.sentense == VAR_PREDICATE)) preposition = word.term;
                    if ((word.kind == VAR_PRONOUN) && (word.sentense == VAR_PREDICATE)) pronoun_predicate = word.term;
                    if ((word.kind == VAR_NUMERAL) && (word.sentense == VAR_PREDICATE)) digit_predicate = word.term;
                    if ((word.kind == VAR_ARTICLE) && (word.sentense == VAR_PREDICATE)) article_predicate = word.term;
                    if ((word.kind == VAR_NOUN) && (word.sentense == VAR_PREDICATE)) noun_predicate = word.term;
                }
                bool similarity = false;
                if ((pronoun_subject != string.Empty) && (noun_subject == string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, pronoun_subject, verb);
                }
                if ((noun_subject != string.Empty) && (digit_subject == string.Empty) && (pronoun_subject == string.Empty) && (article_subject == string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun_subject, verb);
                }
                if ((noun_subject != string.Empty) && (digit_subject != string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, digit_subject, noun_subject);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun_subject, verb);
                }
                if ((noun_subject != string.Empty) && (pronoun_subject != string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, pronoun_subject, noun_subject);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun_subject, verb);
                }
                if ((noun_subject != string.Empty) && (article_subject != string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, article_subject, noun_subject);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun_subject, verb);
                }
                if (!similarity) return new_word;
                else
                {
                    if ((preposition == string.Empty) && (pronoun_predicate == string.Empty) && (digit_predicate == string.Empty) && (article_predicate == string.Empty) && (noun_predicate == string.Empty))
                    {
                        List<Word> item = new List<Word>();
                        item = this._morphologyViewModel.GetSuject(pronoun_subject, noun_subject, article_subject, digit_subject, verb, model);
                        item.ForEach(index =>
                        {
                            new_word.Add(index);
                        });
                        return new_word;
                    }
                }
                if ((pronoun_predicate != string.Empty) && (noun_predicate == string.Empty) && (preposition == string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, verb, pronoun_predicate);
                }
                if ((pronoun_predicate != string.Empty) && (noun_predicate == string.Empty) && (preposition != string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, verb, preposition);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, pronoun_predicate);
                }
                if ((pronoun_predicate != string.Empty) && (noun_predicate != string.Empty) && (preposition == string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, verb, pronoun_predicate);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, pronoun_predicate, noun_predicate);
                }
                if ((pronoun_predicate != string.Empty) && (noun_predicate != string.Empty) && (preposition != string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, verb, preposition);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, pronoun_predicate);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, pronoun_predicate, noun_predicate);
                }
                if ((article_predicate != string.Empty) && (noun_predicate != string.Empty) && (preposition == string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, verb, article_predicate);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, article_predicate, noun_predicate);
                }
                if ((article_predicate != string.Empty) && (noun_predicate != string.Empty) && (preposition != string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, verb, preposition);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, article_predicate);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, article_predicate, noun_predicate);
                }
                if ((digit_predicate != string.Empty) && (noun_predicate != string.Empty) && (preposition == string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, verb, digit_predicate);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, digit_predicate, noun_predicate);
                }
                if ((digit_predicate != string.Empty) && (noun_predicate != string.Empty) && (preposition != string.Empty))
                {
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, verb, preposition);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, digit_predicate);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, digit_predicate, noun_predicate);
                }
                if (similarity)
                {
                    List<Word> item_two = new List<Word>();
                    item_two = this._morphologyViewModel.GetPredicate(pronoun_predicate, noun_predicate, article_predicate, digit_predicate, preposition);
                    item_two.ForEach(index =>
                    {
                        new_word.Add(index);
                    });

                    item_two = new List<Word>();
                    item_two = this._morphologyViewModel.GetSuject(pronoun_subject, noun_subject, article_subject, digit_subject, verb, model);
                    item_two.ForEach(index =>
                    {
                        new_word.Add(index);
                    });
                }
                return new_word;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Word> MountSyntax(string language, List<Word> words, bool reverse)
        {
            try
            {
                List<Word> new_word = new List<Word>();
                List<Lesson> lessons = SelectOration(language).OrderBy(index => index.order).ToList();

                if (reverse) lessons.Reverse();
                bool next = false;
                int count_foreach = 0;
                foreach (Lesson lesson in lessons)
                {
                    if (!next)
                    {
                        string word = GetSyntax(words);
                        string word_lesson = GetSyntax(lesson.lecture);
                        if (word == word_lesson)
                            next = true;
                    }
                    else
                    {
                        new_word = Authenticate(language, lesson.lecture);
                        if (new_word != null) break;
                    }

                    count_foreach++;
                    if (lessons.Count == count_foreach)
                    {
                        new_word = words;
                        break;
                    }
                }
                if (reverse) lessons.Reverse();
                return new_word;
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
                List<Sentenca> sentence = SelectSentence(language).Distinct().ToList();
                List<Word> result = new List<Word>();

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

                List<Lesson> words = new List<Lesson>();
                words = OrderLesson(MountOrationSample(sentence, matters));
                words = OrderLesson(words, MountOrationCompound(sentence, matters));

                SetOration(language, words);

                foreach (Lesson phrase in words)
                {
                    result = Authenticate(language, phrase.lecture);
                    if (result.Count() > 0) break;
                }
                return result;
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
