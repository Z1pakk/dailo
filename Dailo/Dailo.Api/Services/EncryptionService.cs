using System.Security.Cryptography;
using Dailo.Api.Settings;
using Microsoft.Extensions.Options;

namespace Dailo.Api.Services;

public sealed class EncryptionService(IOptions<EncryptionOptions> options)
{
    private readonly byte[] _masterKey = Convert.FromBase64String(options.Value.Key);
    private const int IvSize = 16;

    public string Encrypt(string plainText)
    {
        try
        {
            using var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = _masterKey;
            aes.IV = RandomNumberGenerator.GetBytes(IvSize);

            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);

            using ICryptoTransform encryptor = aes.CreateEncryptor();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                using var sw = new StreamWriter(cs);
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }
        catch (CryptographicException e)
        {
            throw new InvalidOperationException(
                "Encryption failed. Please check the master key and try again.",
                e
            );
        }
    }

    public string Decrypt(string cipherText)
    {
        try
        {
            byte[] cipherData = Convert.FromBase64String(cipherText);
            if (cipherData.Length < IvSize)
            {
                throw new ArgumentException("Cipher text is too short to contain an IV.");
            }

            byte[] iv = new byte[IvSize];
            byte[] encryptedData = new byte[cipherData.Length - IvSize];

            Buffer.BlockCopy(cipherData, 0, iv, 0, IvSize);
            Buffer.BlockCopy(cipherData, IvSize, encryptedData, 0, cipherData.Length - IvSize);

            using var aes = Aes.Create();
            aes.Key = _masterKey;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.IV = iv;

            using var ms = new MemoryStream(encryptedData);
            using ICryptoTransform decryptor = aes.CreateDecryptor();

            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
        catch (CryptographicException e)
        {
            throw new InvalidOperationException(
                "Encryption failed. Please check the master key and try again.",
                e
            );
        }
    }
}
