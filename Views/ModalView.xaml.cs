namespace LetterStomach.Views;

public partial class ModalView : ContentPage
{
	public ModalView()
	{
		InitializeComponent();
        Application.Current.ModalPushed += OnModalPushed;
        Application.Current.ModalPopping += OnModalPopping;
    }

    private void OnModalPushed(object sender, ModalPushedEventArgs e)
    {
        this.BackgroundColor = Color.FromArgb("#80000000");
    }

    private void OnModalPopping(object sender, ModalPoppingEventArgs e)
    {
        this.BackgroundColor = Color.FromArgb("#00000000");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}