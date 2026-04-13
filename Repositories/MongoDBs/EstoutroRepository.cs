using MongoDB.Driver;
using LetterStomach.Models;
using LetterStomach.Data;

namespace LetterStomach.Repositories.MongoDBs
{
    public class EstoutroRepository : IEstoutroRepository
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
        private readonly IMongoCollection<Estoutro> _collection;
        #endregion

        #region CONSTRUCTOR
        public EstoutroRepository()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Estoutro\" repository failed!");
                else this._error_message = string.Empty;

                string connection = "mongodb://berthazatz:freedown@ac-brgkjgc-shard-00-00.px3emy6.mongodb.net:27017,ac-brgkjgc-shard-00-01.px3emy6.mongodb.net:27017,ac-brgkjgc-shard-00-02.px3emy6.mongodb.net:27017/?ssl=true&replicaSet=atlas-13rqdd-shard-0&authSource=admin&appName=pronoun";
                string database = "stomach";
                string collection = "pronoun";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                this._collection = mongoDatabase.GetCollection<Estoutro>(collection);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public EstoutroRepository(EstoutroContext estoutroContext)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Estoutro\" repository failed!");
                else this._error_message = string.Empty;

                this._collection = estoutroContext.GetCollection();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Estoutro> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Estoutro\" repository failed!");

                return this._collection.Find(index => index.linguagem == language).ToList<Estoutro>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Estoutro>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Estoutro\" repository failed!");

                return await this._collection.Find(index => index.linguagem == language).ToListAsync<Estoutro>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public Estoutro GetName(string name)
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Estoutro\" repository failed!");

                return this._collection.Find(index => index.nome == name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<Estoutro> GetNameAsync(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name async \"Estoutro\" repository failed!");

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
