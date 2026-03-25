using LetterStomach.Models;

namespace LetterStomach.Repositories.SQLites
{
    public class EstoutroRepository : IEstoutroRepository
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
        private readonly List<Estoutro>? _estoutro;
        #endregion

        #region CONSTRUCTOR
        public EstoutroRepository(List<Estoutro> estoutro)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Pronoun\" view model failed!");
                else this._error_message = string.Empty;

                this._estoutro = estoutro;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<Estoutro> FilterLanguage(List<Estoutro>? list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Pronoun\" view model failed!");

                List<Estoutro> new_list = new List<Estoutro>();
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

        private Estoutro FilterName(List<Estoutro>? list, string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter name \"Pronoun\" view model failed!");

                Estoutro pronoun = new Estoutro();
                if (list == null) return pronoun;
                foreach (Estoutro item in list)
                {
                    if (item.nome == name)
                    {
                        pronoun = item;
                        break;
                    }
                }
                return pronoun;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Estoutro> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Pronoun\" view model failed!");

                return FilterLanguage(this._estoutro, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Estoutro>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Pronoun\" view model failed!");

                return FilterLanguage(this._estoutro, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public Estoutro GetName(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Pronoun\" view model failed!");

                return FilterName(this._estoutro, name);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<Estoutro> GetNameAsync(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name async \"Pronoun\" view model failed!");

                return FilterName(this._estoutro, name);
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
