using System;
using System.Collections.Generic;

namespace ChatApp.Mobile.Models
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }
        
        public ICollection<ConversationModel> Conversations { get; set; }
    }
}
