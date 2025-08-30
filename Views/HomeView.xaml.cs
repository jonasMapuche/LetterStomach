using LetterStomach.ViewModels;

namespace LetterStomach.Views;

public partial class HomeView : ContentPage
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

    public HomeView(HomeViewModel DetailView)
	{
        try
        { 
		    InitializeComponent();
            BindingContext = DetailView;
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }

    private async void OnSettingClicked(object sender, EventArgs e)
    {
        try
        { 
            await Shell.Current.GoToAsync(nameof(SettingView));
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }

    private async void OnDirectionSwiped(object sender, SwipedEventArgs e)
    {
        try 
        { 
            var box = (BoxView)sender;
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    box.Color = Colors.Red;
                    break;
                case SwipeDirection.Right:
                    box.Color = Colors.Blue;
                    break;
                case SwipeDirection.Up:
                    box.Color = Colors.Green;
                    break;
                case SwipeDirection.Down:
                    box.Color = Colors.Yellow;
                    break;
            }
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }
}