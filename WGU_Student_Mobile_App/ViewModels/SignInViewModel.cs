using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Views;

namespace WGU_Student_Mobile_App.ViewModels
{
    class SignInViewModel : BaseViewModel
    {
        string username;
        string password;

        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }


        IUserService userService;

        public AsyncCommand LoginCommand { get; }
        public AsyncCommand RegisterCommand { get; }

        public SignInViewModel()
        {
            Title = "Sign In";
            LoginCommand = new AsyncCommand(Login);
            RegisterCommand = new AsyncCommand(Register);

            userService = DependencyService.Get<IUserService>();
        }

        async Task Login()
        {

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await Application.Current.MainPage.DisplayAlert("Empty Fields", "All fields are required", "OK");
                return;
            }

            int userId = userService.SignIn(username, password);


            if (userId == -1)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Login", "Login attempt failed", "OK");
                return;
            }

            await Shell.Current.GoToAsync($"//{nameof(Terms)}");
        }

        async Task Register()
        {
            await Shell.Current.GoToAsync($"//{nameof(Registration)}");
        }
    }
}
