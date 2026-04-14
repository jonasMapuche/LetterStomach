using LetterStomach.Models;
using LetterStomach.Services.Interfaces;

namespace LetterStomach.Services
{
    public class TextToSpeakService : ITextToSpeakService
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string? _error_message;

        public string? error_message
        {
            get => this._error_message;
            set
            {
                this._error_message = value;
            }
        }

        public event EventHandler<string>? OnError;
        #endregion

        #region VARIABLE
        private Language _language_english;
        private Language _language_deutsch ;
        private Language _language_italiano;
        private Language _language_francais;
        private Language _language_espanol;
        #endregion

        #region CONSTRUCTOR
        public TextToSpeakService()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Text to Speak\" service failed!");
                else this.error_message = string.Empty;

                this._language_english = SettingService.Instance.English;
                this._language_deutsch = SettingService.Instance.Deutsch;
                this._language_italiano = SettingService.Instance.Italino;
                this._language_francais = SettingService.Instance.Francais;
                this._language_espanol = SettingService.Instance.Espanol;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region SPEAK
        public async Task SpeakText(List<Message> messages, string language, float pitch_speak, float volume_speak)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation speak text \"Text to Speak\" service failed!");

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
                if (language == this._language_english.Uppercase) locale = locales.FirstOrDefault(l => l.Language == this._language_english.Code && l.Country == this._language_english.Region);
                if (language == this._language_deutsch.Uppercase) locale = locales.FirstOrDefault(l => l.Language == this._language_deutsch.Code && l.Country == this._language_deutsch.Region);
                if (language == this._language_italiano.Uppercase) locale = locales.FirstOrDefault(l => l.Language == this._language_italiano.Code && l.Country == this._language_italiano.Region);
                if (language == this._language_francais.Uppercase) locale = locales.FirstOrDefault(l => l.Language == this._language_francais.Code && l.Country == this._language_francais.Region);
                if (language == this._language_espanol.Uppercase) locale = locales.FirstOrDefault(l => l.Language == this._language_espanol.Code && l.Country == this._language_espanol.Region);

                float pitch = pitch_speak;
                float volume = volume_speak;

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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion
    }
}
