using LetterStomach.Models;
using SQLite;

namespace LetterStomach.Repositories.SQLites
{
    public class DitadoRepository : IDitadoRepository
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
        public DitadoRepository(SQLiteAsyncConnection database)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Sentenca\" view model failed!");
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
        public async Task<List<Sentencas>> GetAll()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get all \"Ditado\" repository failed!");

                return await this._database.Table<Sentencas>().ToListAsync();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<int> Add(List<Sentencas> sentenca)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation add \"Ditado\" repository failed!");

                return await this._database.InsertAllAsync(sentenca);
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
                if (this._error_off) throw new InvalidOperationException("Operation create table \"Ditado\" repository failed!");

                await this._database.CreateTableAsync<Sentencas>();
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
                if (this._error_off) throw new InvalidOperationException("Operation delete all \"Ditado\" repository failed!");

                return await this._database.DeleteAllAsync<Sentencas>();
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
                if (this._error_off) throw new InvalidOperationException("Operation drop table \"Ditado\" repository failed!");

                return await this._database.DropTableAsync<Sentencas>();
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
                if (this._error_off) throw new InvalidOperationException("Operation exist async \"Ditado\" repository failed!");

                string sql = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Sentencas'";
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
                if (this._error_off) throw new InvalidOperationException("Operation exist \"Ditado\" repository failed!");

                string sql = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Sentencas'";
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
