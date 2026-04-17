using CommunityToolkit.Maui.Core;
using LetterStomach.ViewModels;

namespace LetterStomach.Views;

public partial class BotView : ContentPage
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

    private async void OnError(object sender, string error_message)
    {
        await DisplayAlert("Error", error_message, "OK");
    }

    private async void OnError(string error_message)
    {
        await DisplayAlert("Error", error_message, "OK");
        await Shell.Current.GoToAsync("..");
    }
    #endregion

    #region VARIABLE
    private BotViewModel _botViewModel;
    #endregion

    #region CONSTRUCTOR
    public BotView(BotViewModel ViewModel)
	{
		try
		{
            if (this._error_off) throw new InvalidOperationException("Operation contructor \"Bot\" view failed!");
            else this.error_message = string.Empty;

            InitializeComponent();
            ViewModel.OnError += OnError;
		    BindingContext = ViewModel;
            this._botViewModel = ViewModel;
            this._botViewModel.ViewCamera = Camera;
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this.error_message);
        }
    }
    #endregion

    #region EVENT
    private void OnMediaCaptured(object sender, MediaCapturedEventArgs e)
    {
        try
        {
            if (this._error_off) throw new InvalidOperationException("Operation media captured \"Bot\" view failed!");

            MemoryStream? memoryStream = new MemoryStream();
            e.Media.CopyTo(memoryStream);
            this._botViewModel.Bytes = memoryStream.ToArray();
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this.error_message);
        }
    }
    #endregion
}