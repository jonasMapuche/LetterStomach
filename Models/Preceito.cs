using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LetterStomach.Models
{
    public class Preceito
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string nome { get; set; }
        public string linguagem { get; set; }
        public List<string> tipo { get; set; }
        public List<string> numero { get; set; }
        public List<string> genero { get; set; }
    }
}
