using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CrossCutting.JOSE;

/// <summary>
/// for 跨裝置/平台時的 anti-replay 
/// </summary>
/// <remarks>
/// - 採用 JWE、建議的 Library 計有...
/// 1. Java – Nimbus JOSE + JWT
/// 2. Swift - jose-swift
/// 3. dotNet/C# - Jose.Jwt
/// </remarks>
public class JwtFactory<TSubject>(IJwtConfig config) where TSubject : IJwtSubject
{
    private const int ExpiryInMilliseconds = 10_000; //允許多少時間算過期？｛設計值、勿更動｝
    private const int ClockSkewInMilliseconds = 60_000; //允許多少時間算誤差？｛設計值、勿更動｝
    
    // 共用方法：產出JwtPublicKey
    private RSA CreatePublicKey()
    {
        var pem = File.ReadAllText(config.PublicKeyPath);
        var rsa = RSA.Create();
        rsa.ImportFromPem(pem);
        return rsa;
    }
    
    // 共用方法：產出JwtPrivateKey
    private RSA CreatePrivateKey()
    {
        var pem = File.ReadAllText(config.PrivateKeyPath);
        var rsa = RSA.Create();
        rsa.ImportFromPem(pem);
        return rsa;
    }
    
    // 共用方法：產出Jti
    private string CreateJti()
    {
        var input = $"{config.Issuer}|{config.Audience}|{Guid.NewGuid():N}|{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        var jti = Convert.ToBase64String(hashBytes).Replace('+', '-').Replace('/', '_').TrimEnd('=');
        Console.WriteLine($"jti: {jti}");
        return jti;
    }
    
    // 由華航/華信端執行加密
    public string? Encode(TSubject subject)
    {
        // 發行時間戳
        var now = DateTimeOffset.UtcNow;
        var nowUnix = now.ToUnixTimeMilliseconds();
        
        // 過期時間戳
        var exp = now.AddMilliseconds(ExpiryInMilliseconds);
        var expUnix = exp.ToUnixTimeMilliseconds();

        // Subject
        var subString = JsonSerializer.Serialize<TSubject>(subject);
        Console.WriteLine($"subject: {subString}");
        
        // 標準格式、勿更動
        var payload = new
        {
            iss = config.Issuer,
            aud = config.Audience,
            sub = subString,
            iat = nowUnix,
            exp = expUnix,
            jti = CreateJti(),
        };

        // 加密：請使用 Jose 工具處理，且按照的設計的演算法以及編碼模式｛RSA_OAEP_256/A256GCM｝
        var encoded = Jose.JWT.Encode(payload, CreatePublicKey(), Jose.JweAlgorithm.RSA_OAEP_256, Jose.JweEncryption.A256GCM);
        
        return encoded;
    }
    
    public JwtResult Decode(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return new JwtResult
            {
                IsSuccess = false,
                Message = $"invalid token",
                Data = null,
            };
        }

        try
        {
            // 解密：請使用 Jose 工具處理，且按照的設計的演算法以及編碼模式｛RSA_OAEP_256/A256GCM｝
            var payload = Jose.JWT.Decode<Dictionary<string, object>>(token, CreatePrivateKey(), Jose.JweAlgorithm.RSA_OAEP_256, Jose.JweEncryption.A256GCM);
            
            // 確認發行者
            if (!payload.TryGetValue("iss", out var issuer)
                || issuer.ToString() != config.Issuer)
                return new JwtResult
                {
                    IsSuccess = false,
                    Message = $"invalid iss",
                    Data = null
                };

            // 確認接受者
            if (!payload.TryGetValue("aud", out var audience)
                || audience.ToString() != config.Audience)
                return new JwtResult
                {
                    IsSuccess = false,
                    Message = $"invalid aud",
                    Data = null
                };

            // 提取發行時間戳
            if (!payload.TryGetValue("iat", out var issueAt)
                || !long.TryParse(issueAt?.ToString(), out var iatUnix))
                return new JwtResult
                {
                    IsSuccess = false,
                    Message = $"invalid iat",
                    Data = null
                };

            // 提取過期時間戳
            if (!payload.TryGetValue("exp", out var expire)
                || !long.TryParse(expire.ToString(), out var expUnix))
                return new JwtResult
                {
                    IsSuccess = false,
                    Message = $"invalid exp",
                    Data = null
                };
            
            // 現在時刻
            var nowUnix = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            // 是否超過誤差值？
            if (Math.Abs(iatUnix - nowUnix) > ClockSkewInMilliseconds)
                return new JwtResult
                {
                    IsSuccess = false,
                    Message = $"invalid ClockSkew",
                    Data = null
                };

            // 是否超過定義值？
            if (Math.Abs(expUnix - iatUnix) > ExpiryInMilliseconds)
                return new JwtResult
                {
                    IsSuccess = false,
                    Message = $"invalid Expiry",
                    Data = null
                };

            // 是否超時了？
            if (nowUnix - expUnix > ClockSkewInMilliseconds)
                return new JwtResult
                {
                    IsSuccess = false,
                    Message = $"expired token",
                    Data = null
                };

            // 提取 Subject
            if (!payload.TryGetValue("sub", out var subject) 
                || string.IsNullOrEmpty(subject.ToString()))
                return new JwtResult
                {
                    IsSuccess = false,
                    Message = $"invalid subject",
                    Data = null
                };
            
            // 提取 JTI
            if (!payload.TryGetValue("jti", out var jti)
                || string.IsNullOrEmpty(jti.ToString()))
                return new JwtResult
                {
                    IsSuccess = false,
                    Message = $"invalid jti",
                    Data = null
                };

            // 還原資料
            return new JwtResult
            {
                IsSuccess = true,
                Message = null,
                Data = subject,
            };
        }
        catch (Exception ex)
        {
            return new JwtResult
            {
                IsSuccess = false,
                Message = $"decode failed: {ex.Message}",
                Data = null
            };
        }
    }
}

public class JwtResult
{
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
    public object? Data { get; init; }
}