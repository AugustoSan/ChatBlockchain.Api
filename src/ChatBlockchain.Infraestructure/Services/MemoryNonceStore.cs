using System.Collections.Concurrent;
using ChatBlockchain.Core.Interfaces.Services;

namespace ChatBlockchain.Infraestructure.Services;
public class MemoryNonceStore : INonceStore
{
    private readonly ConcurrentDictionary<string, (string nonce, string challenge, DateTime expiry)> _nonces = new();
    public void Add(string address, string nonce, string challenge, TimeSpan lifetime)
    {
        _nonces[address] = (nonce, challenge, DateTime.UtcNow.Add(lifetime));
    }

    public bool TryGet(string address, out (string nonce, string challenge, DateTime expiry) stored)
    {
        return _nonces.TryGetValue(address, out stored);
    }

    public void Remove(string address) => _nonces.TryRemove(address, out _);
}