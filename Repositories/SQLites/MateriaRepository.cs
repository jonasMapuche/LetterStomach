using LetterStomach.Models;
using SQLite;

namespace LetterStomach.Repositories.SQLites
{
    public class MateriaRepository : IMateriaRepository
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
        private readonly List<Materia>? _materia;
        #endregion

        #region CONSTRUCTOR
        public MateriaRepository(List<Materia> materia)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Letter\" repository failed!");
                else this._error_message = string.Empty;

                this._materia = materia;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<Materia> FilterLanguage(List<Materia>? list, string language, bool lesson)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Letter\" repository failed!");

                List<Materia> new_list = new List<Materia>();
                if (list == null) return new_list;
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Materia> GetLessonSimple(bool lesson, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get lesson simple \"Letter\" repository failed!");

                return FilterLanguage(this._materia, language, lesson);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Materia>> GetLessonSimpleAsync(bool lesson, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get lesson simple async \"Letter\" repository failed!");

                return FilterLanguage(this._materia, language, lesson);
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
