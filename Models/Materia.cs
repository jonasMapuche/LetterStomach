using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LetterStomach.Models
{
    public class Materia
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string nome { get; set; }
        public string linguagem { get; set; }
        public bool licao { get; set; }
        public string titulo { get; set; }
        public int ordem { get; set; }
        public Conteudo conteudo { get; set; }
    }
}
