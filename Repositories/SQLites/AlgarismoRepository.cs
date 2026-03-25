using LetterStomach.Models;

namespace LetterStomach.Repositories.SQLites
{
    public class AlgarismoRepository : IAlgarismoRepository
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
        private readonly List<Algarismo>? _algarismo;
        #endregion

        #region CONSTRUCTOR
        public AlgarismoRepository(List<Algarismo> algarismo)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Numeral\" repository failed!");
                else this._error_message = string.Empty;

                this._algarismo = algarismo;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<Algarismo> FilterLanguage(List<Algarismo>? list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Numeral\" repository failed!");

                List<Algarismo> new_list = new List<Algarismo>();
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
        public List<Algarismo> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Numeral\" repository failed!");

                return FilterLanguage(this._algarismo, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Algarismo>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Numeral\" repository failed!");

                return FilterLanguage(this._algarismo, language);
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
