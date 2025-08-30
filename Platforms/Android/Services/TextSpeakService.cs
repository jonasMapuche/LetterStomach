using Android.Runtime;
using Android.Speech.Tts;
using LetterStomach.Helpers;
using LetterStomach.Interfaces;
using TextToSpeech = Android.Speech.Tts.TextToSpeech;

namespace LetterStomach.Platforms.Android.Services
{
    public class TextSpeakService : Java.Lang.Object, ITextSpeakService, TextToSpeech.IOnInitListener
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

        private TextToSpeech _textToSpeech;
        private string _text;

        public TextSpeakService()
        {
            try 
            { 
                this._textToSpeech = new TextToSpeech(Platform.AppContext, this);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public string FileText(string text)
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            try 
            {
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
                OnError?.Invoke(this, error_message);
            }
        }

        public void SpeakText(string text)
        {
            try 
            { 
                if (this._textToSpeech != null && this._textToSpeech.IsSpeaking == false)
                    this._textToSpeech.Speak(text, QueueMode.Flush, null, null);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
    }
}
