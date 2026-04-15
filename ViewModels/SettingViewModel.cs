using CommunityToolkit.Mvvm.Input;
using LetterStomach.Enums;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using LetterStomach.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LetterStomach.ViewModels
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string? _error_message;

        public string? error_message
        {
            get => _error_message;
            set
            {
                _error_message = value;
            }
        }

        public event EventHandler<string>? OnError;

        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region VARIABLE
        public List<Hunks> Items { get; set; }

        private Hunks _selectedItem;
        public Hunks SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isUpdateTable;
        public bool IsUpdateTable 
        {
            get => _isUpdateTable;
            set
            {
                if (_isUpdateTable != value)
                {
                    _isUpdateTable = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSQLiteTable;
        public bool IsSQLiteTable 
        {
            get => _isSQLiteTable;
            set
            {
                if (_isSQLiteTable != value)
                {
                    _isSQLiteTable = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSQLiteDrop;
        public bool IsSQLiteDrop 
        {
            get => _isSQLiteDrop;
            set
            {
                if (_isSQLiteDrop != value)
                {
                    _isSQLiteDrop = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isCheckSetting;
        public bool IsCheckSetting
        {
            get => _isCheckSetting;
            set
            {
                if (_isCheckSetting != value)
                {
                    _isCheckSetting = value;
                    OnPropertyChanged();
                }
            }
        }

        public int IsPitchSpeak { get; set; }
        public int IsVolumeSpeak { get; set; }

        public ICommand CheckCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private SettingService _settingService;

        private ISQLiteService _sQLiteService;
        public ISQLiteService SQLiteService
        {
            get => _sQLiteService;
            set 
            {
                _sQLiteService = value;
            }
        }

        private int _pitch_init = 0;
        private int _volume_init = 0;

        private bool _update_setting = false;
        private bool _update_database = false;
        #endregion

        #region CONSTRUCTOR
        public SettingViewModel(SQLiteService sQLiteService)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation contructor \"Setting\" view model failed!");
                else this.error_message = string.Empty;

                this._sQLiteService = sQLiteService;
                this._settingService = SettingService.Instance;

                Items = new List<Hunks>
                {
                    new Hunks { Name = "Nothing", Value = (int)Hunk.Nothing },
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

                this.IsUpdateTable = this._settingService.UpdateDatabase;
                this.IsSQLiteTable = this._settingService.SQLiteDatabase;
                this.IsSQLiteDrop = this._settingService.DropDatabase;
                this.IsPitchSpeak = this._settingService.PitchSpeak;
                this.IsVolumeSpeak = this._settingService.VolumeSpeak;
                this._pitch_init = this._settingService.PitchSpeak;
                this._volume_init = this._settingService.VolumeSpeak;

                this.IsCheckSetting = false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region EVENTO CHANGE
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private string MessageQuestion(bool init_database, bool sqlite_database, bool update_database, bool drop_database, bool pitch_modify, bool volume_modify)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation message question \"Setting\" view model failed!");

                bool conjunction = false;
                string message = "I would like";
                if (!init_database)
                {
                    if (sqlite_database)
                    {
                        message += " to upgrade database";
                        conjunction = true;
                    }
                } else
                {
                    if (!sqlite_database)
                    {
                        message += " to upgrade database";
                        conjunction = true;
                    }
                }
                if (update_database)
                {
                    if (conjunction) message += " and"; 
                    message += " to update database";
                    conjunction = true;
                }
                if (drop_database)
                {
                    if (conjunction) message += " and";
                    message += " to drop database";
                    conjunction = true;
                }
                if (pitch_modify)
                {
                    if (conjunction) message += " and";
                    message += " to update pitch";
                    conjunction = true;
                }
                if (volume_modify)
                {
                    if (conjunction) message += " and";
                    message += " to update volume";
                    conjunction = true;
                }
                return message;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private string MessageCheck(bool init_database, bool sqlite_database, bool update_database, bool drop_database, bool pitch_modify, bool volume_modify)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation message check \"Setting\" view model failed!");

                bool conjunction = false;
                string message = "Update setting";
                if (!init_database)
                {
                    if (sqlite_database)
                    {
                        message += " upgrade database";
                        conjunction = true;
                    }
                }
                else
                {
                    if (!sqlite_database)
                    {
                        message += " upgrade database";
                        conjunction = true;
                    }
                }
                if (update_database)
                {
                    if (conjunction) message += " and";
                    message += " update database";
                    conjunction = true;
                }
                if (drop_database)
                {
                    if (conjunction) message += " and";
                    message += " drop database";
                    conjunction = true;
                }
                if (pitch_modify)
                {
                    if (conjunction) message += " and";
                    message += " pitch";
                    conjunction = true;
                }
                if (volume_modify)
                {
                    if (conjunction) message += " and";
                    message += " volume";
                    conjunction = true;
                }
                return message;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private string MessageError()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation message check \"Setting\" view model failed!");

                string message = "Setting do not update";
                return message;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
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
                bool drop_database = setting.DropDatabase == "True" ? true : false;
                int select_table = int.Parse(setting.SelectTable);
                int pitch_speak = int.Parse(setting.PitchSpeak);
                int volume_speak = int.Parse(setting.VolumeSpeak);
                bool init_database = this._settingService.SQLiteDatabase;
                bool pitch_modify = this._pitch_init != pitch_speak ? true : false;
                bool volume_modify = this._volume_init != volume_speak ? true : false;
                this.IsCheckSetting = !this.IsCheckSetting;

                string message = string.Empty;
                message = MessageQuestion(init_database, sqlite_database, update_database, drop_database, pitch_modify, volume_modify);

                answer = await Application.Current.MainPage.DisplayAlert("Question?", message, "Yes", "No");
                if (answer)
                {
                    bool update = false;

                    await Shell.Current.GoToAsync(nameof(ModalView));
                    Thread backgroundThread = new Thread(async () =>
                    {
                        if (update_database) update = await UpdateSQLite(select_table);
                        if ((!init_database) && (sqlite_database)) update = await UpgradeSQLite();
                        if ((init_database) && (!sqlite_database))
                        {
                            this._settingService.SQLiteDatabase = false;
                            update = true;
                        }
                        if (pitch_modify) update = await UpdatePitch(pitch_speak);
                        if (volume_modify) update = await UpdateVolume(volume_speak);
                        if (drop_database) update = await DropSQLite();

                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await Shell.Current.GoToAsync("..");
                            if (update)
                            {
                                if ((init_database != sqlite_database) || (this._update_database)) this._update_setting = true;
                                message = MessageCheck(init_database, sqlite_database, update_database, drop_database, pitch_modify, volume_modify);
                                this.IsCheckSetting = !this.IsCheckSetting;
                            }
                            else message = MessageError();
                            await Application.Current.MainPage.DisplayAlert("Conclusion", message, "Ok");
                        });
                    });
                    backgroundThread.Start();
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
        private async Task<bool> UpdateSQLite(int select_table)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation update sqlite \"Setting\" view model failed!");

                if ((select_table == (int)Hunk.Null) || (select_table == (int)Hunk.Nothing)) 
                {
                    return false;
                }
                if (select_table == 0)
                {
                    await this._sQLiteService.Create(-1, true);
                    await this._sQLiteService.Delete(-1, true);
                    await this._sQLiteService.InsertAdverb();
                    await this._sQLiteService.InsertPronoun();
                    await this._sQLiteService.InsertArticle();
                    await this._sQLiteService.InsertNumeral();
                    await this._sQLiteService.InsertPreposition();
                    await this._sQLiteService.InsertNoun();
                    await this._sQLiteService.InsertAdjective();
                    await this._sQLiteService.InsertVerb();
                    await this._sQLiteService.InsertSentence();
                    await this._sQLiteService.InsertConjunction();
                    await this._sQLiteService.InsertAuxiliary();
                    await this._sQLiteService.InsertModel();
                }
                else
                {
                    await this._sQLiteService.Create(select_table, false);
                    await this._sQLiteService.Delete(select_table, false);
                };

                if (select_table == (int)Hunk.Adverb) await this._sQLiteService.InsertAdverb();
                if (select_table == (int)Hunk.Pronoun) await this._sQLiteService.InsertPronoun();
                if (select_table == (int)Hunk.Article) await this._sQLiteService.InsertArticle();
                if (select_table == (int)Hunk.Numeral) await this._sQLiteService.InsertNumeral();
                if (select_table == (int)Hunk.Preposition) await this._sQLiteService.InsertPreposition();
                if (select_table == (int)Hunk.Noun) await this._sQLiteService.InsertNoun();
                if (select_table == (int)Hunk.Adjective) await this._sQLiteService.InsertAdjective();
                if (select_table == (int)Hunk.Verb) await this._sQLiteService.InsertVerb();
                if (select_table == (int)Hunk.Sentence) await this._sQLiteService.InsertSentence();
                if (select_table == (int)Hunk.Conjunction) await this._sQLiteService.InsertConjunction();
                if (select_table == (int)Hunk.Auxiliary) await this._sQLiteService.InsertAuxiliary();
                if (select_table == (int)Hunk.Model) await this._sQLiteService.InsertModel();

                this.IsUpdateTable = !this.IsUpdateTable;
                foreach (Hunks item in Items)
                {
                    if (item.Value == (int)Hunk.Nothing) this.SelectedItem = item;
                }
                this._update_database = true;
                return true;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<bool> DropSQLite()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation drop sqlite \"Setting\" view model failed!");

                await this._sQLiteService.Drop(-1, true);
                this._settingService.SQLiteDatabase = false;
                this.IsSQLiteTable = false;
                this.IsSQLiteDrop = false;
                return true;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<bool> UpgradeSQLite()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation upgrade sqlite \"Setting\" view model failed!");

                bool exist = await this._sQLiteService.ExistAsync();
                if (exist)
                {
                    this._settingService.SQLiteDatabase = true;
                    return true;
                } else this.IsSQLiteTable = false;
                return false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<bool> UpdatePitch(int pitch)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation update pitch \"Setting\" view model failed!");

                float pitch_float = this._settingService.PitchFloat;
                int pitch_speak = this._settingService.PitchSpeak;
                float value = (pitch_float * pitch) / pitch_speak;
                this._settingService.PitchFloat = value;
                this._settingService.PitchSpeak = pitch;
                return true;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<bool> UpdateVolume(int volume)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation update volume \"Setting\" view model failed!");

                float volume_float = this._settingService.VolumeFloat;
                int volume_speak = this._settingService.VolumeSpeak;
                float value = (volume_float * volume) / volume_speak;
                this._settingService.VolumeFloat = value;
                this._settingService.VolumeSpeak = volume;
                return true;
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
