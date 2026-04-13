using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Data
{
    public class LigacaoContext
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
        public LigacaoContext()
        {
            if (this._error_off) throw new InvalidOperationException("Operation constructor \"Ligacao\" context failed!");
            else this._error_message = string.Empty;

            string connection = "mongodb://berthazatz:freedown@ac-caq66mq-shard-00-00.55ugps8.mongodb.net:27017,ac-caq66mq-shard-00-01.55ugps8.mongodb.net:27017,ac-caq66mq-shard-00-02.55ugps8.mongodb.net:27017/?ssl=true&replicaSet=atlas-m1uiq4-shard-0&authSource=admin&appName=conjunction";
            string database = "stomach";
            this._collection = "conjunction";

            MongoClient client = new MongoClient(connection);
            this._database = client.GetDatabase(database);
        }
        #endregion

        #region GET
        public IMongoCollection<Ligacao> GetCollection() => this._database.GetCollection<Ligacao>(this._collection);
        #endregion
    }
}
