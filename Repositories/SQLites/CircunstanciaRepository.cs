using LetterStomach.Models;
using SQLite;

namespace LetterStomach.Repositories.SQLites
{
    public class CircunstanciaRepository : ICircunstanciaRepository
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
        private readonly List<Circunstancia>? _circunstancias;
        #endregion

        #region CONSTRUCTOR
        public CircunstanciaRepository(List<Circunstancia> circunstancias)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Adverb\" repository failed!");
                else this._error_message = string.Empty;

                this._circunstancias = circunstancias;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<Circunstancia> FilterLanguage(List<Circunstancia>? list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Adverb\" repository failed!");

                List<Circunstancia> new_list = new List<Circunstancia>();
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

        private Circunstancia FilterName(List<Circunstancia>? list, string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter name \"Adverb\" repository failed!");

                Circunstancia adverb = new Circunstancia();
                if (list == null) return adverb;
                foreach (Circunstancia item in list)
                {
                    if (item.nome == name)
                    {
                        adverb = item;
                        break;
                    }
                }
                return adverb;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Circunstancia> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Adverb\" repository failed!");

                return FilterLanguage(this._circunstancias, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Circunstancia>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Adverb\" repository failed!");

                return FilterLanguage(this._circunstancias, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public Circunstancia GetName(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Adverb\" repository failed!");

                return FilterName(this._circunstancias, name);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<Circunstancia> GetNameAsync(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name async \"Adverb\" repository failed!");

                return FilterName(this._circunstancias, name);
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
