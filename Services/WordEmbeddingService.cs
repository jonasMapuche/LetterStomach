using CommunityToolkit.Mvvm.ComponentModel;
using LetterStomach.Models;
using LetterStomach.Services.Interfaces;
using System.Globalization;
using System.Text;

namespace LetterStomach.Services
{
    public class WordEmbeddingService : ObservableObject, IWordEmbeddingService
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

        public bool Similarity(Dictionary<(string, string), int> word_2_vec, HashSet<string> vocabulary, string target, string target1)
        {
            try
            {
                //---
                if ((Array.IndexOf(vocabulary.ToArray(), target) == -1) || (Array.IndexOf(vocabulary.ToArray(), target1) == -1))
                {
                    return false;
                }
                //---
                bool similarity = false;
                foreach (KeyValuePair<(string, string), int> value in word_2_vec)
                {
                    if ((value.Key.Item1 == target) && (value.Key.Item2 == target1))
                    {
                        similarity = true;
                        break;
                    }
                }
                //---
                return similarity;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return false;
            }
        }

        public string Saw(List<Sentenca> sentences)
        {
            try
            {
                //---
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
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public HashSet<string> Vocabulary(List<Sentenca> sentences)
        {
            try
            {
                //---
                string ditado = Saw(sentences);
                //---
                HashSet<string> vocabulary = new HashSet<string>(ditado.Split(' '));
                //---
                return vocabulary;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public string[] RemoveScore(string[] words)
        {
            try
            {
                //---
                string[] new_words = new string[words.Length];
                //---
                int quantity = 0;
                //---
                for (int i = 0; i < words.Length - 1; i++)
                {
                    //---
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
                //---
                int vector = new_words.Length - quantity;
                string[] end_words = new string[vector];
                //---
                int count = 0;
                //---
                for (int i = 0; i < new_words.Length - 1; i++)
                {
                    //---
                    if (new_words[i] == null) continue;
                    end_words[count] = new_words[i];
                    count++;
                }
                //---
                return end_words;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public Dictionary<(string, string), int> Word2Vec(List<Sentenca> sentences)
        {
            try 
            { 
                //---
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
                //---
                return word_pairs;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public string RemoveAccent(string input)
        {
            try
            { 
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
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
    }
}
