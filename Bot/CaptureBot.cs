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
        private Dictionary<string, string> VAR_SHOOT = SettingService.Instance.Shoot;
        private Dictionary<string, string> VAR_DONT_SHOOT = SettingService.Instance.Dont_Shoot;

        private Dictionary<string, string> VAR_FRONT = SettingService.Instance.Front;
        private Dictionary<string, string> VAR_REAR = SettingService.Instance.Rear;

        private Dictionary<string, string> VAR_ON = SettingService.Instance.On;
        private Dictionary<string, string> VAR_OFF = SettingService.Instance.Off;
        private Dictionary<string, string> VAR_AUTO = SettingService.Instance.Auto;

        private Dictionary<string, string> VAR_CATCH_FLASH = SettingService.Instance.Catch_Flash;
        private Dictionary<string, string> VAR_CATCH_ROTATE = SettingService.Instance.Catch_Rotate;
        private Dictionary<string, string> VAR_CATCH_CAPTURE = SettingService.Instance.Catch_Capture;

        private Dictionary<string, string> VAR_TURN = SettingService.Instance.Turn;
        private Dictionary<string, string> VAR_FLASH = SettingService.Instance.Flash;

        private Dictionary<string, string> VAR_ROTATE = SettingService.Instance.Rotate;
        private Dictionary<string, string> VAR_CAMERA = SettingService.Instance.Camera;
        #endregion

        #region BUTTON
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

                string ask = $"Choose options: \"{on}\", \"{off}\" or \"{auto}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Flash(string language, string parameter)
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

                HashSet<string> turn = VAR_TURN
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> flash = VAR_FLASH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{turn} {flash} ";
                if (Array.IndexOf(on.ToArray(), parameter) != -1) ask += $"{on}.";
                if (Array.IndexOf(off.ToArray(), parameter) != -1) ask += $"{off}.";
                if (Array.IndexOf(auto.ToArray(), parameter) != -1) ask += $"{auto}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Rotate(string language)
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

                string ask = $"Choose options: \"{front}\" or \"{rear}\".";
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

                string ask = $"{rotate} {camera} ";
                if (Array.IndexOf(front.ToArray(), parameter) != -1) ask += $"{front}.";
                if (Array.IndexOf(rear.ToArray(), parameter) != -1) ask += $"{rear}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Capture(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture \"Capture\" bot failed!");

                HashSet<string> capture = VAR_SHOOT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> dont_capture = VAR_DONT_SHOOT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"Choose options: \"{capture}\" or \"{dont_capture}\".";
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

                HashSet<string> dont_capture = VAR_DONT_SHOOT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(capture.ToArray(), parameter) != -1) ask = $"{capture} {camera}.";
                if (Array.IndexOf(dont_capture.ToArray(), parameter) != -1) ask = $"{dont_capture} {camera}.";
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
                if (this._error_off) throw new InvalidOperationException("Operation load \"Capture\" bot failed!");

                HashSet<string> flashs = VAR_CATCH_FLASH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> rotates = VAR_CATCH_ROTATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                bool flash = true;
                bool rotate = true;

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);

                foreach (Message memo in memos)
                {
                    if (Array.IndexOf(flashs.ToArray(), memo) != -1) flash = true;
                    if (Array.IndexOf(rotates.ToArray(), memo) != -1) rotate = true;
                }

                string response = string.Empty;

                if ((flash) && (!rotate)) response = await Rotate(language);
                if ((flash) && (rotate)) response = await Capture(language);

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
        public async Task<string> Mount(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount \"Capture\" bot failed!");

                HashSet<string> flashs = VAR_CATCH_FLASH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> rotates = VAR_CATCH_ROTATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> captures = VAR_CATCH_CAPTURE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(flashs.ToArray(), parameter) != -1) ask = await Flash(language, parameter);
                if (Array.IndexOf(rotates.ToArray(), parameter) != -1) ask = await Rotate(language, parameter);
                if (Array.IndexOf(captures.ToArray(), parameter) != -1) ask = await Capture(language, parameter);
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
