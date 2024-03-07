using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Bot.Configuration.Encryption;

namespace EncryptionApp
{
    internal class AESEncryption
    {
        public static byte[] AESEncrypt(String text, byte[] key, byte[] iv) 
        {
            byte[] cipheredtext;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encrpytor = aes.CreateEncryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encrpytor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream)) {
                        streamWriter.Write(text);}
                    }
                    cipheredtext = memoryStream.ToArray();
                }
            }

                return cipheredtext;
        }

        public static string AESDecrypt(byte[] cipheredText, byte[] key, byte[] iv) 
        {
            string text = String.Empty;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform AESDecryptor = aes.CreateDecryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream(cipheredText)) 
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, AESDecryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            text = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return text;
        }

    }
}
