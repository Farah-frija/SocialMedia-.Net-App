namespace Core.Domain.Entities
{
    public class Message
    {
        public string Id { get; set; } // Identifiant du message
        public string Content { get; set; } // Contenu du message
        public  string SenderId { get; set; } // Identifiant de l'utilisateur qui envoie le message
        public  User Sender { get; set; } // Relation avec l'utilisateur qui envoie le message
        public string ChatRoomId { get; set; } // Identifiant de la chat room
        public virtual ChatRoom ChatRoom { get; set; } // Relation avec la chat room
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Horodatage du message
    }
}