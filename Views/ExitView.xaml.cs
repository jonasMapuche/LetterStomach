namespace LetterStomach.Views;

public partial class ExitView : ContentPage
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

    public ExitView()
	{
		try
		{ 
		    InitializeComponent();
            System.Environment.Exit(0);
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }
}