using LetterStomach.Enums;
using LetterStomach.Models;
using LetterStomach.Services.Interfaces;
using SQLite;

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
        private static SQLiteAsyncConnection _database;

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

        private static string FILE_SQLITE = "letter.db";

        private IHttpService _httpService;
        private IModelService _modelService;
        #endregion

        #region CONSTRUCTOR
        public SQLiteService()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation contructor \"SQLite\" service failed!");
                else this.error_message = string.Empty;

                _httpService = new HttpService();
                _modelService = new ModelService();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
            }
        }
        #endregion

        #region CONNECT
        public void Connect()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect \"SQLite\" service failed!");

                string DataBasePach = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "letter.db");
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, FILE_SQLITE);
                _database = new SQLiteAsyncConnection(DataBasePach);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion

        #region DELETE
        public async Task<int> DeleteAll()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation delete all \"SQLite\" service failed!");

                int quantity = 0;
                quantity += await _database.DeleteAllAsync<Adverbios>();
                quantity += await _database.DeleteAllAsync<Pronomes>();
                quantity += await _database.DeleteAllAsync<Artigos>();
                quantity += await _database.DeleteAllAsync<Numerais>();
                quantity += await _database.DeleteAllAsync<Preposicoes>();
                quantity += await _database.DeleteAllAsync<Substantivo>();
                quantity += await _database.DeleteAllAsync<Adjetivo>();
                quantity += await _database.DeleteAllAsync<Verbos>();
                quantity += await _database.DeleteAllAsync<Sentencas>();
                quantity += await _database.DeleteAllAsync<Conjuncoes>();
                quantity += await _database.DeleteAllAsync<Auxiliares>();
                return quantity;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return -1;
            }
        }

        public async Task<int> Delete(int select_table)
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation delete \"SQLite\" service failed!");

                int quantity = 0;
                if (select_table == (int)Hunk.Adverb) quantity += await _database.DeleteAllAsync<Adverbios>();
                if (select_table == (int)Hunk.Pronoun) quantity += await _database.DeleteAllAsync<Pronomes>();
                if (select_table == (int)Hunk.Article) quantity += await _database.DeleteAllAsync<Artigos>();
                if (select_table == (int)Hunk.Numeral) quantity += await _database.DeleteAllAsync<Numerais>();
                if (select_table == (int)Hunk.Preposition) quantity += await _database.DeleteAllAsync<Preposicoes>();
                if (select_table == (int)Hunk.Noun) quantity += await _database.DeleteAllAsync<Substantivo>();
                if (select_table == (int)Hunk.Adjective) quantity += await _database.DeleteAllAsync<Adjetivo>();
                if (select_table == (int)Hunk.Verb) quantity += await _database.DeleteAllAsync<Verbos>();
                if (select_table == (int)Hunk.Sentence) quantity += await _database.DeleteAllAsync<Sentencas>();
                if (select_table == (int)Hunk.Conjunction) quantity += await _database.DeleteAllAsync<Conjuncoes>();
                if (select_table == (int)Hunk.Auxiliary) quantity += await _database.DeleteAllAsync<Auxiliares>();
                return quantity;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return -1;
            }
        }
        #endregion

        #region CREATE
        public async Task CreateAll()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation create all \"SQLite\" service failed!");

                await _database.CreateTableAsync<Adverbios>();
                List<Adverbios> adverb = new List<Adverbios>();
                await _database.InsertAllAsync(adverb);
                await _database.CreateTableAsync<Pronomes>();
                List<Pronomes> pronoun = new List<Pronomes>();
                await _database.InsertAllAsync(pronoun);
                await _database.CreateTableAsync<Artigos>();
                List<Artigos> article = new List<Artigos>();
                await _database.InsertAllAsync(article);
                await _database.CreateTableAsync<Numerais>();
                List<Numerais> numeral = new List<Numerais>();
                await _database.InsertAllAsync(numeral);
                await _database.CreateTableAsync<Preposicoes>();
                List<Preposicoes> preposition = new List<Preposicoes>();
                await _database.InsertAllAsync(preposition);
                await _database.CreateTableAsync<Substantivo>();
                List<Substantivo> noun = new List<Substantivo>();
                await _database.InsertAllAsync(noun);
                await _database.CreateTableAsync<Adjetivo>();
                List<Adjetivo> adjective = new List<Adjetivo>();
                await _database.InsertAllAsync(adjective);
                await _database.CreateTableAsync<Verbos>();
                List<Verbos> verb = new List<Verbos>();
                await _database.InsertAllAsync(verb);
                await _database.CreateTableAsync<Sentencas>();
                List<Sentencas> sentence = new List<Sentencas>();
                await _database.InsertAllAsync(sentence);
                await _database.CreateTableAsync<Conjuncoes>();
                List<Conjuncoes> conjunction = new List<Conjuncoes>();
                await _database.InsertAllAsync(conjunction);
                await _database.CreateTableAsync<Auxiliares>();
                List<Auxiliares> auxiliary = new List<Auxiliares>();
                await _database.InsertAllAsync(auxiliary);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task Create(int select_table)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation create \"SQLite\" service failed!");

                if (select_table == (int)Hunk.Adverb) await _database.CreateTableAsync<Adverbios>();
                if (select_table == (int)Hunk.Pronoun) await _database.CreateTableAsync<Pronomes>();
                if (select_table == (int)Hunk.Article) await _database.CreateTableAsync<Artigos>();
                if (select_table == (int)Hunk.Numeral) await _database.CreateTableAsync<Numerais>();
                if (select_table == (int)Hunk.Preposition) await _database.CreateTableAsync<Preposicoes>();
                if (select_table == (int)Hunk.Noun) await _database.CreateTableAsync<Substantivo>();
                if (select_table == (int)Hunk.Adjective) await _database.CreateTableAsync<Adjetivo>();
                if (select_table == (int)Hunk.Verb) await _database.CreateTableAsync<Verbos>();
                if (select_table == (int)Hunk.Sentence) await _database.CreateTableAsync<Sentencas>();
                if (select_table == (int)Hunk.Conjunction) await _database.CreateTableAsync<Conjuncoes>();
                if (select_table == (int)Hunk.Auxiliary) await _database.CreateTableAsync<Auxiliares>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
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
                await _database.InsertAllAsync(adverb);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task InsertPronoun()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert pronoun \"SQLite\" service failed!");

                List<Pronomes> pronoun = new List<Pronomes>();
                pronoun = await this._httpService.HttpPronoun();
                await _database.InsertAllAsync(pronoun);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task InsertArticle()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert article \"SQLite\" service failed!");

                List<Artigos> article = new List<Artigos>();
                article = await this._httpService.HttpArticle();
                await _database.InsertAllAsync(article);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task InsertNumeral()
        {
            try
            {
                List<Numerais> numeral = new List<Numerais>();
                numeral = await this._httpService.HttpNumeral();
                await _database.InsertAllAsync(numeral);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task InsertPreposition()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert preposition \"SQLite\" service failed!");

                List<Preposicoes> preposition = new List<Preposicoes>();
                preposition = await this._httpService.HttpPreposition();
                await _database.InsertAllAsync(preposition);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task InsertNoun()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert noun \"SQLite\" service failed!");

                List<Substantivo> noun = new List<Substantivo>();
                noun = await this._httpService.HttpNoun();
                await _database.InsertAllAsync(noun);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task InsertAdjective()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert adjective \"SQLite\" service failed!");

                List<Adjetivo> adjective = new List<Adjetivo>();
                adjective = await this._httpService.HttpAdjective();
                await _database.InsertAllAsync(adjective);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task InsertVerb()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert verb \"SQLite\" service failed!");

                List<Verbos> verb = new List<Verbos>();
                verb = await this._httpService.HttpVerb();
                await _database.InsertAllAsync(verb);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task InsertSentence()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert sentence \"SQLite\" service failed!");

                List<Sentencas> sentence = new List<Sentencas>();
                sentence = await this._httpService.HttpSentence();
                await _database.InsertAllAsync(sentence);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task InsertConjunction()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert conjunction \"SQLite\" service failed!");

                List<Conjuncoes> conjunction = new List<Conjuncoes>();
                conjunction = await this._httpService.HttpConjunction();
                await _database.InsertAllAsync(conjunction);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task InsertAuxiliary()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert auxiliary \"SQLite\" service failed!");

                List<Auxiliares> auxiliary = new List<Auxiliares>();
                auxiliary = await this._httpService.HttpAuxiliary();
                await _database.InsertAllAsync(auxiliary);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
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
                adverb = await _database.Table<Adverbios>().ToListAsync();
                this.Circunstancia = await this._modelService.LoadAdverb(adverb);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task LoadPronoun()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load pronoun \"SQLite\" service failed!");

                List<Pronomes> pronoun = new List<Pronomes>();
                pronoun = await _database.Table<Pronomes>().ToListAsync();
                this.Estoutro = await this._modelService.LoadPronoun(pronoun);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task LoadArticle()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load article \"SQLite\" service failed!");

                List<Artigos> article = new List<Artigos>();
                article = await _database.Table<Artigos>().ToListAsync();
                this.Preceito = await this._modelService.LoadArticle(article);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task LoadNumeral()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load numeral \"SQLite\" service failed!");

                List<Numerais> numeral = new List<Numerais>();
                numeral = await _database.Table<Numerais>().ToListAsync();
                this.Algarismo = await this._modelService.LoadNumeral(numeral);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task LoadPreposition()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load preposition \"SQLite\" service failed!");

                List<Preposicoes> preposition = new List<Preposicoes>();
                preposition = await _database.Table<Preposicoes>().ToListAsync();
                this.Juncao = await this._modelService.LoadPreposition(preposition);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task LoadLetter()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load letter \"SQLite\" service failed!");

                List<Substantivo> noun = new List<Substantivo>();
                noun = await _database.Table<Substantivo>().ToListAsync();
                List<Adjetivo> adjective = new List<Adjetivo>();
                adjective = await _database.Table<Adjetivo>().ToListAsync();
                this.Materia = await this._modelService.LoadMateria(noun, adjective); 
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task LoadVerb()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load verb \"SQLite\" service failed!");

                List<Verbos> verb = new List<Verbos>();
                verb = await _database.Table<Verbos>().ToListAsync();
                this.Elocucao = await this._modelService.LoadElocucao(verb);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task LoadSentence()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load sentence \"SQLite\" service failed!");

                List<Sentencas> sentence = new List<Sentencas>();
                sentence = await _database.Table<Sentencas>().ToListAsync();
                this.Sentenca = await this._modelService.LoadSentenca(sentence);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task LoadConjunction()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load conjunction \"SQLite\" service failed!");

                List<Conjuncoes> conjunction = new List<Conjuncoes>();
                conjunction = await _database.Table<Conjuncoes>().ToListAsync();
                this.Ligacao = await this._modelService.LoadLigacao(conjunction);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        
        public async Task LoadAuxiliary()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load auxiliary \"SQLite\" service failed!");

                List<Auxiliares> auxiliary = new List<Auxiliares>();
                auxiliary = await _database.Table<Auxiliares>().ToListAsync();
                this.Assistente = await this._modelService.LoadAssistente(auxiliary);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion
    }
}
