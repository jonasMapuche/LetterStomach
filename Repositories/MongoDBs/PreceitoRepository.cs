using MongoDB.Driver;
using LetterStomach.Models;

namespace LetterStomach.Repositories.MongoDBs
{
    public class PreceitoRepository : IPreceitoRepository
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
        private readonly IMongoCollection<Preceito> _collection;
        #endregion

        #region CONSTRUCTOR
        public PreceitoRepository()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Article\" view model failed!");
                else this._error_message = string.Empty;

                string connection = "mongodb://berthazatz:freedown@ac-knhxaxk-shard-00-00.rzn652o.mongodb.net:27017,ac-knhxaxk-shard-00-01.rzn652o.mongodb.net:27017,ac-knhxaxk-shard-00-02.rzn652o.mongodb.net:27017/?ssl=true&replicaSet=atlas-amy702-shard-0&authSource=admin&appName=article";
                string database = "stomach";
                string collection = "article";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                this._collection = mongoDatabase.GetCollection<Preceito>(collection);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Preceito> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Article\" view model failed!");

                return this._collection.Find(index => index.linguagem == language).ToList<Preceito>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        public async Task<List<Preceito>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Article\" view model failed!");

                return await this._collection.Find(index => index.linguagem == language).ToListAsync<Preceito>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public Preceito GetName(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Article\" view model failed!");

                return this._collection.Find(index => index.nome == name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<Preceito> GetNameAsync(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name async \"Article\" view model failed!");

                return await this._collection.Find(index => index.nome == name).FirstOrDefaultAsync();
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
