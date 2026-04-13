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
        private Dictionary<string, string> _write;
        private Dictionary<string, string> _stop;
        private Dictionary<string, string> _terminate;
        private Dictionary<string, string> _bot;
        private Dictionary<string, string> _record;

        private Dictionary<string, string> _mp3;
        private Dictionary<string, string> _wav;

        private Dictionary<string, string> _catch_audio;
        private Dictionary<string, string> _catch_stop;
        #endregion

        #region CONSTRUCTOR
        public RecordBot()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Record\" bot failed!");
                else this.error_message = string.Empty;

                this._write = SettingService.Instance.Write;
                this._stop = SettingService.Instance.Stop;
                this._terminate = SettingService.Instance.Terminate;
                this._bot = SettingService.Instance.Bot;
                this._record = SettingService.Instance.Record;

                this._mp3 = SettingService.Instance.MP3;
                this._wav = SettingService.Instance.WAV;

                this._catch_audio = SettingService.Instance.Catch_Audio;
                this._catch_stop = SettingService.Instance.Stop;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region ASK
        public async Task<string> Audio(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation audio \"Record\" bot failed!");

                HashSet<string> mp3 = this._mp3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> wav = this._wav
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> terminate = this._terminate
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"Choose options: \"{mp3.ToArray()[0]}\" or \"{wav.ToArray()[0]}\" or \"{terminate.ToArray()[0]}\".";
                return ask;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<string> Stop(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation record \"Record\" bot failed!");

                HashSet<string> stop = this._stop
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> write = this._write
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> record = this._record
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string ask = $"{write.ToArray()[0]} \"{stop.ToArray()[0]}\" to {stop.ToArray()[0]} the {record.ToArray()[0]}.";
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
        private async Task<string> Audio(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation audio \"Record\" bot failed!");

                HashSet<string> mp3 = this._mp3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> wav = this._wav
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> record = this._record
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> Stop(string language, string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation record \"Record\" bot failed!");

                HashSet<string> stop = this._stop
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> mp3 = this._mp3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> wav = this._wav
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region LOAD
        public async Task<string> Choose(string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation choose \"Record\" bot failed!");

                HashSet<string> mp3s = this._mp3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> wavs = this._wav
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                bool wav = false;
                bool mp3 = false;

                List<Message> memos = new List<Message>();
                memos = messages.FindAll(index => index.Sender == null);

                foreach (Message memo in memos)
                {
                    if (Array.IndexOf(wavs.ToArray(), memo.Text) != -1) wav = true;
                    if (Array.IndexOf(mp3s.ToArray(), memo.Text) != -1) mp3 = true;
                }

                string response = string.Empty;
                if (wav || mp3) response = await Stop(language);
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
                if (this._error_off) throw new InvalidOperationException("Operation load \"Record\" bot failed!");

                HashSet<string> audios = this._catch_audio
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> stops = this._catch_stop
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> mp3s = this._mp3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> wavs = this._wav
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

                List<string> result = new List<string>();
                string ask = string.Empty;
                if (Array.IndexOf(audios.ToArray(), parameter) != -1)
                {
                    ask = await Audio(language, parameter);
                    result.Add(ask);
                }
                if (Array.IndexOf(stops.ToArray(), parameter) != -1)
                {
                    ask = await Stop(language, kind);
                    result.Add(ask);
                    ask = await Terminate(language);
                    result.Add(ask);
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
