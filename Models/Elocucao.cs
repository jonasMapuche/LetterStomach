using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LetterStomach.Models
{
    public class Elocucao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string nome { get; set; }
        public string linguagem { get; set; }
        public string modelo { get; set; }
        public List<Teor> teor { get; set; }
    }
}
