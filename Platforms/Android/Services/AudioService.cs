using Android.Media;
using LetterStomach.Interfaces;
using Stream = Android.Media.Stream;

namespace LetterStomach.Platforms.Android.Services
{
    public class AudioService : IAudioService
    {
        private string _error_message;

        public string error_message
        {
            get => _error_message;
            set
            {
                _error_message = value;
            }
        }

        public event EventHandler<string> OnError;

        private MediaPlayer _mediaPlayer;
        private bool _prepared;
        private int _position;

        public void PlayAudio(string file_path)
        {
            try
            {
                if ((this._mediaPlayer != null) && (!this._mediaPlayer.IsPlaying))
                {
                    this._mediaPlayer.SeekTo(this._position);
                    this._position = 0;
                    this._mediaPlayer.Start();
                }
                else if (this._mediaPlayer == null || !this._mediaPlayer.IsPlaying)
                {
                    this._mediaPlayer = new MediaPlayer();
                    this._mediaPlayer.SetDataSource(file_path);
                    this._mediaPlayer.SetAudioStreamType(Stream.Music);
                    this._mediaPlayer.PrepareAsync();
                    this._mediaPlayer.Prepared += (sender, args) =>
                    {
                        this._prepared = true;
                        this._mediaPlayer.Start();
                    };
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void StopAudio()
        {
            try
            {
                if (this._mediaPlayer != null)
                {
                    if (_prepared)
                    {
                        this._mediaPlayer.Stop();
                        this._mediaPlayer.Release();
                        this._prepared = false;
                    }
                    this._mediaPlayer = null;
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
    }
}
