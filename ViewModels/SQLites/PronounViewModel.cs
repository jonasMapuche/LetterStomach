using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
{
    public class PronounViewModel : IPronounViewModel
    {
        private List<Estoutro> FilterLanguage(List<Estoutro> list, string language)
        {
            try
            {
                List<Estoutro> new_list = new List<Estoutro>();
                list.ForEach(value =>
                {
                    if (value.linguagem == language)
                        new_list.Add(value);
                });
                return new_list;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        private Estoutro FilterName(List<Estoutro> list, string name)
        {
            try
            {
                Estoutro pronoun = new Estoutro();
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
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<Estoutro> GetLanguage(string language)
        {
            try
            { 
                return FilterLanguage(App.DataService.Estoutro, language);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Estoutro GetName(string name)
        {
            try
            { 
                return FilterName(App.DataService.Estoutro, name);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
