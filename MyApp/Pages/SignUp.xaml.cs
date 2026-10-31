using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MyApp;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
	}
	private void UserName_Unfocused(object sender, FocusEventArgs e)
	{
		bool IsFocused = e.IsFocused;
		string name = UserName.Text;
		if (name.Length < 3 && !IsFocused)
		{
			UserNameErrorLabel.Opacity=1;
			UserNameErrorLabel.Text = "Username must be at least 3 characters.";
		}
		else if (name.Length > 20 && !IsFocused)
		{
			UserNameErrorLabel.Opacity=1;
			UserNameErrorLabel.Text = "Username must be less than 20 characters.";
		}
		else if(name.Any(ch => char.IsPunctuation(ch)) || name.Any(ch => char.IsSymbol(ch)))
		{
			UserNameErrorLabel.Opacity=1;
			UserNameErrorLabel.Text = "Username cannot contain punctuation or symbols.";
		}
		else 
		{
			UserNameErrorLabel.Opacity = 0;
        }
    }
	private void ValidEmail_Unfocused(object sender, FocusEventArgs e)
	{
		bool IsFocused = e.IsFocused;
		string email = ValidEmail.Text;
		string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
		if (string.IsNullOrEmpty(email) && !IsFocused)
		{
			EmailErrorLabel.Text = "cannot leave field empty";
			EmailErrorLabel.Opacity = 1;

		}
		else if (!IsFocused && !Regex.IsMatch(email, emailPattern))
		{
			EmailErrorLabel.Text = "Formet isn't valid. Please enter a valid formet";
			EmailErrorLabel.Opacity = 1;
		}
		else
			EmailErrorLabel.Opacity = 0;
    }
	private void PassWord_Unfocused(object sender, FocusEventArgs e)
	{
		bool IsFocused = e.IsFocused;
		string password = Password.Text;
		if (string.IsNullOrEmpty(password) && !IsFocused)
		{
			PasswordErrorLabel.Text = "cannot leave field empty";
			PasswordErrorLabel.Opacity = 1;
		}
		else if (password.Length < 3 && !IsFocused)
		{
			PasswordErrorLabel.Text = "password must contain 4 or more characters";
			PasswordErrorLabel.Opacity = 1;
		}
		else if (!password.Any(char.IsDigit) || !password.Any(ch => char.IsSymbol(ch)))
		{
			PasswordErrorLabel.Text = "password must contain atleast one number and one spacial character";
			PasswordErrorLabel.Opacity = 1;
		}
		else
			PasswordErrorLabel.Opacity = 0;
    }
	private void ConfirmPassWord_Unfocused(object sender, FocusEventArgs e)
	{
		bool IsFocus = e.IsFocused;
		if (ConfirmPassword.Text != Password.Text && !IsFocus)
		{
			ConfirmPasswordErrorLabel.Text = "The password does not match";
			ConfirmPasswordErrorLabel.Opacity = 1;
		}
		else
			ConfirmPasswordErrorLabel.Opacity = 0;
	}
}