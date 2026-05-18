namespace ChatBlockchain.Core.Models
{
    public class ContactModel
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public required string Address { get; set; } = string.Empty;
        public required string PublicKey { get; set; } = string.Empty;
        public required string Name { get; set; } = string.Empty;
        // Navegación inversa
        public UserModel User { get; set; } = null!;
    }
}