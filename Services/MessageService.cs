using LetterStomach.Models;

namespace LetterStomach.Services
{
    public class MessageService
    {
        private static MessageService _instance;
        private static readonly object _lock = new object();

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

        public List<Message> Messages { get; set; } = new List<Message>();

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
            catch (Exception)
            {
                throw;
            }
        }

        public List<Message> GetChats()
        {
            try 
            {
                return new List<Message>
                {
                    new Message
                    {
                        Sender = user6,
                        Time = "18:32",
                        Text = "Hey there! What\'s up? Is everything ok?",
                    },
                    new Message
                    {
                        Sender = user1,
                        Time = "14:05",
                        Text = "Can I call you back later?, I\'m in a meeting.",
                    },
                    new Message
                    {
                        Sender = user3,
                        Time = "14:00",
                        Text = "Yeah. Do you have any good song to recommend?",
                    },
                    new Message
                    {
                        Sender = user2,
                        Time = "13:35",
                        Text = "Hi! I went shopping today and found a nice t-shirt.",
                    },
                    new Message
                    {
                        Sender = user4,
                        Time = "12:11",
                        Text = "I passed you on the ride to work today, see you later.",
                    },
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Message> GetChatsSQLite()
        {
            try 
            { 
                return new List<Message>
                {
                    new Message
                    {
                        Sender = user6,
                        Time = "18:32",
                        Text = "Hey there! What\'s up?",
                    },
                    new Message
                    {
                        Sender = user1,
                        Time = "14:05",
                        Text = "Can I call you back later?, I\'m in a meeting.",
                    },
                    new Message
                    {
                        Sender = user3,
                        Time = "14:00",
                        Text = "Yeah. Do you have any good song to recommend?",
                    },
                    new Message
                    {
                        Sender = user2,
                        Time = "13:35",
                        Text = "Hi! I went laundry today and found t-shirt.",
                    },
                    new Message
                    {
                        Sender = user4,
                        Time = "12:11",
                        Text = "I passed you on the ride to work shopping, see you later.",
                    },
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Message> GetMessages(User sender)
        {
            try 
            { 
                return new List<Message> {
                    new Message
                    {
                        Sender = null,
                        Time = "18:42",
                        Text = "Yeah I know. I\'m in the same position 😂",
                    },
                    new Message
                    {
                        Sender = sender,
                        Time = "18:39",
                        Text = "It\'s hard to be productive, man 😞",
                    },
                    new Message
                    {
                        Sender = sender,
                        Time = "18:39",
                        Text = "Same here! Been watching YouTube for the past 5 hours despite of having so much to do! 😅",
                    },
                    new Message
                    {
                        Sender = null,
                        Time = "18:36",
                        Text = "Nothing. Just chilling and watching YouTube. What about you?",
                    },
                    new Message
                    {
                        Sender= sender,
                        Time = "18:35",
                        Text= "Hey there! What\'s up?",
                    },
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
