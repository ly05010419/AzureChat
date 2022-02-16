using ChatApp.Managers.Interfaces;
using ChatApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Managers
{
    public class UsersManager :  IUsersManager
    {
        private static List<UserModel> users = new List<UserModel> ();
        private static List<ConnectionModel> connections = new List<ConnectionModel> ();
        private static long ID=0;

        public UserModel GetUserById(long userId)
        {
            return users.FirstOrDefault(x => x.ID == userId);        
        }

        public UserModel GetUserByEmail(string email)
        {
            return users.FirstOrDefault(x => x.Email == email);
        }

        public void AddUserConnections(ConnectionModel conversationModel)
        {
            connections.Add(conversationModel);
        }


        public UserModel Login(LoginModel loginModel, HttpContext httpContext)
        {
            UserModel user;
            if (!users.Exists(x => x.Email == loginModel.Email))
            { 
                user = new UserModel {Name = loginModel.Email, ID = ID++, Email = loginModel.Email};
                users.Add(user);
            }
            else
            {
                user = users.First(x => x.Email == loginModel.Email);
            }
            return user;
        }

        public IEnumerable<UserModel> GetMyFriends(long userID)
        {
            return users.FindAll(x => x.ID != userID);
         }
        public IEnumerable<UserModel> GetAllUsers()
        {
            return users;
        }
        

        public IEnumerable<ConversationReplyModel> GetMyMessages(long userID)
        {
            throw new System.NotImplementedException();
        }

        public UserModel GetUserByConnectionId(string connectionId)
        {
            ConnectionModel connectionModel = new ConnectionModel();
            foreach (var c in connections)
            {
                if (c.ConnectionID == connectionId) connectionModel = c;
            }
            return connectionModel.User;
        }
    }
}
