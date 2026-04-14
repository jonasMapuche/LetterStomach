using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface ITextToSpeakService
    {
        Task SpeakText(List<Message> messages, string language, float pitch_speak, float volume_speak);
    }
}
