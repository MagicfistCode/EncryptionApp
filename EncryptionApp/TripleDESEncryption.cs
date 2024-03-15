using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EncryptionApp
{
    internal class TripleDESEncryption
    {
        public static byte[] TDESEncrpyt(byte[] key, byte[] iv, string plainText)
        {
            byte[] tdesEncrypted;
            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                ICryptoTransform tdesEncryptor = tdes.CreateEncryptor(key, iv);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, tdesEncryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        tdesEncrypted = ms.ToArray();
                    }
                };
            }

            return tdesEncrypted;
        }

        public static string TDESDecrpyt(byte[] key, byte[] iv, byte[] cipherText)
        {
            string plainText;
            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                ICryptoTransform tdesDecryptor = tdes.CreateDecryptor(key, iv);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, tdesDecryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                            plainText = sr.ReadToEnd();

                    }
                };
            }
            return plainText;
        }
    }
}
