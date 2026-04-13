using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Data
{
    public class MaterialContext
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
        public MaterialContext()
        {
            if (this._error_off) throw new InvalidOperationException("Operation constructor \"Material\" context failed!");
            else this._error_message = string.Empty;

            string connection = "mongodb://berthazatz:freedown@ac-dgizj8w-shard-00-00.88ewwu7.mongodb.net:27017,ac-dgizj8w-shard-00-01.88ewwu7.mongodb.net:27017,ac-dgizj8w-shard-00-02.88ewwu7.mongodb.net:27017/?ssl=true&replicaSet=atlas-4bl81c-shard-0&authSource=admin&appName=letter";
            string database = "stomach";
            this._collection = "letter";

            MongoClient client = new MongoClient(connection);
            this._database = client.GetDatabase(database);
        }
        #endregion

        #region GET
        public IMongoCollection<Materia> GetCollection() => this._database.GetCollection<Materia>(this._collection);
        #endregion
    }
}
