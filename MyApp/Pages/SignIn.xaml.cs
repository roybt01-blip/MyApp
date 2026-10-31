namespace MyApp;

public partial class SignIn : ContentPage
{
	public SignIn()
	{
		InitializeComponent();
	}
    private bool _isPasswordVisible = false;

    private void OnTogglePasswordClicked(object sender, EventArgs e)
    {
        _isPasswordVisible = !_isPasswordVisible;

        // Toggle the obscurity of the entry text
        PasswordEntry.IsPassword = !_isPasswordVisible;

        // Update the icon: 
        // \ue8f5 = visibility_off (crossed eye) when visible
        // \ue8f4 = visibility (open eye) when hidden
        TogglePasswordButton.Text = _isPasswordVisible ? "\ue8f5" : "\ue8f4";
    }
}