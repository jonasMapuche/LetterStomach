using SQLite;

namespace LetterStomach.Models
{
    public class Numerais
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public int initial { get; set; }
        public string type { get; set; }
    }
}
