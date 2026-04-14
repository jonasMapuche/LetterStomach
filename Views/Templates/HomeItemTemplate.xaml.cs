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

    #region CONSTRUCTOR
    public HomeItemTemplate()
	{
        try 
        {
            if (this._error_off) throw new InvalidOperationException("Operation contructor \"Home Item Template\" view failed!!");
            else this.error_message = string.Empty;

            InitializeComponent();
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            throw new InvalidOperationException(this.error_message);
        }
    }
    #endregion

}