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
        private Dictionary<string, string> _options;
        private Dictionary<string, string> _choose;
        private Dictionary<string, string> _file;
        private Dictionary<string, string> _scan;
        private Dictionary<string, string> _bot;
        private Dictionary<string, string> _connection;
        private Dictionary<string, string> _name;
        private Dictionary<string, string> _or;
        private Dictionary<string, string> _by;

        private Dictionary<string, string> _terminate;

        private Dictionary<string, string> _upload;
        private Dictionary<string, string> _download;

        private Dictionary<string, string> _bluetooths;
        private Dictionary<string, string> _bluetooth3;
        private Dictionary<string, string> _bluetooth4;
        private Dictionary<string, string> _raspberry;

        private Dictionary<string, string> _send;
        private Dictionary<string, string> _connect;
        private Dictionary<string, string> _disconnect;

        private List<string> _shareScan;
        private string _device;

        public List<string> ShareScan { get => _shareScan; set => _shareScan = value; }
        public string ShareDevice { get => _device; set => _device = value; }
        #endregion

        #region CONSTRUCTOR
        public ShareBot()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Share\" bot failed!");
                else this.error_message = string.Empty;

                this._options = SettingService.Instance.Options;
                this._choose = SettingService.Instance.Choose;
                this._file = SettingService.Instance.File;
                this._scan = SettingService.Instance.Scan;
                this._bot = SettingService.Instance.Bot;
                this._connection = SettingService.Instance.Connection;
                this._name = SettingService.Instance.Name;
                this._or = SettingService.Instance.Or;
                this._by = SettingService.Instance.By;

                this._terminate = SettingService.Instance.Terminate;

                this._upload = SettingService.Instance.Upload;
                this._download = SettingService.Instance.Download;

                this._bluetooths = SettingService.Instance.Bluetooths;
                this._bluetooth3 = SettingService.Instance.Bluetooth3;
                this._bluetooth4 = SettingService.Instance.Bluetooth4;
                this._raspberry = SettingService.Instance.Raspberry;

                this._send = SettingService.Instance.Send;
                this._connect = SettingService.Instance.Connect;
                this._disconnect = SettingService.Instance.Disconnect;

                this._shareScan = new List<string>();
                this._device = string.Empty;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region ASK
        public async Task<string> Share(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation share \"Share\" bot failed!");

                HashSet<string> raspberry = this._raspberry
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooth3 = this._bluetooth3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooth4 = this._bluetooth4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> choose = this._choose
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> or = this._or
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> options = this._options
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> terminate = this._terminate
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{choose.ToArray()[0]} {options.ToArray()[0]}: \"{raspberry.ToArray()[0]}\" {or.ToArray()[0]} \"{bluetooth3.ToArray()[0]}\" or \"{bluetooth4.ToArray()[0]}\" or \"{terminate.ToArray()[0]}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<string> Scan(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation scan \"Share\" bot failed!");

                HashSet<string> scan = this._scan
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> choose = this._choose
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> options = this._options
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> or = this._or
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> connection = this._connection
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> name = this._name
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> terminate = this._terminate
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{choose.ToArray()[0]} {options.ToArray()[0]}: \"{scan.ToArray()[0]}\" {or.ToArray()[0]} \"{connection.ToArray()[0]} {name.ToArray()[0]}\" {or.ToArray()[0]} \"{terminate.ToArray()[0]}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<string> Send(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation send \"Share\" bot failed!");

                HashSet<string> send = this._send
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> choose = this._choose
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> options = this._options
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> or = this._or
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> terminate = this._terminate
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{choose.ToArray()[0]} {options.ToArray()[0]}: \"{send.ToArray()[0]}\" {or.ToArray()[0]} \"{terminate.ToArray()[0]}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region MOUNT
        private async Task<string> Upload(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation upload \"Share\" bot failed!");

                HashSet<string> upload = this._upload
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> file = this._file
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(upload.ToArray(), parameter) != -1) ask = $"{upload.ToArray()[0]} {file.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> Download(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation download \"Share\" bot failed!");

                HashSet<string> download = this._download
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> file = this._file
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(download.ToArray(), parameter) != -1) ask = $"{download.ToArray()[0]} {file.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> Bluetooth(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation bluetooth \"Share\" bot failed!");

                HashSet<string> bluetooth3 = this._bluetooth3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooth4 = this._bluetooth4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> scan = this._scan
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{scan.ToArray()[0]} ";
                if (Array.IndexOf(bluetooth3.ToArray(), parameter) != -1) ask += $"{bluetooth3.ToArray()[0]}.";
                if (Array.IndexOf(bluetooth4.ToArray(), parameter) != -1) ask += $"{bluetooth4.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> Send(string language, string parameter, string bluetooth)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation send \"Share\" bot failed!");

                HashSet<string> send = this._send
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> file = this._file
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> by = this._by
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> bluetooth3 = this._bluetooth3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> bluetooth4 = this._bluetooth4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{send.ToArray()[0]} {file.ToArray()[0]} {by.ToArray()[0]} ";
                if (Array.IndexOf(bluetooth3.ToArray(), bluetooth) != -1) ask += $"{bluetooth3.ToArray()[0]}.";
                if (Array.IndexOf(bluetooth4.ToArray(), bluetooth) != -1) ask += $"{bluetooth4.ToArray()[0]}.";

                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> Disconnect(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation disconnect \"Share\" bot failed!");

                HashSet<string> disconnnect = this._disconnect
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> bluetooth3 = this._bluetooth3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> bluetooth4 = this._bluetooth4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{disconnnect.ToArray()[0]} ";
                if (Array.IndexOf(bluetooth3.ToArray(), parameter) != -1) ask += $"{bluetooth3.ToArray()[0]}.";
                if (Array.IndexOf(bluetooth4.ToArray(), parameter) != -1) ask += $"{bluetooth4.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> Connect(string language, string parameter, string bluetooth)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect \"Share\" bot failed!");

                HashSet<string> connect = this._connect
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> by = this._by
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> bluetooth3 = this._bluetooth3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> bluetooth4 = this._bluetooth4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{connect.ToArray()[0]} {parameter} {by.ToArray()[0]} ";
                if (Array.IndexOf(bluetooth3.ToArray(), bluetooth) != -1) ask += $"{bluetooth3.ToArray()[0]}.";
                if (Array.IndexOf(bluetooth4.ToArray(), bluetooth) != -1) ask += $"{bluetooth4.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region LOAD
        public async Task<string> Choose(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation choose \"Share\" bot failed!");

                HashSet<string> bluetooths3 = this._bluetooth3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> bluetooths4 = this._bluetooth4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> connects = this._connect
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                bool bluetooth3 = false;
                bool bluetooth4 = false;
                bool connect = false;

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);
                foreach (Message memo in memos)
                {
                    if (Array.IndexOf(bluetooths3.ToArray(), memo.Text) != -1) bluetooth3 = true;
                    if (Array.IndexOf(bluetooths4.ToArray(), memo.Text) != -1) bluetooth4 = true;
                    if (await FindDevice(language, messages, memo.Text) != string.Empty) connect = true;
                }

                string response = string.Empty;
                if ((bluetooth3 || bluetooth4) && (!connect)) response = await Scan(language);
                if ((bluetooth3 || bluetooth4) && connect) response = await Send(language);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<string>> Load(string language, string parameter, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load \"Share\" bot failed!");

                HashSet<string> bluetooths = this._bluetooths
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> raspberrys = this._raspberry
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> downloads = this._download
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> uploads = this._upload
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> scans = this._scan
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> bluetooths3 = this._bluetooth3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> bluetooths4 = this._bluetooth4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> sends = this._send
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
                    return result;
                }
                if (Array.IndexOf(bluetooths.ToArray(), parameter) != -1)
                {
                    ask = await Bluetooth(language, parameter);
                    result.Add(ask);
                    return result;
                }
                if (Array.IndexOf(scans.ToArray(), parameter) != -1)
                {
                    string bluetooth = string.Empty;
                    bluetooth = FindBluetooth(language, messages);
                    ask = await Bluetooth(language, bluetooth);
                    result.Add(ask);
                    return result;
                }
                if (Array.IndexOf(sends.ToArray(), parameter) != -1)
                {
                    string bluetooth = string.Empty;
                    bluetooth = FindBluetooth(language, messages);
                    ask = await Send(language, parameter, bluetooth);
                    result.Add(ask);
                    ask = await Disconnect(language, bluetooth);
                    result.Add(ask);
                    ask = await Terminate(language);
                    result.Add(ask);
                    return result;
                }
                if (this._shareScan.Count > 0)
                {
                    string device = string.Empty;
                    device = await FindDevice(language, messages, parameter);
                    if (device != string.Empty)
                    {
                        this._device = device;
                        string bluetooth = string.Empty;
                        bluetooth = FindBluetooth(language, messages);
                        ask = await Connect(language, device, bluetooth);
                        result.Add(ask);
                        return result;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region UTILS
        private string FindBluetooth(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation find bluetooth \"Record\" bot failed!");

                HashSet<string> bluetooths3 = this._bluetooth3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bluetooths4 = this._bluetooth4
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);
                string bluetooth = string.Empty;
                foreach (Message memo in memos)
                {
                    if (Array.IndexOf(bluetooths3.ToArray(), memo.Text) != -1) bluetooth = bluetooths3.ToArray()[0];
                    if (Array.IndexOf(bluetooths4.ToArray(), memo.Text) != -1) bluetooth = bluetooths4.ToArray()[0];
                }
                return bluetooth;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<string> FindDevice(string language, List<Message> messages, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation find device \"Record\" bot failed!");

                string bluetooth = string.Empty;
                bluetooth = FindBluetooth(language, messages);

                List<string> memo = new List<string>();
                memo = this._shareScan;
                if (memo.Count == 0) return string.Empty;
                
                string? device = string.Empty;
                device = memo.Find(index => index.Contains(parameter));
                if (device == null) return string.Empty;
                return device;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region TERMINATE
        public async Task<string> Terminate(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation terminate \"Record\" bot failed!");

                HashSet<string> terminate = this._terminate
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> bot = this._bot
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{terminate.ToArray()[0]} {bot.ToArray()[0]}.";
                return ask;
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
