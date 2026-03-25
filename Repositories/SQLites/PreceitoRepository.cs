using LetterStomach.Models;
using SQLite;

namespace LetterStomach.Repositories.SQLites
{
    public class PreceitoRepository : IPreceitoRepository
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
        private readonly List<Preceito>? _preceito;
        #endregion

        #region CONSTRUCTOR
        public PreceitoRepository(List<Preceito> preceito)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Article\" repository failed!");
                else this._error_message = string.Empty;

                this._preceito = preceito;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<Preceito> FilterLanguage(List<Preceito>? list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Article\" repository failed!");

                List<Preceito> new_list = new List<Preceito>();
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

        private Preceito FilterName(List<Preceito>? list, string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter name \"Article\" repository failed!");

                Preceito article = new Preceito();
                if (list == null) return article;
                foreach (Preceito item in list)
                {
                    if (item.nome == name)
                    {
                        article = item;
                        break;
                    }
                }
                return article;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Preceito> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Article\" repository failed!");

                return FilterLanguage(this._preceito, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Preceito>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Article\" repository failed!");

                return FilterLanguage(this._preceito, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public Preceito GetName(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Article\" repository failed!");

                return FilterName(this._preceito, name);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<Preceito> GetNameAsync(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Article\" repository failed!");

                return FilterName(this._preceito, name);
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
