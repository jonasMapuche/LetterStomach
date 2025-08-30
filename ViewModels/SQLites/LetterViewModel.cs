using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
{
    public class LetterViewModel : ILetterViewModel
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

        private List<Materia> FilterLanguage(List<Materia> list, string language, bool lesson)
        {
            try
            {
                List<Materia> new_list = new List<Materia>();
                list.ForEach(value =>
                {
                    if ((value.linguagem == language) && (lesson))
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

        public List<Materia> GetLessonSimple(bool lesson, string language)
        {
            try
            {
                return FilterLanguage(App.DataService.Materia, language, lesson);
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
