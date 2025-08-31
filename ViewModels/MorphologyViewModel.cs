using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels
{
    public class MorphologyViewModel : IMorphologyViewModel
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

        private IWordEmbeddingService _wordEmbeddingService = new WordEmbeddingService();

        #region UTILS
        private List<string> FilterList(List<string> values, List<Sentenca> sentences)
        {
            try
            {
                List<string> words = new List<string>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string item = null;
                values.ForEach(word =>
                {
                    item = this._wordEmbeddingService.RemoveAccent(word.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), item) != -1)
                    {
                        words.Add(word);
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

        #region WORD
        public List<Word> Word(string term, string type, string sentence, string model)
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

        public List<Word> WordAdverbAdverb(string adverb_main, string adverb)
        {
            try
            {
                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb, VAR_ADVERB_ADVERB, null, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                terms = new List<Word>();
                terms = Word(adverb_main, VAR_ADVERB, null, null);
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

        public List<Word> WordAdjectiveAdverb(string adjective, string adverb, string adverb_adverb)
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

        public List<Word> WordArticleNoun(string noun, string article)
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

        public List<Word> WordAdjectiveNounArticle(string noun, string adjective, string article)
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

        public List<Word> WordAdjectiveNounArticle(string noun, string adjective, string adverb, string article)
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

        public List<Word> WordAdjectiveNoun(string noun, string adjective, string adverb)
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
                terms = WordArticleNoun(noun, adjective);
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

        #region ADVERB
        private List<Circunstancia> FilterAdverb(List<Circunstancia> adverbs, List<Sentenca> sentences)
        {
            try
            {
                List<Circunstancia> words = new List<Circunstancia>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string item = null;
                adverbs.ForEach(word =>
                {
                    item = this._wordEmbeddingService.RemoveAccent(word.nome.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), item) != -1)
                    {
                        words.Add(word);
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

        public List<Lesson> MountAdverbAdverb(List<Circunstancia> adverbs)
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
                        List<Word> terms = new List<Word>();
                        terms = WordAdverbAdverb(first.nome, last.nome);
                        terms.ForEach(item =>
                        {
                            words.Add(item);
                        });
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

        public List<Lesson> VerifyAdverb(List<Lesson> adverbs_adverbs, List<Sentenca> sentences)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                adverbs_adverbs.ForEach(word =>
                {
                    List<Word> words = new List<Word>();
                    string adverb = null;
                    string adverb_adverb = null;
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
        public List<Lesson> MountAdjectiveAdverb(List<string> adjectives, List<Circunstancia> adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                adjectives.ForEach(quality =>
                {
                    adverbs.ForEach(circumstance =>
                    {
                        List<Word> words = new List<Word>();
                        List<Word> terms = new List<Word>();
                        terms = WordAdjectiveAdverb(quality, circumstance.nome);
                        terms.ForEach(item =>
                        {
                            words.Add(item);
                        });
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

        public List<Lesson> MountAdjectiveAdverb(List<string> adjectives, List<Lesson> adverbs_adverbs)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                adjectives.ForEach(quality =>
                {
                    adverbs_adverbs.ForEach(circumstance =>
                    {
                        List<Word> words = new List<Word>();
                        string adverb = null;
                        string adverb_adverb = null;
                        circumstance.lecture.ForEach(item =>
                        {
                            if (item.kind == VAR_ADVERB) adverb = item.term;
                            if (item.kind == VAR_ADVERB_ADVERB) adverb_adverb = item.term;
                        });
                        List<Word> item = new List<Word>();
                        item = WordAdjectiveAdverb(quality, adverb, adverb_adverb);
                        item.ForEach(item =>
                        {
                            words.Add(item);
                        });
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
                    string first = null;
                    string last = null;
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

        public List<Lesson> UnionAdjective(List<string> adjectives, List<Lesson> adjectives_adverbs)
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

        public List<Lesson> UnionAdjective(List<Lesson> adjectives_adverbs, List<Lesson> adjectives_adverbs_adverbs)
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
                string word = "";
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

        public HashSet<string> MountArticle(List<Preceito> articles)
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
        public List<Lesson> MountAdjectiveNoun(List<string> nouns, List<Lesson> adjectives_adverbs, List<Preceito> articles)
        {
            try
            {
                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> precepts = new HashSet<string>();
                precepts = MountArticle(articles);

                nouns.ForEach(value_noun =>
                {
                    adjectives_adverbs.ForEach(value_adjective_adverb =>
                    {
                        List<Word> words = new List<Word>();
                        List<Word> terms = new List<Word>();
                        HashSet<string> word = new HashSet<string>(value_noun.Split(' '));
                        if (word.Count > 1)
                        {
                            if (Array.IndexOf(precepts.ToArray(), word.First()) != -1)
                            {
                                string item_adjective = null;
                                string item_adverb = null;
                                value_adjective_adverb.lecture.ForEach(value_lecture =>
                                {
                                    if (value_lecture.kind == VAR_ADJECTIVE) item_adjective = value_lecture.term;
                                    if (value_lecture.kind == VAR_ADVERB) item_adverb = value_lecture.term;
                                });

                                if (item_adverb != null) terms = WordAdjectiveNounArticle(word.Last(), item_adjective, item_adverb, word.First());
                                else terms = WordAdjectiveNounArticle(word.Last(), item_adjective, word.First());
                            }
                        }
                        else
                        {
                            string item_adjective = null;
                            string item_adverb = null;
                            value_adjective_adverb.lecture.ForEach(value_lecture =>
                            {
                                if (value_lecture.kind == VAR_ADJECTIVE) item_adjective = value_lecture.term;
                                if (value_lecture.kind == VAR_ADVERB) item_adverb = value_lecture.term;
                            });
                            if (item_adverb != null) terms = WordAdjectiveNoun(word.First(), item_adjective, item_adverb);
                            else terms = WordAdjectiveNoun(word.First(), item_adjective);
                        }
                        terms.ForEach(item =>
                        {
                            words.Add(item);
                        });
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    });
                });
                //---
                return lessons;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        #endregion

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

        public List<Lesson> GetArticle(List<Sentenca> sentence, List<Preceito> article)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> GetNoun(List<Sentenca> sentence, List<string> noun, List<Estoutro> pronoun, List<Preceito> article, List<Algarismo> digit)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> GetNumeral(List<Sentenca> sentence, List<Algarismo> digit)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> GetPreposition(List<Sentenca> sentence, List<Juncao> preposition)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> GetPronoun(List<Sentenca> sentence, List<Estoutro> pronoun)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> GetVerb(List<Sentenca> sentence, List<string> model, List<Elocucao> verb, List<Circunstancia> adverb)
        {
            throw new NotImplementedException();
        }

        public List<string> GetLessonAdjective(Materia lesson, List<Materia> book)
        {
            throw new NotImplementedException();
        }

        public List<string> GetLessonNoun(string language, Materia lesson, List<Materia> book)
        {
            throw new NotImplementedException();
        }

        public List<string> GetLessonVerb(Materia lesson)
        {
            throw new NotImplementedException();
        }

        public List<Lesson> Union(List<Lesson> list_fist, List<Lesson> list_last)
        {
            throw new NotImplementedException();
        }

        public List<Word> GetSuject(string pronoun_subject, string noun_subject, string article_subject, string digit_subject, string verb, string model)
        {
            throw new NotImplementedException();
        }

        public List<Word> GetPredicate(string pronoun_predicate, string noun_predicate, string article_predicate, string digit_predicate, string preposition)
        {
            throw new NotImplementedException();
        }
    }
}
