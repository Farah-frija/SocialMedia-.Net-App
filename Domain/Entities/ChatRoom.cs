using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    
        public class ChatRoom
        {   public bool isGroup {  get; set; }
            public string Id { get; set; } // Identifiant unique de la chat room
            public string Name { get; set; } // Nom de la chat room (peut être laissé vide ou utilisé pour les groupes)
            public string CreatedBy { get; set; }
            public ICollection<User> Mambers { get; set; } // Membres de la chat room
            public ICollection<Message> Messages { get; set; } // Messages envoyés dans cette chat room
        }
      
    
}
