using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Data
{
    public class EstoutroContext
    {
        private readonly IMongoDatabase _database;
        private readonly string _collection;

        public EstoutroContext()
        {
            string connection = "mongodb://berthazatz:freedown@ac-brgkjgc-shard-00-00.px3emy6.mongodb.net:27017,ac-brgkjgc-shard-00-01.px3emy6.mongodb.net:27017,ac-brgkjgc-shard-00-02.px3emy6.mongodb.net:27017/?ssl=true&replicaSet=atlas-13rqdd-shard-0&authSource=admin&appName=pronoun";
            string database = "stomach";
            this._collection = "pronoun";

            MongoClient client = new MongoClient(connection);
            this._database = client.GetDatabase(database);
        }

        public IMongoCollection<Estoutro> GetCollection() => this._database.GetCollection<Estoutro>(this._collection);
    }
}
