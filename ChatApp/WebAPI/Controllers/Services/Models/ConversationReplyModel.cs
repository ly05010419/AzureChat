
namespace ChatApp.Models
{
    public class ConversationReplyModel: BaseModel
    {
	    public long SenderUserId { get; set; }
		
		public string Content { get; set; }

        public long ConversationID { get; set; }

        public ConversationModel Conversation { get; set; }
    }
}
