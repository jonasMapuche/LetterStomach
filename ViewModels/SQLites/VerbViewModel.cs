using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
{
    public class VerbViewModel : IVerbViewModel
    {
        private string _error_message;

        public string error_message
        {
            get => _error_message;
            set
            {
                _error_message = value;
            }
        }

        public event EventHandler<string> OnError;

        private List<Elocucao> FilterLanguage(List<Elocucao> list, string language)
        {
            try
            {
                List<Elocucao> new_list = new List<Elocucao>();
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
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Elocucao> FilterModel(List<Elocucao> list, string language, string model)
        {
            try
            {
                List<Elocucao> new_list = new List<Elocucao>();
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
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Elocucao> GetLanguage(string language)
        {
            try
            {
                return FilterLanguage(App.DataService.Elocucao, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Elocucao> GetModel(string language, string model)
        {
            try
            {
                return FilterModel(App.DataService.Elocucao, language, model);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
    }
}
