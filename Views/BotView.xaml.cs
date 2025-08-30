using CommunityToolkit.Maui.Core;
using LetterStomach.ViewModels;

namespace LetterStomach.Views;

public partial class BotView : ContentPage
{
    private bool _error_test = false;
    private string _error_message;

    public string error_message
    {
        get => _error_message;
        set
        {
            _error_message = value;
        }
    }

    private async void OnError(object sender, string error_message)
    {
        await DisplayAlert("Erro", error_message, "OK");
    }

    private async void OnError(string error_message)
    {
        await DisplayAlert("Erro", error_message, "OK");
    }

    private ICameraProvider _cameraProvider;
    private BotViewModel _botViewModel;

    public BotView(BotViewModel ViewModel)
	{
		try
		{ 
		    InitializeComponent();
		    BindingContext = ViewModel;
            this._botViewModel = ViewModel;
            this._botViewModel.MediaCamera = this._cameraProvider;
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }

    private void OnMediaCaptured(object sender, MediaCapturedEventArgs e)
    {
        var memoryStream = new MemoryStream();
        e.Media.CopyTo(memoryStream);
        this._botViewModel.Bytes = memoryStream.ToArray();
    }
}