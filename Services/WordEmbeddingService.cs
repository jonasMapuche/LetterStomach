using CommunityToolkit.Mvvm.ComponentModel;
using LetterStomach.Models;
using LetterStomach.Services.Interfaces;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace LetterStomach.Services
{
    public class WordEmbeddingService : ObservableObject, IWordEmbeddingService
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
        private HashSet<string> _morphology;
        private HashSet<string> _syntax;
        private HashSet<int> _order;

        private SettingService _settingService;
        #endregion

        #region CONSTRUCTOR
        public WordEmbeddingService()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Word Embedding\" service failed!");

                this._settingService = SettingService.Instance;
                this._morphology = this._settingService.Morphology;
                this._syntax = this._settingService.Syntax;
                this._order = this._settingService.Order;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region REMOVE
        private string Saw(List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation saw \"Word Embedding\" service failed!");

                string ditado = "";
                sentences.ForEach(index =>
                {
                    ditado = ditado + index.impulso;
                });
                ditado = ditado.ToLower();
                ditado = ditado.Replace(".", " . ");
                ditado = ditado.Replace("!", " ! ");
                ditado = ditado.Replace("?", " ? ");
                ditado = ditado.Replace("¿", " ¿ ");
                ditado = ditado.Replace("'", " ' ");
                ditado = RemoveAccent(ditado);
                return ditado;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private string[] RemoveScore(string[] words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation remove score \"Word Embedding\" service failed!");

                string[] new_words = new string[words.Length];
                int quantity = 0;
                for (int i = 0; i < words.Length - 1; i++)
                {
                    switch (words[i])
                    {
                        case ".":
                            new_words[i] = "<br>";
                            break;
                        case "!":
                            new_words[i] = "<br>";
                            break;
                        case "?":
                            new_words[i] = "<br>";
                            break;
                        case "¿":
                            new_words[i] = "<br>";
                            break;
                        case "'":
                            quantity++;
                            continue;
                        default:
                            new_words[i] = words[i];
                            break;
                    }
                }
                int vector = new_words.Length - quantity;
                string[] end_words = new string[vector];
                int count = 0;
                for (int i = 0; i < new_words.Length - 1; i++)
                {
                    if (new_words[i] == null) continue;
                    end_words[count] = new_words[i];
                    count++;
                }
                return end_words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public string RemoveAccent(string input)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation remove accent \"Word Embedding\" service failed!");

                string normalized_string = input.Normalize(NormalizationForm.FormD);
                StringBuilder builder = new StringBuilder();
                foreach (char i in normalized_string)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(i) != UnicodeCategory.NonSpacingMark)
                    {
                        builder.Append(i);
                    }
                }
                return builder.ToString();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region SIMILARITY
        public bool Similarity(Dictionary<(string, string), int> word_2_vec, HashSet<string> vocabulary, string? target, string? target1)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation similarity \"Word Embedding\" service failed!");

                if ((Array.IndexOf(vocabulary.ToArray(), target) == -1) || (Array.IndexOf(vocabulary.ToArray(), target1) == -1))
                {
                    return false;
                }
                bool similarity = false;
                foreach (KeyValuePair<(string, string), int> value in word_2_vec)
                {
                    if ((value.Key.Item1 == target) && (value.Key.Item2 == target1))
                    {
                        similarity = true;
                        break;
                    }
                }
                return similarity;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public bool Similarity(Dictionary<(byte[], byte[]), int> word_2_vec, byte[]? target, byte[]? target2)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation similarity \"Word Embedding\" service failed!");

                foreach (KeyValuePair<(byte[], byte[]), int> value in word_2_vec)
                {
                    if ((value.Key.Item1.AsSpan().SequenceEqual(target)) 
                        && (value.Key.Item2.AsSpan().SequenceEqual(target2))) return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region VOCABULARY
        public HashSet<string> Vocabulary(List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation vocabulary \"Word Embedding\" service failed!");

                string ditado = Saw(sentences);
                HashSet<string> vocabulary = new HashSet<string>(ditado.Split(' '));
                return vocabulary;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public HashSet<byte[]> VocabularySHA256(List<Sentenca> sentences)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation vocabulary sha256 \"Word Embedding\" service failed!");

                string ditado = Saw(sentences);
                HashSet<string> vocabulary = new HashSet<string>();
                vocabulary = Vocabulary(sentences);
                HashSet<byte[]> sha_256 = new HashSet<byte[]>();
                for (int quantity = 0; quantity < vocabulary.Count; quantity++)
                {
                    byte[] value = HashSHA256(quantity);
                    sha_256.Add(value);
                }
                return sha_256;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<byte[]> VocabularySHA256(HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation vocabulary sha256 \"Word Embedding\" service failed!");

                List<byte[]> sha_256 = new List<byte[]>();
                for (int quantity = 0; quantity < vocabulary.Count; quantity++)
                {
                    byte[] value = HashSHA256(quantity);
                    sha_256.Add(value);
                }
                return sha_256;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private List<byte[]> VocabularySHA256(HashSet<int> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation vocabulary sha256 \"Word Embedding\" service failed!");

                List<byte[]> sha_256 = new List<byte[]>();
                for (int quantity = 0; quantity < vocabulary.Count; quantity++)
                {
                    byte[] value = HashSHA256(quantity);
                    sha_256.Add(value);
                }
                return sha_256;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region WORD2VEC
        public Dictionary<(string, string), int> Word2Vec(List<Sentenca> sentences)
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation word 2 vec \"Word Embedding\" service failed!");

                Dictionary<(string, string), int> word_pairs = new Dictionary<(string, string), int>();
                string[] words = Saw(sentences).Split(' ');
                string[] new_words = RemoveScore(words);
                for (int i = 0; i < new_words.Length - 1; i++)
                {
                    var pair = (new_words[i], new_words[i + 1]);
                    if ((pair.Item1 == "<br>") || (pair.Item2 == "<br>")) continue;
                    if (word_pairs.TryGetValue(pair, out int value))
                    {
                        word_pairs[pair] = ++value;
                    }
                    else
                    {
                        word_pairs[pair] = 1;
                    }
                }
                return word_pairs;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public Dictionary<(byte[], byte[]), int> Word2VecSHA256(List<Sentenca> sentences, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation word 2 vec sha256 \"Word Embedding\" service failed!");

                Dictionary<(string, string), int> dictionary = new Dictionary<(string, string), int>();
                dictionary = Word2Vec(sentences);
                Dictionary<(byte[], byte[]), int> sha256 = new Dictionary<(byte[], byte[]), int>();
                foreach (KeyValuePair<(string, string), int> item in dictionary)
                {
                    int key1 = Array.IndexOf(vocabulary.ToArray(), item.Key.Item1);
                    int key2 = Array.IndexOf(vocabulary.ToArray(), item.Key.Item2);
                    (byte[], byte[]) pair = (HashSHA256(key1), HashSHA256(key2));
                    sha256[pair] = item.Value;
                }
                return sha256;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region SHR256
        public byte[] HashSHA256(string texto)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation hash sha256 \"Word Embedding\" service failed!");

                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] bytes_hash = SHA256.HashData(bytes);
                /*
                //string hash_string = Convert.ToHexString(bytes_hash);
                StringBuilder builder = new StringBuilder();
                for (int index = 0; index < bytes_hash.Length; index++)
                {
                    builder.Append(bytes_hash[index].ToString("x2"));
                }
                return builder.ToString();
                */
                return bytes_hash;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public byte[] HashSHA256(int value)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation hash sha256 \"Word Embedding\" service failed!");

                byte[] bytes = BitConverter.GetBytes(value);
                byte[] bytes_hash = SHA256.HashData(bytes);
                /*
                //string hash_string = Convert.ToHexString(bytes_hash);
                StringBuilder builder = new StringBuilder();
                for (int index = 0; index < bytes_hash.Length; index++)
                {
                    builder.Append(bytes_hash[index].ToString("x2"));
                }
                return builder.ToString();
                */
                return bytes_hash;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region ENCODE SHA256
        private HashSet<byte[]> EncodeSeries(HashSet<string> morphologies)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation encode series \"Word Embedding\" service failed!");

                HashSet<byte[]> series = new HashSet<byte[]>();
                for (int quantity = 0; quantity < morphologies.Count; quantity++)
                { 
                    byte[] sha_256 = HashSHA256(quantity);
                    series.Add(sha_256);
                }
                return series;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Tutorial> EncodeLesson(List<Lesson> lessons, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation encode lesson \"Word Embedding\" service failed!");

                List<Tutorial> tutorials = new List<Tutorial>();
                foreach (Lesson lesson in lessons)
                {
                    Tutorial tutorial = new Tutorial();
                    tutorial.team = Encode(lesson.team, this._morphology);
                    List<Instruction> instructions = new List<Instruction>();
                    foreach (Word word in lesson.lecture)
                    {
                        Instruction instruction = new Instruction();
                        int term = Array.IndexOf(vocabulary.ToArray(), word.term);
                        instruction.term = HashSHA256(term);
                        instruction.kind = Encode(word.kind, this._morphology);
                        instructions.Add(instruction);
                    };
                    tutorial.lecture = instructions;
                    tutorials.Add(tutorial);
                };
                return tutorials;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public byte[] Encode(string kind, HashSet<string> briefs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation encode morphology \"Word Embedding\" service failed!");

                HashSet<string> vocalularies = new HashSet<string>(briefs);
                int term = Array.IndexOf(vocalularies.ToArray(), kind);
                byte[] sha_256 = HashSHA256(term);
                return sha_256;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public byte[] Encode(int kind, HashSet<int> briefs)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation encode morphology \"Word Embedding\" service failed!");

                HashSet<int> vocalularies = new HashSet<int>(briefs);
                int term = Array.IndexOf(vocalularies.ToArray(), kind);
                byte[] sha_256 = HashSHA256(term);
                return sha_256;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region DECODE SHA256
        public List<Lesson> DecodeLesson(List<Tutorial> tutorials, HashSet<string> vocabulary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation decode lesson \"Word Embedding\" service failed!");
                
                List<Lesson> lessons = new List<Lesson>();

                List<byte[]> glossaries = VocabularySHA256(vocabulary);
                List<byte[]> morphologies = VocabularySHA256(this._morphology);
                List<byte[]> syntaxes = VocabularySHA256(this._syntax);
                List<byte[]> orders = VocabularySHA256(this._order);

                foreach (Tutorial tutorial in tutorials)
                {
                    Lesson lesson = new Lesson();
                    List<Word> words = new List<Word>();
                    foreach (Instruction instruction in tutorial.lecture)
                    {
                        byte[] term = instruction.term;
                        int index_term = glossaries.FindIndex(index => index.SequenceEqual(term));

                        byte[] kind = instruction.kind;
                        int index_kind = morphologies.FindIndex(index => index.SequenceEqual(kind));

                        byte[] sentence = instruction.sentence;
                        int index_sentence = syntaxes.FindIndex(index => index.SequenceEqual(sentence));

                        byte[] team = instruction.team;
                        int index_team = morphologies.FindIndex(index => index.SequenceEqual(team));

                        byte[] order = instruction.order;
                        int index_order = orders.FindIndex(index => index.SequenceEqual(order));

                        Word word = new Word();
                        word.term = vocabulary.ElementAt(index_term);
                        word.kind = this._morphology.ElementAt(index_kind);
                        word.sentence = this._syntax.ElementAt(index_sentence);
                        word.team = this._morphology.ElementAt(index_team);
                        word.order = this._order.ElementAt(index_order);
                        words.Add(word);
                    }
                    lesson.lecture = words;
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
    }
}
