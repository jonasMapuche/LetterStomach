using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Repositories.MongoDBs
{
    public class ConjunctionRepoitory : ILigacaoRepository
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
        private readonly IMongoCollection<Ligacao> _collection;
        #endregion

        #region CONSTRUCTOR
        public ConjunctionRepoitory()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Conjunction\" view model failed!");
                else this._error_message = string.Empty;

                string connection = "mongodb://berthazatz:freedown@ac-caq66mq-shard-00-00.55ugps8.mongodb.net:27017,ac-caq66mq-shard-00-01.55ugps8.mongodb.net:27017,ac-caq66mq-shard-00-02.55ugps8.mongodb.net:27017/?ssl=true&replicaSet=atlas-m1uiq4-shard-0&authSource=admin&appName=conjunction";
                string database = "stomach";
                string collection = "conjunction";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                this._collection = mongoDatabase.GetCollection<Ligacao>(collection);
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
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Conjunction\" view model failed!");

                return this._collection.Find(index => index.linguagem == language).ToList<Ligacao>();
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
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Conjunction\" view model failed!");

                return await this._collection.Find(index => index.linguagem == language).ToListAsync<Ligacao>();
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
