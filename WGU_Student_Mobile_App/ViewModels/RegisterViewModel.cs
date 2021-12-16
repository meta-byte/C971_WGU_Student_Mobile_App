﻿using MvvmHelpers;
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
    class RegisterViewModel : BaseViewModel
    {
        string username;
        string password;
        string email;
        string confirmPassword;

        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string ConfirmPassword { get => confirmPassword; set => SetProperty(ref confirmPassword, value); }

        IUserService userService;

        public AsyncCommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            Title = "Sign In";

            RegisterCommand = new AsyncCommand(Register);

            userService = DependencyService.Get<IUserService>();
        }

        async Task Register()
        {
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}
