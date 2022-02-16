using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Windows.Input;

namespace ChatApp.Mobile.ViewModels
{
    public class FriendsPageViewModel : ViewModelBase
    {
        private IEnumerable<UserModel> friendsList;
        private readonly IUsersService usersService;
        private readonly IChatService chatService;

        public IEnumerable<UserModel> FriendsList
        {
            get => friendsList;
            set => SetProperty(ref friendsList, value);
        }

        public ICommand GoToPrivateDiscussionCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public FriendsPageViewModel(INavigationService navigationService,
            ISessionService sessionService,
            IUsersService usersService,
             IChatService chatService)
            : base(navigationService, sessionService)
        {
            this.usersService = usersService;
            this.chatService = chatService;
            GoToPrivateDiscussionCommand = new DelegateCommand<UserModel>(GoToPrivateDiscussion);
            RefreshCommand = new DelegateCommand(RefreshFriends);
        }

        private async void RefreshFriends()
        {
            var currentUser = await SessionService.GetConnectedUser();
            FriendsList = await usersService.GetUserFriendsAsync(currentUser.ID);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            var currentUser = await SessionService.GetConnectedUser();
            FriendsList = await usersService.GetUserFriendsAsync(currentUser.ID);
        }

        private async void GoToPrivateDiscussion(UserModel friend)
        {
            var param = new NavigationParameters { { "friend", JsonConvert.SerializeObject(friend) } };
            await NavigationService.NavigateAsync("PrivateChatPage", param);
        }
    }
}
