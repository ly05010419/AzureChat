using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Core
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(ISessionService sessionService,
            INavigationService navigationService)
            : base(sessionService, navigationService)
        {
        }
      
        public async Task<IEnumerable<UserModel>> GetUserFriendsAsync(long userId)
        {
            return await Get<IEnumerable<UserModel>>($"Users/getMyFriends/{userId}");
        }
        
        public async Task<IEnumerable<ConversationReplyModel>> GetMyMessagesAsync(long userId,long friendId)
        {
            return await Get<IEnumerable<ConversationReplyModel>>($"Users/GetMyMessages/{userId}/{friendId}");
        }
    }
}
