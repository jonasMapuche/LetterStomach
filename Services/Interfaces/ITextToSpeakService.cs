using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface ITextToSpeakService
    {
        void SpeakText(List<Message> messages, string language, int pitch_speak, int volume_speak);
    }
}
