using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Data
{
    public class ElocucaoContext
    {
        private readonly IMongoDatabase _database;
        private readonly string _collection;

        public ElocucaoContext()
        {
            string connection = "mongodb://berthazatz:freedown@ac-folgaxm-shard-00-00.q3qzht9.mongodb.net:27017,ac-folgaxm-shard-00-01.q3qzht9.mongodb.net:27017,ac-folgaxm-shard-00-02.q3qzht9.mongodb.net:27017/?ssl=true&replicaSet=atlas-11bsn2-shard-0&authSource=admin&appName=verb";
            string database = "stomach";
            this._collection = "verb";

            MongoClient client = new MongoClient(connection);
            this._database = client.GetDatabase(database);
        }

        public IMongoCollection<Elocucao> GetCollection() => this._database.GetCollection<Elocucao>(this._collection);
    }
}
