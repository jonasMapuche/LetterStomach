using MongoDB.Driver;
using LetterStomach.Models;
using LetterStomach.Data;

namespace LetterStomach.Repositories.MongoDBs
{
    public class CircunstanciaRepository : ICircunstanciaRepository
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
        private readonly IMongoCollection<Circunstancia> _collection;
        #endregion

        #region CONSTRUCTOR
        public CircunstanciaRepository()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Circunstancia\" repository failed!");
                else this._error_message = string.Empty;

                string connection = "mongodb://berthazatz:freedown@ac-3vr780m-shard-00-00.ewolqjf.mongodb.net:27017,ac-3vr780m-shard-00-01.ewolqjf.mongodb.net:27017,ac-3vr780m-shard-00-02.ewolqjf.mongodb.net:27017/?ssl=true&replicaSet=atlas-xmcuf5-shard-0&authSource=admin&appName=adverb";
                string database = "stomach";
                string collection = "adverb";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                this._collection = mongoDatabase.GetCollection<Circunstancia>(collection);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public CircunstanciaRepository(CircunstanciaContext circunstanciaContex)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Circunstancia\" repository failed!");
                else this._error_message = string.Empty;

                this._collection = circunstanciaContex.GetCollection();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Circunstancia> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Circunstancia\" repository failed!");

                return this._collection.Find(index => index.linguagem == language).ToList<Circunstancia>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Circunstancia>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Circunstancia\" repository failed!");

                return await this._collection.Find(index => index.linguagem == language).ToListAsync<Circunstancia>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public Circunstancia GetName(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Circunstancia\" repository failed!");

                return this._collection.Find(index => index.nome == name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<Circunstancia> GetNameAsync(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name async \"Circunstancia\" repository failed!");

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
