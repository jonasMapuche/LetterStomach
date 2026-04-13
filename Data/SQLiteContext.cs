using SQLite;

namespace LetterStomach.Data
{
    public class SQLiteContext
    {
        private SQLiteAsyncConnection _connection;
        private string _file_sqlite = "letter.db";
        private string _path;

        public SQLiteContext()
        {
            string file_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this._file_sqlite);
            this._path = file_path;
            this._connection = new SQLiteAsyncConnection(file_path);
        }

        public SQLiteAsyncConnection GetConnection() => this._connection;
        public string GetFilePath() => this._path;
    }
}
