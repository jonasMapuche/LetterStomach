namespace LetterStomach.Interfaces
{
    public interface ITextSpeakService
    {
        void SpeakText(string text);
        string FileText(string text);
    }
}
