using Android.Media;
using Java.IO;
using LetterStomach.Helpers;
using LetterStomach.Interfaces;

namespace LetterStomach.Platforms.Android.Services
{
    public class RecordService : IRecordService
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string _error_message;

        private static bool _static_error_on = true;
        private static bool _static_error_false = false;

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
        private MediaRecorder _mediaRecorder;
        private string _storage_path;
        public string StoragePath { get => _storage_path; set => _storage_path = value; }

        private AudioRecord? _audioRecord;
        private int _buffer_size;
        private ChannelIn _channel_in = ChannelIn.Mono;
        private Encoding _encoding = Encoding.Pcm16bit;
        private int _sample_rate = 44100;
        private int _bit_depth = 16;
        const int WAVHEADERLENGTH = 44;
        private int _channel_mono = 1;
        #endregion

        #region BUTTON
        public void StartRecordMP3()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation start record mp3 \"Record\" service failed!");

                if (this._mediaRecorder == null)
                {
                    string file_name = FilePath.SetFileName("mp3");
                    string file_path = FilePath.SetAudioFilePath(file_name);
                    this._storage_path = file_path;
                    this._mediaRecorder = new MediaRecorder();
                    this._mediaRecorder.Reset();
                    this._mediaRecorder.SetAudioSource(AudioSource.Mic);
                    this._mediaRecorder.SetOutputFormat(OutputFormat.AacAdts);
                    this._mediaRecorder.SetAudioEncoder(AudioEncoder.Aac);
                    this._mediaRecorder.SetOutputFile(file_path);
                    this._mediaRecorder.Prepare();
                    this._mediaRecorder.Start();
                }
                else this._mediaRecorder.Resume();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public void StartRecordWav()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation start record wav \"Record\" service failed!");

                if (this._mediaRecorder == null)
                {
                    string file_name = FilePath.SetFileName("wav");
                    string file_path = FilePath.SetAudioFilePath(file_name);
                    this._storage_path = file_path;
                    this._buffer_size = AudioRecord.GetMinBufferSize(this._sample_rate, this._channel_in, this._encoding);
                    this._audioRecord = new AudioRecord(AudioSource.Mic, this._sample_rate, this._channel_in, this._encoding, this._buffer_size);
                    this._audioRecord.StartRecording();
                    Task.Run(WriteAudioDataToFile);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public string StopRecordMP3()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation stop record mp3 \"Record\" service failed!");

                if (this._mediaRecorder == null)
                {
                    return string.Empty;
                }
                this._mediaRecorder.Resume();
                this._mediaRecorder.Stop();
                this._mediaRecorder = null;
                return this._storage_path;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public string StopRecordWav()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation stop record mp3 \"Record\" service failed!");

                if (this._audioRecord?.RecordingState == RecordState.Recording)
                this._audioRecord?.Stop();
                UpdateAudioHeaderToFile();
                return this._storage_path;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }
        #endregion

        #region SET
        private void WriteAudioDataToFile()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation write audio data to file \"Record\" service failed!");

                byte[] data = new byte[this._buffer_size];
                string audio_file_path = this._storage_path;
                FileOutputStream? outputStream;
                outputStream = new FileOutputStream(audio_file_path);

                if (this._audioRecord is not null)
                {
                    var header = GetWaveFileHeader(0, 0, this._sample_rate, this._channel_mono, this._bit_depth);
                    outputStream.Write(header, 0, WAVHEADERLENGTH);

                    while (this._audioRecord.RecordingState == RecordState.Recording)
                    {
                        var read = _audioRecord.Read(data, 0, this._buffer_size);
                        outputStream.Write(data, 0, read);
                    }

                    outputStream.Close();
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        private void UpdateAudioHeaderToFile()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation update audio header to file \"Record\" service failed!");

                RandomAccessFile randomAccessFile = new(this._storage_path, "rw");

                long total_audio_length = randomAccessFile.Length();
                long total_data_length = total_audio_length + 36;

                byte[] header = GetWaveFileHeader(total_audio_length, total_data_length, this._sample_rate, this._channel_mono, this._bit_depth);

                randomAccessFile.Seek(0);
                randomAccessFile.Write(header, 0, WAVHEADERLENGTH);

                randomAccessFile.Close();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        private static byte[] GetWaveFileHeader(long audio_length, long data_length, long sample_rate, int channels, int bit_depth)
        {
            try
            {
                if (_static_error_false) throw new InvalidOperationException("Operation get wave file header \"Record\" service failed!");

                int block_align = (int)(channels * (bit_depth / 8));
                long byte_rate = sample_rate * block_align;
                byte[] header = new byte[WAVHEADERLENGTH];

                header[0] = Convert.ToByte('R'); // RIFF/WAVE header
                header[1] = Convert.ToByte('I'); // (byte)'I'
                header[2] = Convert.ToByte('F');
                header[3] = Convert.ToByte('F');
                header[4] = (byte)(data_length & 0xff);
                header[5] = (byte)((data_length >> 8) & 0xff);
                header[6] = (byte)((data_length >> 16) & 0xff);
                header[7] = (byte)((data_length >> 24) & 0xff);
                header[8] = Convert.ToByte('W');
                header[9] = Convert.ToByte('A');
                header[10] = Convert.ToByte('V');
                header[11] = Convert.ToByte('E');
                header[12] = Convert.ToByte('f'); // fmt chunk
                header[13] = Convert.ToByte('m');
                header[14] = Convert.ToByte('t');
                header[15] = (byte)' ';
                header[16] = 16; // 4 bytes - size of fmt chunk
                header[17] = 0;
                header[18] = 0;
                header[19] = 0;
                header[20] = 1; // format = 1
                header[21] = 0;
                header[22] = Convert.ToByte(channels);
                header[23] = 0;
                header[24] = (byte)(sample_rate & 0xff);
                header[25] = (byte)((sample_rate >> 8) & 0xff);
                header[26] = (byte)((sample_rate >> 16) & 0xff);
                header[27] = (byte)((sample_rate >> 24) & 0xff);
                header[28] = (byte)(byte_rate & 0xff);
                header[29] = (byte)((byte_rate >> 8) & 0xff);
                header[30] = (byte)((byte_rate >> 16) & 0xff);
                header[31] = (byte)((byte_rate >> 24) & 0xff);
                header[32] = (byte)(block_align); // block align
                header[33] = 0;
                header[34] = Convert.ToByte(bit_depth); // bits per sample
                header[35] = 0;
                header[36] = Convert.ToByte('d');
                header[37] = Convert.ToByte('a');
                header[38] = Convert.ToByte('t');
                header[39] = Convert.ToByte('a');
                header[40] = (byte)(audio_length & 0xff);
                header[41] = (byte)((audio_length >> 8) & 0xff);
                header[42] = (byte)((audio_length >> 16) & 0xff);
                header[43] = (byte)((audio_length >> 24) & 0xff);

                return header;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Operation get wave file header \"Record\" service failed!");
                return null;
            }
        }
        #endregion
    }
}
