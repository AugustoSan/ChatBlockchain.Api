namespace ChatBlockchain.Core.Interfaces.Services
{
    public interface ICryptoService
    {
        bool VerifySignature(string message, string signature, string expectedAddress);
        string GetAddressFromPublicKey(string publicKey);
    }
}