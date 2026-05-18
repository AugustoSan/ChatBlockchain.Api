namespace ChatBlockchain.Core.Models;
public class UserModel
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PublicKeyHex { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    // Navegacion: un usuario puede tener multiples transferencias de claves
    public ICollection<KeyTransfer> KeyTransfers { get; set; } = new List<KeyTransfer>();
    /// <summary>
    /// Un usuario puede tener muchos contactos
    /// </summary>  
    public ICollection<ContactModel> Contacts { get; set; } = new List<ContactModel>();
}

public class Token
{
    public string Address { get; set; } = string.Empty;
    public string OriginalChallenge { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
}

public class KeyTransfer
{
    public int Id { get; set; }
    /// <summary>
    /// Id del usuario al que pertenece la clave encriptada
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Key de la clave encriptada con la llave privada del usuario 
    /// </summary>
    public string EmisorPrivateKey { get; set; } = string.Empty;
    /// <summary>
    /// Llave pública del usuario destinatario
    /// </summary>
    public string DestinatarioPublicKey { get; set; } = string.Empty;
    /// <summary>
    /// Dirección de la clave encriptada
    /// </summary>
    public string DestinatarioPrivateKey { get; set; } = string.Empty;

    // Navegación inversa
    public UserModel User { get; set; } = null!;
}