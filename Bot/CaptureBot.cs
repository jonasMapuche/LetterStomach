using LetterStomach.Bot.Interface;
using LetterStomach.Models;
using LetterStomach.Services;

namespace LetterStomach.Bot
{
    public class CaptureBot : ICaptureBot
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
        private Dictionary<string, string> VAR_TURN = SettingService.Instance.Turn;
        private Dictionary<string, string> VAR_TURN_ON = SettingService.Instance.Turn_On;
        private Dictionary<string, string> VAR_FLASH = SettingService.Instance.Flash;
        private Dictionary<string, string> VAR_ROTATE = SettingService.Instance.Rotate;
        private Dictionary<string, string> VAR_CAMERA = SettingService.Instance.Camera;
        private Dictionary<string, string> VAR_TERMINATE = SettingService.Instance.Terminate;
        private Dictionary<string, string> VAR_BOT = SettingService.Instance.Bot;

        private Dictionary<string, string> VAR_FRONT = SettingService.Instance.Front;
        private Dictionary<string, string> VAR_REAR = SettingService.Instance.Rear;

        private Dictionary<string, string> VAR_ON = SettingService.Instance.On;
        private Dictionary<string, string> VAR_OFF = SettingService.Instance.Off;
        private Dictionary<string, string> VAR_AUTO = SettingService.Instance.Auto;

        private Dictionary<string, string> VAR_START = SettingService.Instance.Start;
        private Dictionary<string, string> VAR_STOP = SettingService.Instance.Stop;

        private Dictionary<string, string> VAR_SAVE = SettingService.Instance.Save;

        private Dictionary<string, string> VAR_SHOOT = SettingService.Instance.Shoot;

        private Dictionary<string, string> VAR_CATCH_FLASH = SettingService.Instance.Catch_Flash;
        private Dictionary<string, string> VAR_CATCH_ROTATE = SettingService.Instance.Catch_Rotate;
        private Dictionary<string, string> VAR_CATCH_CAPTURE = SettingService.Instance.Catch_Capture;

        #endregion

        #region ASK
        public async Task<string> Rotate(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation rotate \"Capture\" bot failed!");

                HashSet<string> front = VAR_FRONT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> rear = VAR_REAR
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> terminate = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"Choose options: \"{front.ToArray()[0]}\" or \"{rear.ToArray()[0]}\" or \"{terminate.ToArray()[0]}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> Flash(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation flash \"Capture\" bot failed!");

                HashSet<string> on = VAR_ON
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> off = VAR_OFF
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> auto = VAR_AUTO
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> terminate = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"Choose options: \"{on.ToArray()[0]}\" or \"{off.ToArray()[0]}\" or \"{auto.ToArray()[0]}\" or \"{terminate.ToArray()[0]}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> Capture(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture \"Capture\" bot failed!");

                HashSet<string> capture = VAR_SHOOT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> terminate = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"Choose options: \"{capture.ToArray()[0]}\" or \"{terminate.ToArray()[0]}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> Save(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation save \"Capture\" bot failed!");

                HashSet<string> save = VAR_SAVE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> terminate = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"Choose options: \"{save.ToArray()[0]}\" or \"{terminate.ToArray()[0]}\".";
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
        private async Task<string> Flash(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation flash \"Capture\" bot failed!");

                HashSet<string> turn_on = VAR_TURN_ON
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> on = VAR_ON
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> off = VAR_OFF
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> auto = VAR_AUTO
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> turn = VAR_TURN
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> flash = VAR_FLASH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(on.ToArray(), parameter) != -1) ask += $"{turn_on.ToArray()[0]} {flash.ToArray()[0]}.";
                if (Array.IndexOf(off.ToArray(), parameter) != -1) ask += $"{turn.ToArray()[0]} {flash.ToArray()[0]} {off.ToArray()[0]}.";
                if (Array.IndexOf(auto.ToArray(), parameter) != -1) ask += $"{turn.ToArray()[0]} {flash.ToArray()[0]} {auto.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Rotate(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation rotate \"Capture\" bot failed!");

                HashSet<string> front = VAR_FRONT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> rear = VAR_REAR
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> rotate = VAR_ROTATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{rotate.ToArray()[0]} {camera.ToArray()[0]} ";
                if (Array.IndexOf(front.ToArray(), parameter) != -1) ask += $"{front.ToArray()[0]}.";
                if (Array.IndexOf(rear.ToArray(), parameter) != -1) ask += $"{rear.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Capture(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture \"Capture\" bot failed!");

                HashSet<string> capture = VAR_SHOOT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(capture.ToArray(), parameter) != -1) ask = $"{capture.ToArray()[0]} {camera.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Save(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation save \"Capture\" bot failed!");

                HashSet<string> save = VAR_SAVE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(save.ToArray(), parameter) != -1) ask = $"{save.ToArray()[0]} {camera.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Start(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture \"Capture\" bot failed!");

                HashSet<string> start = VAR_START
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(start.ToArray(), parameter) != -1) ask = $"{start.ToArray()[0]} {camera.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Stop(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture \"Capture\" bot failed!");

                HashSet<string> stop = VAR_STOP
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(stop.ToArray(), parameter) != -1) ask = $"{stop.ToArray()[0]} {camera.ToArray()[0]}.";
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
                if (this._error_off) throw new InvalidOperationException("Operation choose \"Capture\" bot failed!");

                HashSet<string> flashs = VAR_CATCH_FLASH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> rotates = VAR_CATCH_ROTATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> captures = VAR_CATCH_CAPTURE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                bool flash = false;
                bool rotate = false;
                bool capture = false;

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);

                foreach (Message memo in memos)
                {
                    if (Array.IndexOf(flashs.ToArray(), memo.Text) != -1) flash = true;
                    if (Array.IndexOf(rotates.ToArray(), memo.Text) != -1) rotate = true;
                    if (Array.IndexOf(captures.ToArray(), memo.Text) != -1) capture = true;
                }

                string response = string.Empty;

                if ((rotate) && (!flash)) response = await Flash(language);
                if (flash && rotate && !capture) response = await Capture(language);
                if (flash && rotate && capture) response = await Save(language);

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
                if (this._error_off) throw new InvalidOperationException("Operation load \"Capture\" bot failed!");

                HashSet<string> flashs = VAR_CATCH_FLASH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> rotates = VAR_CATCH_ROTATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> captures = VAR_CATCH_CAPTURE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> starts = VAR_START
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> stops = VAR_STOP
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> saves = VAR_SAVE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                List<string> result = new List<string>();
                string ask = string.Empty;
                if (Array.IndexOf(rotates.ToArray(), parameter) != -1)
                {
                    ask = await Start(language, starts.ToArray()[0]);
                    result.Add(ask);
                    ask = await Rotate(language, parameter);
                    result.Add(ask);
                }
                if (Array.IndexOf(flashs.ToArray(), parameter) != -1)
                {
                    ask = await Flash(language, parameter);
                    result.Add(ask);
                }
                if (Array.IndexOf(captures.ToArray(), parameter) != -1)
                {
                    ask = await Capture(language, parameter);
                    result.Add(ask);
                }
                if (Array.IndexOf(saves.ToArray(), parameter) != -1)
                {
                    ask = await Save(language, parameter);
                    result.Add(ask);
                    ask = await Stop(language, stops.ToArray()[0]);
                    result.Add(ask);
                    ask = await Terminate(language);
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
        #endregion
    }
}
