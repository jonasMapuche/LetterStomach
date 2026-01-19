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
        private Dictionary<string, string> VAR_SEND = SettingService.Instance.Start;
        private Dictionary<string, string> VAR_DONT_SEND = SettingService.Instance.Dont_Start;

        private Dictionary<string, string> VAR_UPLOAD = SettingService.Instance.Upload;
        private Dictionary<string, string> VAR_BLUETOOTH = SettingService.Instance.Bluetooth;

        private Dictionary<string, string> VAR_FILE = SettingService.Instance.File;

        private Dictionary<string, string> VAR_LOAD = SettingService.Instance.Load_Share;

        private Dictionary<string, string> VAR_SCAN = SettingService.Instance.scan;
        private Dictionary<string, string> VAR_CONNECT = SettingService.Instance.connect;

        private Dictionary<string, string> VAR_CONNECTION = SettingService.Instance.connection;
        private Dictionary<string, string> VAR_NAME = SettingService.Instance.name;
        private Dictionary<string, string> VAR_IS = SettingService.Instance.is_be;
        private Dictionary<string, string> VAR_WHAT = SettingService.Instance.what;

        private Dictionary<string, string> VAR_OPTIONS = SettingService.Instance.options;
        private Dictionary<string, string> VAR_CHOOSE = SettingService.Instance.choose;

        private Dictionary<string, string> VAR_CATCH_SHARE = SettingService.Instance.Catch_Share;
        private Dictionary<string, string> VAR_CATCH_SCAN = SettingService.Instance.Catch_Scan;

        private Language ESPANOL = SettingService.Instance.Espanol;
        #endregion

        #region BUTTON
        public async Task<string> Share(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation share \"Share\" bot failed!");

                HashSet<string> upload = VAR_UPLOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooth = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> choose = VAR_CHOOSE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> options = VAR_OPTIONS
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{choose} {options}: \"{upload}\" or \"{bluetooth} 3\" or \"{bluetooth} 4\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Share(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation share \"Share\" bot failed!");

                HashSet<string> upload = VAR_UPLOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooth = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> file = VAR_FILE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> load = VAR_LOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                string[] split = parameter.Split(' ');
                if (split.Length > 1)
                {
                    if (Array.IndexOf(bluetooth.ToArray(), split[0]) != -1) ask = $"{load} {bluetooth} {split[1]}.";
                }
                else
                {
                    if (Array.IndexOf(upload.ToArray(), parameter) != -1) ask = $"{upload} {file}.";
                }
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Send(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation send \"Share\" bot failed!");

                HashSet<string> send = VAR_SEND
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> dont_send = VAR_DONT_SEND
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> choose = VAR_CHOOSE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> options = VAR_OPTIONS
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{choose} {options}: \"{send}\" or \"{dont_send}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Send(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation send \"Share\" bot failed!");

                HashSet<string> send = VAR_SEND
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> dont_send = VAR_DONT_SEND
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> file = VAR_FILE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(send.ToArray(), parameter) != -1) ask = $"{send} {file}.";
                if (Array.IndexOf(dont_send.ToArray(), parameter) != -1) ask = $"{dont_send} {file}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Heap(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation scan \"Share\" bot failed!");

                HashSet<string> scan = VAR_SCAN
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooth = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> connect = VAR_CONNECT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> choose = VAR_CHOOSE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> options = VAR_OPTIONS
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{choose} {options}: \"{scan} {bluetooth} 3\" or \"{scan} {bluetooth} 4\" or \"{connect} {bluetooth} 3\" or \"{connect} {bluetooth} 4\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Connect(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect \"Share\" bot failed!");

                HashSet<string> what = VAR_WHAT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> be_is = VAR_IS
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> connection = VAR_CONNECTION
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> name = VAR_NAME
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (language == ESPANOL.Lowercase) ask = $"¿";
                ask += $"{what} {be_is} {connection} {name}?.";
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
        public async Task<string> Load(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load \"Share\" bot failed!");

                HashSet<string> bluetooth = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> scans = VAR_SCAN
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                bool bluetooth3 = false;
                bool bluetooth4 = false;
                bool scan = false;
                bool gadget = false;
                bool device = false;

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);
                foreach (Message memo in memos)
                {
                    string []split = memo.Text.Split(' ');
                    if (split.Length > 1)
                    {
                        if (Array.IndexOf(bluetooth.ToArray(), split[0]) != -1) 
                        { 
                            if (split[1] == "3") bluetooth3 = true;
                            if (split[1] == "4") bluetooth4 = true;
                        }
                    } 
                    else 
                    {
                        if (Array.IndexOf(scans.ToArray(), memo.Text) != -1) scan = true;
                    }
                }

                string response = string.Empty;
                if (((bluetooth3) && (!scan)) && ((bluetooth4) && (!scan))) response = await Heap(language);
                if (((bluetooth3) && (scan)) && ((bluetooth4) && (scan)) && (!device)) response = await Connect(language);
                if (((bluetooth3) && (scan)) && ((bluetooth4) && (scan)) && (device)) response = await Send(language);
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
        public async Task<string> Mount(string language, string parameter, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount \"Share\" bot failed!");

                HashSet<string> uploads = VAR_UPLOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooths = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> scans = VAR_SCAN
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> connects = VAR_CONNECT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;

                string []split = parameter.Replace(".", "").Split(' ');

                if (split.Length == 1)
                {
                    if (Array.IndexOf(uploads.ToArray(), parameter) != -1)
                    {
                        ask = await Share(language, parameter);
                        return ask;
                    }
                }
                if (split.Length == 2)
                {
                    if ((Array.IndexOf(bluetooths.ToArray(), split[0]) != -1) &&
                        ((split[1] == "3") || (split[1] == "4")))
                    {
                        ask = await Share(language, parameter);
                        return ask;
                    }
                }
                if (split.Length == 3)
                {
                    if ((Array.IndexOf(scans.ToArray(), split[0]) != -1) &&
                        (Array.IndexOf(bluetooths.ToArray(), split[1]) != -1) &&
                        ((split[2] == "3") || (split[2] == "4")))
                    {
                        ask = await Share(language, parameter);
                        return ask;
                    }
                    if ((Array.IndexOf(connects.ToArray(), split[0]) != -1) &&
                        (Array.IndexOf(bluetooths.ToArray(), split[1]) != -1) &&
                        ((split[2] == "3") || (split[2] == "4")))
                    {
                        ask = await Connect(language);
                        return ask;
                    }
                }
                ask = await Bluetooth(language, messages);

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

        #region BLUETOOTH
        private async Task<string> Bluetooth(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation bluetooth \"Share\" bot failed!");

                HashSet<string> bluetooths = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> scans = VAR_SCAN
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> connects = VAR_CONNECT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> whats = VAR_WHAT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> be_iss = VAR_IS
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> connections = VAR_CONNECTION
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> names = VAR_NAME
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                bool scan = false;
                bool bluetooth3 = false;
                bool bluetooth4 = false;
                bool connect = false;
                bool device = false;

                string ask = string.Empty;

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);
                foreach (Message memo in memos)
                {
                    HashSet<string> devices = new HashSet<string>();
                    string[] split = memo.Text.Replace("?", "").Replace("¿", "").Replace(".", "").Split(' ');
                    if (split.Length == 3)
                    {
                        if (Array.IndexOf(scans.ToArray(), split[0]) != -1) scan = true;
                        if (Array.IndexOf(bluetooths.ToArray(), split[1]) != -1)
                        {
                            if (split[2] == "3") bluetooth3 = true;
                            if (split[2] == "4") bluetooth4 = true;
                            if ((bluetooth3) || (bluetooth4)) continue;
                        }
                    }
                    if (split.Length == 4)
                    {
                        if ((Array.IndexOf(whats.ToArray(), split[0]) != -1) &&
                            (Array.IndexOf(be_iss.ToArray(), split[0]) != -1) &&
                            (Array.IndexOf(connections.ToArray(), split[0]) != -1) &&
                            (Array.IndexOf(names.ToArray(), split[0]) != -1))
                        {
                            connect = true;
                            continue;
                        }
                    }
                    if ((scan) && ((bluetooth3) || (bluetooth4)) && (connect))
                    {
                        HashSet<string> gadgets = new HashSet<string>(memo.Text.Split("or"));
                        devices = gadgets;
                        device = true;
                        continue;
                    }
                    if ((scan) && ((bluetooth3) || (bluetooth4)) && (connect) && (device))
                    {
                        string number = bluetooth3 == true ? "3" : "4";
                        if ((Array.IndexOf(devices.ToArray(), memo.Text) != -1))
                        {
                            ask = $"{connects} {bluetooths} {number}: \"{memo.Text}\".";
                            return ask;
                        }
                    }
                }
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
