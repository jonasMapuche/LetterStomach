namespace LetterStomach.Models
{
    public class Tutorial
    {
        public byte[] order { get; set; }
        public byte[] team { get; set; }
        public List<Instruction> lecture { get; set; }
    }
}
