using SQLite;

namespace LetterStomach.Models
{
    public class Pronomes
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String name { get; set; }
        public String language { get; set; }
        public String type { get; set; }
        public String number { get; set; }
        public int person { get; set; }
        public String gender { get; set; }
        public string context { get; set; }
    }
}
