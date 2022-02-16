using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Models
{
    public class ConversationModel: BaseModel
    {
        public ICollection<ConversationReplyModel> ConversationsReplies { get; set; }
        
        public long UserOneID { get; set; }
        
        public long UserTwoID { get; set; }
    }
}
