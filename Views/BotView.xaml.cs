using CommunityToolkit.Maui.Core;
using LetterStomach.ViewModels;

namespace LetterStomach.Views;

public partial class BotView : ContentPage
{
    #region ERROR
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

    private async void OnDownError(object sender, string error_message)
    {
        await DisplayAlert("Erro", error_message, "OK");
    }

    private async void OnError(string error_message)
    {
        await DisplayAlert("Erro", error_message, "OK");
    }
    #endregion

    #region VARIABLE
    private ICameraProvider _cameraProvider;
    private BotViewModel _botViewModel;
    #endregion

    #region CONSTRUCTOR
    public BotView(BotViewModel ViewModel)
	{
		try
		{
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
            InitializeComponent();
            ViewModel.OnError += OnDownError;
		    BindingContext = ViewModel;
            this._botViewModel = ViewModel;
            this._botViewModel.MediaCamera = this._cameraProvider;
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }
    #endregion

    #region EVENT
    private void OnMediaCaptured(object sender, MediaCapturedEventArgs e)
    {
        var memoryStream = new MemoryStream();
        e.Media.CopyTo(memoryStream);
        this._botViewModel.Bytes = memoryStream.ToArray();
    }
    #endregion
}