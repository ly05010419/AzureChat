using ChatApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ChatApp.Managers.Interfaces
{
    public interface IUsersManager
    {
        UserModel GetUserById(long userId);
        UserModel GetUserByEmail(string email);
        void AddUserConnections(ConnectionModel conversationModel);
        UserModel Login(LoginModel loginModel, HttpContext httpContext);
        IEnumerable<UserModel> GetMyFriends(long userID);
        IEnumerable<ConversationReplyModel> GetMyMessages(long userID);
        UserModel GetUserByConnectionId(string connectionId);
    }
}
