using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Repositories.MongoDBs
{
    public class AlgarismoRepository : IAlgarismoRepository
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
        private readonly IMongoCollection<Algarismo> _collection;
        #endregion

        #region CONSTRUCTOR
        public AlgarismoRepository()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Numeral\" view model failed!");
                else this._error_message = string.Empty;

                string connection = "mongodb://berthazatz:freedown@ac-5g3pi3r-shard-00-00.ulgehxk.mongodb.net:27017,ac-5g3pi3r-shard-00-01.ulgehxk.mongodb.net:27017,ac-5g3pi3r-shard-00-02.ulgehxk.mongodb.net:27017/?ssl=true&replicaSet=atlas-e1clwb-shard-0&authSource=admin&appName=numeral";
                string database = "stomach";
                string collection = "numeral";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Algarismo> ConfigurationValue = mongoDatabase.GetCollection<Algarismo>(collection);
                this._collection = mongoDatabase.GetCollection<Algarismo>(collection);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Algarismo> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Numeral\" view model failed!");

                return this._collection.Find(index => index.linguagem == language).ToList<Algarismo>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Algarismo>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Numeral\" view model failed!");

                return await this._collection.Find(index => index.linguagem == language).ToListAsync<Algarismo>();
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
