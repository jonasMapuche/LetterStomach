using SQLite;

namespace LetterStomach.Models
{
    public class Verbos
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public string model { get; set; }
        public string mode { get; set; }
        public string pronoun { get; set; }
    }
}
