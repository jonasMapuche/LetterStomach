using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface ITextToSpeakService
    {
        void SpeakText(List<Message> messages, string language, float pitch_speak, float volume_speak);
    }
}
