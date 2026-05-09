namespace ChatBlockchain.Core.Models;

public class ChallengeRequest
{
    public string Address { get; set; } = string.Empty;
}

public class ChallengeResponse
{
    public string Challenge { get; set; } = string.Empty;
}

public class LoginRequest
{
    public string Address { get; set; } = string.Empty;
    public string Signature { get; set; } = string.Empty;
    public string OriginalChallenge { get; set; } = string.Empty;
}

public class RegisterPublicKeyRequest
{
    public string PublicKeyHex { get; set; } = string.Empty;
}

public class User
{
    public string Address { get; set; } = string.Empty;
    public string PublicKeyHex { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}