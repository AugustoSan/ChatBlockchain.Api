using Nethereum.Signer;

namespace ChatBlockchain.Api.Services
{
    public class CryptoService(ILogger<CryptoService> logger)
    {
        private readonly ILogger<CryptoService> _logger = logger;
        public bool VerifySignature(string message, string signature, string expectedAddress)
        {
            try
            {
                var signer = new EthereumMessageSigner();
                var recoveredAddress = signer.EncodeUTF8AndEcRecover(message, signature);
                return string.Equals(recoveredAddress, expectedAddress, StringComparison.OrdinalIgnoreCase);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while verifying signature.");
                return false;
            }
        }
    }
}