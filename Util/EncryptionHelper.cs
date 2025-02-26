using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PaceBackend.Util;

public class EncryptionHelper
{
    private static readonly string EncryptionKey = "your-strong-secret-key"; // Store securely

    public static string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
        aes.IV = new byte[16]; // Use a secure IV in production

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using (var writer = new StreamWriter(cs)) writer.Write(plainText);

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string encryptedText)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
        aes.IV = new byte[16];

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(Convert.FromBase64String(encryptedText));
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(cs);

        return reader.ReadToEnd();
    }
}