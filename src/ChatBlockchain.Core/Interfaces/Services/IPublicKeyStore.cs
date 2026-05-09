namespace ChatBlockchain.Core.Interfaces.Services
{
    public interface IPublicKeyStore
    {
        void Add(string address, string publicKeyHex);
        bool TryGet(string address, out string publicKeyHex);
    }
}