using LetterStomach.Data;
using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Repositories.MongoDBs
{
    public class JuncaoRepository : IJuncaoRepository
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
        private readonly IMongoCollection<Juncao> _collection;
        #endregion

        #region CONSTRUCTOR
        public JuncaoRepository()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Juncao\" repository failed!");
                else this._error_message = string.Empty;

                string connection = "mongodb://berthazatz:freedown@ac-bhaknbb-shard-00-00.egswt6j.mongodb.net:27017,ac-bhaknbb-shard-00-01.egswt6j.mongodb.net:27017,ac-bhaknbb-shard-00-02.egswt6j.mongodb.net:27017/?ssl=true&replicaSet=atlas-m120wo-shard-0&authSource=admin&appName=preposition";
                string database = "stomach";
                string collection = "preposition";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Juncao> ConfigurationValue = mongoDatabase.GetCollection<Juncao>(collection);
                this._collection = mongoDatabase.GetCollection<Juncao>(collection);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public JuncaoRepository(JuncaoContext juncaoContext)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Juncao\" repository failed!");
                else this._error_message = string.Empty;

                this._collection = juncaoContext.GetCollection();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Juncao> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Juncao\" repository failed!");

                return this._collection.Find(index => index.linguagem == language).ToList<Juncao>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Juncao>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Juncao\" repository failed!");

                return await this._collection.Find(index => index.linguagem == language).ToListAsync<Juncao>();
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
