namespace MyApp;

public partial class StartUpPage : ContentPage
{
	public StartUpPage()
	{
		InitializeComponent();
	}
    private async void SignInButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignIn());
    }
    private async void SignUpButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUp());
    }
}