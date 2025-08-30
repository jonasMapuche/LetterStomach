using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LetterStomach.Models
{
    public class Sentenca
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string linguagem { get; set; }
        public string impulso { get; set; }
        public List<string> repouso { get; set; }
    }
}
