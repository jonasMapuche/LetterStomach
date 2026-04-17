using LetterStomach.Models;

namespace LetterStomach.Services
{
    public class MessageService
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

        public event EventHandler<string> OnError;
        #endregion

        #region VARIABLE
        private static MessageService _instance;
        private static readonly object _lock = new object();
        private List<Message> _messages_english = new List<Message>();
        private List<Message> _messages_deutsch = new List<Message>();
        private List<Message> _messages_italiano = new List<Message>();
        private List<Message> _messages_francais = new List<Message>();
        private List<Message> _messages_espanol = new List<Message>();
        private List<Message> _messages_portugues = new List<Message>();

        private Language _language_english = SettingService.Instance.English;
        private Language _language_deutsch = SettingService.Instance.Deutsch;
        private Language _language_italiano = SettingService.Instance.Italino;
        private Language _language_francais = SettingService.Instance.Francais;
        private Language _language_espanol = SettingService.Instance.Espanol;
        private Language _language_portugues = SettingService.Instance.Portugues;

        private List<Message> _bots_english = new List<Message>();
        private List<Message> _bots_deutsch = new List<Message>();
        private List<Message> _bots_italiano = new List<Message>();
        private List<Message> _bots_francais = new List<Message>();
        private List<Message> _bots_espanol = new List<Message>();
        private List<Message> _bots_portugues = new List<Message>();

        public readonly string image_speak_on = "speaker_notes_62dp_white.png";
        public readonly string image_speak_off = "speaker_notes_off_62dp_white.png";
        public readonly string image_move_on = "play_arrow_62dp_white.png";
        public readonly string image_move_off = "play_disabled_62dp_white.png";
        #endregion

        #region CONSTRUCTOR
        public static MessageService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MessageService();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        #region USER
        readonly User user1 = new()
        {
            Name = "Deutsch",
            Image = "emoji1.png",
            Color = Color.FromArgb("#FFE0EC")
        };

        readonly User user2 = new()
        {
            Name = "Français",
            Image = "emoji2.png",
            Color = Color.FromArgb("#BFE9F2")
        };

        readonly User user3 = new()
        {
            Name = "Italiano",
            Image = "emoji3.png",
            Color = Color.FromArgb("#FFD6C4")
        };

        readonly User user4 = new()
        {
            Name = "Español",
            Image = "emoji4.png",
            Color = Color.FromArgb("#C3C1E6")
        };

        readonly User user5 = new()
        {
            Name = "Português",
            Image = "contacts_product_62dp_letter.png", //"emoji5.png",
            Color = Color.FromArgb("#FFE0EC")
        };

        readonly User user6 = new()
        {
            Name = "English",
            Image = "emoji6.png",
            Color = Color.FromArgb("#FFE5A6")
        };

        readonly User user7 = new()
        {
            Name = "Руссо",
            Image = "emoji7.png",
            Color = Color.FromArgb("#FFE0EC")
        };

        readonly User user8 = new()
        {
            Name = "Tupi",
            Image = "emoji8.png",
            Color = Color.FromArgb("#FFE0EC")
        };

        readonly User user9 = new()
        {
            Name = "עִברִית",
            Image = "emoji9.png",
            Color = Color.FromArgb("#C3C1E6")
        };

        readonly User user10 = new()
        {
            Name = "عربي",
            Image = "emoji10.png",
            Color = Color.FromArgb("#FF95A2")
        };

        public User GetUser(string user)
        {
            try 
            { 
                switch (user)
                {
                    case "deutsch":
                        return user1;
                    case "français":
                        return user2;
                    case "italiano":
                        return user3;
                    case "español":
                        return user4;
                    case "português":
                        return user5;
                    case "english":
                        return user6;
                    case "pуссо":
                        return user7;
                    case "tupi":
                        return user8;
                    case "עִברִית":
                        return user9;
                    default:
                        return user10;
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public string GetLanguage(User? sender)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation languages \"Message\" service failed!");

                string language = sender.Name;

                if ((language == this._language_english.Uppercase) || (language == this._language_english.Lowercase))
                {
                    return this._language_english.Lowercase;
                }
                if ((language == this._language_deutsch.Uppercase) || (language == this._language_deutsch.Lowercase))
                {
                    return this._language_deutsch.Lowercase;
                }
                if ((language == this._language_italiano.Uppercase) || (language == this._language_italiano.Lowercase))
                {
                    return this._language_italiano.Lowercase;
                }
                if ((language == this._language_francais.Uppercase) || (language == this._language_francais.Lowercase))
                {
                    return this._language_francais.Lowercase;
                }
                if ((language == this._language_espanol.Uppercase) || (language == this._language_espanol.Lowercase))
                {
                    return this._language_espanol.Lowercase;
                }
                if ((language == this._language_portugues.Uppercase) || (language == this._language_portugues.Lowercase))
                {
                    return this._language_espanol.Lowercase;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region MESSAGES
        public List<Message> Chats { get; set; } = new List<Message>();

        public List<Message> Messages(User? sender, string text, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation messages \"Message\" service failed!");

                Message message = new Message();
                message.Sender = sender;
                message.Text = text;
                if ((language == this._language_english.Uppercase) || (language == this._language_english.Lowercase))
                {
                    this._messages_english.Add(message);
                    return this._messages_english;
                }
                if ((language == this._language_deutsch.Uppercase) || (language == this._language_deutsch.Lowercase))
                {
                    this._messages_deutsch.Add(message);
                    return this._messages_deutsch;
                }
                if ((language == this._language_italiano.Uppercase) || (language == this._language_italiano.Lowercase))
                {
                    this._messages_italiano.Add(message);
                    return this._messages_italiano;
                }
                if ((language == this._language_francais.Uppercase) || (language == this._language_francais.Lowercase))
                {
                    this._messages_francais.Add(message);
                    return this._messages_francais;
                }
                if ((language == this._language_espanol.Uppercase) || (language == this._language_espanol.Lowercase))
                {
                    this._messages_espanol.Add(message);
                    return this._messages_espanol;
                }
                this._messages_portugues.Add(message);
                return this._messages_portugues;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public void Clear(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation clear \"Message\" service failed!");

                if ((language == this._language_english.Uppercase) || (language == this._language_english.Lowercase))
                    this._messages_english.Clear();
                if ((language == this._language_deutsch.Uppercase) || (language == this._language_deutsch.Lowercase))
                    this._messages_deutsch.Clear();
                if ((language == this._language_italiano.Uppercase) || (language == this._language_italiano.Lowercase))
                    this._messages_italiano.Clear();
                if ((language == this._language_francais.Uppercase) || (language == this._language_francais.Lowercase))
                    this._messages_francais.Clear();
                if ((language == this._language_espanol.Uppercase) || (language== this._language_espanol.Lowercase))
                    this._messages_espanol.Clear();
                if ((language == this._language_portugues.Uppercase) || (language == this._language_portugues.Lowercase))
                    this._messages_portugues.Clear();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Message> Messages(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation messages \"Message\" service failed!");

                if ((language == this._language_english.Uppercase) || (language == this._language_english.Lowercase))
                    return this._messages_english;
                if ((language == this._language_deutsch.Uppercase) || (language == this._language_deutsch.Lowercase))
                    return this._messages_deutsch;
                if ((language == this._language_italiano.Uppercase) || (language == this._language_italiano.Lowercase))
                    return _messages_italiano;
                if ((language == this._language_francais.Uppercase) || (language == this._language_francais.Lowercase))
                    return this._messages_francais;
                if ((language == this._language_espanol.Uppercase) || (language== this._language_espanol.Lowercase))
                    return this._messages_espanol;
                return this._messages_portugues;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region BOTS
        public List<Message> Bots(User? sender, string text, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation bots \"Message\" service failed!");

                Message message = new Message();
                message.Sender = sender;
                message.Text = text;
                if ((language == this._language_english.Uppercase) || (language == this._language_english.Lowercase))
                {
                    this._bots_english.Add(message);
                    return this._bots_english;
                }
                if ((language == this._language_deutsch.Uppercase) || (language == this._language_deutsch.Lowercase))
                {
                    this._bots_deutsch.Add(message);
                    return this._bots_deutsch;
                }
                if ((language == this._language_italiano.Uppercase) || (language == this._language_italiano.Lowercase))
                {
                    this._bots_italiano.Add(message);
                    return this._bots_italiano;
                }
                if ((language == this._language_francais.Uppercase) || (language == this._language_francais.Lowercase))
                {
                    this._bots_francais.Add(message);
                    return this._bots_francais;
                }
                if ((language == this._language_espanol.Uppercase) || (language == this._language_espanol.Lowercase))
                {
                    this._bots_espanol.Add(message);
                    return this._bots_espanol;
                }
                this._bots_portugues.Add(message);
                return this._bots_portugues;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public void Remove(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation remove \"Message\" service failed!");

                if ((language == this._language_english.Uppercase) || (language == this._language_english.Lowercase))
                    this._bots_english.Clear();
                if ((language == this._language_deutsch.Uppercase) || (language == this._language_deutsch.Lowercase))
                    this._bots_deutsch.Clear();
                if ((language == this._language_italiano.Uppercase) || (language == this._language_italiano.Lowercase))
                    this._bots_italiano.Clear();
                if ((language == this._language_francais.Uppercase) || (language == this._language_francais.Lowercase))
                    this._bots_francais.Clear();
                if ((language == this._language_espanol.Uppercase) || (language == this._language_espanol.Lowercase))
                    this._bots_espanol.Clear();
                if ((language == this._language_portugues.Uppercase) || (language == this._language_portugues.Lowercase))
                    this._bots_portugues.Clear();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Message> Bots(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation bots \"Message\" service failed!");

                if ((language == this._language_english.Uppercase) || (language == this._language_english.Lowercase))
                    return this._bots_english;
                if ((language == this._language_deutsch.Uppercase) || (language== this._language_deutsch.Lowercase))
                    return this._bots_deutsch;
                if ((language == this._language_italiano.Uppercase) || (language == this._language_italiano.Lowercase))
                    return this._bots_italiano;
                if ((language == this._language_francais.Uppercase) || (language == this._language_francais.Lowercase))
                    return this._bots_francais;
                if ((language == this._language_espanol.Uppercase) || (language == this._language_espanol.Lowercase))
                    return this._bots_espanol;
                return this._bots_portugues;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region CHATS
        public List<Message> GetChats()
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation get chats \"Message\" service failed!");

                return new List<Message>
                {
                    new Message
                    {
                        Sender = user6,
                        Time = "18:32",
                        Text = "Hey there! What\'s up? Is everything ok?",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user1,
                        Time = "14:05",
                        Text = "Can I call you back later?, I\'m in a meeting.",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user3,
                        Time = "14:00",
                        Text = "Yeah. Do you have any good song to recommend?",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user2,
                        Time = "13:35",
                        Text = "Hi! I went shopping today and found a nice t-shirt.",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user4,
                        Time = "12:11",
                        Text = "I passed you on the ride to work today, see you later.",
                        Speak = image_speak_off,
                        Move = image_move_on
                    }
                };
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Message> GetChatsClear()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get chats \"Message\" service failed!");

                return new List<Message>
                {
                    new Message
                    {
                        Sender = user6,
                        Time = "18:32",
                        Text = "...",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user1,
                        Time = "14:05",
                        Text = "...",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user3,
                        Time = "14:00",
                        Text = "...",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user2,
                        Time = "13:35",
                        Text = "...",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user4,
                        Time = "12:11",
                        Text = "...",
                        Speak = image_speak_off,
                        Move = image_move_on
                    }
                };
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public List<Message> GetChatsSQLite()
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation get chats sqlite \"Message\" service failed!");

                return new List<Message>
                {
                    new Message
                    {
                        Sender = user6,
                        Time = "18:32",
                        Text = "Hey there! What\'s up?",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user1,
                        Time = "14:05",
                        Text = "Can I call you back later?, I\'m in a meeting.",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user3,
                        Time = "14:00",
                        Text = "Yeah. Do you have any good song to recommend?",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user2,
                        Time = "13:35",
                        Text = "Hi! I went laundry today and found t-shirt.",
                        Speak = image_speak_off,
                        Move = image_move_on
                    },
                    new Message
                    {
                        Sender = user4,
                        Time = "12:11",
                        Text = "I passed you on the ride to work shopping, see you later.",
                        Speak = image_speak_off,
                        Move = image_move_on
                    }
                };
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
