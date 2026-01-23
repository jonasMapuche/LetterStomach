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
        private Dictionary<string, string> VAR_TERMINATE = SettingService.Instance.Terminate;
        private Dictionary<string, string> VAR_BOT = SettingService.Instance.Bot;

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
               
                string response = await this._shareBot.Choose(language, messages);
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
        public async Task<List<string>> CaptureCamera(string language, string parameter, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture camera \"Bot\" service failed!");
                List<string> response = new List<string>();
                response = await this._captureBot.Load(language, parameter, messages);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return new List<string>();
            }
        }

        public async Task<List<string>> RecordAudio(string language, string parameter, List<Message> messages)
        {   
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation record audio \"Bot\" service failed!");
                List<string> response = new List<string>();
                response = await this._recordBot.Load(language, parameter, messages);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return new List<string>();
            }
        }

        public async Task<List<string>> ShareFile(string language, string parameter, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation share file \"Bot\" service failed!");
                List<string> response = new List<string>(); 
                response = await this._shareBot.Load(language, parameter, messages);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return new List<string>();
            }
        }
        #endregion

        #region TERMINATE
        private async Task<string> Terminate(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation terminate \"Record\" bot failed!");

                HashSet<string> terminate = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bot = VAR_BOT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{terminate.ToArray()[0]} {bot.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<List<string>> Terminate(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation terminate \"Bot\" service failed!");

                HashSet<string> terminates = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                bool terminate = false;

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);

                foreach (Message memo in memos)
                {
                    if (Array.IndexOf(terminates.ToArray(), memo.Text) != -1) terminate = true;
                }

                List<string> response = new List<string>();
                if (terminate)
                {
                    string result = string.Empty;
                    result = await Terminate(language);
                    response.Add(result);
                }
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return new List<string>();
            }
        }
        #endregion

        #region SCAN
        public async Task ShareScan(List<string> scan)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation share scan \"Bot\" service failed!");
                this._shareBot.ShareScan = scan;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task<List<string>> ShareScan()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation share scan \"Bot\" service failed!");
                return this._shareBot.ShareScan;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return new List<string>();
            }
        }

        public async Task<bool> DeviceShare(string language, List<Message> messages, string device)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation device share \"Bot\" service failed!");

                string result = string.Empty;
                result = await this._shareBot.FindDevice(language, messages, device);
                return result != string.Empty ? true : false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return false;
            }
        }

        public async Task<string> DeviceShare()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation device share \"Bot\" service failed!");

                string result = string.Empty;
                result = this._shareBot.ShareDevice;
                return result;
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
