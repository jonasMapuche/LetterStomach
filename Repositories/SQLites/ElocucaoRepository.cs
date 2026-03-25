using LetterStomach.Models;

namespace LetterStomach.Repositories.SQLites
{
    public class ElocucaoRepository : IElocucaoRepository
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
        private readonly List<Elocucao>? _elocucao;
        #endregion

        #region CONSTRUCTOR
        public ElocucaoRepository(List<Elocucao> elocucao)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Verb\" repository failed!");
                else this._error_message = string.Empty;

                this._elocucao = elocucao;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<Elocucao> FilterLanguage(List<Elocucao>? list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Verb\" repository failed!");

                List<Elocucao> new_list = new List<Elocucao>();
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

        private List<Elocucao> FilterModel(List<Elocucao>? list, string language, string model)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter model \"Verb\" repository failed!");

                List<Elocucao> new_list = new List<Elocucao>();
                if (list == null) return new_list;
                list.ForEach(value =>
                {
                    if ((value.linguagem == language) && (value.modelo == model))
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
        public List<Elocucao> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Verb\" repository failed!");

                return FilterLanguage(this._elocucao, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Elocucao>> GetLanguageAsync(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language async \"Verb\" repository failed!");

                return FilterLanguage(this._elocucao, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Elocucao> GetModel(string language, string model)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get model \"Verb\" repository failed!");

                return FilterModel(this._elocucao, language, model);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Elocucao>> GetModelAsync(string language, string model)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get model async \"Verb\" repository failed!");

                return FilterModel(this._elocucao, language, model);
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
