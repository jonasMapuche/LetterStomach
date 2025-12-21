using LetterStomach.Models;
using LetterStomach.Services;

namespace LetterStomach.Bot
{
    public class RecordBot
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
        private Dictionary<string, string> VAR_START = SettingService.Instance.Start;
        private Dictionary<string, string> VAR_DONT_START = SettingService.Instance.Dont_Start;

        private Dictionary<string, string> VAR_MP3 = SettingService.Instance.MP3;
        private Dictionary<string, string> VAR_WAV = SettingService.Instance.WAV;

        private Dictionary<string, string> VAR_AUDIO = SettingService.Instance.Audio;
        private Dictionary<string, string> VAR_RECORD = SettingService.Instance.Record;

        private Dictionary<string, string> VAR_CATCH_AUDIO = SettingService.Instance.Catch_Audio;
        private Dictionary<string, string> VAR_CATCH_START = SettingService.Instance.Catch_Start;
        #endregion

        #region BUTTON
        public async Task<string> Audio(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation audio \"Record\" bot failed!");

                HashSet<string> mp3 = VAR_MP3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> wav = VAR_WAV
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"Choose options: \"{mp3}\" or \"{wav}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Audio(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation audio \"Record\" bot failed!");

                HashSet<string> mp3 = VAR_MP3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> wav = VAR_WAV
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> record = VAR_RECORD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> audio = VAR_AUDIO
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{record} {audio} ";
                if (Array.IndexOf(mp3.ToArray(), parameter) != -1) ask += $"{mp3}.";
                if (Array.IndexOf(wav.ToArray(), parameter) != -1) ask += $"{wav}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Record(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation record \"Record\" bot failed!");

                HashSet<string> start = VAR_START
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> dont_start = VAR_DONT_START
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"Choose options: \"{start}\" or \"{dont_start}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Record(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation record \"Record\" bot failed!");

                HashSet<string> start = VAR_START
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> dont_start = VAR_DONT_START
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> record = VAR_RECORD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(start.ToArray(), parameter) != -1) ask = $"{start} {record}.";
                if (Array.IndexOf(dont_start.ToArray(), parameter) != -1) ask = $"{dont_start} {record}.";
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
                if (this._error_off) throw new InvalidOperationException("Operation load \"Record\" bot failed!");

                HashSet<string> audios = VAR_CATCH_AUDIO
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                bool audio = true;

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);

                foreach (Message memo in memos)
                {
                    if (Array.IndexOf(audios.ToArray(), memo) != -1) audio = true;
                }

                string response = string.Empty;
                if ((audio)) response = await Record(language);
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
                if (this._error_off) throw new InvalidOperationException("Operation mount \"Record\" bot failed!");

                HashSet<string> audios = VAR_CATCH_AUDIO
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> starts = VAR_CATCH_START
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = string.Empty;
                if (Array.IndexOf(audios.ToArray(), parameter) != -1) ask = await Audio(language, parameter);
                if (Array.IndexOf(starts.ToArray(), parameter) != -1) ask = await Record(language, parameter);
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
