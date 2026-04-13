using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Data
{
    public class PreceitoContext
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
        public PreceitoContext()
        {
            if (this._error_off) throw new InvalidOperationException("Operation constructor \"Preceito\" context failed!");
            else this._error_message = string.Empty;

            string connection = "mongodb://berthazatz:freedown@ac-knhxaxk-shard-00-00.rzn652o.mongodb.net:27017,ac-knhxaxk-shard-00-01.rzn652o.mongodb.net:27017,ac-knhxaxk-shard-00-02.rzn652o.mongodb.net:27017/?ssl=true&replicaSet=atlas-amy702-shard-0&authSource=admin&appName=article";
            string database = "stomach";
            this._collection = "article";

            MongoClient client = new MongoClient(connection);
            this._database = client.GetDatabase(database);
        }
        #endregion

        #region GET
        public IMongoCollection<Preceito> GetCollection() => this._database.GetCollection<Preceito>(this._collection);
        #endregion

    }
}
