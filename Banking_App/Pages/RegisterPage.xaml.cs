using Banking_App.Models;
using Banking_App.Services;

namespace Banking_App.Pages;

public partial class RegisterPage : ContentPage
{
    private ApiService _apiService;

    public RegisterPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string firstName = FirstNameEntry.Text;
        string lastName = LastNameEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        // Basic validation
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
            string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        // Create the DTO for registration
        var registerDto = new RegisterDto
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        // Call the API service to register
        var isSuccess = await _apiService.RegisterUser(registerDto);

        if (isSuccess)
        {
            await DisplayAlert("Success", "You have registered successfully!", "OK");
            // Optionally navigate back to login page
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Registration failed. Please try again.", "OK");
        }
    }
}