using LetterStomach.Models;
using SQLite;

namespace LetterStomach.Repositories.SQLites  
{
    public class PreposicaoRepository : IPreposicaoRepository
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
        private readonly SQLiteAsyncConnection? _database;
        #endregion

        #region CONSTRUCTOR
        public PreposicaoRepository(SQLiteAsyncConnection database)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Preposicao\" repository failed!");
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

        #region CRUD SQLITE
        public async Task<List<Preposicoes>> GetAll()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get all \"Preposicao\" repository failed!");

                return await this._database.Table<Preposicoes>().ToListAsync();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<int> Add(List<Preposicoes> preposition)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation add \"Preposicao\" repository failed!");

                return await this._database.InsertAllAsync(preposition);
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
                if (this._error_off) throw new InvalidOperationException("Operation create table \"Preposicao\" repository failed!");

                await this._database.CreateTableAsync<Preposicoes>();
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
                if (this._error_off) throw new InvalidOperationException("Operation delete all \"Preposicao\" repository failed!");

                return await this._database.DeleteAllAsync<Preposicoes>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<int> DropTable()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation drop table \"Preposicao\" repository failed!");

                return await this._database.DropTableAsync<Preposicoes>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<int> ExistAsync()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation exist async \"Preposicao\" repository failed!");

                string sql = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Preposicoes'";
                int result = await this._database.ExecuteScalarAsync<int>(sql);
                return result;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public int Exist()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation exist \"Preposicao\" repository failed!");

                string sql = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Preposicoes'";
                int result = this._database.ExecuteScalarAsync<int>(sql).GetAwaiter().GetResult();
                return result;
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
