namespace LetterStomach.Helpers
{
    public static class FilePath
    {
        private static string _error_message;

        public static string error_message
        {
            get => _error_message;
            set
            {
                _error_message = value;
            }
        }

        public static event EventHandler<string> OnError;

        public static string SetFileName(string extension)
        {
            try
            {
                string file_name = "/Record_" + DateTime.UtcNow.ToString("ddMMM_hhmmss") + (extension == "mp3" ? ".mp3" : ".wav");
                return file_name;
            }
            catch (Exception ex)
            {
                error_message = ex.Message;
                OnError?.Invoke(null, error_message);
                return null;
            }
        }

        public static string SetAudioFilePath(string file_name)
        {
            try
            {
                string path = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                string file_path = path + file_name;
                Directory.CreateDirectory(file_path);
                return file_path;
            }
            catch (Exception ex)
            {
                error_message = ex.Message;
                OnError?.Invoke(null, error_message);
                return null;
            }
        }
    }
}
