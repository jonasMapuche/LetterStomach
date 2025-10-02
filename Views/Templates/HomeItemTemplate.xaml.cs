using LetterStomach.Models;
using LetterStomach.Services;

namespace LetterStomach.Views.Templates;

public partial class HomeItemTemplate : ContentView
{
    #region VARIABLE
    private Language ENGLISH = SettingService.Instance.English;
    private Language DEUTSCH = SettingService.Instance.Deutsch;
    private Language ITALIANO = SettingService.Instance.Italino;
    private Language FRANCAIS = SettingService.Instance.Francais;
    private Language ESPANOL = SettingService.Instance.Espanol;
    #endregion

    #region CONSTRUCTOR
    public HomeItemTemplate()
	{
        try 
        {
		    InitializeComponent();
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region BUTTON
    private async void OnLeftClicked(object sender, EventArgs e)
    {
        try
        { 
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
        catch (Exception)
        {
            throw;
        }
    }

    private void ChangePause(string text, bool status)
    {
        try
        { 
            if (text == ENGLISH.Uppercase) SingletonService.Instance.PauseEnglish = status;
            if (text == DEUTSCH.Uppercase) SingletonService.Instance.PauseDeutsch = status;
            if (text == ITALIANO.Uppercase) SingletonService.Instance.PauseItaliano = status;
            if (text == FRANCAIS.Uppercase) SingletonService.Instance.PauseFrancais = status;
            if (text == ESPANOL.Uppercase) SingletonService.Instance.PauseEspanol = status;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async void OnRightClicked(object sender, EventArgs e)
    {
        try
        { 
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
        catch (Exception)
        {
            throw;
        }
    }

    private void ChangeSpeak(string text, bool status)
    {
        try 
        { 
            if (text == ENGLISH.Uppercase) SingletonService.Instance.SpeakEnglish = status;
            if (text == DEUTSCH.Uppercase) SingletonService.Instance.SpeakDeutsch = status;
            if (text == ITALIANO.Uppercase) SingletonService.Instance.SpeakItaliano = status;
            if (text == FRANCAIS.Uppercase) SingletonService.Instance.SpeakFrancais = status;
            if (text == ESPANOL.Uppercase) SingletonService.Instance.SpeakEspanol = status;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
}