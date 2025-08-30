using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels
{
    public class GrammarViewModel : WordEmbeddingService
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

        public IAdverbViewModel _adverbsViewModel;
        public IArticleViewModel _articlesViewModel;
        public IPronounViewModel _pronounsViewModel;
        public INumeralViewModel _numeralsViewModel;
        public IPrepositionViewModel _prepositionsViewModel;
        public IVerbViewModel _verbsViewModel;
        public ILetterViewModel _lettersViewModel;
        public ISentenceViewModel _sentencesViewModel;
        public IConjunctionViewModel _conjunctionViewModel;

        protected List<Circunstancia> _adverb_english = new List<Circunstancia>();
        protected List<Circunstancia> _adverb_deutsch = new List<Circunstancia>();
        protected List<Circunstancia> _adverb_italiano = new List<Circunstancia>();
        protected List<Circunstancia> _adverb_francais = new List<Circunstancia>();
        protected List<Circunstancia> _adverb_espanol = new List<Circunstancia>();

        protected List<Preceito> _article_english = new List<Preceito>();
        protected List<Preceito> _article_deutsch = new List<Preceito>();
        protected List<Preceito> _article_italiano = new List<Preceito>();
        protected List<Preceito> _article_francais = new List<Preceito>();
        protected List<Preceito> _article_espanol = new List<Preceito>();

        protected List<Estoutro> _pronoun_english = new List<Estoutro>();
        protected List<Estoutro> _pronoun_deutsch = new List<Estoutro>();
        protected List<Estoutro> _pronoun_italiano = new List<Estoutro>();
        protected List<Estoutro> _pronoun_francais = new List<Estoutro>();
        protected List<Estoutro> _pronoun_espanol = new List<Estoutro>();

        protected List<Algarismo> _numeral_english = new List<Algarismo>();
        protected List<Algarismo> _numeral_deutsch = new List<Algarismo>();
        protected List<Algarismo> _numeral_italiano = new List<Algarismo>();
        protected List<Algarismo> _numeral_francais = new List<Algarismo>();
        protected List<Algarismo> _numeral_espanol = new List<Algarismo>();

        protected List<Juncao> _preposition_english = new List<Juncao>();
        protected List<Juncao> _preposition_deutsch = new List<Juncao>();
        protected List<Juncao> _preposition_italiano = new List<Juncao>();
        protected List<Juncao> _preposition_francais = new List<Juncao>();
        protected List<Juncao> _preposition_espanol = new List<Juncao>();

        protected List<Elocucao> _verb_english = new List<Elocucao>();
        protected List<Elocucao> _verb_deutsch = new List<Elocucao>();
        protected List<Elocucao> _verb_italiano = new List<Elocucao>();
        protected List<Elocucao> _verb_francais = new List<Elocucao>();
        protected List<Elocucao> _verb_espanol = new List<Elocucao>();

        protected List<Materia> _letter_english = new List<Materia>();
        protected List<Materia> _letter_deutsch = new List<Materia>();
        protected List<Materia> _letter_italiano = new List<Materia>();
        protected List<Materia> _letter_francais = new List<Materia>();
        protected List<Materia> _letter_espanol = new List<Materia>();

        protected List<Sentenca> _sentence_english = new List<Sentenca>();
        protected List<Sentenca> _sentence_deutsch = new List<Sentenca>();
        protected List<Sentenca> _sentence_italiano = new List<Sentenca>();
        protected List<Sentenca> _sentence_francais = new List<Sentenca>();
        protected List<Sentenca> _sentence_espanol = new List<Sentenca>();

        protected List<Ligacao> _conjunction_english = new List<Ligacao>();
        protected List<Ligacao> _conjunction_deutsch = new List<Ligacao>();
        protected List<Ligacao> _conjunction_italiano = new List<Ligacao>();
        protected List<Ligacao> _conjunction_francais = new List<Ligacao>();
        protected List<Ligacao> _conjunction_espanol = new List<Ligacao>();

        protected Language ENGLISH = SettingService.Instance.English;
        protected Language DEUTSCH = SettingService.Instance.Deutsch;
        protected Language ITALIANO = SettingService.Instance.Italino;
        protected Language FRANCAIS = SettingService.Instance.Francais;
        protected Language ESPANOL = SettingService.Instance.Espanol;

        public void MongoDB()
        {
            try
            { 
                _adverbsViewModel = new MongoDBs.AdverbViewModel();
                _articlesViewModel = new MongoDBs.ArticleViewModel();
                _pronounsViewModel = new MongoDBs.PronounViewModel();
                
                _numeralsViewModel = new MongoDBs.NumeralViewModel();
                _prepositionsViewModel = new MongoDBs.PrepositionViewModel();
                _verbsViewModel = new MongoDBs.VerbViewModel();
                _lettersViewModel = new MongoDBs.LetterViewModel();
                _sentencesViewModel = new MongoDBs.SentenceViewModel();
                _conjunctionViewModel = new MongoDBs.ConjunctionViewModel();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void SQLite()
        {
            try
            { 
                _adverbsViewModel = new SQLites.AdverbViewModel();
                _articlesViewModel = new SQLites.ArticleViewModel();
                _pronounsViewModel = new SQLites.PronounViewModel();

                _numeralsViewModel = new SQLites.NumeralViewModel();
                _prepositionsViewModel = new SQLites.PrepositionViewModel();
                _verbsViewModel = new SQLites.VerbViewModel();
                _lettersViewModel = new SQLites.LetterViewModel();
                _sentencesViewModel = new SQLites.SentenceViewModel();
                _conjunctionViewModel = new SQLites.ConjunctionViewModel();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public List<Circunstancia> GetAdverb(string language)
        {
            try
            {
                return _adverbsViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Preceito> GetArticle(string language)
        {
            try
            {
                return _articlesViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Estoutro> GetPronoun(string language)
        {
            try
            {
                return _pronounsViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Algarismo> GetNumeral(string language)
        {
            try
            {
                return _numeralsViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Juncao> GetPreposition(string language)
        {
            try
            {
                return _prepositionsViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Elocucao> GetVerb(string language)
        {
            try
            {
                return _verbsViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Sentenca> GetSentence(string language)
        {
            try
            {
                return _sentencesViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Ligacao> GetConjunction(string language)
        {
            try
            {
                return _conjunctionViewModel.GetLanguage(language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Materia> GetLetter(string language)
        {
            try
            {
                return _lettersViewModel.GetLessonSimple(true, language);
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
