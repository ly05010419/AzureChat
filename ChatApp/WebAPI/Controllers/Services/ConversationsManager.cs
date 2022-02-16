using ChatApp.Managers.Interfaces;
using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Managers
{
    public class ConversationsManager :  IConversationsManager
    {
        private static List<ConversationModel> Conversations = new List<ConversationModel>();
        private static long ID=0;
        
        public ConversationModel GetConversationByUsersId(long firstUser, long secondUser)
        {
            return Conversations.FirstOrDefault(x => (x.UserOneID == firstUser && x.UserTwoID == secondUser) || (x.UserOneID == secondUser && x.UserTwoID == firstUser));
        }
        public long AddOrUpdateConversation(long firstUser, long secondUser)
        {
            var now = DateTime.UtcNow;
            var conversation = GetConversationByUsersId(firstUser, secondUser);
            if (conversation == null)
            {
                conversation = new ConversationModel
                {
                    ID = ID++,
                    UserOneID = firstUser,
                    UserTwoID = secondUser,
                    CreationDate = now,
                    ModificationDate = now,
                    ConversationsReplies = new List<ConversationReplyModel>()
                };
                Conversations.Add(conversation);
            }
            else
            {
                conversation.ModificationDate = DateTime.Now;
            }
            return conversation.ID;
        }
        public void AddReply(string message, long conversationId, long userID)
        {
            ConversationModel conversationModel= Conversations.FirstOrDefault(x => x.ID == conversationId);
            conversationModel.ConversationsReplies.Add(new ConversationReplyModel
            {
                Content = message,
                ConversationID = conversationId,
                CreationDate = DateTime.Now,
                SenderUserId = userID
            });
        }

        
        public ICollection<ConversationReplyModel> GetReplies(long firstUser, long secondUser)
        {
            var conversation = GetConversationByUsersId(firstUser, secondUser); 
            
            return conversation!=null?conversation.ConversationsReplies:new List<ConversationReplyModel>();
        }
    }
}
