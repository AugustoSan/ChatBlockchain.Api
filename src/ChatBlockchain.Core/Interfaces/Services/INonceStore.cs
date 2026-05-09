namespace ChatBlockchain.Core.Interfaces.Services
{
    public interface INonceStore
    {
        void Add(string address, string nonce, string challenge, TimeSpan lifetime);
        bool TryGet(string address, out (string nonce, string challenge, DateTime expiry) stored);
        void Remove(string address);
    }
}