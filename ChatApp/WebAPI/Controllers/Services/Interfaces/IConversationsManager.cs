using ChatApp.Models;
using System.Collections.Generic;

namespace ChatApp.Managers.Interfaces
{
    public interface IConversationsManager
    {
        ICollection<ConversationReplyModel> GetReplies(long firstUser, long secondUser);
        ConversationModel GetConversationByUsersId(long firstUser, long secondUser);

        long AddOrUpdateConversation(long firstUser, long secondUser);
        void AddReply(string message, long conversationId, long userID);
    }
}
