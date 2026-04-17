using Android.Runtime;
using Android.Speech.Tts;
using LetterStomach.Helpers;
using LetterStomach.Interfaces;
using TextToSpeech = Android.Speech.Tts.TextToSpeech;

namespace LetterStomach.Platforms.Android.Services
{
    public class TextSpeakService : Java.Lang.Object, ITextSpeakService, TextToSpeech.IOnInitListener
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
        private TextToSpeech? _textToSpeech;
        private string? _text;
        #endregion

        #region CONSTRUCTOR
        public TextSpeakService()
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation contructor \"Text Speak\" service failed!");
                else this.error_message = string.Empty;

                this._textToSpeech = new TextToSpeech(Platform.AppContext, this);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
            }
        }
        #endregion

        #region BUTTON
        public string FileText(string text)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation file text \"Text Speak\" service failed!");

                this._text = text;
                OperationResult result = OperationResult.Error;
                string file_name = FilePath.SetFileName("mp3");
                string file_path = FilePath.SetAudioFilePath(file_name);
                if (this._textToSpeech != null && this._textToSpeech.IsSpeaking == false)
                {
                    Dictionary<string, string> parameter = new Dictionary<string, string>();
                    parameter.Add(TextToSpeech.Engine.KeyParamUtteranceId, "fileSynthesis");
                    result = this._textToSpeech.SynthesizeToFile(_text, parameter, file_path);
                }
                if (result == OperationResult.Success) return file_path;
                else return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public void SpeakText(string text)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation speak text \"Text Speak\" service failed!");

                if (this._textToSpeech != null && this._textToSpeech.IsSpeaking == false)
                    this._textToSpeech.Speak(text, QueueMode.Flush, null, null);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion

        #region EVENT
        public void OnInit([GeneratedEnum] OperationResult status)
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation on init \"Text Speak\" service failed!");

                if (status == OperationResult.Success)
                {
                    if (!string.IsNullOrEmpty(this._text))
                        this._textToSpeech.Speak(this._text, QueueMode.Flush, null, null);
                }
                else 
                    throw new InvalidOperationException("Error operation!"); 
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion
    }
}
