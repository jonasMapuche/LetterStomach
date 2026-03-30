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
        private bool _error_on = true;
        private bool _error_off = false;
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
        public int IsVolumeSpeak { get; set; }

        public ICommand CheckCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private SettingService _setting;
        private ISQLiteService _sqlite_service;

        private int _pitch_init = 0;
        private int _volume_init = 0;

        private bool _update_setting = false;
        #endregion

        #region CONSTRUCTOR
        public SettingViewModel()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation contructor \"Setting\" view model failed!");
                else this.error_message = string.Empty;

                this._sqlite_service = HomeViewModel._sQLiteService;
                this._setting = SettingService.Instance;

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
                    new Hunks { Name = "Auxiliary", Value = (int)Hunk.Auxiliary },
                    new Hunks { Name = "Model", Value = (int)Hunk.Model }
                };

                this.CheckCommand = new AsyncRelayCommand<object>(OnCheckCommand);
                this.BackCommand = new AsyncRelayCommand(OnBackCommand);

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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region COMMAND
        private async Task OnBackCommand()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation back command \"Setting\" view model failed!");

                bool update_setting = this._update_setting;
                await Shell.Current.GoToAsync($"..?refresh={update_setting}");
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        private async Task OnCheckCommand(object parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation check command \"Setting\" view model failed!");

                bool answer = false;
                Setting setting = (Setting)parameter;
                bool update_database = setting.UpdateDatabase == "True" ? true : false;
                bool sqlite_database = setting.SQLiteDatabase == "True" ? true : false;
                int select_table = int.Parse(setting.SelectTable);
                int pitch_speak = int.Parse(setting.PitchSpeak);
                int volume_speak = int.Parse(setting.VolumeSpeak);
                bool init_database = this._setting.SQLiteDatabase;
                bool pitch_modify = this._pitch_init != pitch_speak ? true : false;
                bool volume_modify = this._volume_init != volume_speak ? true : false;

                string message = "";

                if (!init_database)
                {
                    if ((sqlite_database) && (update_database) && (pitch_modify) && (volume_modify)) message = "I would like to update database and to upgrade database and to update pitch and to update volume";
                    if ((sqlite_database) && (update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to update database and to upgrade database and to update pitch";
                    if ((sqlite_database) && (update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to update database and to upgrade database";
                    if ((sqlite_database) && (!update_database) && (pitch_modify) && (volume_modify)) message = "I would like to uprade database and to update pitch and to update volume";
                    if ((sqlite_database) && (!update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to uprade database and to update pitch";
                    if ((sqlite_database) && (!update_database) && (!pitch_modify) && (volume_modify)) message = "I would like to uprade database and to update volume";
                    if ((sqlite_database) && (!update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to uprade database";

                    if ((!sqlite_database) && (update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to update database";
                    if ((!sqlite_database) && (update_database) && (pitch_modify) && (volume_modify)) message = "I would like to update database and to update pitch and to update volume";
                    if ((!sqlite_database) && (update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to update database and to update pitch";
                    if ((!sqlite_database) && (update_database) && (!pitch_modify) && (volume_modify)) message = "I would like to update database and to update volume";
                    if ((!sqlite_database) && (!update_database) && (pitch_modify) && (volume_modify)) message = "I would like to update pitch and to update volume";
                    if ((!sqlite_database) && (!update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to update pitch";
                    if ((!sqlite_database) && (!update_database) && (!pitch_modify) && (volume_modify)) message = "I would like to update volume";
                }
                else
                {
                    if ((sqlite_database) && (update_database) && (pitch_modify) && (volume_modify)) message = "I would like to update database and to update pitch and to update volume";
                    if ((sqlite_database) && (update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to update database and to update pitch";
                    if ((sqlite_database) && (update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to update database";
                    if ((sqlite_database) && (!update_database) && (pitch_modify) && (volume_modify)) message = "I would like to update pitch and to update volume";
                    if ((sqlite_database) && (!update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to update pitch";
                    if ((sqlite_database) && (!update_database) && (!pitch_modify) && (volume_modify)) message = "I would like to update volume";

                    if ((!sqlite_database) && (!update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to uprade database";
                    if ((!sqlite_database) && (update_database) && (!pitch_modify) && (!volume_modify)) message = "I would like to update database";
                    if ((!sqlite_database) && (update_database) && (pitch_modify) && (volume_modify)) message = "I would like to upgrade database and to update database and to update pitch and to update volume";
                    if ((!sqlite_database) && (update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to upgrade database and to update database and to update pitch";
                    if ((!sqlite_database) && (update_database) && (!pitch_modify) && (volume_modify)) message = "I would like to upgrade database and to update database and to update volume";
                    if ((!sqlite_database) && (!update_database) && (pitch_modify) && (volume_modify)) message = "I would like to upgrade database and to update pitch and to update volume";
                    if ((!sqlite_database) && (!update_database) && (pitch_modify) && (!volume_modify)) message = "I would like to upgrade database and to update pitch";
                    if ((!sqlite_database) && (!update_database) && (!pitch_modify) && (volume_modify)) message = "I would like to upgrade database and to update volume";
                };

                answer = await Application.Current.MainPage.DisplayAlert("Question?", message, "Yes", "No");
                if (answer)
                {
                    if (update_database) await UpdateSQLite(select_table);
                    if ((!init_database) && (sqlite_database)) await UpgradeSQLite();
                    if ((init_database) && (!sqlite_database)) this._setting.SQLiteDatabase = false;
                    if (pitch_modify) await UpdatePitch(pitch_speak);
                    if (volume_modify) await UpdateVolume(volume_speak);
                    this._update_setting = true;   
                };
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion

        #region DATABASE
        private async Task UpdateSQLite(int select_table)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation update sqlite \"Setting\" view model failed!");

                if (select_table == 0)
                {
                    await this._sqlite_service.Create(-1, true);
                    await this._sqlite_service.Delete(-1, true);
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
                    await this._sqlite_service.InsertModel();
                }
                else
                {
                    await this._sqlite_service.Create(select_table, false);
                    await this._sqlite_service.Delete(select_table, false);
                }
                ;
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
                if (select_table == (int)Hunk.Model) await this._sqlite_service.InsertModel();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task UpgradeSQLite()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation upgrade sqlite \"Setting\" view model failed!");

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task UpdatePitch(int pitch)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation update pitch \"Setting\" view model failed!");

                float pitch_float = this._setting.PitchFloat;
                int pitch_speak = this._setting.PitchSpeak;
                float value = (pitch_float * pitch) / pitch_speak;

                this._setting.PitchFloat = value;
                this._setting.PitchSpeak = pitch;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task UpdateVolume(int volume)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation update volume \"Setting\" view model failed!");

                float volume_float = this._setting.VolumeFloat;
                int volume_speak = this._setting.VolumeSpeak;
                float value = (volume_float * volume) / volume_speak;

                this._setting.VolumeFloat = value;
                this._setting.VolumeSpeak = volume;
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
