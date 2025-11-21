using Android.Media;
using LetterStomach.Interfaces;
using Stream = Android.Media.Stream;

namespace LetterStomach.Platforms.Android.Services
{
    public class AudioService : IAudioService
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
        private MediaPlayer _mediaPlayer;
        private bool _prepared;
        private int _position;
        #endregion

        #region BUTTON
        public void PlayAudio(string file_path)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation play audio \"Audio\" service failed!");

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
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public void StopAudio()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation stop audio \"Audio\" service failed!");

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
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion
    }
}
