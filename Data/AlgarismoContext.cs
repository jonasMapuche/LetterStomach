using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Data
{
    public class AlgarismoContext
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
        public AlgarismoContext()
        {
            if (this._error_off) throw new InvalidOperationException("Operation constructor \"Algarismo\" context failed!");
            else this._error_message = string.Empty;

            string connection = "mongodb://berthazatz:freedown@ac-5g3pi3r-shard-00-00.ulgehxk.mongodb.net:27017,ac-5g3pi3r-shard-00-01.ulgehxk.mongodb.net:27017,ac-5g3pi3r-shard-00-02.ulgehxk.mongodb.net:27017/?ssl=true&replicaSet=atlas-e1clwb-shard-0&authSource=admin&appName=numeral";
            string database = "stomach";
            this._collection = "numeral";

            MongoClient client = new MongoClient(connection);
            this._database = client.GetDatabase(database);
        }
        #endregion

        #region GET
        public IMongoCollection<Algarismo> GetCollection() => this._database.GetCollection<Algarismo>(this._collection);
        #endregion
    }
}
