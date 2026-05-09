using Nethereum.Signer;
using ChatBlockchain.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;

namespace ChatBlockchain.Api.Services
{
    public class CryptoService(ILogger<CryptoService> logger): ICryptoService
    {
        private readonly ILogger<CryptoService> _logger = logger;
        public bool VerifySignature(string message, string signature, string expectedAddress)
        {
            try
            {
                var signer = new EthereumMessageSigner();
                _logger.LogInformation("Verificando firma. Message: {Message}, Signature: {Signature}, Expected Address: {ExpectedAddress}", message, signature, expectedAddress);
                var recoveredAddress = signer.EncodeUTF8AndEcRecover(message, signature);
                _logger.LogInformation("Dirección recuperada: {RecoveredAddress}", recoveredAddress);
                return string.Equals(recoveredAddress, expectedAddress, StringComparison.OrdinalIgnoreCase);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while verifying signature.");
                return false;
            }
        }
        public string GetAddressFromPublicKey(string publicKey)
        {
            // Aplica el checksum para un formato válido y legible (formato EIP-55)
            _logger.LogInformation("Obteniendo dirección desde clave pública: {PublicKey}", publicKey);

            string cleanKey = publicKey.StartsWith("0x") ? publicKey[2..] : publicKey;
            var ecKey = new EthECKey(Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.HexToByteArray(cleanKey), false);
            string derivedAddress = ecKey.GetPublicAddress();
            return Nethereum.Util.AddressUtil.Current.ConvertToChecksumAddress(derivedAddress);
        }
    }
}