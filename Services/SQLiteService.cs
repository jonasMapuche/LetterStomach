using LetterStomach.Enums;
using LetterStomach.Models;
using LetterStomach.Repositories;
using LetterStomach.Repositories.SQLites;
using LetterStomach.Services.Interfaces;
using SQLite;
using Environment = System.Environment;

namespace LetterStomach.Services
{
    public class SQLiteService : ISQLiteService
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
        public List<Circunstancia> Circunstancia { get; set; }
        public List<Estoutro> Estoutro { get; set; }
        public List<Preceito> Preceito { get; set; }
        public List<Algarismo> Algarismo { get; set; }
        public List<Juncao> Juncao { get; set; }
        public List<Materia> Materia { get; set; }
        public List<Elocucao> Elocucao { get; set; }
        public List<Sentenca> Sentenca { get; set; }
        public List<Ligacao> Ligacao { get; set; }
        public List<Assistente> Assistente { get; set; }

        private static SQLiteAsyncConnection _database;
        private const string FILE_SQLITE = "letter.db";
        private string file_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FILE_SQLITE);
        private const int QUANTITY_SQLITE = 12;

        private readonly IAdverbioRepository _adverbioRepository;
        private readonly IArtigoRepository _artigoRepository;
        private readonly IAuxiliarRepository _auxiliarRepository;
        private readonly IConjuncaoRepository _conjuncaoRepository;
        private readonly ISubstantivoRepository _substantivoRepository;
        private readonly IAdjetivoRepository _adjetivoRepository;
        private readonly IModelRepository _modelRepository;
        private readonly INumeralRepository _numeroRepository;
        private readonly IPreposicaoRepository _preposicaoRepository;
        private readonly IPronomeRepository _pronomeRepository;
        private readonly IDitadoRepository _ditadoRepository;
        private readonly IVerboRepository _verboRepository;

        private IHttpService _httpService;
        private IModelService _modelService;
        private SettingService _settingService;
        #endregion

        #region CONSTRUCTOR
        public SQLiteService()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"SQLite\" service failed!");
                else this.error_message = string.Empty;

                this._httpService = new HttpService();
                this._modelService = new ModelService();

                this._settingService = SettingService.Instance;

                Connect();

                this._adverbioRepository = new AdverbioRepository(_database);
                this._artigoRepository = new ArtigoRepository(_database);
                this._auxiliarRepository = new AuxiliarRepository(_database);
                this._conjuncaoRepository = new ConjuncaoRepository(_database);
                this._substantivoRepository = new SubstantivoRepository(_database);
                this._adjetivoRepository = new AdjetivoRepository(_database);
                this._modelRepository = new ModelRepository(_database);
                this._numeroRepository = new NumeralRepository(_database);
                this._preposicaoRepository = new PreposicaoRepository(_database);
                this._pronomeRepository = new PronomeRepository(_database);
                this._ditadoRepository = new DitadoRepository(_database);
                this._verboRepository = new VerboRepository(_database);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region CONNECT
        private void Connect()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect \"SQLite\" service failed!");

                _database = new SQLiteAsyncConnection(file_path);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region EXIST
        public void Exist()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation exit \"SQLite\" service failed!");

                bool exist = File.Exists(file_path);
                bool connect = (_database is not null) ? true : false;
                int quantity = 0;
                if (exist && connect)
                {
                    quantity += this._adverbioRepository.Exist();
                    quantity += this._artigoRepository.Exist();
                    quantity += this._auxiliarRepository.Exist();
                    quantity += this._conjuncaoRepository.Exist();
                    quantity += this._substantivoRepository.Exist();
                    quantity += this._adjetivoRepository.Exist();
                    quantity += this._modelRepository.Exist();
                    quantity += this._numeroRepository.Exist();
                    quantity += this._preposicaoRepository.Exist();
                    quantity += this._pronomeRepository.Exist();
                    quantity += this._ditadoRepository.Exist();
                    quantity += this._verboRepository.Exist();
                    if (quantity == QUANTITY_SQLITE) this._settingService.SQLiteDatabase = true;
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<bool> ExistAsync()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation exit async \"SQLite\" service failed!");

                bool exist = File.Exists(file_path);
                bool connect = (_database is not null) ? true : false;
                int quantity = 0;
                if (exist && connect)
                {
                    quantity += await this._adverbioRepository.ExistAsync();
                    quantity += await this._artigoRepository.ExistAsync();
                    quantity += await this._auxiliarRepository.ExistAsync();
                    quantity += await this._conjuncaoRepository.ExistAsync();
                    quantity += await this._substantivoRepository.ExistAsync();
                    quantity += await this._adjetivoRepository.ExistAsync();
                    quantity += await this._modelRepository.ExistAsync();
                    quantity += await this._numeroRepository.ExistAsync();
                    quantity += await this._preposicaoRepository.ExistAsync();
                    quantity += await this._pronomeRepository.ExistAsync();
                    quantity += await this._ditadoRepository.ExistAsync();
                    quantity += await this._verboRepository.ExistAsync();
                    if (quantity == QUANTITY_SQLITE) return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region DELETE
        public async Task<int> Delete(int select_table, bool select_all)
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation delete \"SQLite\" service failed!");

                int quantity = 0;
                if ((select_table == (int)Hunk.Adverb) || (select_all))
                    quantity += await this._adjetivoRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Pronoun) || (select_all))
                    quantity += await this._pronomeRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Article) || (select_all))
                    quantity += await this._artigoRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Numeral) || (select_all))
                    quantity += await this._numeroRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Preposition) || (select_all))
                    quantity += await this._preposicaoRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Noun) || (select_all))
                    quantity += await this._substantivoRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Adjective) || (select_all))
                    quantity += await this._adjetivoRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Verb) || (select_all))
                    quantity += await this._verboRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Sentence) || (select_all))
                    quantity += await this._ditadoRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Conjunction) || (select_all))
                    quantity += await this._conjuncaoRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Auxiliary) || (select_all))
                    quantity += await this._auxiliarRepository.DeleteAll(); 
                if ((select_table == (int)Hunk.Model) || (select_all))
                    quantity += await this._modelRepository.DeleteAll(); 
                return quantity;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region CREATE
        public async Task Create(int select_table, bool select_all)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation create \"SQLite\" service failed!");

                if ((select_table == (int)Hunk.Adverb) || (select_all))
                {
                    this._adverbioRepository.CreateTable();
                    List<Adverbios> adverb = new List<Adverbios>();
                    await this._adverbioRepository.Add(adverb);
                }
                if (select_table == (int)Hunk.Adjective)
                {
                    this._adjetivoRepository.CreateTable();
                    List<Adjetivo> adjective = new List<Adjetivo>();
                    await this._adjetivoRepository.Add(adjective);
                }
                if ((select_table == (int)Hunk.Article) || (select_all))
                {
                    this._artigoRepository.CreateTable();
                    List<Artigos> article = new List<Artigos>();
                    await this._artigoRepository.Add(article);
                }
                if ((select_table == (int)Hunk.Numeral) || (select_all))
                {
                    this._numeroRepository.CreateTable(); 
                    List<Numerais> numeral = new List<Numerais>();
                    await this._numeroRepository.Add(numeral);
                }
                if ((select_table == (int)Hunk.Preposition) || (select_all))
                {
                    this._preposicaoRepository.CreateTable(); 
                    List<Preposicoes> preposition = new List<Preposicoes>();
                    await this._preposicaoRepository.Add(preposition);
                }
                if ((select_table == (int)Hunk.Pronoun) || (select_all))
                {
                    this._pronomeRepository.CreateTable();
                    List<Pronomes> pronoun = new List<Pronomes>();
                    await this._pronomeRepository.Add(pronoun);
                }
                if ((select_table == (int)Hunk.Noun) || (select_all))
                {
                    this._substantivoRepository.CreateTable();
                    List<Substantivo> noun = new List<Substantivo>();
                    await this._substantivoRepository.Add(noun);
                }
                if ((select_table == (int)Hunk.Verb) || (select_all))
                {
                    this._verboRepository.CreateTable();
                    List<Verbos> verb = new List<Verbos>();
                    await this._verboRepository.Add(verb);
                }
                if ((select_table == (int)Hunk.Sentence) || (select_all))
                {
                    this._ditadoRepository.CreateTable();
                    List<Sentencas> sentence = new List<Sentencas>();
                    await this._ditadoRepository.Add(sentence);
                }
                if ((select_table == (int)Hunk.Conjunction) || (select_all))
                {
                    this._conjuncaoRepository.CreateTable();
                    List<Conjuncoes> conjunction = new List<Conjuncoes>();
                    await _conjuncaoRepository.Add(conjunction);
                }
                if ((select_table == (int)Hunk.Auxiliary) || (select_all))
                {
                    this._auxiliarRepository.CreateTable();
                    List<Auxiliares> auxiliary = new List<Auxiliares>();
                    await this._auxiliarRepository.Add(auxiliary);
                }
                if ((select_table == (int)Hunk.Model) || (select_all))
                {
                    this._modelRepository.CreateTable();
                    List<Model> model = new List<Model>();
                    await this._modelRepository.Add(model);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region INSERT
        public async Task InsertAdverb()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert adverb \"SQLite\" service failed!");

                List<Adverbios> adverb = new List<Adverbios>();
                adverb = await this._httpService.HttpAdverb();
                await this._adverbioRepository.Add(adverb);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertPronoun()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert pronoun \"SQLite\" service failed!");

                List<Pronomes> pronoun = new List<Pronomes>();
                pronoun = await this._httpService.HttpPronoun();
                await this._pronomeRepository.Add(pronoun);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertArticle()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert article \"SQLite\" service failed!");

                List<Artigos> article = new List<Artigos>();
                article = await this._httpService.HttpArticle();
                await this._artigoRepository.Add(article);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertNumeral()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert numeral \"SQLite\" service failed!");

                List<Numerais> numeral = new List<Numerais>();
                numeral = await this._httpService.HttpNumeral();
                await this._numeroRepository.Add(numeral);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertPreposition()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert preposition \"SQLite\" service failed!");

                List<Preposicoes> preposition = new List<Preposicoes>();
                preposition = await this._httpService.HttpPreposition();
                await this._preposicaoRepository.Add(preposition);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertNoun()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert noun \"SQLite\" service failed!");

                List<Substantivo> noun = new List<Substantivo>();
                noun = await this._httpService.HttpNoun();
                await this._substantivoRepository.Add(noun);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertModel()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert model \"SQLite\" service failed!");

                List<Model> model = new List<Model>();
                model = await this._httpService.HttpModel();
                await this._modelRepository.Add(model);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertAdjective()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert adjective \"SQLite\" service failed!");

                List<Adjetivo> adjective = new List<Adjetivo>();
                adjective = await this._httpService.HttpAdjective();
                await this._adjetivoRepository.Add(adjective);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertVerb()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert verb \"SQLite\" service failed!");

                List<Verbos> verb = new List<Verbos>();
                verb = await this._httpService.HttpVerb();
                await this._verboRepository.Add(verb);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertSentence()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert sentence \"SQLite\" service failed!");

                List<Sentencas> sentence = new List<Sentencas>();
                sentence = await this._httpService.HttpSentence();
                await this._ditadoRepository.Add(sentence);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertConjunction()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert conjunction \"SQLite\" service failed!");

                List<Conjuncoes> conjunction = new List<Conjuncoes>();
                conjunction = await this._httpService.HttpConjunction();
                await this._conjuncaoRepository.Add(conjunction);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task InsertAuxiliary()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert auxiliary \"SQLite\" service failed!");

                List<Auxiliares> auxiliary = new List<Auxiliares>();
                auxiliary = await this._httpService.HttpAuxiliary();
                await this._auxiliarRepository.Add(auxiliary);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region LOAD
        public async Task LoadAdverb()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load adverb \"SQLite\" service failed!");

                List<Adverbios> adverb = new List<Adverbios>();
                adverb = await this._adverbioRepository.GetAll();
                this.Circunstancia = await this._modelService.LoadAdverb(adverb);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task LoadPronoun()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load pronoun \"SQLite\" service failed!");

                List<Pronomes> pronoun = new List<Pronomes>();
                pronoun = await this._pronomeRepository.GetAll();
                this.Estoutro = await this._modelService.LoadPronoun(pronoun);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task LoadArticle()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load article \"SQLite\" service failed!");

                List<Artigos> article = new List<Artigos>();
                article = await this._artigoRepository.GetAll();
                this.Preceito = await this._modelService.LoadArticle(article);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task LoadNumeral()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load numeral \"SQLite\" service failed!");

                List<Numerais> numeral = new List<Numerais>();
                numeral = await this._numeroRepository.GetAll();
                this.Algarismo = await this._modelService.LoadNumeral(numeral);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task LoadPreposition()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load preposition \"SQLite\" service failed!");

                List<Preposicoes> preposition = new List<Preposicoes>();
                preposition = await this._preposicaoRepository.GetAll();
                this.Juncao = await this._modelService.LoadPreposition(preposition);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task LoadLetter()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load letter \"SQLite\" service failed!");

                List<Substantivo> noun = new List<Substantivo>();
                noun = await this._substantivoRepository.GetAll();
                List<Adjetivo> adjective = new List<Adjetivo>();
                adjective = await this._adjetivoRepository.GetAll();
                List<Model> model = new List<Model>();
                model = await this._modelRepository.GetAll();
                this.Materia = await this._modelService.LoadMateria(noun, adjective, model); 
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task LoadVerb()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load verb \"SQLite\" service failed!");

                List<Verbos> verb = new List<Verbos>();
                verb = await this._verboRepository.GetAll();
                this.Elocucao = await this._modelService.LoadElocucao(verb);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task LoadSentence()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load sentence \"SQLite\" service failed!");

                List<Sentencas> sentence = new List<Sentencas>();
                sentence = await this._ditadoRepository.GetAll();
                this.Sentenca = await this._modelService.LoadSentenca(sentence);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task LoadConjunction()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load conjunction \"SQLite\" service failed!");

                List<Conjuncoes> conjunction = new List<Conjuncoes>();
                conjunction = await this._conjuncaoRepository.GetAll();
                this.Ligacao = await this._modelService.LoadLigacao(conjunction);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        
        public async Task LoadAuxiliary()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load auxiliary \"SQLite\" service failed!");

                List<Auxiliares> auxiliary = new List<Auxiliares>();
                auxiliary = await this._auxiliarRepository.GetAll();
                this.Assistente = await this._modelService.LoadAssistente(auxiliary);
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
