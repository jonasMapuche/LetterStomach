using LetterStomach.Bot;
using LetterStomach.Bot.Interface;
using LetterStomach.Models;
using LetterStomach.Services.Interfaces;

namespace LetterStomach.Services
{
    public class BotService : IBotService
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string _error_message;

        public string error_message
        {
            get => this._error_message;
            set
            {
                this._error_message = value;
            }
        }

        public event EventHandler<string> OnError;
        #endregion

        #region VARIABLE
        ICaptureBot _captureBot = new CaptureBot();
        #endregion

        #region CONSTRUCTOR
        public BotService() 
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Bot\" service failed!");
                else this.error_message = string.Empty;

                this._captureBot.OnError += OnError;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
            }
        }
        #endregion

        #region BUTTON
        public async Task<string> Init(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init \"Bot\" service failed!");

                string response = await this._captureBot.Flash(language);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> Load(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load \"Bot\" service failed!");

                string response = await this._captureBot.Load(language, messages);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }
        #endregion

        #region MOUNT
        public async Task<string> Capture(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation Capture \"Bot\" service failed!");

                string response = await this._captureBot.Mount(language, parameter);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }
        #endregion
    }
}
