using CommunityToolkit.Mvvm.Input;
using LetterStomach.Enums;
using LetterStomach.Models;
using LetterStomach.Services;
using System.Windows.Input;

namespace LetterStomach.ViewModels
{

    public class SettingViewModel
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

        public List<Hunks> Items { get; set; }

        public bool IsUpdateTable { get; set; }

        public bool IsSQLiteTable { get; set; }

        public int IsPitchSpeak { get; set; }

        public int IsVolumeSpeak {  get; set; }

        public ICommand CheckCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private readonly SettingService _setting;

        private int _pitch_init = 0;
        private int _volume_init = 0;

        public SettingViewModel(SettingService setting)
        {
            try
            { 
                _setting = setting;

                bool sqlite_database = _setting.SQLiteDatabase;
                bool update_database = _setting.UpdateDatabase;
                int pitch_speak = _setting.PitchSpeak;
                int volume_speak = _setting.VolumeSpeak;
                int pitch_init = _setting.PitchSpeak;
                int volume_init = _setting.VolumeSpeak;

                Items = new List<Hunks>
                {
                    new Hunks { Name = "All", Value = (int)Hunk.All },
                    new Hunks { Name = "Adverb", Value = (int)Hunk.Adverb },
                    new Hunks { Name = "Pronoun", Value = (int)Hunk.Pronoun },
                    new Hunks { Name = "Article", Value = (int)Hunk.Article },
                    new Hunks { Name = "Numeral", Value = (int)Hunk.Numeral },
                    new Hunks { Name = "Conjunction", Value = (int)Hunk.Conjunction },
                    new Hunks { Name = "Preposition", Value = (int)Hunk.Preposition },
                    new Hunks { Name = "Noun", Value = (int)Hunk.Noun },
                    new Hunks { Name = "Verb", Value = (int)Hunk.Verb },
                    new Hunks { Name = "Adjective", Value = (int)Hunk.Adjective },
                    new Hunks { Name = "Sentence", Value = (int)Hunk.Sentence },
                    new Hunks { Name = "Auxiliary", Value = (int)Hunk.Auxiliary }
                };

                CheckCommand = new AsyncRelayCommand<object>(OnCheckCommand);
                BackCommand = new AsyncRelayCommand(OnBackCommand);

                this.IsUpdateTable = update_database;
                this.IsSQLiteTable = sqlite_database;
                this.IsPitchSpeak = pitch_speak;
                this.IsVolumeSpeak = volume_speak;
                this._pitch_init = pitch_init;
                this._volume_init = volume_init;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private async Task OnBackCommand()
        {
            try
            { 
                bool sqlite_database = _setting.SQLiteDatabase;
                await Shell.Current.GoToAsync($"..?refresh={sqlite_database}");
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private async Task OnCheckCommand(object parameter)
        {
            try
            { 
                bool answer = false;
                Setting setting = (Setting)parameter;
                bool update_database = setting.UpdateDatabase == "True" ? true : false;
                bool sqlite_database = setting.SQLiteDatabase == "True" ? true : false;
                int select_table = int.Parse(setting.SelectTable);
                int pitch_speak = int.Parse(setting.PitchSpeak);
                int volume_speak = int.Parse(setting.VolumeSpeak);
                bool init_database = _setting.SQLiteDatabase;
                bool pitch_modify = this._pitch_init != pitch_speak? true: false;
                bool volume_modify = this._volume_init != volume_speak? true: false;    

                string message = "";

                if (!init_database)
                {
                    if ((sqlite_database) && (update_database) && (pitch_modify) && (volume_modify)) message = "I would like to update database and to upgrade database and to update pitch and to update volume";
                    if ((sqlite_database) && (update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to update database and to upgrade database and to update pitch";
                    if ((sqlite_database) && (update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to update database and to upgrade database";
                    if ((sqlite_database) && (!update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to uprade database";
                    if ((!sqlite_database) && (update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to update database";
                    if ((!sqlite_database) && (!update_database) && (pitch_modify) && (volume_modify)) message = "I would like to update pitch and to update volume";
                    if ((!sqlite_database) && (!update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to update pitch";
                    if ((!sqlite_database) && (!update_database) && (!pitch_modify) && (volume_modify)) message = "I would like to update volume";
                }
                else
                {
                    if ((!sqlite_database) && (update_database) && (pitch_modify) && (volume_modify)) message = "I would like to update database and to upgrade database and to update pitch and to update volume";
                    if ((!sqlite_database) && (update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to update database and to upgrade database and to update pitch";
                    if ((!sqlite_database) && (update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to update database and to upgrade database";
                    if ((!sqlite_database) && (!update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to uprade database";
                    if ((sqlite_database) && (update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to update database";
                    if ((sqlite_database) && (!update_database) && (pitch_modify) && (volume_modify)) message = "I would like to update pitch and to update volume";
                    if ((sqlite_database) && (!update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to update pitch";
                    if ((sqlite_database) && (!update_database) && (!pitch_modify) && (volume_modify)) message = "I would like to update volume";
                };

                answer = await Application.Current.MainPage.DisplayAlert("Question?", message, "Yes", "No");
                if (answer)
                {
                    if (update_database) await UpdateSQLite(select_table);
                    if ((!init_database) && (sqlite_database)) await UpgradeSQLite();
                    if ((init_database) && (!sqlite_database)) _setting.SQLiteDatabase = false;
                    if (pitch_modify) _setting.PitchSpeak = pitch_speak;
                    if (volume_modify) _setting.VolumeSpeak = volume_speak;
                };
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public async Task UpdateSQLite(int select_table)
        {
            try 
            { 
                SQLiteService sqlite_service = App.DataService;
                if (select_table == 0)
                {
                    await sqlite_service.CreateAll();
                    await sqlite_service.DeleteAll();
                    await sqlite_service.InsertAdverb();
                    await sqlite_service.InsertPronoun();
                    await sqlite_service.InsertArticle();
                    
                    await sqlite_service.InsertNumeral();
                    await sqlite_service.InsertPreposition();
                    await sqlite_service.InsertNoun();
                    await sqlite_service.InsertAdjective();
                    await sqlite_service.InsertVerb();
                    await sqlite_service.InsertSentence();
                    await sqlite_service.InsertConjunction();
                    await sqlite_service.InsertAuxiliary();
                } else
                {
                    await sqlite_service.Create(select_table);
                    await sqlite_service.Delete(select_table);
                };
                if (select_table == (int)Hunk.Adverb) await sqlite_service.InsertAdverb();
                if (select_table == (int)Hunk.Pronoun) await sqlite_service.InsertPronoun();
                if (select_table == (int)Hunk.Article) await sqlite_service.InsertArticle();

                if (select_table == (int)Hunk.Numeral) await sqlite_service.InsertNumeral();
                if (select_table == (int)Hunk.Preposition) await sqlite_service.InsertPreposition();
                if (select_table == (int)Hunk.Noun) await sqlite_service.InsertNoun();
                if (select_table == (int)Hunk.Adjective) await sqlite_service.InsertAdjective();
                if (select_table == (int)Hunk.Verb) await sqlite_service.InsertVerb();
                if (select_table == (int)Hunk.Sentence) await sqlite_service.InsertSentence();
                if (select_table == (int)Hunk.Conjunction) await sqlite_service.InsertConjunction();
                if (select_table == (int)Hunk.Auxiliary) await sqlite_service.InsertAuxiliary();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public async Task UpgradeSQLite()
        {
            try
            { 
                SQLiteService sqlite_service = App.DataService;
                await sqlite_service.LoadAdverb();
                await sqlite_service.LoadPronoun();
                await sqlite_service.LoadArticle();

                await sqlite_service.LoadNumeral();
                await sqlite_service.LoadPreposition();
                await sqlite_service.LoadLetter();
                await sqlite_service.LoadVerb();
                await sqlite_service.LoadSentence();
                await sqlite_service.LoadConjunction();
                await sqlite_service.LoadAuxiliary();

                _setting.SQLiteDatabase = true;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
    }
}
