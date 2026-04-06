namespace LetterStomach.Models
{
    public class Instruction
    {
        public byte[] term { get; set; }
        public byte[] kind { get; set; }
        public byte[] sentence { get; set; }
        public byte[] model { get; set; }
        public byte[] team { get; set; }
        public byte[] order { get; set; }
    }
}
