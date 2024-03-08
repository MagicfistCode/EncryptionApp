using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.Bot.Configuration.Encryption;

namespace EncryptionApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //declaring the message variable to encrypt
            string message = "";

            //declaring the variable for user's choice of encryption type
            string choice;

            byte[] key = new byte[16];
            byte[] iv = new byte[16];

            RandomNumberGenerator.Create().GetBytes(key);
            RandomNumberGenerator.Create().GetBytes(iv);

            //asking the user to enter a message
            Console.Write("Enter a message you want to encrypt: ");
            //assigning the user input as the message
            message = Console.ReadLine();

            //asking the user the encyption algorithm they want
            Console.WriteLine("Choose your prefered encryption algorithm (enter 1, 2, 3, or 4): \n" +
                "1) AES \n" +
                "2) RSA \n" +
                "3) DES \n" +
                "4) 3DES");

            //user input is assigned to choice
            choice = Console.ReadLine();

            //validation that user's input is either 1, 2, 3, or 4
            while(!choice.Equals("1") && !choice.Equals("2") && !choice.Equals("3") && !choice.Equals("4")) 
            {
                Console.WriteLine("That is not a valid option, please choose again...");
                choice = Console.ReadLine();
            }

            //user chooses AES encrption type
            if (choice.Equals("1"))
            {
                //encrpyting the message
                byte[] aesEncryptedMessage = AESEncryption.AESEncrypt(message, key, iv);
                string aesEncryptedMessageString = Convert.ToBase64String(aesEncryptedMessage);
                Console.WriteLine("Encrypted Message: " + aesEncryptedMessageString);

                //decrypting the message
                string aesDecryptedMessage = AESEncryption.AESDecrypt(aesEncryptedMessage, key, iv);
                Console.WriteLine("Decrypted Message: " + aesDecryptedMessage);
            } else if (choice.Equals("2"))
            {
                //initializing the encrpytion service
                var rsa = new RSAEncryption();

                //displaying the public key
                Console.WriteLine("--------------------------- Public Key Below ---------------------------\n" + rsa.GetPublicKey());
                //displaying the private key
                Console.WriteLine("--------------------------- Private Key Below ---------------------------\n" + rsa.GetPrivateKey());

                //encrpyting the message
                var rsaEncryptedMessage = rsa.RSAEncrypt(message);
                Console.WriteLine("Encrypted Message: " + rsaEncryptedMessage);

                //decrypting the message
                Console.WriteLine("Decrypted Message: " + rsa.RSADecrypt(rsaEncryptedMessage));
            }

            Console.ReadLine();
        }
    }
}