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
        private IMateriaRepository? _materiasRepository;
        private ICircunstanciaRepository? _circunstanciasRepository;
        private IPreceitoRepository? _preceitosRepository;
        private IEstoutroRepository? _estoutrosRepository;
        private IAlgarismoRepository? _algarismosRepository;
        private IJuncaoRepository? _juncoesRepository;
        private IElocucaoRepository? _elocucoesRepository;
        private ISentencaRepository? _sentencasRepository;
        private ILigacaoRepository? _ligacoesRepository;

        private ISyntaxService _syntaxService;
        private IMorphologyService _morphologyService;
        private IWordEmbeddingService _wordEmbeddingService;

        private List<Tutorial>? _novel_english;
        private List<Tutorial>? _novel_deutsch;
        private List<Tutorial>? _novel_italiano;
        private List<Tutorial>? _novel_francais;
        private List<Tutorial>? _novel_espanol;

        private List<Lesson>? _book_english;
        private List<Lesson>? _book_deutsch;
        private List<Lesson>? _book_italiano;
        private List<Lesson>? _book_francais;
        private List<Lesson>? _book_espanol;

        private List<Circunstancia> _adverb_english;
        private List<Circunstancia> _adverb_deutsch;
        private List<Circunstancia> _adverb_italiano;
        private List<Circunstancia> _adverb_francais;
        private List<Circunstancia> _adverb_espanol;

        private List<Preceito> _article_english;
        private List<Preceito> _article_deutsch;
        private List<Preceito> _article_italiano;
        private List<Preceito> _article_francais;
        private List<Preceito> _article_espanol;

        private List<Estoutro> _pronoun_english;
        private List<Estoutro> _pronoun_deutsch;
        private List<Estoutro> _pronoun_italiano;
        private List<Estoutro> _pronoun_francais;
        private List<Estoutro> _pronoun_espanol;

        private List<Algarismo> _numeral_english;
        private List<Algarismo> _numeral_deutsch;
        private List<Algarismo> _numeral_italiano;
        private List<Algarismo> _numeral_francais;
        private List<Algarismo> _numeral_espanol;

        private List<Juncao> _preposition_english;
        private List<Juncao> _preposition_deutsch;
        private List<Juncao> _preposition_italiano;
        private List<Juncao> _preposition_francais;
        private List<Juncao> _preposition_espanol;

        private List<Elocucao> _verb_english;
        private List<Elocucao> _verb_deutsch;
        private List<Elocucao> _verb_italiano;
        private List<Elocucao> _verb_francais;
        private List<Elocucao> _verb_espanol;

        private List<Sentenca> _sentence_english;
        private List<Sentenca> _sentence_deutsch;
        private List<Sentenca> _sentence_italiano;
        private List<Sentenca> _sentence_francais;
        private List<Sentenca> _sentence_espanol;

        private List<Ligacao> _conjunction_english;
        private List<Ligacao> _conjunction_deutsch;
        private List<Ligacao> _conjunction_italiano;
        private List<Ligacao> _conjunction_francais;
        private List<Ligacao> _conjunction_espanol;

        private Language _language_english;
        private Language _language_deutsch;
        private Language _language_italiano;
        private Language _language_francais;
        private Language _language_espanol;

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
        private string _demostrative;
        private string _adverb;
        private string _adverb_adverb;
        private string _adjective_noun;
        private string _conjunction;
        private string _numeral_noun;
        private string _adjective_adverb;
        private string _sentence;
        private string _adnominal_adjunct;
        private string _adverbial_verb;
        private string _adverbial_adjective;

        private int _order_3 = 3;
        private int _order_4 = 4;
        private int _order_5 = 5;
        private int _order_6 = 6;
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

                this._adverb_english = new List<Circunstancia>();
                this._adverb_deutsch = new List<Circunstancia>();
                this._adverb_italiano = new List<Circunstancia>();
                this._adverb_francais = new List<Circunstancia>();
                this._adverb_espanol = new List<Circunstancia>();

                this._article_english = new List<Preceito>();
                this._article_deutsch = new List<Preceito>();
                this._article_italiano = new List<Preceito>();
                this._article_francais = new List<Preceito>();
                this._article_espanol = new List<Preceito>();

                this._pronoun_english = new List<Estoutro>();
                this._pronoun_deutsch = new List<Estoutro>();
                this._pronoun_italiano = new List<Estoutro>();
                this._pronoun_francais = new List<Estoutro>();
                this._pronoun_espanol = new List<Estoutro>();

                this._numeral_english = new List<Algarismo>();
                this._numeral_deutsch = new List<Algarismo>();
                this._numeral_italiano = new List<Algarismo>();
                this._numeral_francais = new List<Algarismo>();
                this._numeral_espanol = new List<Algarismo>();

                this._preposition_english = new List<Juncao>();
                this._preposition_deutsch = new List<Juncao>();
                this._preposition_italiano = new List<Juncao>();
                this._preposition_francais = new List<Juncao>();
                this._preposition_espanol = new List<Juncao>();

                this._verb_english = new List<Elocucao>();
                this._verb_deutsch = new List<Elocucao>();
                this._verb_italiano = new List<Elocucao>();
                this._verb_francais = new List<Elocucao>();
                this._verb_espanol = new List<Elocucao>();

                this._sentence_english = new List<Sentenca>();
                this._sentence_deutsch = new List<Sentenca>();
                this._sentence_italiano = new List<Sentenca>();
                this._sentence_francais = new List<Sentenca>();
                this._sentence_espanol = new List<Sentenca>();

                this._conjunction_english = new List<Ligacao>();
                this._conjunction_deutsch = new List<Ligacao>();
                this._conjunction_italiano = new List<Ligacao>();
                this._conjunction_francais = new List<Ligacao>();
                this._conjunction_espanol = new List<Ligacao>();

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
                this._demostrative = SettingService.Instance.Demostrtive;
                this._adverb = SettingService.Instance.Adverb;
                this._adverb_adverb = SettingService.Instance.Adverb_Adverb;
                this._adjective_noun = SettingService.Instance.Adjective_Noun;
                this._conjunction = SettingService.Instance.Conjunction;
                this._numeral_noun = SettingService.Instance.Numeral_Noun;
                this._adjective_adverb = SettingService.Instance.Adjective_Adverb;
                this._sentence = SettingService.Instance.Sentence;
                this._adnominal_adjunct = SettingService.Instance.Adnominal_Adjunct;
                this._adverbial_verb = SettingService.Instance.Adverbial_Verb;
                this._adverbial_adjective = SettingService.Instance.Adverbial_Adjective;
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

        public async Task InitAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init async \"Grammar\" service failed!");

                if ((this._language_english.Lowercase != null) && (this._language_english.Lowercase == language)) 
                {
                    this._adverb_english = await GetAdverbAsync(this._language_english.Lowercase);
                    this._pronoun_english = await GetPronounAsync(this._language_english.Lowercase);
                    this._article_english = await GetArticleAsync(this._language_english.Lowercase);
                    this._numeral_english = await GetNumeralAsync(this._language_english.Lowercase);
                    this._preposition_english = await GetPrepositionAsync(this._language_english.Lowercase);
                    this._conjunction_english = await GetConjunctionAsync(this._language_english.Lowercase);
                    this._verb_english = await GetVerbAsync(this._language_english.Lowercase);
                    this._sentence_english = await GetSentenceAsync(this._language_english.Lowercase);
                }
                if ((this._language_deutsch.Lowercase != null) && (this._language_deutsch.Lowercase == language))
                {
                    this._adverb_deutsch = await GetAdverbAsync(this._language_deutsch.Lowercase);
                    this._pronoun_deutsch = await GetPronounAsync(this._language_deutsch.Lowercase);
                    this._article_deutsch = await GetArticleAsync(this._language_deutsch.Lowercase);
                    this._numeral_deutsch = await GetNumeralAsync(this._language_deutsch.Lowercase);
                    this._preposition_deutsch = await GetPrepositionAsync(this._language_deutsch.Lowercase);
                    this._conjunction_deutsch = await GetConjunctionAsync(this._language_deutsch.Lowercase);
                    this._verb_deutsch = await GetVerbAsync(this._language_deutsch.Lowercase);
                    this._sentence_deutsch = await GetSentenceAsync(this._language_deutsch.Lowercase);
                }
                if ((this._language_italiano.Lowercase != null) && (this._language_italiano.Lowercase == language))
                {
                    this._adverb_italiano = await GetAdverbAsync(this._language_italiano.Lowercase);
                    this._pronoun_italiano = await GetPronounAsync(this._language_italiano.Lowercase);
                    this._article_italiano = await GetArticleAsync(this._language_italiano.Lowercase);
                    this._numeral_italiano = await GetNumeralAsync(this._language_italiano.Lowercase);
                    this._preposition_italiano = await GetPrepositionAsync(this._language_italiano.Lowercase);
                    this._conjunction_italiano = await GetConjunctionAsync(this._language_italiano.Lowercase);
                    this._verb_italiano = await GetVerbAsync(this._language_italiano.Lowercase);
                    this._sentence_italiano = await GetSentenceAsync(this._language_italiano.Lowercase);
                }
                if ((this._language_francais.Lowercase != null) && (this._language_francais.Lowercase == language))
                {
                    this._adverb_francais = await GetAdverbAsync(this._language_francais.Lowercase);
                    this._pronoun_francais = await GetPronounAsync(this._language_francais.Lowercase);
                    this._article_francais = await GetArticleAsync(this._language_francais.Lowercase);
                    this._numeral_francais = await GetNumeralAsync(this._language_francais.Lowercase);
                    this._preposition_francais = await GetPrepositionAsync(this._language_francais.Lowercase);
                    this._conjunction_francais = await GetConjunctionAsync(this._language_francais.Lowercase);
                    this._verb_francais = await GetVerbAsync(this._language_francais.Lowercase);
                    this._sentence_francais = await GetSentenceAsync(this._language_francais.Lowercase);
                }
                if ((this._language_espanol.Lowercase != null) && (this._language_espanol.Lowercase == language))
                {
                    this._adverb_espanol = await GetAdverbAsync(this._language_espanol.Lowercase);
                    this._pronoun_espanol = await GetPronounAsync(this._language_espanol.Lowercase);
                    this._article_espanol = await GetArticleAsync(this._language_espanol.Lowercase);
                    this._numeral_espanol = await GetNumeralAsync(this._language_espanol.Lowercase);
                    this._preposition_espanol = await GetPrepositionAsync(this._language_espanol.Lowercase);
                    this._conjunction_espanol = await GetConjunctionAsync(this._language_espanol.Lowercase);
                    this._verb_espanol = await GetVerbAsync(this._language_espanol.Lowercase);
                    this._sentence_espanol = await GetSentenceAsync(this._language_espanol.Lowercase);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public void MongoDB(IMongoDBService mongoDBService)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mongodb \"Grammar\" service failed!");

                this._circunstanciasRepository = new Repositories.MongoDBs.CircunstanciaRepository(mongoDBService.CircunstanciaContex);
                this._preceitosRepository = new Repositories.MongoDBs.PreceitoRepository(mongoDBService.PreceitoContext);
                this._estoutrosRepository = new Repositories.MongoDBs.EstoutroRepository(mongoDBService.EstoutroContext);
                this._algarismosRepository = new Repositories.MongoDBs.AlgarismoRepository(mongoDBService.AlgarismoContext);
                this._juncoesRepository = new Repositories.MongoDBs.JuncaoRepository(mongoDBService.JuncaoContext);
                this._elocucoesRepository = new Repositories.MongoDBs.ElocucaoRepository(mongoDBService.ElocucaoContext);
                this._materiasRepository = new Repositories.MongoDBs.MateriaRepository(mongoDBService.MaterialContext);
                this._sentencasRepository = new Repositories.MongoDBs.SentencaRepository(mongoDBService.SentencaContext);
                this._ligacoesRepository = new Repositories.MongoDBs.LigacaoRepoitory(mongoDBService.LigacaoContext);
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

        public async Task SQLiteAsync(ISQLiteService sQLiteService)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation sqlite async \"Grammar\" service failed!");

                this._circunstanciasRepository = new Repositories.SQLites.CircunstanciaRepository(await sQLiteService.GetCircunstancia());
                this._preceitosRepository = new Repositories.SQLites.PreceitoRepository(await sQLiteService.GetPreceito());
                this._estoutrosRepository = new Repositories.SQLites.EstoutroRepository(await sQLiteService.GetEstoutro());
                this._algarismosRepository = new Repositories.SQLites.AlgarismoRepository(await sQLiteService.GetAlgarismo());
                this._juncoesRepository = new Repositories.SQLites.JuncaoRepository(await sQLiteService.GetJuncao());
                this._elocucoesRepository = new Repositories.SQLites.ElocucaoRepository(await sQLiteService.GetElocucao());
                this._materiasRepository = new Repositories.SQLites.MateriaRepository(await sQLiteService.GetMateria());
                this._sentencasRepository = new Repositories.SQLites.SentencaRepository(await sQLiteService.GetSentenca());
                this._ligacoesRepository = new Repositories.SQLites.LigacaoRepository(await sQLiteService.GetLigacao());
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET REPOSITORY
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

        #region BOOK
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
        #endregion

        #region SELECT MODEL
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

        #region MOUNT SYNTAX
        private List<Lesson> Union(List<Lesson> first, List<Lesson> lasts)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation union \"Grammar\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                first.ForEach(item =>
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

        private List<Tutorial> Union(List<Tutorial> firsts, List<Tutorial> lasts)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation union \"Grammar\" service failed!");

                List<Tutorial> tutorials = new List<Tutorial>();
                firsts.ForEach(first => tutorials.Add(first));
                lasts.ForEach(last => tutorials.Add(last));
                return tutorials;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

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

                int order_sample = this._order_3;
                int order_predicate = this._order_4;
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

        private List<Tutorial> MountOrationSample(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount oration sample \"Grammar\" view model failed!");

                List<Tutorial> words = new List<Tutorial>();
                
                List<Tutorial> sampleSubjectVerb = new List<Tutorial>();
                List<Tutorial> compoundSubjectVerb = new List<Tutorial>();
                List<Tutorial> subjectVerb = new List<Tutorial>();
                
                List<Tutorial> predicatePredicative = new List<Tutorial>();
                List<Tutorial> predicateDirectObject = new List<Tutorial>();
                List<Tutorial> predicateIndirectObject = new List<Tutorial>();

                List<Tutorial> predicateDirectObjectIndirectObject = new List<Tutorial>();
                List<Tutorial> predicateDirectObjectPredicative = new List<Tutorial>();
                List<Tutorial> predicateIndirectObjectPredicative = new List<Tutorial>();

                int order_sample = this._order_3;
                int order_predicate = this._order_4;

                sampleSubjectVerb = this._syntaxService.SampleSubjectVerb(tutorials, word_2_vec);
                words = sampleSubjectVerb;

                /*                
                compoundSubjectVerb = this._syntaxService.CompoundSubjectVerb(tutorials, word_2_vec);
                words = Union(words, compoundSubjectVerb);
                subjectVerb = Union(sampleSubjectVerb, compoundSubjectVerb);

                predicatePredicative = this._syntaxService.PredicatePredicative(tutorials, word_2_vec, subjectVerb, order_sample);
                words = Union(words, predicatePredicative);

                predicateDirectObject = this._syntaxService.PredicateDirectObject(tutorials, word_2_vec, subjectVerb, order_sample);
                words = Union(words, predicateDirectObject);

                predicateIndirectObject = this._syntaxService.PredicateIndirectObject(tutorials, word_2_vec, subjectVerb, order_sample);
                words = Union(words, predicateIndirectObject);

                predicateDirectObjectIndirectObject = this._syntaxService.PredicateDirectObjectIndirectObject(tutorials, word_2_vec, predicateDirectObject, order_sample, order_predicate);
                words = Union(words, predicateDirectObjectIndirectObject);

                predicateDirectObjectPredicative = this._syntaxService.PredicateDirectObjectPredicative(tutorials, word_2_vec, predicateDirectObject, order_sample, order_predicate);
                words = Union(words, predicateDirectObjectPredicative);

                predicateIndirectObjectPredicative = this._syntaxService.PredicateIndirectObjectPredicative(tutorials, word_2_vec, predicateIndirectObject, order_sample, order_predicate);
                words = Union(words, predicateIndirectObjectPredicative);
                */

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

                int order_sample = this._order_5;
                int order_predicate = this._order_6;
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

        #region MOUNT PERIOD
        private List<Lesson> Conjunction(List<Lesson> firsts, List<Lesson> lasts, List<Word> conjunctions)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation adnominal \"Grammar\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                foreach (Lesson first in firsts)
                {
                    foreach (Lesson last in lasts)
                    {
                        List<Word> words = new List<Word>();
                        words = first.lecture;
                        foreach (Word conjunction in conjunctions)
                        {
                            words.Add(conjunction);
                        }
                        foreach (Word item in last.lecture)
                        {
                            words.Add(item);
                        }
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

        private List<Lesson> Numeral(List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation numeral \"Grammar\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                Lesson lesson = new Lesson();
                List<Word> terms = new List<Word>();

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> numerals = new List<Word>();
                numerals = words.FindAll(index => index.team == this._numeral && index.kind == this._numeral);
                foreach (Word numeral in numerals)
                {
                    terms.Add(numeral);
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> Adverbial(List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation adverbial \"Grammar\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                Lesson lesson = new Lesson();
                List<Word> terms = new List<Word>();

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> adjectives = new List<Word>();
                adjectives = words.FindAll(index => index.team == this._adverbial_adjective && index.kind == this._adjective);
                foreach (Word adjective in adjectives)
                {
                    terms.Add(adjective);
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> adverbs = new List<Word>();
                adverbs = words.FindAll(index => index.team == this._adverbial_adjective && index.kind == this._adverb);
                foreach (Word adverb in adverbs)
                {
                    foreach (Word adjective in adjectives)
                    {
                        terms.Add(adjective);
                        terms.Add(adverb);
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> adverbs_adverbs = new List<Word>();
                adverbs_adverbs = words.FindAll(index => index.team == this._adverbial_adjective && index.kind == this._adverb_adverb);
                foreach (Word adverb_adverb in adverbs_adverbs)
                {
                    foreach (Word adverb in adverbs)
                    {
                        foreach (Word adjective in adjectives)
                        {
                            terms.Add(adjective);
                            terms.Add(adverb);
                            terms.Add(adverb_adverb);
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> verbs = new List<Word>();
                verbs = words.FindAll(index => index.team == this._adverbial_verb && index.kind == this._verb);
                foreach (Word verb in verbs)
                {
                    terms.Add(verb);
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> adverbs_verbs = new List<Word>();
                adverbs_verbs = words.FindAll(index => index.team == this._adverbial_verb && index.kind == this._adverb);
                foreach (Word adverb_verb in adverbs_verbs)
                {
                    foreach (Word verb in verbs)
                    {
                        terms.Add(verb);
                        terms.Add(adverb_verb);
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> adverbs_adverbs_verbs = new List<Word>();
                adverbs_adverbs_verbs = words.FindAll(index => index.team == this._adverbial_verb && index.kind == this._adverb_adverb);
                foreach (Word adverb_adverb_verb in adverbs_adverbs_verbs)
                {
                    foreach (Word adverb_verb in adverbs_verbs)
                    {
                        foreach (Word verb in verbs)
                        {
                            terms.Add(verb);
                            terms.Add(adverb_verb);
                            terms.Add(adverb_adverb_verb);
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> Adnominal(List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation adnominal \"Grammar\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                Lesson lesson = new Lesson();
                List<Word> terms = new List<Word>();

                List<Word> pronouns = new List<Word>();
                pronouns = words.FindAll(index => index.team != this._adnominal_adjunct && index.kind == this._pronoun);
                foreach (Word pronoun in pronouns)
                {
                    terms.Add(pronoun);
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> nouns = new List<Word>();
                nouns = words.FindAll(index => index.team == this._adnominal_adjunct && index.kind == this._noun);
                foreach (Word noun in nouns)
                {
                    terms.Add(noun);
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> numerals = new List<Word>();
                numerals = words.FindAll(index => index.team == this._adnominal_adjunct && index.kind == this._numeral);
                foreach (Word numeral in numerals)
                {
                    foreach (Word noun in nouns)
                    {
                        terms.Add(numeral);
                        terms.Add(noun);
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> articles = new List<Word>();
                articles = words.FindAll(index => index.team == this._adnominal_adjunct && index.kind == this._article);
                foreach (Word article in articles)
                {
                    foreach (Word noun in nouns)
                    {
                        terms.Add(article);
                        terms.Add(noun);
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> pronouns_adnominals = new List<Word>();
                pronouns_adnominals = words.FindAll(index => index.team == this._adnominal_adjunct && index.kind == this._pronoun);
                foreach (Word pronoun in pronouns_adnominals)
                {
                    foreach (Word noun in nouns)
                    {
                        terms.Add(pronoun);
                        terms.Add(noun);
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> adjectives = new List<Word>();
                adjectives = words.FindAll(index => index.team == this._adnominal_adjunct && index.kind == this._adjective);
                foreach (Word adjective in adjectives)
                {
                    foreach (Word noun in nouns)
                    {
                        terms.Add(adjective);
                        terms.Add(noun);
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                foreach (Word numeral in numerals)
                {
                    foreach (Word adjective in adjectives)
                    {
                        foreach (Word noun in nouns)
                        {
                            terms.Add(numeral);
                            terms.Add(adjective);
                            terms.Add(noun);
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                foreach (Word article in articles)
                {
                    foreach (Word adjective in adjectives)
                    {
                        foreach (Word noun in nouns)
                        {
                            terms.Add(article);
                            terms.Add(adjective);
                            terms.Add(noun);
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                foreach (Word pronoun in pronouns)
                {
                    foreach (Word adjective in adjectives)
                    {
                        foreach (Word noun in nouns)
                        {
                            terms.Add(pronoun);
                            terms.Add(adjective);
                            terms.Add(noun);
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> adverbs = new List<Word>();
                adverbs = words.FindAll(index => index.team == this._adnominal_adjunct && index.kind == this._adverb);
                foreach (Word adverb in adverbs)
                {
                    foreach (Word adjective in adjectives)
                    {
                        foreach (Word noun in nouns)
                        {
                            terms.Add(adjective);
                            terms.Add(adverb);
                            terms.Add(noun);
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                foreach (Word numeral in numerals)
                {
                    foreach (Word adverb in adverbs)
                    {
                        foreach (Word adjective in adjectives)
                        {
                            foreach (Word noun in nouns)
                            {
                                terms.Add(numeral);
                                terms.Add(adjective);
                                terms.Add(adverb);
                                terms.Add(noun);
                            }
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                foreach (Word article in articles)
                {
                    foreach (Word adverb in adverbs)
                    {
                        foreach (Word adjective in adjectives)
                        {
                            foreach (Word noun in nouns)
                            {
                                terms.Add(article);
                                terms.Add(adjective);
                                terms.Add(adverb);
                                terms.Add(noun);
                            }
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                foreach (Word pronoun in pronouns)
                {
                    foreach (Word adverb in adverbs)
                    {
                        foreach (Word adjective in adjectives)
                        {
                            foreach (Word noun in nouns)
                            {
                                terms.Add(pronoun);
                                terms.Add(adjective);
                                terms.Add(adverb);
                                terms.Add(noun);
                            }
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                List<Word> adverbs_adverbs = new List<Word>();
                adverbs_adverbs = words.FindAll(index => index.team == this._adnominal_adjunct && index.kind == this._adverb_adverb);
                foreach (Word adverb_adverb in adverbs_adverbs)
                {
                    foreach (Word adverb in adverbs)
                    {
                        foreach (Word adjective in adjectives)
                        {
                            foreach (Word noun in nouns)
                            {
                                terms.Add(adjective);
                                terms.Add(adverb);
                                terms.Add(adverb_adverb);
                                terms.Add(noun);
                            }
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                foreach (Word numeral in numerals)
                {
                    foreach (Word adverb_adverb in adverbs_adverbs)
                    {
                        foreach (Word adverb in adverbs)
                        {
                            foreach (Word adjective in adjectives)
                            {
                                foreach (Word noun in nouns)
                                {
                                    terms.Add(numeral);
                                    terms.Add(adjective);
                                    terms.Add(adverb);
                                    terms.Add(adverb_adverb);
                                    terms.Add(noun);
                                }
                            }
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                foreach (Word article in articles)
                {
                    foreach (Word adverb_adverb in adverbs_adverbs)
                    {
                        foreach (Word adverb in adverbs)
                        {
                            foreach (Word adjective in adjectives)
                            {
                                foreach (Word noun in nouns)
                                {
                                    terms.Add(article);
                                    terms.Add(adjective);
                                    terms.Add(adverb);
                                    terms.Add(adverb_adverb);
                                    terms.Add(noun);
                                }
                            }
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }

                lesson = new Lesson();
                terms = new List<Word>();
                foreach (Word pronoun in pronouns)
                {
                    foreach (Word adverb_adverb in adverbs_adverbs)
                    {
                        foreach (Word adverb in adverbs)
                        {
                            foreach (Word adjective in adjectives)
                            {
                                foreach (Word noun in nouns)
                                {
                                    terms.Add(pronoun);
                                    terms.Add(adjective);
                                    terms.Add(adverb);
                                    terms.Add(adverb_adverb);
                                    terms.Add(noun);
                                }
                            }
                        }
                    }
                }
                if (terms.Count > 0)
                {
                    lesson.lecture = terms;
                    lessons.Add(lesson);
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> Verify(Dictionary<(string, string), int> word_2_vec, List<Lesson> seminars)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify \"Grammar\" service failed!");

                List<Word> words = new List<Word>();
                bool last = false;

                foreach (Lesson seminar in seminars)
                {
                    Word before = new Word();
                    foreach (Word item in seminar.lecture)
                    {
                        last = false;
                        Word word = new Word();
                        word = item;
                        if (before.term == null)
                        {
                            before = word;
                            last = true;
                            continue;
                        }
                        bool similarity = this._wordEmbeddingService.Similarity(word_2_vec, before.term, word.term);
                        if (!similarity) break;
                        words.Add(before);
                        before = word;
                        last = true;
                    }
                    if (last)
                    {
                        words.Add(before);
                    }
                }
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> Subject(Dictionary<(string, string), int> word_2_vec, List<Word> seminars)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation subject \"Grammar\" service failed!");

                List<Word> subjects = seminars.FindAll(index => index.sentence == this._subject);
                List<Word> conjunctions = subjects.FindAll(index => index.kind == this._conjunction);

                List<Lesson> terms = new List<Lesson>();
                if (conjunctions.Count == 0)
                {
                    terms = Adnominal(subjects);
                }
                else
                {
                    List<Lesson> firsts = Adnominal(subjects);
                    List<Lesson> lasts = Adnominal(subjects);
                    terms = Conjunction(firsts, lasts, conjunctions);
                }

                List<Word> words = new List<Word>();
                words = Verify(word_2_vec, terms);
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> Predicate(Dictionary<(string, string), int> word_2_vec, List<Word> seminars)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation subject \"Grammar\" service failed!");

                List<Word> predicates = seminars.FindAll(index => index.sentence == this._predicate);
                List<Word> conjunctions = predicates.FindAll(index => index.kind == this._conjunction);

                List<Lesson> lessons = new List<Lesson>();

                int first = predicates.OrderBy(index => index.order).First().order;
                int order = predicates.OrderBy(index => index.order).Last().order;
                for (int quantity = first; quantity <= order; quantity++)
                {
                    List<Lesson> terms = new List<Lesson>();
                    List<Lesson> firsts = new List<Lesson>();
                    List<Lesson> lasts = new List<Lesson>();
                    if (conjunctions.Count == 0)
                    {
                        terms = Adnominal(predicates);
                        terms = Adverbial(predicates);
                    }
                    else
                    {
                        List<Lesson> locals = new List<Lesson>();
                        locals = Adnominal(predicates);
                        foreach (Lesson item in locals)
                        {
                            firsts.Add(item);
                        }
                        locals = Adverbial(predicates);
                        foreach (Lesson item in locals)
                        {
                            firsts.Add(item);
                        }
                        locals = Numeral(predicates);
                        foreach (Lesson item in locals)
                        {
                            firsts.Add(item);
                        }
                        locals = Adnominal(predicates);
                        foreach (Lesson item in locals)
                        {
                            lasts.Add(item);
                        }
                        locals = Adverbial(predicates);
                        foreach (Lesson item in locals)
                        {
                            lasts.Add(item);
                        }
                        locals = Numeral(predicates);
                        foreach (Lesson item in locals)
                        {
                            lasts.Add(item);
                        }
                        terms = Conjunction(firsts, lasts, conjunctions);
                    }
                    foreach (Lesson item in terms)
                    {
                        lessons.Add(item);
                    }
                }
                List<Word> words = new List<Word>();
                words = Verify(word_2_vec, lessons);
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> Oration(Dictionary<(string, string), int> word_2_vec, List<Word> lessons)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation oration \"Grammar\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> subjects = new List<Word>();
                List<Word> predicates = new List<Word>();
                subjects = Subject(word_2_vec, lessons);
                foreach (Word subject in subjects)
                {
                    words.Add(subject);
                }
                predicates = Predicate(word_2_vec, lessons);
                foreach (Word predicate in predicates)
                {
                    words.Add(predicate);
                }
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public string Oration(List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation oration \"Grammar\" service failed!");

                string word = string.Empty;
                if (words.Count == 0) return word;
                words.OrderBy(index => index.order);
                List<Word> terms = new List<Word>();
                terms = words;
                foreach (Word item in terms)
                {
                    if (word != string.Empty) word += " ";
                    word += item.term;
                }
                return word;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Word>? NovelSHA256(string language, List<Sentenca> sentences, List<Lesson> lessons)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation novel sha 256 \"Grammar\" service failed!");

                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                Dictionary<(byte[], byte[]), int> word_2_vec_sha256 = this._wordEmbeddingService.Word2VecSHA256(sentences, vocabulary);

                List<Tutorial> tutorials = new List<Tutorial>();
                tutorials = this._wordEmbeddingService.EncodeLesson(lessons, vocabulary);

                List<Tutorial> seminars = new List<Tutorial>();
                seminars = MountOrationSample(tutorials, word_2_vec_sha256);

                this._novel_english = seminars;

                List<Lesson> guides = new List<Lesson>();
                guides = this._wordEmbeddingService.DecodeLesson(seminars, vocabulary);

                SetBook(language, guides);

                List<Word> words = new List<Word>();
                foreach (Lesson guide in guides)
                {
                    words = Oration(word_2_vec, guide.lecture);
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

        public List<Word> Syntax(string language, List<Word> terms, bool reverse)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation syntax \"Grammar\" service failed!");

                List<Word> words = new List<Word>();
                List<Lesson> lessons = SelectBook(language).OrderBy(index => index.order).Distinct().ToList();

                List<Sentenca> sentences = SelectSentence(language).Distinct().ToList();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);

                if (reverse) lessons.Reverse();
                bool next = false;
                int count_foreach = 0;
                foreach (Lesson lesson in lessons)
                {
                    if (!next)
                    {
                        string term = Oration(terms);
                        string word = Oration(lesson.lecture);
                        if (term == word)
                            next = true;
                    }
                    else
                    {
                        words = Oration(word_2_vec, lesson.lecture);
                        if (words != null)
                        {
                            string word = Oration(words);
                            string term = Oration(terms);
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

        public List<Word> Syntax(string language, Materia lesson, List<Materia> book)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount syntax \"Grammar\" service failed!");

                List<Sentenca> sentence = SelectSentence(language).Distinct().ToList();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentence);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentence);

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

                List<Lesson> mount_pronoun = this._morphologyService.GetPronoun(vocabulary, list_pronoun);
                List<Lesson> mount_conjunction = this._morphologyService.GetConjunction(vocabulary, list_conjunction);
                List<Lesson> mount_numeral = this._morphologyService.GetNumeral(vocabulary, list_numeral);
                List<Lesson> mount_article = this._morphologyService.GetArticle(vocabulary, list_article);
                List<Lesson> mount_preposition = this._morphologyService.GetPreposition(vocabulary, list_preposition);

                List<Lesson> adnominal_adjunct = this._morphologyService.GetAdnominalAdjunct(word_2_vec, vocabulary, list_noun, list_pronoun, list_article, list_numeral, list_adjective, list_adverb);
                List<Lesson> adverbial_adjunct_verb = this._morphologyService.GetAdverbialAdjunct(word_2_vec, vocabulary, list_model, list_verb, list_adverb);
                List<Lesson> adverbial_adjunct_adjective = this._morphologyService.GetAdverbialAdjunct(word_2_vec, vocabulary, list_adjective, list_adverb);

                List<Lesson> matters = Union(adnominal_adjunct, adverbial_adjunct_verb);
                matters = Union(matters, adverbial_adjunct_adjective);
                matters = Union(matters, mount_pronoun);
                matters = Union(matters, mount_conjunction);
                matters = Union(matters, mount_numeral);
                matters = Union(matters, mount_article);
                matters = Union(matters, mount_preposition);

                List<Word>? words = new List<Word>();

                words = NovelSHA256(language, sentence, matters);

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
