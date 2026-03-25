using LetterStomach.Models;

namespace LetterStomach.Repositories.SQLites  
{
    public class JuncaoRepository : IJuncaoRepository
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
        private readonly List<Juncao>? _juncao;
        #endregion

        #region CONSTRUCTOR
        public JuncaoRepository(List<Juncao> juncao)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Preposition\" repository failed!");
                else this._error_message = string.Empty;

                this._juncao = juncao;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<Juncao> FilterLanguage(List<Juncao>? list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Preposition\" repository failed!");

                List<Juncao> new_list = new List<Juncao>();
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
        public List<Juncao> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Preposition\" repository failed!");

                return FilterLanguage(this._juncao, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Juncao>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Preposition\" repository failed!");

                return FilterLanguage(this._juncao, language);
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
