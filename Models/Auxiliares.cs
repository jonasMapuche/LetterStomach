using SQLite;

namespace LetterStomach.Models
{
    public class Auxiliares
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public string mode { get; set; }
        public string prefix { get; set; }
        public string preverb { get; set; }
        public string premode { get; set; }
    }
}
