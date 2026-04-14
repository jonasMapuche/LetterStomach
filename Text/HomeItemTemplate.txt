using LetterStomach.Models;
using LetterStomach.Services;

namespace LetterStomach.Views.Templates;

public partial class HomeItemTemplate : ContentView
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
    private Language _language_english;
    private Language _language_deutsch;
    private Language _language_italiano;
    private Language _language_francais;
    private Language _language_espanol;

    private SettingService _settingService;
    #endregion

    #region CONSTRUCTOR
    public HomeItemTemplate()
	{
        try 
        {
            if (this._error_off) throw new InvalidOperationException("Operation contructor \"Home Item Template\" view failed!!");
            else this.error_message = string.Empty;

            InitializeComponent();

            this._settingService = SettingService.Instance;

            this._language_english = SettingService.Instance.English;
            this._language_deutsch = SettingService.Instance.Deutsch;
            this._language_italiano = SettingService.Instance.Italino;
            this._language_francais = SettingService.Instance.Francais;
            this._language_espanol = SettingService.Instance.Espanol;
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            throw new InvalidOperationException(this.error_message);
        }
    }
    #endregion

    #region BUTTON
    private async void OnLeftClicked(object sender, EventArgs e)
    {
        try
        {
            if (this._error_off) throw new InvalidOperationException("Operation left clicked \"Home Item Template\" view failed!!");

            string image = swiLeft.IconImageSource.ToString().ToLower();
            string play = "play_arrow_62dp_white.png";
            if (image.Contains(play))
            {
                swiLeft.IconImageSource = "play_disabled_62dp_white.png";
                ChangePause(lblName.Text, true);
            }
            else
            {
                swiLeft.IconImageSource = "play_arrow_62dp_white.png";
                ChangePause(lblName.Text, false);
            }
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            throw new InvalidOperationException(this.error_message);
        }
    }

    private void ChangePause(string text, bool status)
    {
        try
        {
            if (this._error_off) throw new InvalidOperationException("Operation change pause \"Home Item Template\" view failed!!");

            if (text == this._language_english.Uppercase) this._settingService.PauseEnglish = status;
            if (text == this._language_deutsch.Uppercase) this._settingService.PauseDeutsch = status;
            if (text == this._language_italiano.Uppercase) this._settingService.PauseItaliano = status;
            if (text == this._language_francais.Uppercase) this._settingService.PauseFrancais = status;
            if (text == this._language_espanol.Uppercase) this._settingService.PauseEspanol = status;
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            throw new InvalidOperationException(this.error_message);
        }
    }

    private async void OnRightClicked(object sender, EventArgs e)
    {
        try
        {
            if (this._error_off) throw new InvalidOperationException("Operation right clicked \"Home Item Template\" view failed!!");

            string image = swiRight.IconImageSource.ToString().ToLower();
            string play = "speaker_notes_off_62dp_white.png";
            if (image.Contains(play))
            {
                swiRight.IconImageSource = "speaker_notes_62dp_white.png";
                ChangeSpeak(lblName.Text, true);
            }
            else
            {
                swiRight.IconImageSource = "speaker_notes_off_62dp_white.png";
                ChangeSpeak(lblName.Text, false);
            }
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            throw new InvalidOperationException(this.error_message);
        }
    }

    private void ChangeSpeak(string text, bool status)
    {
        try 
        {
            if (this._error_off) throw new InvalidOperationException("Operation change speak \"Home Item Template\" view failed!!");

            if (text == this._language_english.Uppercase) this._settingService.SpeakEnglish = status;
            if (text == this._language_deutsch.Uppercase) this._settingService.SpeakDeutsch = status;
            if (text == this._language_italiano.Uppercase) this._settingService.SpeakItaliano = status;
            if (text == this._language_francais.Uppercase) this._settingService.SpeakFrancais = status;
            if (text == this._language_espanol.Uppercase) this._settingService.SpeakEspanol = status;
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            throw new InvalidOperationException(this.error_message);
        }
    }
    #endregion
}