using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels
{
    public class MorphologyViewModel : IMorphologyViewModel
    {
        #region ERROR
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
        #endregion

        #region VARIABLE
        protected string VAR_SUBJECT = SettingService.Instance.Suject;
        protected string VAR_PREDICATE = SettingService.Instance.Predicate;
        protected string VAR_PRONOUN = SettingService.Instance.Pronoun;
        protected string VAR_NOUN = SettingService.Instance.Noun;
        protected string VAR_VERB = SettingService.Instance.Verb;
        protected string VAR_PERSONAL = SettingService.Instance.Personal;
        protected string VAR_ADJECTIVE = SettingService.Instance.Adjective;
        protected string VAR_ARTICLE = SettingService.Instance.Article;
        protected string VAR_NUMERAL = SettingService.Instance.Numeral;
        protected string VAR_PREPOSITION = SettingService.Instance.Preposition;
        protected string VAR_POSSESSIVE = SettingService.Instance.Possessive;
        protected string VAR_DEMONSTRATIVE = SettingService.Instance.Demostrtive;
        protected string VAR_ADVERB = SettingService.Instance.Adverb;
        protected string VAR_ADVERB_ADVERB = SettingService.Instance.Adverb_Adverb;
        protected string VAR_ADJECTIVE_NOUN = SettingService.Instance.Adjective_Noun;
        protected string VAR_SINGLE = SettingService.Instance.Single;
        protected string VAR_PLURAL = SettingService.Instance.Plural;

        private IWordEmbeddingService _wordEmbeddingService = new WordEmbeddingService();
        #endregion

        #region UTILS
        private List<string> FilterList(List<string> list, List<Sentenca> sentences)
        {
            try
            {
                List<string> words = new List<string>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string item = string.Empty;
                list.ForEach(word =>
                {
                    item = this._wordEmbeddingService.RemoveAccent(word.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), item) != -1)
                        words.Add(word);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region WORD
        private List<Word> Word(string term, string type, string sentence, string model)
        {
            try
            {
                List<Word> words = new List<Word>();
                Word word = new Word();
                term = this._wordEmbeddingService.RemoveAccent(term.ToLower());
                word.term = term;
                word.kind = type;
                if (sentence != null) word.sentense = sentence;
                if (model != null)
                {
                    model = this._wordEmbeddingService.RemoveAccent(model.ToLower());
                    word.model = model;
                }
                words.Add(word);
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordAdjectiveAdverb(string adjective, string adverb)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb, VAR_ADVERB, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(adjective, VAR_ADJECTIVE, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordAdverbAdverb(string adverb_main, string adverb)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb, VAR_ADVERB_ADVERB, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(adverb_main, VAR_ADVERB, null, null);
                terms.ForEach(item=>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordAdjectiveAdverb(string adjective, string adverb, string adverb_adverb)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb_adverb, VAR_ADVERB_ADVERB, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = WordAdjectiveAdverb(adjective, adverb);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordArticleNoun(string noun, string article)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = new List<Word>();
                terms = Word(article, VAR_ARTICLE, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(noun, VAR_NOUN, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordAdjectiveNounArticle(string noun, string adjective, string article)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(article, VAR_ARTICLE, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = WordArticleNoun(noun, adjective);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordAdjectiveNounArticle(string noun, string adjective, string adverb, string article)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb, VAR_ADVERB, null, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                terms = new List<Word>();
                terms = WordAdjectiveNounArticle(noun, adjective, article);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordAdjectiveNoun(string noun, string adjective, string adverb)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb, VAR_ADVERB, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = WordArticleNoun(noun, adjective);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordAdjectiveNoun(string noun, string adjective)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adjective, VAR_ADJECTIVE, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(noun, VAR_NOUN, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordPronounNoun(string noun, string pronoun)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(pronoun, VAR_PRONOUN, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(noun, VAR_NOUN, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordNumeralNoun(string noun, string digit)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = new List<Word>();
                terms = Word(digit, VAR_NUMERAL, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms= new List<Word>();
                terms = Word(noun, VAR_NOUN, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordVerbAdverb(string verb, string adverb)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(verb, VAR_VERB, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(adverb, VAR_ADVERB, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Word> WordVerbAdverb(string verb, string adverb, string adverb_adverb)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb_adverb, VAR_ADVERB_ADVERB, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = WordVerbAdverb(verb, adverb);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region ADVERB
        private List<Circunstancia> FilterAdverb(List<Circunstancia> adverbs, List<Sentenca> sentences)
        {
            try
            {
                List<Circunstancia> words = new List<Circunstancia>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string item = string.Empty;
                adverbs.ForEach(word =>
                {
                    item = this._wordEmbeddingService.RemoveAccent(word.nome.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), item) != -1)
                        words.Add(word);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> MountAdverbAdverb(List<Circunstancia> adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                List<Circunstancia> circunstances = new List<Circunstancia>();
                adverbs.ForEach(item =>
                {
                    circunstances.Add(item);
                });
                adverbs.ForEach(first =>
                {
                    circunstances.ForEach(last =>
                    {
                        List<Word> words = new List<Word>();
                        words = WordAdverbAdverb(first.nome, last.nome);
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    });
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> VerifyAdverb(List<Lesson> adverbs_adverbs, List<Sentenca> sentences)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                adverbs_adverbs.ForEach(word =>
                {
                    List<Word> words = new List<Word>();
                    string adverb = string.Empty;
                    string adverb_adverb = string.Empty;
                    word.lecture.ForEach(item =>
                    {
                        if (item.kind == VAR_ADVERB) adverb = item.term;
                        if (item.kind == VAR_ADVERB_ADVERB) adverb_adverb = item.term;
                        words.Add(item);
                    });
                    bool similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, adverb, adverb_adverb);
                    if (similarity)
                    {
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    }
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region ADJECTIVE
        private List<Lesson> MountAdjectiveAdverb(List<string> adjectives, List<Circunstancia> adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                adjectives.ForEach(quality =>
                {
                    adverbs.ForEach(circumstance =>
                    {
                        List<Word> words = new List<Word>();
                        words = WordAdjectiveAdverb(quality, circumstance.nome);
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    });
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> MountAdjectiveAdverb(List<string> adjectives, List<Lesson> adverbs_adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                adjectives.ForEach(quality =>
                {
                    adverbs_adverbs.ForEach(circumstance =>
                    {
                        List<Word> words = new List<Word>();
                        string adverb = string.Empty;
                        string adverb_adverb = string.Empty;
                        circumstance.lecture.ForEach(item =>
                        {
                            if (item.kind == VAR_ADVERB) adverb = item.term;
                            if (item.kind == VAR_ADVERB_ADVERB) adverb_adverb = item.term;
                        });
                        words = WordAdjectiveAdverb(quality, adverb, adverb_adverb);
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    });
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> VerifyAdjective(List<Lesson> adjectives_adverbs, List<Sentenca> sentences)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                adjectives_adverbs.ForEach(word =>
                {
                    List<Word> words = new List<Word>();
                    string first = string.Empty;
                    string last = string.Empty;
                    word.lecture.ForEach(item =>
                    {
                        if (item.kind == VAR_ADJECTIVE) first = item.term;
                        if (item.kind != VAR_ADJECTIVE) last = item.term;
                        words.Add(item);
                    });
                    bool similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, first, last);
                    if (similarity)
                    {
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    }
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> UnionAdjective(List<string> adjectives, List<Lesson> adjectives_adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                adjectives.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = Word(item, VAR_ADJECTIVE, null, null);
                    lessons.Add(lesson);
                });
                adjectives_adverbs.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> UnionAdjective(List<Lesson> adjectives_adverbs, List<Lesson> adjectives_adverbs_adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                adjectives_adverbs.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    item.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                adjectives_adverbs_adverbs.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region ARTICLE
        private List<Preceito> FilterArticle(List<Preceito> articles, List<Sentenca> sentences)
        {
            try
            {
                List<Preceito> words = new List<Preceito>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string word = string.Empty;
                articles.ForEach(item =>
                {
                    word = this._wordEmbeddingService.RemoveAccent(item.nome.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), word) != -1)
                        words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private HashSet<string> MountArticle(List<Preceito> articles)
        {
            try
            {
                HashSet<string> precepts = new HashSet<string>();
                articles.ForEach(index =>
                {
                    precepts.Add(index.nome);
                });
                return precepts;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region NOUN
        private List<Lesson> MountAdjectiveNoun(List<string> nouns, List<Lesson> adjectives_adverbs, List<Preceito> articles)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> precepts = new HashSet<string>();
                precepts = MountArticle(articles);
                nouns.ForEach(item_noun =>
                {
                    adjectives_adverbs.ForEach(item_adjective_adverb =>
                    {
                        List<Word> words = new List<Word>();
                        HashSet<string> word = new HashSet<string>(item_noun.Split(' '));
                        if (word.Count > 1)
                        {
                            if (Array.IndexOf(precepts.ToArray(), word.First()) != -1)
                            {
                                string adjective = string.Empty;
                                string adverb = string.Empty;
                                item_adjective_adverb.lecture.ForEach(item_lecture =>
                                {
                                    if (item_lecture.kind == VAR_ADJECTIVE) adjective = item_lecture.term;
                                    if (item_lecture.kind == VAR_ADVERB) adverb = item_lecture.term;
                                });
                                if (adverb != string.Empty) words = WordAdjectiveNounArticle(word.Last(), adjective, adverb, word.First());
                                else words = WordAdjectiveNounArticle(word.Last(), adjective, word.First());
                            }
                        }
                        else
                        {
                            string adjective = string.Empty;
                            string adverb = string.Empty;
                            item_adjective_adverb.lecture.ForEach(item_lecture =>
                            {
                                if (item_lecture.kind == VAR_ADJECTIVE) adjective = item_lecture.term;
                                if (item_lecture.kind == VAR_ADVERB) adverb = item_lecture.term;
                            });
                            if (adverb != string.Empty) words = WordAdjectiveNoun(word.First(), adjective, adverb);
                            else words = WordAdjectiveNoun(word.First(), adjective);
                        }
                        if (words.Count > 0)
                        {
                            Lesson lesson = new Lesson();
                            lesson.lecture = words;
                            lessons.Add(lesson);
                        }
                    });
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> MountNounPronoun(List<string> nouns, List<Estoutro> pronouns, List<Preceito> articles)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> precepts = new HashSet<string>();
                precepts = MountArticle(articles);
                nouns.ForEach(substantive =>
                {
                    pronouns.ForEach(estoutro =>
                    {
                        List<Word> words = new List<Word>();
                        HashSet<string> word = new HashSet<string>(substantive.Split(' '));
                        if (word.Count > 1)
                        {
                            if (Array.IndexOf(precepts.ToArray(), word.First()) != -1)
                                words = WordPronounNoun(word.Last(), estoutro.nome);
                        }
                        else words = WordPronounNoun(word.First(), estoutro.nome);
                        if (words.Count > 0)
                        {
                            Lesson lesson = new Lesson();
                            lesson.lecture = words;
                            lessons.Add(lesson);
                        }
                    });
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> MountNounNumeral(List<string> nouns, List<Algarismo> numerals, List<Preceito> articles)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> precepts = new HashSet<string>();
                precepts = MountArticle(articles);
                nouns.ForEach(substantive =>
                {
                    numerals.ForEach(digit =>
                    {
                        List<Word> words = new List<Word>();
                        HashSet<string> word = new HashSet<string>(substantive.Split(' '));
                        if (word.Count > 1)
                        {
                            if (Array.IndexOf(precepts.ToArray(), word.First()) != -1)
                                words = WordNumeralNoun(word.Last(), digit.nome);
                        }
                        else words = WordNumeralNoun(word.First(), digit.nome);
                        if (words.Count > 0)
                        {
                            Lesson lesson = new Lesson();
                            lesson.lecture = words;
                            lessons.Add(lesson);
                        }
                    });
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> MountNounArticle(List<string> nouns, List<Preceito> articles)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> precepts = new HashSet<string>();
                precepts = MountArticle(articles);
                nouns.ForEach(substantive =>
                {
                    articles.ForEach(norm =>
                    {
                        List<Word> words = new List<Word>();
                        HashSet<string> word = new HashSet<string>(substantive.Split(' '));
                        if (word.Count > 1)
                        {
                            if (Array.IndexOf(precepts.ToArray(), word.First()) != -1)
                                words = WordArticleNoun(word.Last(), word.First());
                        }
                        else words = WordArticleNoun(word.First(), norm.nome);
                        if (words.Count > 0)
                        {
                            Lesson lesson = new Lesson();
                            lesson.lecture = words;
                            lessons.Add(lesson);
                        }
                    });
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> UnionNoun(List<Lesson> firsts, List<Lesson> seconds)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                firsts.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                seconds.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> UnionNoun(List<string> firsts, List<Lesson> seconds)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                firsts.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = Word(item, VAR_NOUN, null, null);
                    lessons.Add(lesson);
                });
                seconds.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> VerifyNoun(List<Lesson> matters, List<Sentenca> sentences)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                matters.ForEach(instruction =>
                {
                    List<Word> words = new List<Word>();
                    string first = string.Empty;
                    string middle = string.Empty;
                    string last = string.Empty;
                    bool three = false;
                    if (instruction.lecture.Count > 2) three = true;
                    instruction.lecture.ForEach(item =>
                    {
                        if (three)
                        {
                            if (item.kind == VAR_ARTICLE) first = item.term;
                            if ((item.kind != VAR_NOUN) && (item.kind != VAR_ARTICLE)) middle = item.term;
                            if (item.kind == VAR_NOUN) last = item.term;
                        }
                        else
                        {
                            if (item.kind != VAR_NOUN) first = item.term;
                            if (item.kind == VAR_NOUN) last = item.term;
                        }
                        words.Add(item);
                    });
                    bool similarity = false;
                    if (three)
                    {
                        similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, first, middle);
                        if (similarity)
                            similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, middle, last);
                    }
                    else
                        similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, first, last);
                    if (similarity)
                    {
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    }
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region NUMERAL
        private List<Algarismo> FilterDigit(List<Algarismo> numerals, List<Sentenca> sentences)
        {
            try
            {
                List<Algarismo> words = new List<Algarismo>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string word = null;
                numerals.ForEach(item =>
                {
                    word = this._wordEmbeddingService.RemoveAccent(item. nome.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), word) != -1)
                    {
                        words.Add(item);
                    }
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region PRONOUN
        private List<Estoutro> FilterPronoun(List<Estoutro> pronouns, List<Sentenca> sentences)
        {
            try
            {
                List<Estoutro> words = new List<Estoutro>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string word = null;
                pronouns.ForEach(item =>
                {
                    word = this._wordEmbeddingService.RemoveAccent(item.nome.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), word) != -1)
                        words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Estoutro> SortPronoun(List<Estoutro> pronouns)
        {
            try
            {
                List<Estoutro> words = new List<Estoutro>();
                words = pronouns;
                int quantity = pronouns.Count();
                for (int first = 0; first < quantity - 1; first++)
                {
                    int index = first;
                    for (int second = first + 1; second < quantity; second++)
                    {
                        int index_second = 0;
                        words[second].contento.ForEach(item =>
                        {
                            index_second = item.pessoa[0];
                        });
                        int index_first = 0;
                        words[index].contento.ForEach(item =>
                        {
                            index_first = item.pessoa[0];
                        });
                        if (index_second < index_first)
                        {
                            index = second;
                        }
                    }
                    Estoutro temp = words[first];
                    words[first] = words[index];
                    words[index] = temp;
                }
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Estoutro> FilterTypePronoun(List<Estoutro> pronouns, List<string> type)
        {
            try
            {
                List<Estoutro> words = new List<Estoutro>();
                for (int quantity = 0; quantity < pronouns.Count() - 1; quantity++)
                {
                    type.ForEach(item =>
                    {
                        if (pronouns[quantity].tipo.Contains(item))
                            words.Add(pronouns[quantity]);
                    });
                }
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Estoutro> MountPronoun(List<string> types, List<Estoutro> pronouns)
        {
            try
            {
                List<Estoutro> surrogates = new List<Estoutro>();
                List<Estoutro> single = new List<Estoutro>();
                List<Estoutro> kind = FilterTypePronoun(pronouns, types);
                kind.ForEach(item =>
                {
                    item.contento.ForEach(contento =>
                    {
                        if (contento.numero.Contains(VAR_SINGLE))
                            single.Add(item);
                    });
                });
                single = SortPronoun(single);
                List<Estoutro> plural = new List<Estoutro>();
                kind.ForEach(item =>
                {
                    item.contento.ForEach(contento =>
                    {
                        if (contento.numero.Contains(VAR_PLURAL))
                            plural.Add(item);
                    });
                });
                plural = SortPronoun(plural);
                single.ForEach(estrouto => surrogates.Add(estrouto));
                plural.ForEach(estrouto => surrogates.Add(estrouto));
                return surrogates;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> MountAdjectivePronoun(List<string> nouns, List<Estoutro> pronouns, List<Preceito> articles)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> precepts = new HashSet<string>();
                precepts = MountArticle(articles);
                nouns.ForEach(substantive =>
                {
                    pronouns.ForEach(surrogate =>
                    {
                        List<Word> words = new List<Word>();
                        HashSet<string> word = new HashSet<string>(substantive.Split(' '));
                        if (word.Count > 1)
                        {
                            if (Array.IndexOf(precepts.ToArray(), word.First()) != -1)
                                words = WordPronounNoun(word.Last(), surrogate.nome);
                        }
                        else words = WordPronounNoun(word.First(), surrogate.nome);
                        if (words.Count > 0)
                        {
                            Lesson lesson = new Lesson();
                            lesson.lecture = words;
                            lessons.Add(lesson);
                        }
                    });
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region PREPOSITION
        private List<Juncao> FilterPreposition(List<Juncao> prepositions, List<Sentenca> sentences)
        {
            try
            {
                List<Juncao> words = new List<Juncao>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string word = string.Empty;
                prepositions.ForEach(item =>
                {
                    word = this._wordEmbeddingService.RemoveAccent(item.nome.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), word) != -1)
                        words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region VERB
        private List<Elocucao> MountVerb(List<string> models, List<Elocucao> verbs)
        {
            try
            {
                List<Elocucao> words = new List<Elocucao>();
                for (int quantity = 0; quantity < verbs.Count() - 1; quantity++)
                {
                    models.ForEach(item =>
                    {
                        string word = verbs[quantity].modelo.ToString().ToLower();
                        word = this._wordEmbeddingService.RemoveAccent(word);
                        if (word.Contains(item))
                            words.Add(verbs[quantity]);
                    });
                }
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Elocucao> FilterVerb(List<Elocucao> verbs, List<Sentenca> sentences)
        {
            try
            {
                List<Elocucao> words = new List<Elocucao>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string word = string.Empty;
                verbs.ForEach(item =>
                {
                    word = this._wordEmbeddingService.RemoveAccent(item.nome.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), word) != -1)
                        words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> VerifyVerb(List<Lesson> verbs_adverbs, List<Sentenca> sentences)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                verbs_adverbs.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    string first = string.Empty;
                    string last = string.Empty;
                    item.lecture.ForEach(word =>
                    {
                        if (word.kind == VAR_VERB) first = word.term;
                        if (word.kind != VAR_VERB) last = word.term;
                        words.Add(word);
                    });

                    bool similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, first, last);
                    if (similarity)
                    {
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    }
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> MountVerbAdverb(List<Elocucao> verbs, List<Circunstancia> adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                verbs.ForEach(elocution =>
                {
                    adverbs.ForEach(circumstance =>
                    {
                        List<Word> words = new List<Word>();
                        words = WordVerbAdverb(elocution.nome, circumstance.nome);
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    });
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> MountVerbAdverb(List<Elocucao> verbs, List<Lesson> adverbs_adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                verbs.ForEach(verb =>
                {
                    adverbs_adverbs.ForEach(circumstance =>
                    {
                        List<Word> words = new List<Word>();
                        string adverb = string.Empty;
                        string adverb_adverb = string.Empty;
                        circumstance.lecture.ForEach(item =>
                        {
                            if (item.kind == VAR_ADVERB) adverb = item.term;
                            if (item.kind == VAR_ADVERB_ADVERB) adverb_adverb = item.term;
                        });
                        words = WordVerbAdverb(verb.nome, adverb, adverb_adverb);
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    });
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> UnionVerb(List<Elocucao> verbs, List<Lesson> verbs_adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                verbs.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = Word(item.nome, VAR_VERB, null, null);
                    lessons.Add(lesson);
                });
                verbs_adverbs.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private List<Lesson> UnionVerb(List<Lesson> verbs_adverbs, List<Lesson> verbs_adverbs_adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                verbs_adverbs.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                verbs_adverbs_adverbs.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region PUBLIC
        public List<Lesson> GetAdjective(List<Sentenca> sentences, List<string> adjectives, List<Circunstancia> adverbs)
        {
            try
            {
                List<string> filter_adjective = FilterList(adjectives, sentences);
                List<Circunstancia> filter_adverb = FilterAdverb(adverbs, sentences);
                List<Lesson> adjective_adverb = MountAdjectiveAdverb(filter_adjective, filter_adverb);
                List<Lesson> adverb_adverb = MountAdverbAdverb(filter_adverb);
                List<Lesson> verify_adjective_adverb = VerifyAdjective(adjective_adverb, sentences);
                List<Lesson> verify_adverb_adverb = VerifyAdverb(adverb_adverb, sentences);
                List<Lesson> adjective_adverb_adverb = MountAdjectiveAdverb(filter_adjective, verify_adverb_adverb);
                List<Lesson> union_adjective_adverb = UnionAdjective(filter_adjective, verify_adjective_adverb);
                List<Lesson> union_adjective_adverb_adverb = UnionAdjective(union_adjective_adverb, adjective_adverb_adverb);

                List<Lesson> lessons = new List<Lesson>();
                union_adjective_adverb_adverb.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.team = VAR_ADJECTIVE;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> GetAdjectiveNoun(List<Sentenca> sentences, List<string> adjectives, List<Circunstancia> adverbs, List<string> nouns, List<Preceito> articles)
        {
            try
            {
                List<string> filter_noun = FilterList(nouns, sentences);
                List<string> filter_adjective = FilterList(adjectives, sentences);
                List<Preceito> filter_article = FilterArticle(articles, sentences);
                List<Circunstancia> filter_adverb = FilterAdverb(adverbs, sentences);
                List<Lesson> adjective_adverb = MountAdjectiveAdverb(filter_adjective, filter_adverb);
                List<Lesson> adjective_noun = MountAdjectiveNoun(filter_noun, adjective_adverb, filter_article);

                List<Lesson> lessons = new List<Lesson>();
                adjective_noun.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.team = VAR_ADJECTIVE_NOUN;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> GetArticle(List<Sentenca> sentences, List<Preceito> articles)
        {
            try
            {
                List<Preceito> filter_article = FilterArticle(articles, sentences);
                List<Lesson> lessons = new List<Lesson>();
                filter_article.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, VAR_ARTICLE, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = VAR_ARTICLE;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> GetNoun(List<Sentenca> sentences, List<string> nouns, List<Estoutro> pronouns, List<Preceito> articles, List<Algarismo> numerals)
        {
            try
            {
                List<string> filter_noun = FilterList(nouns, sentences);
                List<Preceito> filter_article = FilterArticle(articles, sentences);
                List<Algarismo> filter_digit = FilterDigit(numerals, sentences);
                List<string> type_adjective = new List<string>();
                List<string> type_demostrative = new List<string>();
                type_adjective.Add(VAR_ADJECTIVE);
                List<Estoutro> list_pronoun_adjective = MountPronoun(type_adjective, pronouns);
                type_demostrative.Add(VAR_DEMONSTRATIVE);
                List<Estoutro> list_pronoun_demostrative = MountPronoun(type_demostrative, pronouns);
                List<Estoutro> filter_pronoun_adjective = FilterPronoun(list_pronoun_adjective, sentences);
                List<Estoutro> filter_pronoun_demostrative = FilterPronoun(list_pronoun_demostrative, sentences);
                List<Lesson> noun_possessive = MountNounPronoun(filter_noun, filter_pronoun_adjective, filter_article);
                List<Lesson> noun_demostrative = MountAdjectivePronoun(filter_noun, filter_pronoun_demostrative, filter_article);
                List<Lesson> noun_numeral = MountNounNumeral(filter_noun, filter_digit, filter_article);
                List<Lesson> noun_article = MountNounArticle(filter_noun, filter_article);
                List<Lesson> union_substantive_one = UnionNoun(noun_article, noun_possessive);
                List<Lesson> union_substantive_two = UnionNoun(union_substantive_one, noun_numeral);
                List<Lesson> union_substantive_three = UnionNoun(union_substantive_two, noun_demostrative);
                List<Lesson> verify_noun = VerifyNoun(union_substantive_three, sentences);
                List<Lesson> union_noun = UnionNoun(filter_noun, verify_noun);

                List<Lesson> lessons = new List<Lesson>();
                union_noun.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.team = VAR_NOUN;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> GetNumeral(List<Sentenca> sentences, List<Algarismo> numerals)
        {
            try
            {
                List<Algarismo> filter_digit = FilterDigit(numerals, sentences);
                List<Lesson> lessons = new List<Lesson>();

                filter_digit.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, VAR_NUMERAL, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = VAR_NUMERAL;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> GetPreposition(List<Sentenca> sentences, List<Juncao> prepositions)
        {
            try
            {
                List<Juncao> filter_preposition = FilterPreposition(prepositions, sentences);
                List<Lesson> lessons = new List<Lesson>();
                filter_preposition.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, VAR_PREPOSITION, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = VAR_PREPOSITION;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> GetPronoun(List<Sentenca> sentences, List<Estoutro> pronouns)
        {
            try
            {
                List<string> type_personal = new List<string>();
                List<string> type_possessive = new List<string>();
                List<string> type_demostrative = new List<string>();
                type_personal.Add(VAR_PERSONAL);
                List<Estoutro> list_pronoun_personal = MountPronoun(type_personal, pronouns);
                type_possessive.Add(VAR_POSSESSIVE);
                List<Estoutro> list_pronoun_possessive = MountPronoun(type_possessive, pronouns);
                type_demostrative.Add(VAR_DEMONSTRATIVE);
                List<Estoutro> list_pronoun_demostrative = MountPronoun(type_demostrative, pronouns);
                List<Estoutro> filter_pronoun_personal = FilterPronoun(list_pronoun_personal, sentences);
                List<Estoutro> filter_pronoun_possessive = FilterPronoun(list_pronoun_possessive, sentences);
                List<Estoutro> filter_pronoun_demostrative = FilterPronoun(list_pronoun_demostrative, sentences);

                List<Lesson> lessons = new List<Lesson>();
                filter_pronoun_personal.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, VAR_PRONOUN, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = VAR_PERSONAL;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                filter_pronoun_possessive.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, VAR_PRONOUN, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = VAR_POSSESSIVE;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                filter_pronoun_demostrative.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, VAR_PRONOUN, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = VAR_DEMONSTRATIVE;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> GetVerb(List<Sentenca> sentences, List<string> models, List<Elocucao> verbs, List<Circunstancia> adverbs)
        {
            try
            {
                List<Elocucao> list_verb_model = MountVerb(models, verbs);
                List<Elocucao> filter_verb = FilterVerb(list_verb_model, sentences);
                List<Circunstancia> filter_adverb = FilterAdverb(adverbs, sentences);
                List<Lesson> verb_adverb = MountVerbAdverb(filter_verb, filter_adverb);
                List<Lesson> adverb_adverb = MountAdverbAdverb(filter_adverb);
                List<Lesson> verify_verb_adverb = VerifyVerb(verb_adverb, sentences);
                List<Lesson> verify_adverb_adverb = VerifyAdverb(adverb_adverb, sentences);
                List<Lesson> verb_adverb_adverb = MountVerbAdverb(filter_verb, verify_adverb_adverb);
                List<Lesson> union_verb_adverb = UnionVerb(filter_verb, verify_verb_adverb);
                List<Lesson> union_verb_adverb_adverb = UnionVerb(union_verb_adverb, verb_adverb_adverb);

                List<Lesson> lessons = new List<Lesson>();
                union_verb_adverb_adverb.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.team = VAR_VERB;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<string> GetLessonAdjective(Materia lesson, List<Materia> book)
        {
            try
            {
                List<String> adjectives = new List<String>();
                foreach (Materia lecture in book.OrderBy(index => index.ordem).ToList())
                {
                    if (lecture.ordem <= lesson.ordem)
                    {
                        lecture.conteudo.adjetivo.ForEach(adjective =>
                        {
                            if (adjective.Trim().Length > 0)
                            {
                                string word = this._wordEmbeddingService.RemoveAccent(adjective.ToString());
                                adjectives.Add(word);
                            }
                        });
                    }
                }
                adjectives.Distinct();
                return adjectives;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<string> GetLessonNoun(string language, Materia lesson, List<Materia> book)
        {
            try
            {
                List<String> nouns = new List<String>();
                foreach (Materia lecture in book.OrderBy(index => index.ordem).ToList())
                {
                    if (lecture.ordem <= lesson.ordem)
                    {
                        lecture.conteudo.substantivo.ForEach(noun =>
                        {
                            if (noun.Trim().Length > 0)
                            {
                                string word = this._wordEmbeddingService.RemoveAccent(noun.ToString());
                                nouns.Add(word);
                            }
                        });
                    }
                }
                nouns.Distinct();
                return nouns;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<string> GetLessonVerb(Materia lesson)
        {
            try
            {
                List<String> verbs = new List<String>();
                lesson.conteudo.verbo.ForEach(verb =>
                {
                    if (verb.Trim().Length > 0)
                    {
                        string word = this._wordEmbeddingService.RemoveAccent(verb.ToString());
                        verbs.Add(word);
                    }
                });
                verbs.Distinct();
                return verbs;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Lesson> Union(List<Lesson> fists, List<Lesson> lasts)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                fists.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lesson.team = item.team;
                    lessons.Add(lesson);
                });
                lasts.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lesson.team = item.team;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Word> GetSuject(string pronoun, string noun, string article, string numeral, string verb, string model)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                if (pronoun != null) terms = Word(pronoun, VAR_PRONOUN, VAR_SUBJECT, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                if (noun != null) terms = Word(noun, VAR_NOUN, VAR_SUBJECT, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                if (numeral != null) terms = Word(numeral, VAR_NUMERAL, VAR_SUBJECT, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                if (article != null) terms = Word(article, VAR_ARTICLE, VAR_SUBJECT, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                if (verb != null) terms = Word(verb, VAR_VERB, VAR_PREDICATE, model);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Word> GetPredicate(string pronoun, string noun, string article, string numeral, string preposition)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                if (pronoun != null) terms = Word(pronoun, VAR_PRONOUN, VAR_PREDICATE, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                terms = new List<Word>();
                if (article != null) terms = Word(article, VAR_ARTICLE, VAR_PREDICATE, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                terms = new List<Word>();
                if (numeral != null) terms = Word(numeral, VAR_NUMERAL, VAR_PREDICATE, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                terms = new List<Word>();
                if (noun != null) terms = Word(noun, VAR_NOUN, VAR_PREDICATE, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                terms = new List<Word>();
                if (preposition != null) terms = Word(preposition, VAR_PREPOSITION, VAR_PREDICATE, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion
    }
}
