using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
{
    public class ArticleViewModel : IArticleViewModel
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

        private List<Preceito> FilterLanguage(List<Preceito> list, string language)
        {
            try
            {
                List<Preceito> new_list = new List<Preceito>();
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

        private Preceito FilterName(List<Preceito> list, string name)
        {
            try
            {
                Preceito article = new Preceito();
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
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Preceito> GetLanguage(string language)
        {
            try
            { 
                return FilterLanguage(App.DataService.Preceito, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public Preceito GetName(string name)
        {
            try
            { 
                return FilterName(App.DataService.Preceito, name);
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
