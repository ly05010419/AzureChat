using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using ChatApp.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace ChatApp.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        private readonly IAuthenticationService authenticationService;
        public ICommand SignInCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService,
             IAuthenticationService authenticationService,
             ISessionService sessionService)
            : base(navigationService, sessionService)
        {
            SignInCommand = new DelegateCommand(SignIn);
            this.authenticationService = authenticationService;
        }

        private async void SignIn()
        {
           var user = await authenticationService.Login(new LoginModel { Email = Email});
           await SessionService.SetConnectedUser(user);
           await NavigationService.NavigateAsync("../FriendsPage");
        }
        
    }
}
