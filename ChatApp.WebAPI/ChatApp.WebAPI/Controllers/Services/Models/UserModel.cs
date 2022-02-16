using System.Collections.Generic;

namespace ChatApp.Models
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<ConversationModel> Conversations { get; set; }

        public ICollection<FriendModel> Friends { get; set; }

        public ConnectionModel Connection { get; set; }
        
    }
}
