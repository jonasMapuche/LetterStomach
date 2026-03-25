using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Repositories.MongoDBs
{
    public class AssistenteRepository : IAssistenteRepository
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
        private readonly IMongoCollection<Assistente> _collection;
        #endregion

        #region CONSTRUCTOR
        public AssistenteRepository()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Auxiliary\" view model failed!");
                else this._error_message = string.Empty;

                string connection = "mongodb://berthazatz:freedown@ac-grilrgv-shard-00-00.tohxtxd.mongodb.net:27017,ac-grilrgv-shard-00-01.tohxtxd.mongodb.net:27017,ac-grilrgv-shard-00-02.tohxtxd.mongodb.net:27017/?ssl=true&replicaSet=atlas-q1zd06-shard-0&authSource=admin&appName=auxiliary";
                string database = "stomach";
                string collection = "auxiliary";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                this._collection = mongoDatabase.GetCollection<Assistente>(collection);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Assistente> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Auxiliary\" view model failed!");

                return this._collection.Find(index => index.linguagem == language).ToList<Assistente>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Assistente>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Auxiliary\" view model failed!");

                return await this._collection.Find(index => index.linguagem == language).ToListAsync<Assistente>();
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
