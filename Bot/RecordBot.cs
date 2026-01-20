using LetterStomach.Bot.Interface;
using LetterStomach.Models;
using LetterStomach.Services;

namespace LetterStomach.Bot
{
    public class RecordBot : IRecordBot
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
        private Dictionary<string, string> VAR_WRITE = SettingService.Instance.Write;
        private Dictionary<string, string> VAR_STOP = SettingService.Instance.Stop;
        private Dictionary<string, string> VAR_TERMINATE = SettingService.Instance.Terminate;
        private Dictionary<string, string> VAR_BOT = SettingService.Instance.Bot;
        private Dictionary<string, string> VAR_RECORD = SettingService.Instance.Record;

        private Dictionary<string, string> VAR_MP3 = SettingService.Instance.MP3;
        private Dictionary<string, string> VAR_WAV = SettingService.Instance.WAV;

        private Dictionary<string, string> VAR_CATCH_AUDIO = SettingService.Instance.Catch_Audio;
        private Dictionary<string, string> VAR_CATCH_STOP = SettingService.Instance.Stop;
        #endregion

        #region ASK
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

                HashSet<string> terminate = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"Choose options: \"{mp3.ToArray()[0]}\" or \"{wav.ToArray()[0]}\" or \"{terminate.ToArray()[0]}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> Stop(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation record \"Record\" bot failed!");

                HashSet<string> stop = VAR_STOP
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> write = VAR_WRITE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> record = VAR_RECORD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{write.ToArray()[0]} \"{stop.ToArray()[0]}\" to {stop.ToArray()[0]} the {record.ToArray()[0]}.";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

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

        #region MOUNT
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

                string ask = $"{record.ToArray()[0]} ";
                if (Array.IndexOf(mp3.ToArray(), parameter) != -1) ask += $"{mp3.ToArray()[0]}.";
                if (Array.IndexOf(wav.ToArray(), parameter) != -1) ask += $"{wav.ToArray()[0]}.";
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
                if (this._error_off) throw new InvalidOperationException("Operation record \"Record\" bot failed!");

                HashSet<string> stop = VAR_STOP
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> mp3 = VAR_MP3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> wav = VAR_WAV
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{stop.ToArray()[0]} ";
                if (Array.IndexOf(mp3.ToArray(), parameter) != -1) ask += $"{mp3.ToArray()[0]}.";
                if (Array.IndexOf(wav.ToArray(), parameter) != -1) ask += $"{wav.ToArray()[0]}.";
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
                if (this._error_off) throw new InvalidOperationException("Operation choose \"Record\" bot failed!");

                HashSet<string> mp3s = VAR_MP3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> wavs = VAR_WAV
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> stops = VAR_STOP
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                bool wav = false;
                bool mp3 = false;
                bool stop = false;

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);

                foreach (Message memo in memos)
                {
                    if (Array.IndexOf(wavs.ToArray(), memo.Text) != -1) wav = true;
                    if (Array.IndexOf(mp3s.ToArray(), memo.Text) != -1) mp3 = true;
                    if (Array.IndexOf(stops.ToArray(), memo.Text) != -1) stop = true;
                }

                string response = string.Empty;
                if ((wav || mp3) && !stop) response = await Stop(language);
                if ((wav || mp3) && stop) response = await Terminate(language);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public async Task<string> Load(string language, string parameter, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load \"Record\" bot failed!");

                HashSet<string> audios = VAR_CATCH_AUDIO
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> stops = VAR_CATCH_STOP
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> mp3s = VAR_MP3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> wavs = VAR_WAV
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string kind = string.Empty;
                if (parameter == stops.ToArray()[0])
                {
                    List<Message> memos = new List<Message>();
                    memos = messages.FindAll(index => index.Sender == null);

                    foreach (Message memo in memos)
                    {
                        if (Array.IndexOf(wavs.ToArray(), memo.Text) != -1) kind = wavs.ToArray()[0];
                        if (Array.IndexOf(mp3s.ToArray(), memo.Text) != -1) kind = mp3s.ToArray()[0];
                    }
                }

                string ask = string.Empty;
                if (Array.IndexOf(audios.ToArray(), parameter) != -1) ask = await Audio(language, parameter);
                if (Array.IndexOf(stops.ToArray(), parameter) != -1) ask = await Stop(language, kind);
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
