using BookClient.Models;
using BookClient.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BookClient.ViewModels
{
    // Fluent Validation With MVVM In Xamarin Forms Application
    // https://www.c-sharpcorner.com/article/fluent-validation-with-mvvm-in-xamarin-forms-application/
    public class SignUpPageViewModel : ObservableObject
    {
        public User User { get; set; }

        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _color = "Green";
        private Command _signUpCommand;

        private readonly IValidator _validator;

        public SignUpPageViewModel()
        {
            _validator = new UserValidator();
        }

        public string UserName
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        public Command SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new Command(ExecuteSignUpCommand));
            }
        }

        protected void ExecuteSignUpCommand()
        {
            this.User = new User
            {
                UserName = _username,
                Password = _password,
                ConfirmPassword = _confirmPassword,
                Email = _email
            };

            // var validationResult = _validator.Validate(this.User);
            // if (validationResult.IsValid)
            // {
            //     this.ValidationMessage = "Validation Success!!";
            //     this.Color = "Green";
            // }
            // else
            // {
            //     this.ValidationMessage = string.Empty;
            //     foreach (var error in validationResult.Errors)
            //     {
            //         this.ValidationMessage += error.ErrorMessage + "\n";
            //         this.Color = "Red";
            //     }
            //     App.Current.MainPage.DisplayAlert("FluentValidation: ", this.ValidationMessage, "Ok");
            // }
        }
    }
}
