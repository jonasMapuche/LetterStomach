using LetterStomach.Models;
using SQLite;

namespace LetterStomach.Repositories.SQLites
{
    public class LigacaoRepository : ILigacaoRepository
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

        public event EventHandler<string>? OnError;
        #endregion

        #region VARIABLE
        private readonly List<Ligacao>? _ligacao;

        private readonly SQLiteAsyncConnection? _database;
        #endregion

        #region CONSTRUCTOR
        public LigacaoRepository(List<Ligacao> ligacao)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Conjunction\" repository failed!");
                else this._error_message = string.Empty;

                this._ligacao = ligacao;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public LigacaoRepository(SQLiteAsyncConnection database)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Conjunction\" repository failed!");
                else this._error_message = string.Empty;

                this._database = database;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<Ligacao> FilterLanguage(List<Ligacao>? list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Conjunction\" repository failed!");

                List<Ligacao> new_list = new List<Ligacao>();
                if (list == null) return new_list;
                list.ForEach(value =>
                {
                    if (value.linguagem == language)
                        new_list.Add(value);
                });
                return new_list;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Ligacao> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Conjunction\" repository failed!");

                return FilterLanguage(this._ligacao, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Ligacao>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Conjunction\" repository failed!");

                return FilterLanguage(this._ligacao, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region CRUD SQLITE
        public async Task<List<Conjuncoes>> GetAll()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get all \"Conjunction\" repository failed!");

                return await this._database.Table<Conjuncoes>().ToListAsync();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<int> Add(List<Conjuncoes> conjunction)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation add \"Conjunction\" repository failed!");

                return await this._database.InsertAllAsync(conjunction);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async void CreateTable()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation create table \"Conjunction\" repository failed!");

                await this._database.CreateTableAsync<Conjuncoes>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<int> DeleteAll()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation delete all \"Conjunction\" repository failed!");

                return await this._database.DeleteAllAsync<Conjuncoes>();
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
