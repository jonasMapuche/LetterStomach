namespace LetterStomach.Views;

public partial class ExitView : ContentPage
{
    #region ERROR
    private bool _error_on = true;
    private bool _error_off = false;
    private string _error_message;

    public string error_message
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

    #region CONSTRUCTOR
    public ExitView()
	{
		try
		{
            if (this._error_off) throw new InvalidOperationException("Operation contructor \"Exit\" view failed!");
            else this.error_message = string.Empty;

            InitializeComponent();
            System.Environment.Exit(0);
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this.error_message);
        }
    }
    #endregion
}