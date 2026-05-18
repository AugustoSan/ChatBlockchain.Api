namespace ChatBlockchain.Application.Users.Dtos
{
    public class ContactDto
    {
        public required string Address { get; set; } = string.Empty;
        public required string PublicKey { get; set; } = string.Empty;
        public required string Name { get; set; } = string.Empty;
    }
}