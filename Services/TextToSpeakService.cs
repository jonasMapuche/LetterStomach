using LetterStomach.Models;

namespace LetterStomach.Services
{
    public class TextToSpeakService
    {
        private string _error_message;

        public string error_message
        {
            get => _error_message;
            set
            {
                _error_message = value;
            }
        }

        public event EventHandler<string> OnError;

        private Language ENGLISH = SettingService.Instance.English;
        private Language DEUTSCH = SettingService.Instance.Deutsch;
        private Language ITALIANO = SettingService.Instance.Italino;
        private Language FRANCAIS = SettingService.Instance.Francais;
        private Language ESPANOL = SettingService.Instance.Espanol;

        public async void SpeakText(List<Message> messages, string language, int pitch_speak, int volume_speak)
        {
            try
            { 
                List<Message> message_chat = new List<Message>();
                string text_speak = string.Empty;
                foreach (Message item in messages)
                {
                    if (item.Sender.Name == language)
                    {
                        text_speak = item.Text;
                        break;
                    }                                 
                }

                IEnumerable <Locale> locales = await TextToSpeech.GetLocalesAsync();
                Locale locale = null;
                if (language == ENGLISH.Uppercase) locale = locales.FirstOrDefault(l => l.Language == ENGLISH.Code && l.Country == ENGLISH.Region);
                if (language == DEUTSCH.Uppercase) locale = locales.FirstOrDefault(l => l.Language == DEUTSCH.Code && l.Country == DEUTSCH.Region);
                if (language == ITALIANO.Uppercase) locale = locales.FirstOrDefault(l => l.Language == ITALIANO.Code && l.Country == ITALIANO.Region);
                if (language == FRANCAIS.Uppercase) locale = locales.FirstOrDefault(l => l.Language == FRANCAIS.Code && l.Country == FRANCAIS.Region);
                if (language == ESPANOL.Uppercase) locale = locales.FirstOrDefault(l => l.Language == ESPANOL.Code && l.Country == ESPANOL.Region);

                float pitch = 1.0f;
                float volume = .75f;

                SpeechOptions settings = new SpeechOptions()
                {
                    Volume = volume,
                    Pitch = pitch,
                    Locale = locale
                };
                await TextToSpeech.SpeakAsync(text_speak, settings);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
    }
}
