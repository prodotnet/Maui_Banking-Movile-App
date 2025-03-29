using Banking_App.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Banking_App.Models;
using Banking_App.Pages;


namespace Banking_App
{
    public partial class MainPage : ContentPage
    {
        private ApiService _apiService;

        public MainPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Reset validation labels
            EmailErrorLabel.IsVisible = false;
            PasswordErrorLabel.IsVisible = false;

            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            // Basic validation
            if (string.IsNullOrEmpty(email))
            {
                EmailErrorLabel.IsVisible = true;
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                PasswordErrorLabel.IsVisible = true;
                return;
            }

            // Show loading indicator
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;

            // Create the DTO for login
            var loginDto = new LoginDto { Email = email, Password = password };

            // Call the API service to log in
            var jwtToken = await _apiService.LoginUser(loginDto);

            // Hide loading indicator
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;

            if (!string.IsNullOrEmpty(jwtToken))
            {
                await DisplayAlert("Success", "You have logged in successfully!", "OK");
                // Navigate to another page if needed (e.g. Dashboard)
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password. Please try again.", "OK");
            }
        }

        private async void OnRegisterTapped(object sender, EventArgs e)
        {
            // Navigate to the registration page
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
