using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Data
{
    public class CircunstanciaContext
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
        public CircunstanciaContext()
        {
            if (this._error_off) throw new InvalidOperationException("Operation constructor \"Circunstancia\" context failed!");
            else this._error_message = string.Empty;

            string connection = "mongodb://berthazatz:freedown@ac-3vr780m-shard-00-00.ewolqjf.mongodb.net:27017,ac-3vr780m-shard-00-01.ewolqjf.mongodb.net:27017,ac-3vr780m-shard-00-02.ewolqjf.mongodb.net:27017/?ssl=true&replicaSet=atlas-xmcuf5-shard-0&authSource=admin&appName=adverb";
            string database = "stomach";
            this._collection = "adverb";

            MongoClient client = new MongoClient(connection);
            this._database = client.GetDatabase(database);
        }
        #endregion

        #region GET
        public IMongoCollection<Circunstancia> GetCollection() => this._database.GetCollection<Circunstancia>(this._collection);
        #endregion
    }
}
