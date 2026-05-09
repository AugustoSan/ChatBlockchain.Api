namespace ChatBlockchain.Core.Models;
public class UserModel
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PublicKeyHex { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}

public class Token
{
    public string Address { get; set; } = string.Empty;
    public string OriginalChallenge { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
}