using LetterStomach.Bot.Interface;
using LetterStomach.Models;
using LetterStomach.Services;

namespace LetterStomach.Bot
{
    public class ShareBot : IShareBot
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
        private Dictionary<string, string> VAR_OPTIONS = SettingService.Instance.options;
        private Dictionary<string, string> VAR_CHOOSE = SettingService.Instance.choose;
        private Dictionary<string, string> VAR_TERMINATE = SettingService.Instance.Terminate;
        private Dictionary<string, string> VAR_FILE = SettingService.Instance.File;
        private Dictionary<string, string> VAR_SCAN = SettingService.Instance.scan;
        private Dictionary<string, string> VAR_BOT = SettingService.Instance.Bot;

        private Dictionary<string, string> VAR_UPLOAD = SettingService.Instance.Upload;
        private Dictionary<string, string> VAR_DOWNLOAD = SettingService.Instance.Download;

        private Dictionary<string, string> VAR_BLUETOOTHS = SettingService.Instance.Bluetooths;
        private Dictionary<string, string> VAR_BLUETOOTH3 = SettingService.Instance.Bluetooth3;
        private Dictionary<string, string> VAR_BLUETOOTH4 = SettingService.Instance.Bluetooth4;
        private Dictionary<string, string> VAR_RASPBERRY = SettingService.Instance.Raspberry;
        #endregion

        #region ASK
        public async Task<string> Share(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation share \"Share\" bot failed!");

                HashSet<string> raspberry = VAR_RASPBERRY
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooth3 = VAR_BLUETOOTH3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooth4 = VAR_BLUETOOTH4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> choose = VAR_CHOOSE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> options = VAR_OPTIONS
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> terminate = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{choose.ToArray()[0]} {options.ToArray()[0]}: \"{raspberry.ToArray()[0]}\" or \"{bluetooth3.ToArray()[0]}\" or \"{bluetooth4.ToArray()[0]}\" or \"{terminate.ToArray()[0]}\".";
                return ask;
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
        private async Task<string> Upload(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation upload \"Share\" bot failed!");

                HashSet<string> upload = VAR_UPLOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> file = VAR_FILE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(upload.ToArray(), parameter) != -1) ask = $"{upload.ToArray()[0]} {file.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Download(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation download \"Share\" bot failed!");

                HashSet<string> download = VAR_DOWNLOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> file = VAR_FILE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(download.ToArray(), parameter) != -1) ask = $"{download.ToArray()[0]} {file.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Bluetooth(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation bluetooth \"Share\" bot failed!");

                HashSet<string> bluetooth3 = VAR_BLUETOOTH3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooth4 = VAR_BLUETOOTH4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> scan = VAR_SCAN
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{scan} ";
                if (Array.IndexOf(bluetooth3.ToArray(), parameter) != -1) ask = $"{bluetooth3.ToArray()[0]}.";
                if (Array.IndexOf(bluetooth4.ToArray(), parameter) != -1) ask = $"{bluetooth4.ToArray()[0]}.";
                return ask;
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
        public async Task<string> Choose(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation choose \"Share\" bot failed!");

                HashSet<string> bluetooths3 = VAR_BLUETOOTH3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooths4 = VAR_BLUETOOTH4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                bool bluetooth3 = false;
                bool bluetooth4 = false;

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);

                foreach (Message memo in memos)
                {
                    if (Array.IndexOf(bluetooths3.ToArray(), memo.Text) != -1) bluetooth3 = true;
                    if (Array.IndexOf(bluetooths4.ToArray(), memo.Text) != -1) bluetooth4 = true;
                }

                string response = string.Empty;
                //if (bluetooth3) response = await ScanBluetooth3(language);
                //if (bluetooth4) response = await ScanBluetooth4(language);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<List<string>> Load(string language, string parameter, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load \"Share\" bot failed!");

                HashSet<string> bluetooths = VAR_BLUETOOTHS
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> raspberrys = VAR_RASPBERRY
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> downloads = VAR_DOWNLOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> uploads = VAR_UPLOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                List<string> result = new List<string>();
                string ask = string.Empty;
                if (Array.IndexOf(raspberrys.ToArray(), parameter) != -1)
                {
                    ask = await Upload(language, uploads.ToArray()[0]);
                    result.Add(ask);
                    ask = await Download(language, downloads.ToArray()[0]);
                    result.Add(ask);
                    ask = await Terminate(language);
                    result.Add(ask);
                }
                if (Array.IndexOf(bluetooths.ToArray(), parameter) != -1)
                {
                    ask = await Bluetooth(language, parameter);
                    result.Add(ask);
                }
                return result;
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
        public async Task<string> Terminate(string language)
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
        #endregion
    }
}
