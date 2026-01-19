using LetterStomach.Bot;
using LetterStomach.Bot.Interface;
using LetterStomach.Models;
using LetterStomach.Services.Interfaces;
using System.Collections.Generic;

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
        IRecordBot _recordBot = new RecordBot();
        IShareBot _shareBot = new ShareBot();
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

        #region INIT
        public async Task<string> CaptureCamera(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture camera \"Bot\" service failed!");

                string response = await this._captureBot.Rotate(language);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> RecordAudio(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation record audio \"Bot\" service failed!");

                string response = await this._recordBot.Audio(language);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> ShareFile(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation share file \"Bot\" service failed!");

                string response = await this._shareBot.Share(language);
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

        #region LOAD
        public async Task<string> CaptureCamera(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load \"Bot\" service failed!");

                string response = await this._captureBot.Choose(language, messages);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> RecordAudio(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load \"Bot\" service failed!");

                string response = await this._recordBot.Choose(language, messages);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> ShareFile(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load \"Bot\" service failed!");
                string response = string.Empty;
                //string response = await this._shareBot.Choose(language, messages);
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
        public async Task<string> CaptureCamera(string language, string parameter, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture camera \"Bot\" service failed!");
                string response = string.Empty;
                response = await this._captureBot.Load(language, parameter, messages);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> RecordAudio(string language, string parameter, List<Message> messages)
        {   
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation record audio \"Bot\" service failed!");
                string response = string.Empty;
                response = await this._recordBot.Load(language, parameter, messages);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> ShareFile(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation share file \"Bot\" service failed!");
                string response = string.Empty;
                //response = await this._shareBot.Load(language, parameter);
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
