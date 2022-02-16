using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ChatApp.Mobile.ViewModels
{
    public class PrivateChatPageViewModel : ViewModelBase
    {
        private UserModel friend;
        public UserModel Friend
        {
            get => friend;
            set => SetProperty(ref friend, value);
        }

        private UserModel currentUser;
        public UserModel CurrentUser
        {
            get => currentUser;
            set => SetProperty(ref currentUser, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }


        private List<MessageModel> messageList;
        private readonly IChatService chatService;
        private readonly IUsersService usersService;
     

        public List<MessageModel> MessagesList
        {
            get => messageList;
            set => SetProperty(ref messageList, value);
        }
        public ICommand SendMsgCommand { get; private set; }

        public PrivateChatPageViewModel(INavigationService navigationService,
            ISessionService sessionService,
            IUsersService usersService,
            IChatService chatService)
            : base(navigationService, sessionService)
        {
            SendMsgCommand = new DelegateCommand(SendMsg);
            this.chatService = chatService;
            this.usersService = usersService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            var friendString = parameters.GetValue<string>("friend");
            Friend = JsonConvert.DeserializeObject<UserModel>(friendString);
            Title = friend.Name;
            CurrentUser = await SessionService.GetConnectedUser();
            MessagesList = new List<MessageModel>();
            var messages =await this.usersService.GetMyMessagesAsync(currentUser.ID, friend.ID);
            foreach (var conversationReplyModel in messages)
            {
                MessagesList.Add(new MessageModel
                {
                    Message = conversationReplyModel.Content,
                    IsOwnerMessage = conversationReplyModel.SenderUserId==currentUser.ID
                });
            }
            
            var tempList = MessagesList.ToList(); 
            MessagesList = new List<MessageModel>(tempList);
            
            chatService.ReceiveMessage(GetMessage);
            await chatService.Connect(CurrentUser.Email);
        }

        public override async void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            await chatService.Disconnect();
        }

        private void GetMessage(string userName, string message)
        {
            AddMessage(userName, message, false);
        }

        private void AddMessage(string userName, string message, bool isOwner)
        {
            var tempList = MessagesList.ToList();
            tempList.Add(new MessageModel { IsOwnerMessage = isOwner, Message = message, UseName = userName });
            MessagesList = new List<MessageModel>(tempList);
            Message = string.Empty;
        }

        private void SendMsg()
        {
            chatService.SendMessage(friend.Email, Message);
            AddMessage(friend.Name, Message, true);
        }
    }
}
