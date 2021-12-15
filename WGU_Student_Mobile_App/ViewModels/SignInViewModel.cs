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


        }

        async Task Login()
        {
            //if (string.IsNullOrWhiteSpace(username))
            //{
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(password))
            //{
            //    return;
            //}

            //int userId = userService.SignIn(username, password);

            //if (userId == 0)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Invalid Login", "Login Attempt Failed", "OK");
            //    return;
            //}
            userService = DependencyService.Get<IUserService>();

            await Shell.Current.GoToAsync($"//{nameof(Terms)}");
        }

        async Task Register()
        {
            await Shell.Current.GoToAsync($"//{nameof(Registration)}");
        }
    }
}
