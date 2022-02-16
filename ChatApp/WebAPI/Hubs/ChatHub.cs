using ChatApp.Managers.Interfaces;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.WebAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUsersManager usersManager;
        private readonly IConversationsManager conversationsManager;

        public ChatHub(IUsersManager usersManager, IConversationsManager conversationsManager)
        {
            this.usersManager = usersManager;
            this.conversationsManager = conversationsManager;
        }

        public async Task SendPrivateMessage(string userEmail, string message)
        {
            var senderUser = usersManager.GetUserByConnectionId(Context.ConnectionId);
            var friend = usersManager.GetUserByEmail(userEmail);
            if (friend.Connection != null)
            {
                await Clients.Client(friend.Connection.ConnectionID).SendAsync("ReceivePrivateMessage", userEmail, message);
            }
            SaveToDatabase(senderUser,friend,message);
        }

        private void SaveToDatabase(UserModel senderUser, UserModel friend,string message)
        {
            var conversationModel = conversationsManager.GetConversationByUsersId(senderUser.ID, friend.ID);
            if (conversationModel == null) {
                var conversationId = conversationsManager.AddOrUpdateConversation(senderUser.ID, friend.ID);
                conversationsManager.AddReply(message, conversationId, senderUser.ID);
            }else {
                conversationsManager.AddReply(message, conversationModel.ID, senderUser.ID);
            }
        }

        public async Task OnConnect(string userEmail)
        {
            UserModel user = usersManager.GetUserByEmail(userEmail);
            ConnectionModel connectionModel = new ConnectionModel
            {
                ConnectionID = Context.ConnectionId,
                UserID = user.ID,
                User = user
            };
            usersManager.AddUserConnections(connectionModel);
            user.Connection=connectionModel;
            await base.OnConnectedAsync();
        }

        public async Task OnDisconnect()
        {
            var user = usersManager.GetUserByConnectionId(Context.ConnectionId);
            user.Connection = null;
            await base.OnDisconnectedAsync(null);
        }
    }
}

