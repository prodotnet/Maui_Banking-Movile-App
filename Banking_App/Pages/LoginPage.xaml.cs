using Banking_App.Models;
using Banking_App.Services;

namespace Banking_App.Pages;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService;

    public LoginPage()
    {
        InitializeComponent();
        _apiService = new ApiService(); 
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginModel = new LoginDto
        {
            Email = EmailEntry.Text,
            Password = PasswordEntry.Text
        };

        string jwtToken = await _apiService.LoginUser(loginModel);

        if (!string.IsNullOrEmpty(jwtToken))
        {
            await DisplayAlert("Success", "Login successful!", "OK");
            
        }
        else
        {
            await DisplayAlert("Error", "Login failed", "OK");
        }
    }
}