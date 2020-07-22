using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BookServer.Utilities
{
    // Encrypting & Decrypting a String in C#
    // https://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp
    // Encrypt Decrypt a String in C# .NET
    // http://tekeye.biz/2015/encrypt-decrypt-c-sharp-string
    public class Encryptor
    {
        // The size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.

        // These constants are used to determine the keysize and blocksize of the encryption algorithm
        private const int KEYSIZE = 256;
        private const int BLOCKSIZE = 128;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int ITERATIONS = 1000;

        // Encrypt
        public static string EncryptString(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is prepended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            byte[] saltStringBytes = GenerateRandomBytes(BLOCKSIZE / 8);
            byte[] ivStringBytes = GenerateRandomBytes(BLOCKSIZE / 8);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherTextBytes = null;

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, ITERATIONS))
            {
                using (var symmetricKey = new AesManaged())
                {
                    byte[] keyBytes = password.GetBytes(KEYSIZE / 8);
                    symmetricKey.KeySize = KEYSIZE;
                    symmetricKey.BlockSize = BLOCKSIZE;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    //symmetricKey.IV = ivStringBytes;

                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, 
                                // the random iv bytes, and the cipher bytes.
                                cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                            }
                        }
                    }
                }
            }

            return Convert.ToBase64String(cipherTextBytes);
        }

        // Decrypt
        public static string DecryptString(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            byte[] cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the salt bytes by extracting the first 32 bytes from the supplied cipherText bytes.
            byte[] saltStringBytes = cipherTextBytesWithSaltAndIv.Take(BLOCKSIZE / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            byte[] ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(BLOCKSIZE / 8).Take(BLOCKSIZE / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            byte[] cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((BLOCKSIZE / 8) * 2)
                .Take(cipherTextBytesWithSaltAndIv.Length - ((BLOCKSIZE / 8) * 2)).ToArray();

            byte[] plainTextBytes = null;
            int decryptedByteCount = 0;

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, ITERATIONS))
            {
                byte[] keyBytes = password.GetBytes(KEYSIZE / 8);
                using (var symmetricKey = new AesManaged())
                {
                    symmetricKey.KeySize = KEYSIZE;
                    symmetricKey.BlockSize = BLOCKSIZE;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    //symmetricKey.IV = ivStringBytes;

                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                plainTextBytes = new byte[cipherTextBytes.Length];
                                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            }
                        }
                    }

                }
            }

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        // Get SHA-256 hash
        public static string ComputeHash(string input)
        {
            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                foreach (Byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            // Return the hexadecimal string.
            return sb.ToString();
        }

        // Verify SHA-256 hash against a string.
        public static bool VerifyHash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = ComputeHash(input);

            // Create a StringComparer and compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (comparer.Compare(hashOfInput, hash) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create a random array of bytes
        /// </summary>
        /// <param name="lengthBytes">length in bytes</param>
        /// <returns></returns>
        private static byte[] GenerateRandomBytes(int lengthBytes)
        {
            var randomBytes = new byte[lengthBytes]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
