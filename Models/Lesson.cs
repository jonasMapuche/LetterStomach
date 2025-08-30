namespace LetterStomach.Models
{
    public class Lesson
    {
        public int order { get; set; }
        public string team { get; set; }
        public List<Word> lecture { get; set; }
    }
}
