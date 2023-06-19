using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AtlantidaMDAApi.Helpers
{
    public class Crypto
    {
        private static string key = ConfigurationManager.AppSettings.Get("AES_SECRET");
        private static string vector = ConfigurationManager.AppSettings.Get("AES_VECTOR");
        public static string decrypt(string encryptedString)
        {
            byte[] vectorBytes = Encoding.UTF8.GetBytes(vector);
            byte[] dataBytes = Convert.FromBase64String(encryptedString);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                // aes.Padding = PaddingMode.None;
                aes.IV = vectorBytes;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key,aes.IV);
                using (MemoryStream memory = new MemoryStream(dataBytes))
                {
                    using (
                        CryptoStream cryptoStream = new CryptoStream(
                            (Stream)memory,
                            decryptor,
                            CryptoStreamMode.Read
                        )
                    )
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}