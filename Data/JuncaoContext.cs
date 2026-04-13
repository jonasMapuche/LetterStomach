using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Data
{
    public class JuncaoContext
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
        public JuncaoContext()
        {
            if (this._error_off) throw new InvalidOperationException("Operation constructor \"Juncao\" context failed!");
            else this._error_message = string.Empty;

            string connection = "mongodb://berthazatz:freedown@ac-bhaknbb-shard-00-00.egswt6j.mongodb.net:27017,ac-bhaknbb-shard-00-01.egswt6j.mongodb.net:27017,ac-bhaknbb-shard-00-02.egswt6j.mongodb.net:27017/?ssl=true&replicaSet=atlas-m120wo-shard-0&authSource=admin&appName=preposition";
            string database = "stomach";
            this._collection = "preposition";

            MongoClient client = new MongoClient(connection);
            this._database = client.GetDatabase(database);
        }
        #endregion

        #region GET
        public IMongoCollection<Juncao> GetCollection() => this._database.GetCollection<Juncao>(this._collection);
        #endregion
    }
}
