using AtlantidaMDAApi.Models;
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
                aes.IV = vectorBytes;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key,aes.IV);
                using (MemoryStream memory = new MemoryStream(dataBytes))
                {
                    using (
                        CryptoStream cryptoStream = new CryptoStream(
                            memory,
                            decryptor,
                            CryptoStreamMode.Read
                        )
                    )
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string encrypt(string plainText)
        {
            byte[] vectorBytes = Encoding.UTF8.GetBytes(vector);
            byte[] encryptedBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = vectorBytes;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key,aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream,encryptor,CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        encryptedBytes = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encryptedBytes);
        }
    }
}