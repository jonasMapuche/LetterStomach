using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Repositories.MongoDBs
{
    public class SentencaRepository : ISentencaRepository
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
        private readonly IMongoCollection<Sentenca> _collection;
        #endregion

        #region CONSTRUCTOR
        public SentencaRepository()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Sentence\" view model failed!");
                else this._error_message = string.Empty;

                string connection = "mongodb://berthazatz:freedown@ac-clbogfo-shard-00-00.cydv8si.mongodb.net:27017,ac-clbogfo-shard-00-01.cydv8si.mongodb.net:27017,ac-clbogfo-shard-00-02.cydv8si.mongodb.net:27017/?ssl=true&replicaSet=atlas-2biwj4-shard-0&authSource=admin&appName=sentence";
                string database = "stomach";
                string collection = "sentence";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Sentenca> ConfigurationValue = mongoDatabase.GetCollection<Sentenca>(collection);
                this._collection = mongoDatabase.GetCollection<Sentenca>(collection);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Sentenca> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Sentence\" view model failed!");

                return this._collection.Find(index => index.linguagem == language).ToList<Sentenca>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Sentenca>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Sentence\" view model failed!");

                return await this._collection.Find(index => index.linguagem == language).ToListAsync<Sentenca>();
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
