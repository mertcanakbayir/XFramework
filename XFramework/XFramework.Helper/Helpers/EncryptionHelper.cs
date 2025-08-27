using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace XFramework.Helper.Helpers
{
    public class EncryptionHelper
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncryptionHelper(IConfiguration config)
        {   //Key ve iv db'deki settingsten ?????
            _key = Convert.FromBase64String(config["Encryption:Key"]);
            _iv = Convert.FromBase64String(config["Encryption:IV"]);
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            // plaintext->sw ile byte dizisine ->cryptostream ile aes şifrelenir ve memorystream'e aktarılır-> memorystream ile ram'de saklanır
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs)) { sw.Write(plainText); }

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
