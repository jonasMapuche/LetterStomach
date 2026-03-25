using LetterStomach.Models;

namespace LetterStomach.Repositories.SQLites
{
    public class SentencaRepository : ISentencaRepository
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

        public event EventHandler<string>? OnError;
        #endregion

        #region VARIABLE
        private readonly List<Sentenca>? _sentenca;
        #endregion

        #region CONSTRUCTOR
        public SentencaRepository(List<Sentenca> sentenca)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Sentence\" view model failed!");
                else this._error_message = string.Empty;

                this._sentenca= sentenca;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<Sentenca> FilterLanguage(List<Sentenca>? list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Sentence\" view model failed!");

                List<Sentenca> new_list = new List<Sentenca>();
                if (list == null) return new_list;
                list.ForEach(value =>
                {
                    if (value.linguagem == language)
                        new_list.Add(value);
                });
                return new_list;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Sentenca> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Sentence\" view model failed!");

                return FilterLanguage(this._sentenca, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Sentenca>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Sentence\" view model failed!");

                return FilterLanguage(this._sentenca, language);
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
