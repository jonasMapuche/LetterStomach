using LetterStomach.Models;
using LetterStomach.Services.Interfaces;

namespace LetterStomach.Services
{
    public class MorphologyService : IMorphologyService
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string? _error_message;

        public string? error_message
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
        private string _subject;
        private string _predicate;
        private string _pronoun;
        private string _noun;
        private string _verb;
        private string _personal;
        private string _adjective;
        private string _article;
        private string _numeral;
        private string _preposition;
        private string _possessive;
        private string _demonstrative;
        private string _adverb;
        private string _adverb_adverb;
        private string _adjective_noun;
        private string _adjective_adverb;
        private string _conjunction;
        private string _single;
        private string _plural;
        private string _numeral_noun;
        private string _infinitive;
        private string _adnominal_adjunct;
        private string _adverbial_verb;
        private string _adverbial_adjective;

        private IWordEmbeddingService _wordEmbeddingService;
        #endregion

        #region CONSTRUCTOR
        public MorphologyService()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Morphology\" service failed!");

                this._subject = SettingService.Instance.Suject;
                this._predicate = SettingService.Instance.Predicate;
                this._pronoun = SettingService.Instance.Pronoun;
                this._noun = SettingService.Instance.Noun;
                this._verb = SettingService.Instance.Verb;
                this._personal = SettingService.Instance.Personal;
                this._adjective = SettingService.Instance.Adjective;
                this._article = SettingService.Instance.Article;
                this._numeral = SettingService.Instance.Numeral;
                this._preposition = SettingService.Instance.Preposition;
                this._possessive = SettingService.Instance.Possessive;
                this._demonstrative = SettingService.Instance.Demostrtive;
                this._adverb = SettingService.Instance.Adverb;
                this._adverb_adverb = SettingService.Instance.Adverb_Adverb;
                this._adjective_noun = SettingService.Instance.Adjective_Noun;
                this._adjective_adverb = SettingService.Instance.Adjective_Adverb;
                this._conjunction = SettingService.Instance.Conjunction;
                this._single = SettingService.Instance.Single;
                this._plural = SettingService.Instance.Plural;
                this._numeral_noun = SettingService.Instance.Numeral_Noun;
                this._infinitive = SettingService.Instance.Infinitive;
                this._adnominal_adjunct = SettingService.Instance.Adnominal_Adjunct;
                this._adverbial_verb = SettingService.Instance.Adverbial_Verb;
                this._adverbial_adjective = SettingService.Instance.Adverbial_Adjective;

                this._wordEmbeddingService = new WordEmbeddingService();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region FILTER
        private List<string> FilterList(List<string> list, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter list \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<string> FilterList(List<string> list, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter list \"Morphology\" service failed!");

                List<string> words = new List<string>();
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Circunstancia> FilterAdverb(List<Circunstancia> adverbs, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter adverb \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Circunstancia> FilterAdverb(List<Circunstancia> adverbs, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter adverb \"Morphology\" service failed!");
                List<Circunstancia> words = new List<Circunstancia>();
                string word = string.Empty;
                foreach (Circunstancia item in adverbs)
                {
                    word = this._wordEmbeddingService.RemoveAccent(item.nome.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), word) != -1)
                        words.Add(item);
                }
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Preceito> FilterArticle(List<Preceito> articles, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter article \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Preceito> FilterArticle(List<Preceito> articles, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter article \"Morphology\" service failed!");

                List<Preceito> words = new List<Preceito>();
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Algarismo> FilterNumeral(List<Algarismo> numerals, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter numeral \"Morphology\" service failed!");

                List<Algarismo> words = new List<Algarismo>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string word = string.Empty;
                numerals.ForEach(item =>
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Algarismo> FilterNumeral(List<Algarismo> numerals, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter numeral \"Morphology\" service failed!");

                List<Algarismo> words = new List<Algarismo>();
                string word = string.Empty;
                numerals.ForEach(item =>
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Estoutro> FilterPronoun(List<Estoutro> pronouns, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter pronoun \"Morphology\" service failed!");

                List<Estoutro> words = new List<Estoutro>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string word = string.Empty;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Estoutro> FilterPronoun(List<Estoutro> pronouns, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter pronoun \"Morphology\" service failed!");

                List<Estoutro> words = new List<Estoutro>();
                string word = string.Empty;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Estoutro> FilterTypePronoun(List<Estoutro> pronouns, List<string> kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter type pronoun \"Morphology\" service failed!");

                List<Estoutro> words = new List<Estoutro>();
                for (int quantity = 0; quantity < pronouns.Count(); quantity++)
                {
                    kind.ForEach(item =>
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Juncao> FilterPreposition(List<Juncao> prepositions, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter preposition \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Juncao> FilterPreposition(List<Juncao> prepositions, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter preposition \"Morphology\" service failed!");

                List<Juncao> words = new List<Juncao>();
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Elocucao> FilterVerb(List<Elocucao> verbs, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter verb \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Elocucao> FilterVerb(List<Elocucao> verbs, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter verb \"Morphology\" service failed!");

                List<Elocucao> words = new List<Elocucao>();
                string word = string.Empty;
                foreach (Elocucao item in verbs)
                {
                    word = this._wordEmbeddingService.RemoveAccent(item.nome.ToLower());
                    if (Array.IndexOf(vocabulary.ToArray(), word) != -1)
                        words.Add(item);
                };
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Elocucao> FilterReverseVerb(List<Elocucao> verbs, List<string> modes)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter verb \"Morphology\" service failed!");
                HashSet<string> styles = new HashSet<string>(modes);
                List<Elocucao> words = new List<Elocucao>();
                string word = string.Empty;
                verbs.ForEach(item =>
                {
                    List<Teor> contents = new List<Teor>();
                    item.teor.ForEach(subject =>
                    {
                        subject.modo.ForEach(mode =>
                        {
                            if (Array.IndexOf(styles.ToArray(), mode) == -1)
                                contents.Add(subject);
                        });
                    });
                    Elocucao word = new Elocucao();
                    word.nome = item.nome;
                    word.linguagem = item.linguagem;
                    word.modelo = item.modelo;
                    word.teor = contents;
                    if (contents.Count > 0) words.Add(word);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Ligacao> FilterConjunction(List<Ligacao> conjunctions, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter conjunction \"Morphology\" service failed!");

                List<Ligacao> words = new List<Ligacao>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                string word = string.Empty;
                conjunctions.ForEach(item =>
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Ligacao> FilterConjunction(List<Ligacao> conjunctions, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter conjunction \"Morphology\" service failed!");

                List<Ligacao> words = new List<Ligacao>();
                string word = string.Empty;
                conjunctions.ForEach(item =>
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region UNION
        private List<Lesson> Union(List<Lesson> firsts, List<Lesson> last)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation union \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                firsts.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                last.ForEach(item =>
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> Union(List<string> first, List<Lesson> last)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation union \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                first.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = Word(item, this._adjective, null, null);
                    lessons.Add(lesson);
                });
                last.ForEach(item =>
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region SORT
        private List<Estoutro> SortPronoun(List<Estoutro> pronouns)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation sort pronoun \"Morphology\" service failed!");

                List<Estoutro> words = new List<Estoutro>();
                words = pronouns;
                int quantity = pronouns.Count();
                //for (int first = 0; first < quantity - 1; first++)
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region WORD
        private List<Word> Word(string term, string type, string? sentence, string? model)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation word \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                Word word = new Word();
                term = this._wordEmbeddingService.RemoveAccent(term.ToLower());
                word.term = term;
                word.kind = type;
                if (sentence != null) word.sentence = sentence;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> WordAdjectiveAdverb(string adjective, string adverb)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation word adjective adverb \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb, this._adverb, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(adjective, this._adjective, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> WordAdverbAdverb(string adverb_main, string adverb)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation word adverb adverb \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb, this._adverb_adverb, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(adverb_main, this._adverb, null, null);
                terms.ForEach(item=>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> WordAdjectiveAdverb(string adjective, string adverb, string adverb_adverb)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation word adjective adverb \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb_adverb, this._adverb_adverb, null, null);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> WordArticleNoun(string noun, string article)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation word article noun \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = new List<Word>();
                terms = Word(article, this._article, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(noun, this._noun, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> WordPronounNoun(string noun, string pronoun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation word pronoun noun \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(pronoun, this._pronoun, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(noun, this._noun, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> WordNumeralNoun(string noun, string digit)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation word numeral noun \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = new List<Word>();
                terms = Word(digit, this._numeral, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms= new List<Word>();
                terms = Word(noun, this._noun, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> WordVerbAdverb(string verb, string adverb)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation word verb adverb \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(verb, this._verb, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                terms = Word(adverb, this._adverb, null, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Word> WordVerbAdverb(string verb, string adverb, string adverb_adverb)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation word verb adverb \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                terms = Word(adverb_adverb, this._adverb_adverb, null, null);
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region ADVERB
        private List<Lesson> VerifyAdverb(List<Lesson> adverbs_adverbs, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify adverb \"Morphology\" service failed!");

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
                        if (item.kind == this._adverb) adverb = item.term;
                        if (item.kind == this._adverb_adverb) adverb_adverb = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountAdverbAdverb(List<Circunstancia> adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount adverb adverb \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region ADJECTIVE ADVERBIAL ADJUNCT
        private List<Lesson> VerifyAdjective(List<Lesson> adjectives_adverbs, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify adjective \"Morphology\" service failed!");

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
                        if (item.kind == this._adjective) first = item.term;
                        if (item.kind == this._adverb) last = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> VerifyAdjective(List<Lesson> adjectives_adverbs, Dictionary<(string, string), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify adjective \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                foreach (Lesson word in adjectives_adverbs)
                {
                    List<Word> words = new List<Word>();
                    string first = string.Empty;
                    string last = string.Empty;
                    foreach (Word item in word.lecture)
                    {
                        if (item.kind == this._adjective) first = item.term;
                        if (item.kind == this._adverb) last = item.term;
                        words.Add(item);
                    };
                    bool similarity = this._wordEmbeddingService.Similarity(word_2_vec, first, last);
                    if (similarity)
                    {
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountAdjectiveAdverb(List<string> adjectives, List<Circunstancia> adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount adjective adverb \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountAdjectiveAdverb(List<string> adjectives, List<Lesson> adverbs_adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount adjective adverb \"Morphology\" service failed!");

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
                            if (item.kind == this._adverb) adverb = item.term;
                            if (item.kind == this._adverb_adverb) adverb_adverb = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountAdjectiveAdverb(Dictionary<(string, string), int> word_2_vec, HashSet<string> vocabulary, List<string> adjectives, List<Circunstancia> adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount adjective adverb \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();

                List<string> adjective_vocabulary = FilterList(adjectives, vocabulary);
                List<Circunstancia> adverb_vocabulary = FilterAdverb(adverbs, vocabulary);

                List<Lesson> adjective = MountAdjective(adjective_vocabulary);
                List<Lesson> adjective_adverb = MountAdjectiveAdverb(adjective_vocabulary, adverb_vocabulary);
                List<Lesson> adverb_adverb = MountAdverbAdverb(adverb_vocabulary);
                List<Lesson> adjective_adverb_adverb = MountAdjectiveAdverb(adjective_vocabulary, adverb_adverb);

                List<Lesson> union_adjective = Union(adjective, adjective_adverb);
                union_adjective = Union(union_adjective, adjective_adverb_adverb);
                List<Lesson> adjective_verify = VerifyAdjective(union_adjective, word_2_vec);

                return adjective_verify;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountAdjective(List<string> adjectives)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount adjective \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                adjectives.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = Word(item, this._adjective, null, null);
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetAdjective(List<Sentenca> sentences, List<string> adjectives)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get adjective \"Morphology\" service failed!");

                List<string> filter_adjective = FilterList(adjectives, sentences);
                List<Lesson> mount_adjective = MountAdjective(filter_adjective);

                List<Lesson> lessons = new List<Lesson>();
                mount_adjective.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.team = this._adjective;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetAdjectiveAdverb(List<Sentenca> sentences, List<string> adjectives, List<Circunstancia> adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get adjective adverb \"Morphology\" service failed!");

                List<string> filter_adjective = FilterList(adjectives, sentences);
                List<Circunstancia> filter_adverb = FilterAdverb(adverbs, sentences);
                List<Lesson> mount_adjective_adverb = MountAdjectiveAdverb(filter_adjective, filter_adverb);
                List<Lesson> verify_adjective_adverb = VerifyAdjective(mount_adjective_adverb, sentences);
                List<Lesson> mount_adverb_adverb = MountAdverbAdverb(filter_adverb);
                List<Lesson> verify_adverb_adverb = VerifyAdverb(mount_adverb_adverb, sentences);
                List<Lesson> mount_adjective_adverb_adverb = MountAdjectiveAdverb(filter_adjective, verify_adverb_adverb);
                List<Lesson> verify_adjective_adverb_adverb = VerifyAdjective(mount_adjective_adverb, sentences);
                List<Lesson> union_adjective_adverb = Union(filter_adjective, verify_adjective_adverb);
                List<Lesson> union_adjective_adverb_adverb = Union(union_adjective_adverb, verify_adjective_adverb_adverb);

                List<Lesson> lessons = new List<Lesson>();
                union_adjective_adverb_adverb.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.team = this._adjective_adverb;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetAdverbialAdjunct(Dictionary<(string, string), int> word_2_vec, HashSet<string> vocabulary, List<string> adjectives, List<Circunstancia> adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get adverbial adjunct \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();

                List<Lesson> adjective_adverb = MountAdjectiveAdverb(word_2_vec, vocabulary, adjectives, adverbs);

                foreach (Lesson item in adjective_adverb)
                {
                    Lesson lesson = new Lesson();
                    lesson.team = this._adverbial_adjective;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                };
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region ARTICLE
        private HashSet<string> MountArticle(List<Preceito> articles)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount article \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetArticle(List<Sentenca> sentences, List<Preceito> articles)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get article \"Morphology\" service failed!");

                List<Preceito> filter_article = FilterArticle(articles, sentences);

                List<Lesson> lessons = new List<Lesson>();
                filter_article.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._article, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._article;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetArticle(HashSet<string> vocabulary, List<Preceito> articles)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get article \"Morphology\" service failed!");

                List<Preceito> filter_article = FilterArticle(articles, vocabulary);

                List<Lesson> lessons = new List<Lesson>();
                filter_article.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._article, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._article;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region NOUN ADNOMINAL ADJUNCT
        private List<Lesson> VerifyNoun(List<Lesson> matters, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify noun \"Morphology\" service failed!");

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
                            if (item.kind == this._article) first = item.term;
                            if ((item.kind != this._noun) && (item.kind != this._article)) middle = item.term;
                            if (item.kind == this._noun) last = item.term;
                        }
                        else
                        {
                            if (item.kind != this._noun) first = item.term;
                            if (item.kind == this._noun) last = item.term;
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
                    {
                        if (first != string.Empty)
                            similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, first, last);
                        else
                            similarity = true;
                    }
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> VerifyNoun(List<Lesson> matters, Dictionary<(string, string), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify noun \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                foreach (Lesson instruction in matters)
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
                            if (item.kind == this._article) first = item.term;
                            if ((item.kind != this._noun) && (item.kind != this._article)) middle = item.term;
                            if (item.kind == this._noun) last = item.term;
                        }
                        else
                        {
                            if (item.kind != this._noun) first = item.term;
                            if (item.kind == this._noun) last = item.term;
                        }
                        words.Add(item);
                    });
                    bool similarity = false;
                    if (three)
                    {
                        similarity = this._wordEmbeddingService.Similarity(word_2_vec, first, middle);
                        if (similarity)
                            similarity = this._wordEmbeddingService.Similarity(word_2_vec, middle, last);
                    }
                    else
                    {
                        if (first != string.Empty)
                            similarity = this._wordEmbeddingService.Similarity(word_2_vec, first, last);
                        else
                            similarity = true;
                    }
                    if (similarity)
                    {
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> VerifyAdjectiveNoun(List<Lesson> matters, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify adjective noun \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                matters.ForEach(instruction =>
                {
                    List<Word> words = new List<Word>();
                    string adjective = string.Empty;
                    string noun = string.Empty;
                    instruction.lecture.ForEach(item =>
                    {
                        if (item.kind == this._adjective) adjective = item.term;
                        if (item.kind == this._noun) noun = item.term;
                        words.Add(item);
                    });
                    bool similarity = false;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, adjective, noun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> VerifyAdjectiveNoun(List<Lesson> matters, Dictionary<(string, string), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify adjective noun \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                matters.ForEach(instruction =>
                {
                    List<Word> words = new List<Word>();
                    string adjective = string.Empty;
                    string noun = string.Empty;
                    instruction.lecture.ForEach(item =>
                    {
                        if (item.kind == this._adjective) adjective = item.term;
                        if (item.kind == this._noun) noun = item.term;
                        words.Add(item);
                    });
                    bool similarity = false;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, adjective, noun);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> VerifyAdjectivePrepositionNoun(List<Lesson> matters, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify adjective preposition noun \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> vocabulary = this._wordEmbeddingService.Vocabulary(sentences);
                Dictionary<(string, string), int> word_2_vec = this._wordEmbeddingService.Word2Vec(sentences);
                matters.ForEach(instruction =>
                {
                    List<Word> words = new List<Word>();
                    string adjective = string.Empty;
                    string preposition = string.Empty;
                    string noun = string.Empty;
                    instruction.lecture.ForEach(item =>
                    {
                        if (item.kind == this._adjective) adjective = item.term;
                        if (item.kind == this._noun) noun = item.term;
                        if (item.kind == this._preposition) preposition = item.term;
                        words.Add(item);
                    });
                    bool similarity = false;
                    similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, noun, preposition);
                    if (similarity)
                        similarity = this._wordEmbeddingService.Similarity(word_2_vec, vocabulary, preposition, adjective);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountAdjectivePrepositionNoun(List<Lesson> nouns, List<Lesson> adjectives, List<Lesson> prepositions)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount adjective preposition noun \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                foreach (Lesson preposition in prepositions)
                {
                    foreach (Lesson adjective in adjectives)
                    {
                        foreach (Lesson noun in nouns)
                        {
                            List<Word> words = new List<Word>();
                            noun.lecture.ForEach(item =>
                            {
                                words.Add(item);
                            });
                            adjective.lecture.ForEach(item =>
                            {
                                words.Add(item);
                            });
                            preposition.lecture.ForEach(item =>  
                            { 
                                words.Add(item); 
                            });
                            Lesson lesson = new Lesson();
                            lesson.lecture = words;
                            lessons.Add(lesson);
                        }
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountAdjectiveNoun(List<Lesson> nouns, List<Lesson> adjectives)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount adjective noun \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                foreach (Lesson adjective in adjectives)
                {
                    foreach (Lesson noun in nouns)
                    {
                        List<Word> words = new List<Word>();
                        noun.lecture.ForEach(item =>
                        {
                            words.Add(item);
                        });
                        adjective.lecture.ForEach(item =>
                        { 
                            words.Add(item);
                        });
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountNounPronoun(List<string> nouns, List<Estoutro> pronouns, List<Preceito> articles)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount noun pronoun \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountNounNumeral(List<string> nouns, List<Algarismo> numerals, List<Preceito> articles)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount noun numeral \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountNounArticle(List<string> nouns, List<Preceito> articles)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount noun article \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountNoun(List<string> nouns, List<Preceito> articles)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount noun \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                HashSet<string> precepts = new HashSet<string>();
                precepts = MountArticle(articles);
                nouns.ForEach(substantive =>
                {
                    List<Word> words = new List<Word>();
                    HashSet<string> word = new HashSet<string>(substantive.Split(' '));
                    if (word.Count > 1)
                    {
                        if (Array.IndexOf(precepts.ToArray(), word.First()) != -1)
                            words = Word(word.Last(), this._noun, null, null);
                    }
                    else words = Word(substantive, this._noun, null, null);
                    if (words.Count > 0)
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetAdjectiveNoun(List<Sentenca> sentences, List<Lesson> adjectives, List<Lesson> nouns, List<Lesson> prepositions)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get adjective noun \"Morphology\" service failed!");

                List<Lesson> mount_adjective_noun = MountAdjectiveNoun(nouns, adjectives);
                List<Lesson> verify_adjective_noun = VerifyAdjectiveNoun(mount_adjective_noun, sentences);
                List<Lesson> mount_adjective_preposition_noun = MountAdjectivePrepositionNoun(nouns, adjectives, prepositions);
                List<Lesson> verify_adjective_prepositon_noun = VerifyAdjectivePrepositionNoun(mount_adjective_preposition_noun, sentences);
                List<Lesson> union_adjective_preposition_noun = Union(verify_adjective_noun, verify_adjective_prepositon_noun);

                List<Lesson> lessons = new List<Lesson>();
                union_adjective_preposition_noun.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.team = this._adjective_noun;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetAdnominalAdjunct(Dictionary<(string, string), int> word_2_vec, HashSet<string> vocabulary, List<string> nouns, List<Estoutro> pronouns, List<Preceito> articles, List<Algarismo> numerals, List<string> adjectives, List<Circunstancia> adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get adnominal adjunct \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();

                List<string> noun_vocabulary = FilterList(nouns, vocabulary);
                List<Preceito> article_vocabulary = FilterArticle(articles, vocabulary);
                List<Algarismo> numeral_vocabulary = FilterNumeral(numerals, vocabulary);

                List<string> type_adjective = new List<string>();
                type_adjective.Add(this._adjective);
                List<Estoutro> pronoun_adjective = MountPronoun(type_adjective, pronouns);

                List<string> type_demostrative = new List<string>();
                type_demostrative.Add(this._demonstrative);
                List<Estoutro> pronoun_demostrative = MountPronoun(type_demostrative, pronouns);

                List<Estoutro> pronoun_adjective_vocabulary = FilterPronoun(pronoun_adjective, vocabulary);
                List<Estoutro> pronoun_demostrative_vocabulary = FilterPronoun(pronoun_demostrative, vocabulary);

                List<Lesson> noun = MountNoun(noun_vocabulary, article_vocabulary);

                List<Lesson> noun_numeral = MountNounNumeral(noun_vocabulary, numeral_vocabulary, article_vocabulary);
                List<Lesson> noun_article = MountNounArticle(noun_vocabulary, article_vocabulary);

                List<Lesson> noun_possessive = MountNounPronoun(noun_vocabulary, pronoun_adjective_vocabulary, article_vocabulary);
                List<Lesson> noun_demonstrative = MountNounPronoun(noun_vocabulary, pronoun_demostrative_vocabulary, article_vocabulary);

                List<Lesson> union_noun = Union(noun, noun_numeral);
                union_noun = Union(union_noun, noun_article);
                union_noun = Union(union_noun, noun_possessive);
                union_noun = Union(union_noun, noun_demonstrative);
                List<Lesson> noun_verify = VerifyNoun(union_noun, word_2_vec);

                List<Lesson> adjective_adverb = MountAdjectiveAdverb(word_2_vec, vocabulary, adjectives, adverbs); 

                List<Lesson> noun_adjective = MountAdjectiveNoun(noun_verify, adjective_adverb);
                List<Lesson> adjective_noun_vefiry = VerifyAdjectiveNoun(noun_adjective, word_2_vec);

                List<Lesson> union_lesson = Union(noun_verify, adjective_noun_vefiry);

                foreach (Lesson item in union_lesson)
                {
                    Lesson lesson = new Lesson();
                    lesson.team = this._adnominal_adjunct;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                };
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetNoun(List<Sentenca> sentences, List<string> nouns, List<Estoutro> pronouns, List<Preceito> articles, List<Algarismo> numerals)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get noun \"Morphology\" service failed!");

                List<string> filter_noun = FilterList(nouns, sentences);
                List<Preceito> filter_article = FilterArticle(articles, sentences);
                List<Algarismo> filter_numeral = FilterNumeral(numerals, sentences);

                List<string> type_adjective = new List<string>();
                type_adjective.Add(this._adjective);
                List<string> type_demostrative = new List<string>();
                type_demostrative.Add(this._demonstrative);
                List<Estoutro> list_pronoun_adjective = MountPronoun(type_adjective, pronouns);
                List<Estoutro> list_pronoun_demostrative = MountPronoun(type_demostrative, pronouns);
                List<Estoutro> filter_pronoun_adjective = FilterPronoun(list_pronoun_adjective, sentences);
                List<Estoutro> filter_pronoun_demostrative = FilterPronoun(list_pronoun_demostrative, sentences);

                List<Lesson> mount_noun = MountNoun(filter_noun, filter_article);
                List<Lesson> noun_possessive = MountNounPronoun(filter_noun, filter_pronoun_adjective, filter_article);
                List<Lesson> noun_demonstrative = MountNounPronoun(filter_noun, filter_pronoun_demostrative, filter_article);
                List<Lesson> noun_numeral = MountNounNumeral(filter_noun, filter_numeral, filter_article);
                List<Lesson> noun_article = MountNounArticle(filter_noun, filter_article);
                
                List<Lesson> union_substantive = Union(mount_noun, noun_article);
                List<Lesson> union_substantive_one = Union(union_substantive, noun_possessive);
                List<Lesson> union_substantive_two = Union(union_substantive_one, noun_numeral);
                List<Lesson> union_substantive_three = Union(union_substantive_two, noun_demonstrative);
                List<Lesson> verify_noun = VerifyNoun(union_substantive_three, sentences);

                List<Lesson> lessons = new List<Lesson>();
                verify_noun.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.team = this._noun;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetNumeralNoun(List<Sentenca> sentences, List<string> nouns, List<Preceito> articles, List<Algarismo> numerals)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get numeral noun \"Morphology\" service failed!");

                List<string> filter_noun = FilterList(nouns, sentences);
                List<Preceito> filter_article = FilterArticle(articles, sentences);
                List<Algarismo> filter_numeral = FilterNumeral(numerals, sentences);

                List<Lesson> noun_numeral = MountNounNumeral(filter_noun, filter_numeral, filter_article);
                List<Lesson> verify_noun = VerifyNoun(noun_numeral, sentences);

                List<Lesson> lessons = new List<Lesson>();
                verify_noun.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.team = this._numeral_noun;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region PRONOUN
        private List<Estoutro> MountPronoun(List<string> types, List<Estoutro> pronouns)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount pronoun \"Morphology\" service failed!");

                List<Estoutro> single = new List<Estoutro>();
                List<Estoutro> plural = new List<Estoutro>();
                List<Estoutro> kinds = FilterTypePronoun(pronouns, types);

                foreach (Estoutro item in kinds) 
                {
                    List<Contento> contents = new List<Contento>();
                    item.contento.ForEach(contento =>
                    {
                        if (contento.numero.Contains(_single))
                            contents.Add(contento);
                    });
                    Estoutro pronoun = new Estoutro();
                    pronoun.nome = item.nome;
                    pronoun.linguagem = item.linguagem;
                    pronoun.tipo = item.tipo;
                    pronoun.contento = contents;
                    if (contents.Count > 0) single.Add(pronoun);
                };
                single = SortPronoun(single);

                foreach (Estoutro item in kinds)
                {
                    List<Contento> contents = new List<Contento>();
                    item.contento.ForEach(contento =>
                    {
                        if (contento.numero.Contains(this._plural))
                            contents.Add(contento);
                    });
                    Estoutro pronoun = new Estoutro();
                    pronoun.nome = item.nome;
                    pronoun.linguagem = item.linguagem;
                    pronoun.tipo = item.tipo;
                    pronoun.contento = contents;
                    if (contents.Count > 0) plural.Add(pronoun);
                };
                plural = SortPronoun(plural);

                List<Estoutro> surrogates = new List<Estoutro>();
                single.ForEach(item => surrogates.Add(item));
                plural.ForEach(item => surrogates.Add(item));

                return surrogates;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetPronoun(List<Sentenca> sentences, List<Estoutro> pronouns)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get pronoun \"Morphology\" service failed!");

                List<string> type_personal = new List<string>();
                type_personal.Add(this._personal);
                List<Estoutro> mount_pronoun_personal = MountPronoun(type_personal, pronouns);
                List<string> type_possessive = new List<string>();
                type_possessive.Add(this._possessive);
                List<Estoutro> mount_pronoun_possessive = MountPronoun(type_possessive, pronouns);
                List<string> type_demostrative = new List<string>();
                type_demostrative.Add(this._demonstrative);
                List<Estoutro> mount_pronoun_demostrative = MountPronoun(type_demostrative, pronouns);
                List<Estoutro> filter_pronoun_personal = FilterPronoun(mount_pronoun_personal, sentences);
                List<Estoutro> filter_pronoun_possessive = FilterPronoun(mount_pronoun_possessive, sentences);
                List<Estoutro> filter_pronoun_demostrative = FilterPronoun(mount_pronoun_demostrative, sentences);

                List<Lesson> lessons = new List<Lesson>();
                filter_pronoun_personal.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._pronoun, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._personal;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                filter_pronoun_possessive.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._pronoun, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._possessive;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                filter_pronoun_demostrative.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._pronoun, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._demonstrative;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetPronoun(HashSet<string> vocabulary, List<Estoutro> pronouns)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get pronoun \"Morphology\" service failed!");

                List<string> type_personal = new List<string>();
                type_personal.Add(this._personal);
                List<Estoutro> mount_pronoun_personal = MountPronoun(type_personal, pronouns);
                List<string> type_possessive = new List<string>();
                type_possessive.Add(this._possessive);
                List<Estoutro> mount_pronoun_possessive = MountPronoun(type_possessive, pronouns);
                List<string> type_demostrative = new List<string>();
                type_demostrative.Add(this._demonstrative);
                List<Estoutro> mount_pronoun_demostrative = MountPronoun(type_demostrative, pronouns);
                List<Estoutro> filter_pronoun_personal = FilterPronoun(mount_pronoun_personal, vocabulary);
                List<Estoutro> filter_pronoun_possessive = FilterPronoun(mount_pronoun_possessive, vocabulary);
                List<Estoutro> filter_pronoun_demostrative = FilterPronoun(mount_pronoun_demostrative, vocabulary);

                List<Lesson> lessons = new List<Lesson>();
                filter_pronoun_personal.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._pronoun, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._personal;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                filter_pronoun_possessive.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._pronoun, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._possessive;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                filter_pronoun_demostrative.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._pronoun, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._demonstrative;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region VERB ADVERBIAL ADJUNCT
        private List<Lesson> VerifyVerbAdverb(List<Lesson> verbs_adverbs, List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verbs adverb \"Morphology\" service failed!");

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
                        if (word.kind == this._verb) first = word.term;
                        if (word.kind == this._adverb) last = word.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> VerifyVerbAdverb(List<Lesson> verbs_adverbs, Dictionary<(string, string), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb adverb \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                foreach (Lesson verb_adverb in verbs_adverbs)
                {
                    List<Word> words = new List<Word>();
                    string first = string.Empty;
                    string last = string.Empty;
                    foreach (Word word in verb_adverb.lecture)
                    {
                        if (word.kind == this._verb) first = word.term;
                        if (word.kind == this._adverb) last = word.term;
                        words.Add(word);
                    }
                    bool similarity = this._wordEmbeddingService.Similarity(word_2_vec, first, last);
                    if (similarity)
                    {
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> VerifyVerbAdverbAdverb(List<Lesson> verbs_adverbs, Dictionary<(string, string), int> word_2_vec)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation verify verb adverb adverb \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();

                foreach (Lesson verb_adverb in verbs_adverbs)
                {
                    List<Word> words = new List<Word>();
                    string verb = string.Empty;
                    string adverb = string.Empty;
                    string adverb_adverb = string.Empty;
                    foreach (Word word in verb_adverb.lecture)
                    {
                        if (word.kind == this._verb) verb = word.term;
                        if (word.kind == this._adverb) adverb = word.term;
                        if (word.kind == this._adverb_adverb) adverb_adverb = word.term;
                        words.Add(word);
                    }
                    bool similarity = this._wordEmbeddingService.Similarity(word_2_vec, verb, adverb);
                    if (similarity) similarity = this._wordEmbeddingService.Similarity(word_2_vec, adverb, adverb);
                    if (similarity)
                    {
                        Lesson lesson = new Lesson();
                        lesson.lecture = words;
                        lessons.Add(lesson);
                    }
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> UnionVerb(List<Elocucao> verbs, List<Lesson> verbs_adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation union verb \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();
                verbs.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.lecture = Word(item.nome, this._verb, null, null);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> UnionVerb(List<Lesson> verbs_adverbs, List<Lesson> verbs_adverbs_adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation union verb \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Elocucao> MountVerb(List<string> models, List<Elocucao> verbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb \"Morphology\" service failed!");

                List<Elocucao> words = new List<Elocucao>();
                //for (int quantity = 0; quantity < verbs.Count() - 1; quantity++)
                for (int quantity = 0; quantity < verbs.Count(); quantity++)
                {
                    models.ForEach(item =>
                    {
                        string word = verbs[quantity].modelo.ToString().ToLower();
                        word = this._wordEmbeddingService.RemoveAccent(word);
                        if (word == item)
                            words.Add(verbs[quantity]);
                    });
                }
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountVerbAdverb(List<Elocucao> verbs, List<Circunstancia> adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb adverb \"Morphology\" service failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<Lesson> MountVerbAdverbAdverb(List<Elocucao> verbs, List<Lesson> adverbs_adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount verb adverb \"Morphology\" service failed!");

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
                            if (item.kind == this._adverb) adverb = item.term;
                            if (item.kind == this._adverb_adverb) adverb_adverb = item.term;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetVerb(List<Sentenca> sentences, List<string> models, List<Elocucao> verbs, List<Circunstancia> adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get verb \"Morphology\" service failed!");

                List<Elocucao> list_verb_model = MountVerb(models, verbs);
                List<Elocucao> filter_verb = FilterVerb(list_verb_model, sentences);
                List<string> kind = new List<string>();
                kind.Add(this._infinitive);
                List<Elocucao> filter_infinitive = FilterReverseVerb(filter_verb, kind);
                List<Circunstancia> filter_adverb = FilterAdverb(adverbs, sentences);
                List<Lesson> mount_verb_adverb = MountVerbAdverb(filter_infinitive, filter_adverb);
                List<Lesson> verify_verb_adverb = VerifyVerbAdverb(mount_verb_adverb, sentences);
                List<Lesson> mount_adverb_adverb = MountAdverbAdverb(filter_adverb);
                List<Lesson> verify_adverb_adverb = VerifyAdverb(mount_adverb_adverb, sentences);
                List<Lesson> mount_verb_adverb_adverb = MountVerbAdverbAdverb(filter_infinitive, verify_adverb_adverb);
                List<Lesson> verify_verb_adverb_adverb = VerifyVerbAdverb(mount_verb_adverb_adverb, sentences);
                List<Lesson> union_verb_adverb = UnionVerb(filter_infinitive, verify_verb_adverb);
                List<Lesson> union_verb_adverb_adverb = UnionVerb(union_verb_adverb, verify_verb_adverb_adverb);

                List<Lesson> lessons = new List<Lesson>();
                union_verb_adverb_adverb.ForEach(item =>
                {
                    Lesson lesson = new Lesson();
                    lesson.team = this._verb;
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetAdverbialAdjunct(Dictionary<(string, string), int> word_2_vec, HashSet<string> vocabulary, List<string> models, List<Elocucao> verbs, List<Circunstancia> adverbs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get adverbial adjunct \"Morphology\" service failed!");

                List<Lesson> lessons = new List<Lesson>();

                List<Circunstancia> adverb_vocabulary = FilterAdverb(adverbs, vocabulary);
                List<Elocucao> list_verb = MountVerb(models, verbs);
                List<Elocucao> verb_vocabulary = FilterVerb(list_verb, vocabulary);

                List<string> kind_infinitive = new List<string>();
                kind_infinitive.Add(this._infinitive);

                List<Elocucao> verb_without_infinitive = FilterReverseVerb(verb_vocabulary, kind_infinitive);
                
                List<Lesson> verb_adverb = MountVerbAdverb(verb_without_infinitive, adverb_vocabulary);
                List<Lesson> verify_verb_adverb = VerifyVerbAdverb(verb_adverb, word_2_vec);

                List<Lesson> adverb_adverb = MountAdverbAdverb(adverb_vocabulary);
                List<Lesson> verb_adverb_adverb = MountVerbAdverbAdverb(verb_without_infinitive, adverb_adverb);
                List<Lesson> verify_verb_adverb_adverb = VerifyVerbAdverbAdverb(verb_adverb_adverb, word_2_vec);

                List<Lesson> union_verb = UnionVerb(verb_without_infinitive, verify_verb_adverb);
                union_verb = UnionVerb(union_verb, verb_adverb_adverb);

                foreach (Lesson item in union_verb)
                {
                    Lesson lesson = new Lesson();
                    lesson.team = this._adverbial_verb; 
                    lesson.lecture = item.lecture;
                    lessons.Add(lesson);
                }
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region NUMERAL
        public List<Lesson> GetNumeral(List<Sentenca> sentences, List<Algarismo> numerals)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get numeral \"Morphology\" service failed!");

                List<Algarismo> filter_numeral = FilterNumeral(numerals, sentences);

                List<Lesson> lessons = new List<Lesson>();
                filter_numeral.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._numeral, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._numeral;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetNumeral(HashSet<string> vocabulary, List<Algarismo> numerals)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get numeral \"Morphology\" service failed!");

                List<Algarismo> filter_numeral = FilterNumeral(numerals, vocabulary);

                List<Lesson> lessons = new List<Lesson>();
                filter_numeral.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._numeral, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._numeral;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region CONJUNCTION
        public List<Lesson> GetConjunction(List<Sentenca> sentences, List<Ligacao> conjunctions)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get conjunction \"Morphology\" service failed!");

                List<Ligacao> filter_conjunction = FilterConjunction(conjunctions, sentences);

                List<Lesson> lessons = new List<Lesson>();
                filter_conjunction.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._conjunction, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._conjunction;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetConjunction(HashSet<string> vocabulary, List<Ligacao> conjunctions)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get conjunction \"Morphology\" service failed!");

                List<Ligacao> filter_conjunction = FilterConjunction(conjunctions, vocabulary);

                List<Lesson> lessons = new List<Lesson>();
                filter_conjunction.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._conjunction, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._conjunction;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region PREPOSITION
        public List<Lesson> GetPreposition(List<Sentenca> sentences, List<Juncao> prepositions)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get preposition \"Morphology\" service failed!");

                List<Juncao> filter_preposition = FilterPreposition(prepositions, sentences);

                List<Lesson> lessons = new List<Lesson>();
                filter_preposition.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._preposition, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._preposition;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Lesson> GetPreposition(HashSet<string> vocabulary, List<Juncao> prepositions)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get preposition \"Morphology\" service failed!");

                List<Juncao> filter_preposition = FilterPreposition(prepositions, vocabulary);

                List<Lesson> lessons = new List<Lesson>();
                filter_preposition.ForEach(item =>
                {
                    List<Word> words = new List<Word>();
                    words = Word(item.nome, this._preposition, null, null);
                    Lesson lesson = new Lesson();
                    lesson.team = this._preposition;
                    lesson.lecture = words;
                    lessons.Add(lesson);
                });
                return lessons;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region LESSON
        public List<string> GetLessonAdjective(Materia lesson, List<Materia> book)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get lesson adjective \"Morphology\" service failed!");

                List<String> adjectives = new List<String>();
                foreach (Materia lecture in book.OrderBy(index => index.ordem).ToList())
                {
                    if (lecture.ordem <= lesson.ordem)
                    {
                        if (lecture.conteudo.adjetivo == null) continue; 
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
                adjectives.Sort();  
                return adjectives;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<string> GetLessonNoun(Materia lesson, List<Materia> book)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get lesson noun \"Morphology\" service failed!");

                List<String> nouns = new List<String>();
                foreach (Materia lecture in book.OrderBy(index => index.ordem).ToList())
                {
                    if (lecture.ordem <= lesson.ordem)
                    {
                        if (lecture.conteudo.substantivo == null) continue;
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
                nouns.Sort();
                return nouns;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<string> GetLessonVerb(Materia lesson)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get lesson verb \"Morphology\" service failed!");

                List<String> verbs = new List<String>();
                if (lesson.conteudo.verbo == null) return verbs;
                lesson.conteudo.verbo.ForEach(verb =>
                {
                    if (verb.Trim().Length > 0)
                    {
                        string word = this._wordEmbeddingService.RemoveAccent(verb.ToString());
                        verbs.Add(word);
                    }
                });
                verbs.Distinct();
                verbs.Sort();
                return verbs;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region WORDS
        public List<Word> GetSuject(string pronoun, string noun, string article, string numeral, string verb, string model)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get subject \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                if (pronoun != null) terms = Word(pronoun, this._pronoun, this._subject, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                if (noun != null) terms = Word(noun, this._noun, this._subject, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                if (numeral != null) terms = Word(numeral, this._numeral, this._subject, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                if (article != null) terms = Word(article, this._article, this._subject, null);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                terms = new List<Word>();
                if (verb != null) terms = Word(verb, this._verb, this._predicate, model);
                terms.ForEach(item =>
                {
                    words.Add(item);
                });
                return words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Word> GetPredicate(string pronoun, string noun, string article, string numeral, string preposition)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get predicate \"Morphology\" service failed!");

                List<Word> words = new List<Word>();
                List<Word> terms = new List<Word>();
                if (pronoun != null) terms = Word(pronoun, this._pronoun, this._predicate, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                terms = new List<Word>();
                if (article != null) terms = Word(article, this._article, this._predicate, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                terms = new List<Word>();
                if (numeral != null) terms = Word(numeral, this._numeral, this._predicate, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                terms = new List<Word>();
                if (noun != null) terms = Word(noun, this._noun, this._predicate, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                terms = new List<Word>();
                if (preposition != null) terms = Word(preposition, this._preposition, this._predicate, null);
                terms.ForEach(index =>
                {
                    words.Add(index);
                });
                return words;
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
