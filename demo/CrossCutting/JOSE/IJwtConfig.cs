namespace CrossCutting.JOSE;

public interface IJwtConfig
{
    /// <summary>
    /// 發行者
    /// </summary>
    public string Issuer { get; }
    
    /// <summary>
    /// 接受者
    /// </summary>
    public string Audience { get; }
    
    /// <summary>
    /// RSA Public Key 
    /// </summary>
    public string PublicKeyPath { get; }
    
    /// <summary>
    /// RSA Private Key 
    /// </summary>
    public string PrivateKeyPath { get; }
}