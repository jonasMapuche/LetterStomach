using CommunityToolkit.Mvvm.Input;
using LetterStomach.Enums;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using System.Windows.Input;

namespace LetterStomach.ViewModels
{
    public class SettingViewModel
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
        public List<Hunks> Items { get; set; }

        public bool IsUpdateTable { get; set; }

        public bool IsSQLiteTable { get; set; }

        public int IsPitchSpeak { get; set; }

        public int IsVolumeSpeak {  get; set; }

        public ICommand CheckCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private readonly SettingService _setting;
        private ISQLiteService _sqlite_service;

        private int _pitch_init = 0;
        private int _volume_init = 0;
        #endregion

        #region CONSTRUCTOR
        public SettingViewModel(SettingService setting)
        {
            try
            {
                _sqlite_service = App.DataService;
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
        #endregion

        #region COMMAND
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
        #endregion

        #region DATABASE
        public async Task UpdateSQLite(int select_table)
        {
            try 
            { 
                if (select_table == 0)
                {
                    await this._sqlite_service.CreateAll();
                    await this._sqlite_service.DeleteAll();
                    await this._sqlite_service.InsertAdverb();
                    await this._sqlite_service.InsertPronoun();
                    await this._sqlite_service.InsertArticle();
                    await this._sqlite_service.InsertNumeral();
                    await this._sqlite_service.InsertPreposition();
                    await this._sqlite_service.InsertNoun();
                    await this._sqlite_service.InsertAdjective();
                    await this._sqlite_service.InsertVerb();
                    await this._sqlite_service.InsertSentence();
                    await this._sqlite_service.InsertConjunction();
                    await this._sqlite_service.InsertAuxiliary();
                } else
                {
                    await this._sqlite_service.Create(select_table);
                    await this._sqlite_service.Delete(select_table);
                };
                if (select_table == (int)Hunk.Adverb) await this._sqlite_service.InsertAdverb();
                if (select_table == (int)Hunk.Pronoun) await this._sqlite_service.InsertPronoun();
                if (select_table == (int)Hunk.Article) await this._sqlite_service.InsertArticle();
                if (select_table == (int)Hunk.Numeral) await this._sqlite_service.InsertNumeral();
                if (select_table == (int)Hunk.Preposition) await this._sqlite_service.InsertPreposition();
                if (select_table == (int)Hunk.Noun) await this._sqlite_service.InsertNoun();
                if (select_table == (int)Hunk.Adjective) await this._sqlite_service.InsertAdjective();
                if (select_table == (int)Hunk.Verb) await this._sqlite_service.InsertVerb();
                if (select_table == (int)Hunk.Sentence) await this._sqlite_service.InsertSentence();
                if (select_table == (int)Hunk.Conjunction) await this._sqlite_service.InsertConjunction();
                if (select_table == (int)Hunk.Auxiliary) await this._sqlite_service.InsertAuxiliary();
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
                await this._sqlite_service.LoadAdverb();
                await this._sqlite_service.LoadPronoun();
                await this._sqlite_service.LoadArticle();
                await this._sqlite_service.LoadNumeral();
                await this._sqlite_service.LoadPreposition();
                await this._sqlite_service.LoadLetter();
                await this._sqlite_service.LoadVerb();
                await this._sqlite_service.LoadSentence();
                await this._sqlite_service.LoadConjunction();
                await this._sqlite_service.LoadAuxiliary();
                _setting.SQLiteDatabase = true;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion
    }
}
