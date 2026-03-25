using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Repositories.MongoDBs
{
    public class ElocucaoRepository : IElocucaoRepository
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
        private readonly IMongoCollection<Elocucao> _collection;
        #endregion

        #region CONSTRUCTOR
        public ElocucaoRepository()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Verb\" view model failed!");
                else this._error_message = string.Empty;

                string connection = "mongodb://berthazatz:freedown@ac-folgaxm-shard-00-00.q3qzht9.mongodb.net:27017,ac-folgaxm-shard-00-01.q3qzht9.mongodb.net:27017,ac-folgaxm-shard-00-02.q3qzht9.mongodb.net:27017/?ssl=true&replicaSet=atlas-11bsn2-shard-0&authSource=admin&appName=verb";
                string database = "stomach";
                string collection = "verb";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Elocucao> ConfigurationValue = mongoDatabase.GetCollection<Elocucao>(collection);
                this._collection = mongoDatabase.GetCollection<Elocucao>(collection);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Elocucao> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Verb\" view model failed!");

                return this._collection.Find(index => index.linguagem == language).ToList<Elocucao>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Elocucao>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Verb\" view model failed!");

                return await this._collection.Find(index => index.linguagem == language).ToListAsync<Elocucao>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Elocucao> GetModel(string language, string model)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get model \"Verb\" view model failed!");

                return this._collection.Find(index => index.linguagem == language && index.modelo == model).ToList<Elocucao>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Elocucao>> GetModelAsync(string language, string model)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get model async \"Verb\" view model failed!");

                return await this._collection.Find(index => index.linguagem == language && index.modelo == model).ToListAsync<Elocucao>();
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
