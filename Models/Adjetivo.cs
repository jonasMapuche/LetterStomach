using SQLite;

namespace LetterStomach.Models
{
    public class Adjetivo
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public string lesson { get; set; }
    }
}
