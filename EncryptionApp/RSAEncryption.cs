using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EncryptionApp
{
    internal class RSAEncryption
    {
        private static RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;

        public RSAEncryption()
        {
            _privateKey = rsa.ExportParameters(true);
            _publicKey = rsa.ExportParameters(false);
        }

        public string GetPublicKey()
        {
            var sw = new StringWriter();
            var xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(sw, _privateKey);
            return sw.ToString();
        }

        public string GetPrivateKey() 
        {
            var sw = new StringWriter();
            var xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(sw, _privateKey);
            return sw.ToString();
        }

        public string RSAEncrypt(string text)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(_publicKey);
            var dataBytes = Encoding.Unicode.GetBytes(text);
            var cypherData = rsa.Encrypt(dataBytes, false);
            return Convert.ToBase64String(cypherData);
        }

        public string RSADecrypt(string cypherText)
        {
            var dataBytes = Convert.FromBase64String(cypherText);
            rsa.ImportParameters(_privateKey);
            var plainTextBytes = rsa.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(plainTextBytes);
        }
    }
}
