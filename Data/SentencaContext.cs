using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Data
{
    public class SentencaContext
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
        private readonly IMongoDatabase _database;
        private readonly string _collection;
        #endregion

        #region CONSTRUCTOR
        public SentencaContext()
        {
            if (this._error_off) throw new InvalidOperationException("Operation constructor \"Sentenca\" context failed!");
            else this._error_message = string.Empty;

            string connection = "mongodb://berthazatz:freedown@ac-clbogfo-shard-00-00.cydv8si.mongodb.net:27017,ac-clbogfo-shard-00-01.cydv8si.mongodb.net:27017,ac-clbogfo-shard-00-02.cydv8si.mongodb.net:27017/?ssl=true&replicaSet=atlas-2biwj4-shard-0&authSource=admin&appName=sentence";
            string database = "stomach";
            this._collection = "sentence";

            MongoClient client = new MongoClient(connection);
            this._database = client.GetDatabase(database);
        }
        #endregion

        #region GET
        public IMongoCollection<Sentenca> GetCollection() => this._database.GetCollection<Sentenca>(this._collection);
        #endregion
    }
}
