using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Data
{
    public class AssistenteContext
    {
        private readonly IMongoDatabase _database;
        private readonly string _collection;

        public AssistenteContext()
        {
            string connection = "mongodb://berthazatz:freedown@ac-grilrgv-shard-00-00.tohxtxd.mongodb.net:27017,ac-grilrgv-shard-00-01.tohxtxd.mongodb.net:27017,ac-grilrgv-shard-00-02.tohxtxd.mongodb.net:27017/?ssl=true&replicaSet=atlas-q1zd06-shard-0&authSource=admin&appName=auxiliary";
            string database = "stomach";
            this._collection = "auxiliary";

            MongoClient client = new MongoClient(connection);
            this._database = client.GetDatabase(database);
        }

        public IMongoCollection<Assistente> GetCollection() => this._database.GetCollection<Assistente>(this._collection);
    }
}
