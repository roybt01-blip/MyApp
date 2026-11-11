using MyApp.Service;
using MyApp.ViewModels;

namespace MyApp.Pages;

public partial class SignIn2 : ContentPage
{
    private DBMokup _db;
    private string _userName;
    private string _password;
    public SignIn2()
	{
		InitializeComponent();

        _db = new DBMokup();
        BindingContext = new SignInViewModel();
    }
    public string UserName
    {
        get { return _userName; }
        set
        {
            if(_userName != value)
            {
                _userName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSignInButtonEnabled));
            }
        }
    }
    public string UserPassword
    {
        get { return _password; }
        set
        {
            if(_password != value)
            {
                _password = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSignInButtonEnabled));
            }
        }

    }
    public bool IsSignInButtonEnabled
    {

        get => !(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserPassword));

    }
    private void OnsignInButtonClicked(object sender, EventArgs e)
    {
        if (_db.isExist(UserName, UserPassword))
        {
            DisplayAlert("Success", "You have successfully signed in.", "OK");
        }
        else
        {
            DisplayAlert("Error", "Invalid username or password.", "OK");
        }
    }
}