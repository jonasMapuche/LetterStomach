using LetterStomach.Models;
using LetterStomach.Repositories;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;

namespace LetterStomach.ViewModels
{
    public class GrammarService : IGrammarService
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string _error_message;

        public string error_message
        {
            get => this._error_message;
            set
            {
                this._error_message = value;
            }
        }

        public event EventHandler<string> OnError;
        #endregion

        #region VARIABLE
        private IMateriaRepository _materiasRepository;
        private ICircunstanciaRepository _circunstanciasRepository;
        private IPreceitoRepository _preceitosRepository;
        private IEstoutroRepository _estoutrosRepository;
        private IAlgarismoRepository _algarismosRepository;
        private IJuncaoRepository _juncoesRepository;
        private IElocucaoRepository _elocucoesRepository;
        private ISentencaRepository _sentencasRepository;
        private ILigacaoRepository _ligacoesRepository;

        private ISyntaxService _syntaxService;
        private IMorphologyService _morphologyService;
        private IWordEmbeddingService _wordEmbeddingService;

        private List<Lesson>? _book_english;
        private List<Lesson>? _book_deutsch;
        private List<Lesson>? _book_italiano;
        private List<Lesson>? _book_francais;
        private List<Lesson>? _book_espanol;

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

        private Language _language_english;
        private Language _language_deutsch;
        private Language _language_italiano;
        private Language _language_francais;
        private Language _language_espanol;

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

        #region CONSTRUCTOR
        public GrammarService()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Grammar\" service failed!");

                this._syntaxService = new SyntaxService();
                this._morphologyService = new MorphologyService();
                this._wordEmbeddingService = new WordEmbeddingService();

                this._language_english = SettingService.Instance.English;
                this._language_deutsch = SettingService.Instance.Deutsch;
                this._language_italiano = SettingService.Instance.Italino;
                this._language_francais = SettingService.Instance.Francais;
                this._language_espanol = SettingService.Instance.Espanol;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region INIT
        public void Init()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init \"Grammar\" service failed!");

                this._adverb_english = GetAdverb(this._language_english.Lowercase).Distinct().ToList();
                this._adverb_deutsch = GetAdverb(this._language_deutsch.Lowercase).Distinct().ToList();
                this._adverb_italiano = GetAdverb(this._language_italiano.Lowercase).Distinct().ToList();
                this._adverb_francais = GetAdverb(this._language_francais.Lowercase).Distinct().ToList();
                this._adverb_espanol = GetAdverb(this._language_espanol.Lowercase).Distinct().ToList();

                this._pronoun_english = GetPronoun(this._language_english.Lowercase).Distinct().ToList();
                this._pronoun_deutsch = GetPronoun(this._language_deutsch.Lowercase).Distinct().ToList();
                this._pronoun_italiano = GetPronoun(this._language_italiano.Lowercase).Distinct().ToList();
                this._pronoun_francais = GetPronoun(this._language_francais.Lowercase).Distinct().ToList();
                this._pronoun_espanol = GetPronoun(this._language_espanol.Lowercase).Distinct().ToList();

                this._article_english = GetArticle(this._language_english.Lowercase).Distinct().ToList();
                this._article_deutsch = GetArticle(this._language_deutsch.Lowercase).Distinct().ToList();
                this._article_italiano = GetArticle(this._language_italiano.Lowercase).Distinct().ToList();
                this._article_francais = GetArticle(this._language_francais.Lowercase).Distinct().ToList();
                this._article_espanol = GetArticle(this._language_espanol.Lowercase).Distinct().ToList();

                this._numeral_english = GetNumeral(this._language_english.Lowercase).Distinct().ToList();
                this._numeral_deutsch = GetNumeral(this._language_deutsch.Lowercase).Distinct().ToList();
                this._numeral_italiano = GetNumeral(this._language_italiano.Lowercase).Distinct().ToList();
                this._numeral_francais = GetNumeral(this._language_francais.Lowercase).Distinct().ToList();
                this._numeral_espanol = GetNumeral(this._language_espanol.Lowercase).Distinct().ToList();

                this._preposition_english = GetPreposition(this._language_english.Lowercase).Distinct().ToList();
                this._preposition_deutsch = GetPreposition(this._language_deutsch.Lowercase).Distinct().ToList();
                this._preposition_italiano = GetPreposition(this._language_italiano.Lowercase).Distinct().ToList();
                this._preposition_francais = GetPreposition(this._language_francais.Lowercase).Distinct().ToList();
                this._preposition_espanol = GetPreposition(this._language_espanol.Lowercase).Distinct().ToList();

                this._conjunction_english = GetConjunction(this._language_english.Lowercase).Distinct().ToList();
                this._conjunction_deutsch = GetConjunction(this._language_deutsch.Lowercase).Distinct().ToList();
                this._conjunction_italiano = GetConjunction(this._language_italiano.Lowercase).Distinct().ToList();
                this._conjunction_francais = GetConjunction(this._language_francais.Lowercase).Distinct().ToList();
                this._conjunction_espanol = GetConjunction(this._language_espanol.Lowercase).Distinct().ToList();

                this._verb_english = GetVerb(this._language_english.Lowercase).Distinct().ToList();
                this._verb_deutsch = GetVerb(this._language_deutsch.Lowercase).Distinct().ToList();
                this._verb_italiano = GetVerb(this._language_italiano.Lowercase).Distinct().ToList();
                this._verb_francais = GetVerb(this._language_francais.Lowercase).Distinct().ToList();
                this._verb_espanol = GetVerb(this._language_espanol.Lowercase).Distinct().ToList();

                this._sentence_english = GetSentence(this._language_english.Lowercase).Distinct().ToList();
                this._sentence_deutsch = GetSentence(this._language_deutsch.Lowercase).Distinct().ToList();
                this._sentence_italiano = GetSentence(this._language_italiano.Lowercase).Distinct().ToList();
                this._sentence_francais = GetSentence(this._language_francais.Lowercase).Distinct().ToList();
                this._sentence_espanol = GetSentence(this._language_espanol.Lowercase).Distinct().ToList();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InitAsync()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init async \"Grammar\" service failed!");

                this._adverb_english = await GetAdverbAsync(this._language_english.Lowercase);
                this._adverb_deutsch = await GetAdverbAsync(this._language_deutsch.Lowercase);
                this._adverb_italiano = await GetAdverbAsync(this._language_italiano.Lowercase);
                this._adverb_francais = await GetAdverbAsync(this._language_francais.Lowercase);
                this._adverb_espanol = await GetAdverbAsync(this._language_espanol.Lowercase);

                this._pronoun_english = await GetPronounAsync(this._language_english.Lowercase);
                this._pronoun_deutsch = await GetPronounAsync(this._language_deutsch.Lowercase);
                this._pronoun_italiano = await GetPronounAsync(this._language_italiano.Lowercase);
                this._pronoun_francais = await GetPronounAsync(this._language_francais.Lowercase);
                this._pronoun_espanol = await GetPronounAsync(this._language_espanol.Lowercase);

                this._article_english = await GetArticleAsync(this._language_english.Lowercase);
                this._article_deutsch = await GetArticleAsync(this._language_deutsch.Lowercase);
                this._article_italiano = await GetArticleAsync(this._language_italiano.Lowercase);
                this._article_francais = await GetArticleAsync(this._language_francais.Lowercase);
                this._article_espanol = await GetArticleAsync(this._language_espanol.Lowercase);

                this._numeral_english = await GetNumeralAsync(this._language_english.Lowercase);
                this._numeral_deutsch = await GetNumeralAsync(this._language_deutsch.Lowercase);
                this._numeral_italiano = await GetNumeralAsync(this._language_italiano.Lowercase);
                this._numeral_francais = await GetNumeralAsync(this._language_francais.Lowercase);
                this._numeral_espanol = await GetNumeralAsync(this._language_espanol.Lowercase);

                this._preposition_english = await GetPrepositionAsync(this._language_english.Lowercase);
                this._preposition_deutsch = await GetPrepositionAsync(this._language_deutsch.Lowercase);
                this._preposition_italiano = await GetPrepositionAsync(this._language_italiano.Lowercase);
                this._preposition_francais = await GetPrepositionAsync(this._language_francais.Lowercase);
                this._preposition_espanol = await GetPrepositionAsync(this._language_espanol.Lowercase);

                this._conjunction_english = await GetConjunctionAsync(this._language_english.Lowercase);
                this._conjunction_deutsch = await GetConjunctionAsync(this._language_deutsch.Lowercase);
                this._conjunction_italiano = await GetConjunctionAsync(this._language_italiano.Lowercase);
                this._conjunction_francais = await GetConjunctionAsync(this._language_francais.Lowercase);
                this._conjunction_espanol = await GetConjunctionAsync(this._language_espanol.Lowercase);

                this._verb_english = await GetVerbAsync(this._language_english.Lowercase);
                this._verb_deutsch = await GetVerbAsync(this._language_deutsch.Lowercase);
                this._verb_italiano = await GetVerbAsync(this._language_italiano.Lowercase);
                this._verb_francais = await GetVerbAsync(this._language_francais.Lowercase);
                this._verb_espanol = await GetVerbAsync(this._language_espanol.Lowercase);

                this._sentence_english = await GetSentenceAsync(this._language_english.Lowercase);
                this._sentence_deutsch = await GetSentenceAsync(this._language_deutsch.Lowercase);
                this._sentence_italiano = await GetSentenceAsync(this._language_italiano.Lowercase);
                this._sentence_francais = await GetSentenceAsync(this._language_francais.Lowercase);
                this._sentence_espanol = await GetSentenceAsync(this._language_espanol.Lowercase);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public void MongoDB()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mongodb \"Grammar\" service failed!");

                this._circunstanciasRepository = new Repositories.MongoDBs.CircusnstanciaRepository();
                this._preceitosRepository = new Repositories.MongoDBs.PreceitoRepository();
                this._estoutrosRepository = new Repositories.MongoDBs.EstoutroRepository();
                this._algarismosRepository = new Repositories.MongoDBs.AlgarismoRepository();
                this._juncoesRepository = new Repositories.MongoDBs.JuncaoRepository();
                this._elocucoesRepository = new Repositories.MongoDBs.ElocucaoRepository();
                this._materiasRepository = new Repositories.MongoDBs.MateriaRepository();
                this._sentencasRepository = new Repositories.MongoDBs.SentencaRepository();
                this._ligacoesRepository = new Repositories.MongoDBs.LigacaoRepoitory();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public void SQLite(ISQLiteService sQLiteService)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation sqlite \"Grammar\" service failed!");

                this._circunstanciasRepository = new Repositories.SQLites.CircunstanciaRepository(sQLiteService.Circunstancia);
                this._preceitosRepository = new Repositories.SQLites.PreceitoRepository(sQLiteService.Preceito);
                this._estoutrosRepository = new Repositories.SQLites.EstoutroRepository(sQLiteService.Estoutro);
                this._algarismosRepository = new Repositories.SQLites.AlgarismoRepository(sQLiteService.Algarismo);
                this._juncoesRepository = new Repositories.SQLites.JuncaoRepository(sQLiteService.Juncao);
                this._elocucoesRepository = new Repositories.SQLites.ElocucaoRepository(sQLiteService.Elocucao);
                this._materiasRepository = new Repositories.SQLites.MateriaRepository(sQLiteService.Materia);
                this._sentencasRepository = new Repositories.SQLites.SentencaRepository(sQLiteService.Sentenca);
                this._ligacoesRepository = new Repositories.SQLites.LigacaoRepository(sQLiteService.Ligacao);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        private List<Circunstancia> GetAdverb(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get adverb \"Grammar\" service failed!");

                return this._circunstanciasRepository.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Circunstancia>> GetAdverbAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get adverb async \"Grammar\" service failed!");

                return await this._circunstanciasRepository.GetLanguageAsync(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Preceito> GetArticle(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get article \"Grammar\" service failed!");

                return this._preceitosRepository.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Preceito>> GetArticleAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get article async \"Grammar\" service failed!");

                return await this._preceitosRepository.GetLanguageAsync(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Estoutro> GetPronoun(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get pronoun \"Grammar\" service failed!");

                return this._estoutrosRepository.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Estoutro>> GetPronounAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get pronoun async \"Grammar\" service failed!");

                return await this._estoutrosRepository.GetLanguageAsync(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Algarismo> GetNumeral(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get numeral \"Grammar\" service failed!");

                return this._algarismosRepository.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Algarismo>> GetNumeralAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get numeral async \"Grammar\" service failed!");

                return await this._algarismosRepository.GetLanguageAsync(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Juncao> GetPreposition(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get proposition \"Grammar\" service failed!");

                return this._juncoesRepository.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Juncao>> GetPrepositionAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get proposition async \"Grammar\" service failed!");

                return await this._juncoesRepository.GetLanguageAsync(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Elocucao> GetVerb(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get verb \"Grammar\" service failed!");

                return this._elocucoesRepository.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Elocucao>> GetVerbAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get verb async \"Grammar\" service failed!");

                return await this._elocucoesRepository.GetLanguageAsync(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Sentenca> GetSentence(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get sentence \"Grammar\" service failed!");

                return this._sentencasRepository.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Sentenca>> GetSentenceAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get sentence async \"Grammar\" service failed!");

                return await this._sentencasRepository.GetLanguageAsync(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Ligacao> GetConjunction(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get conjunction \"Grammar\" service failed!");

                return this._ligacoesRepository.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Ligacao>> GetConjunctionAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get conjunction async \"Grammar\" service failed!");

                return await this._ligacoesRepository.GetLanguageAsync(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Materia>> GetLetterAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get letter async \"Grammar\" service failed!");

                return await this._materiasRepository.GetLessonSimpleAsync(true, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Materia> GetLetter(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get letter \"Grammar\" service failed!");

                return this._materiasRepository.GetLessonSimple(true, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region SET
        private void SetBook(string language, List<Lesson> lesson_word)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation set oration \"Grammar\" service failed!");

                if (language == this._language_english.Lowercase) this._book_english = lesson_word;
                if (language == this._language_deutsch.Lowercase) this._book_deutsch = lesson_word;
                if (language == this._language_italiano.Lowercase) this._book_italiano = lesson_word;
                if (language == this._language_francais.Lowercase) this._book_francais = lesson_word;
                if (language == this._language_espanol.Lowercase) this._book_espanol = lesson_word;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region SELECT
        private List<Lesson>? SelectBook(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation select oration \"Grammar\" service failed!");

                if (language == this._language_english.Lowercase) return this._book_english;
                if (language == this._language_deutsch.Lowercase) return this._book_deutsch;
                if (language == this._language_italiano.Lowercase) return this._book_italiano;
                if (language == this._language_francais.Lowercase) return this._book_francais;
                if (language == this._language_espanol.Lowercase) return this._book_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Sentenca>? SelectSentence(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation select sentence \"Grammar\" service failed!");

                if (language == this._language_english.Lowercase) return this._sentence_english;
                if (language == this._language_deutsch.Lowercase) return this._sentence_deutsch;
                if (language == this._language_italiano.Lowercase) return this._sentence_italiano;
                if (language == this._language_francais.Lowercase) return this._sentence_francais;
                if (language == this._language_espanol.Lowercase) return this._sentence_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Juncao>? SelectPreposition(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation select preposition \"Grammar\" service failed!");

                if (language == this._language_english.Lowercase) return this._preposition_english;
                if (language == this._language_deutsch.Lowercase) return this._preposition_deutsch;
                if (language == this._language_italiano.Lowercase) return this._preposition_italiano;
                if (language == this._language_francais.Lowercase) return this._preposition_francais;
                if (language == this._language_espanol.Lowercase) return this._preposition_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Preceito>? SelectArticle(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation select article \"Grammar\" service failed!");

                if (language == this._language_english.Lowercase) return this._article_english;
                if (language == this._language_deutsch.Lowercase) return this._article_deutsch;
                if (language == this._language_italiano.Lowercase) return this._article_italiano;
                if (language == this._language_francais.Lowercase) return this._article_francais;
                if (language == this._language_espanol.Lowercase) return this._article_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Algarismo>? SelectNumeral(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation select numeral \"Grammar\" service failed!");

                if (language == this._language_english.Lowercase) return this._numeral_english;
                if (language == this._language_deutsch.Lowercase) return this._numeral_deutsch;
                if (language == this._language_italiano.Lowercase) return this._numeral_italiano;
                if (language == this._language_francais.Lowercase) return this._numeral_francais;
                if (language == this._language_espanol.Lowercase) return this._numeral_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Circunstancia>? SelectAdverb(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation select adverb \"Grammar\" service failed!");

                if (language == this._language_english.Lowercase) return this._adverb_english;
                if (language == this._language_deutsch.Lowercase) return this._adverb_deutsch;
                if (language == this._language_italiano.Lowercase) return this._adverb_italiano;
                if (language == this._language_francais.Lowercase) return this._adverb_francais;
                if (language == this._language_espanol.Lowercase) return this._adverb_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Ligacao>? SelectConjunction(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation select conjunction \"Grammar\" service failed!");

                if (language == this._language_english.Lowercase) return this._conjunction_english;
                if (language == this._language_deutsch.Lowercase) return this._conjunction_deutsch;
                if (language == this._language_italiano.Lowercase) return this._conjunction_italiano;
                if (language == this._language_francais.Lowercase) return this._conjunction_francais;
                if (language == this._language_espanol.Lowercase) return this._conjunction_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Elocucao>? SelectVerb(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation select verb \"Grammar\" service failed!");

                if (language == this._language_english.Lowercase) return this._verb_english;
                if (language == this._language_deutsch.Lowercase) return this._verb_deutsch;
                if (language == this._language_italiano.Lowercase) return this._verb_italiano;
                if (language == this._language_francais.Lowercase) return this._verb_francais;
                if (language == this._language_espanol.Lowercase) return this._verb_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Estoutro>? SelectPronoun(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation select pronoun \"Grammar\" service failed!");

                if (language == this._language_english.Lowercase) return this._pronoun_english;
                if (language == this._language_deutsch.Lowercase) return this._pronoun_deutsch;
                if (language == this._language_italiano.Lowercase) return this._pronoun_italiano;
                if (language == this._language_francais.Lowercase) return this._pronoun_francais;
                if (language == this._language_espanol.Lowercase) return this._pronoun_espanol;
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region PERIOD
        private List<Lesson> OrderLesson(List<Lesson> firsts)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation order lesson \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> OrderLesson(List<Lesson> firsts, List<Lesson> lasts)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation order lesson \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountOrationSample(List<Sentenca> sentences, List<Lesson> terms)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount oration sample \"Grammar\" service failed!");

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
                sampleSubjectVerb = this._syntaxService.SampleSubjectVerb(sentences, terms);
                words = sampleSubjectVerb;
                predicatePredicative = this._syntaxService.PredicatePredicative(sentences, terms, sampleSubjectVerb, order_sample);
                words = Union(words, predicatePredicative);
                predicateDirectObject = this._syntaxService.PredicateDirectObject(sentences, terms, sampleSubjectVerb, order_sample);
                words = Union(words, predicateDirectObject);
                predicateIndirectObject = this._syntaxService.PredicateIndirectObject(sentences, terms, predicateDirectObject, order_sample);
                words = Union(words, predicateIndirectObject);
                predicateDirectObjectIndirectObject = this._syntaxService.PredicateDirectObjectIndirectObject(sentences, terms, predicateDirectObject, order_predicate);
                words = Union(words, predicateDirectObjectIndirectObject);
                predicateDirectObjectPredicative = this._syntaxService.PredicateDirectObjectPredicative(sentences, terms, predicateDirectObject, order_predicate);
                words = Union(words, predicateDirectObjectPredicative);
                predicateIndirectObjectPredicative = this._syntaxService.PredicateIndirectObjectPredicative(sentences, terms, predicateIndirectObject, order_predicate);
                words = Union(words, predicateIndirectObjectPredicative);
                predicatePredicativeIndirectObject = this._syntaxService.PredicatePredicativeIndirectObject(sentences, terms, predicatePredicative, order_predicate);
                words = Union(words, predicatePredicativeIndirectObject);
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountOrationCompound(List<Sentenca> sentences, List<Lesson> terms)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount oration compound \"Grammar\" service failed!");

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
                compoundSubjectVerb = this._syntaxService.CompoundSubjectVerb(sentences, terms);
                words = compoundSubjectVerb;
                predicatePredicative = this._syntaxService.PredicatePredicative(sentences, terms, compoundSubjectVerb, order_sample);
                words = Union(words, predicatePredicative);
                predicateDirectObject = this._syntaxService.PredicateDirectObject(sentences, terms, compoundSubjectVerb, order_sample);
                words = Union(words, predicateDirectObject);
                predicateIndirectObject = this._syntaxService.PredicateIndirectObject(sentences, terms, compoundSubjectVerb, order_sample);
                words = Union(words, predicateIndirectObject);
                predicateDirectObjectIndirectObject = this._syntaxService.PredicateDirectObjectIndirectObject(sentences, terms, predicateDirectObject, order_predicate);
                words = Union(words, predicateDirectObjectIndirectObject);
                predicateDirectObjectPredicative = this._syntaxService.PredicateDirectObjectPredicative(sentences, terms, predicateDirectObject, order_predicate);
                words = Union(words, predicateDirectObjectPredicative);
                predicateIndirectObjectPredicative = this._syntaxService.PredicateIndirectObjectPredicative(sentences, terms, predicateIndirectObject, order_predicate);
                words = Union(words, predicateIndirectObjectPredicative);
                predicatePredicativeIndirectObject = this._syntaxService.PredicatePredicativeIndirectObject(sentences, terms, predicatePredicative, order_predicate);
                words = Union(words, predicatePredicativeIndirectObject);
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region ORATION
        private Word? SubjectBefore(string language, List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation subject before \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Word? PredicateBefore(string language, List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate before \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Word? SubjectCurrent(string language, List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation subject current \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Word? PredicateCurrent(string language, List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation predicate current \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> MountSubject(List<Sentenca> sentences, string language, List<Word> matters)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount subject \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> MountPredicate(List<Sentenca> sentences, string language, List<Word> matters)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount predicate \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private string? MountExpression(string language, List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount expression \"Grammar\" service failed!");

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
                        word += article.term;
                    if (numeral != null)
                        word += numeral.term;
                    if (pronoun != null)
                        word += pronoun.term;
                }
                if ((team == VAR_NOUN) || (team == VAR_NUMERAL_NOUN))
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public string MountOration(string language, List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount oration \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> MountOration(List<Sentenca> sentences, string language, List<Word> lessons)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount oration \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        #endregion

        #region SYNTAX
        private List<Lesson> Union(List<Lesson> fists, List<Lesson> lasts)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation union \"Grammar\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Word> MountSyntax(string language, Materia lesson, List<Materia> book)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount syntax \"Grammar\" service failed!");

                List<Sentenca> sentence = SelectSentence(language).Distinct().ToList();

                List<string> list_noun = this._morphologyService.GetLessonNoun(lesson, book);
                List<string> list_adjective = this._morphologyService.GetLessonAdjective(lesson, book);
                List<string> list_model = this._morphologyService.GetLessonVerb(lesson);

                List<Juncao> list_preposition = SelectPreposition(language).OrderBy(index => index.nome).ToList();
                List<Preceito> list_article = SelectArticle(language).OrderBy(index => index.nome).ToList();
                List<Algarismo> list_numeral = SelectNumeral(language).OrderBy(index => index.sigla).ToList();
                List<Circunstancia> list_adverb = SelectAdverb(language).OrderBy(index => index.nome).ToList();
                List<Elocucao> list_verb = SelectVerb(language).OrderBy(index => index.nome).ToList();
                List<Estoutro> list_pronoun = SelectPronoun(language).OrderBy(index => index.nome).ToList();
                List<Ligacao> list_conjunction = SelectConjunction(language).OrderBy(index => index.nome).ToList();

                List<Lesson> mount_noun = this._morphologyService.GetNoun(sentence, list_noun, list_pronoun, list_article, list_numeral);
                List<Lesson> mount_verb = this._morphologyService.GetVerb(sentence, list_model, list_verb, list_adverb);
                List<Lesson> mount_adjective_adverb = this._morphologyService.GetAdjectiveAdverb(sentence, list_adjective, list_adverb);
                List<Lesson> mount_adjective = this._morphologyService.GetAdjective(sentence, list_adjective);
                List<Lesson> mount_preposition = this._morphologyService.GetPreposition(sentence, list_preposition);
                List<Lesson> mount_adjective_noun = this._morphologyService.GetAdjectiveNoun(sentence, mount_adjective, mount_noun, mount_preposition);
                List<Lesson> mount_pronoun = this._morphologyService.GetPronoun(sentence, list_pronoun);
                List<Lesson> mount_conjunction = this._morphologyService.GetConjunction(sentence, list_conjunction);
                List<Lesson> mount_numeral_noun = this._morphologyService.GetNumeralNoun(sentence, list_noun, list_article, list_numeral);
                List<Lesson> mount_numeral = this._morphologyService.GetNumeral(sentence, list_numeral);
                List<Lesson> mount_article = this._morphologyService.GetArticle(sentence, list_article);

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

                SetBook(language, terms);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Word> MountSyntax(string language, List<Word> terms, bool reverse)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount syntax \"Grammar\" service failed!");

                List<Word> words = new List<Word>();
                List<Lesson> lessons = SelectBook(language).OrderBy(index => index.order).Distinct().ToList();
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion
    }
}
