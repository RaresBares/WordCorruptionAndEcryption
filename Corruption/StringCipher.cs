using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Corruption
{
   public static class StringCipher
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string passPhrase = "YOUR KEY1";
        public static string saltValue = "YOUR KE2";
        public static string initVector = "HR$2pIjHR$2pIjds";
        public static string FunForEncrypt(string objText)
        {
            int keySize = 256;
            int passwordIterations = 03;
            string hashAlgorithm = "MD5";
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF32.GetBytes(objText);
            PasswordDeriveBytes password = new PasswordDeriveBytes
            (
                passPhrase,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations
            );
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor
            (
                keyBytes,
                initVectorBytes
            );
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream
            (
                memoryStream,
                encryptor,
                CryptoStreamMode.Write
            );
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            cipherText = HttpUtility.UrlEncode(cipherText);
            return cipherText;
        }
        public static string FunForDecrypt(string cipherText)
        {
            string plainText = "";
            int keySize = 256;
            int passwordIterations = 03;
            string hashAlgorithm = "MD5";
            try
            {
                cipherText = HttpUtility.UrlDecode(cipherText);
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = new PasswordDeriveBytes
                (
                    passPhrase,
                    saltValueBytes,
                    hashAlgorithm,
                    passwordIterations
                );
                byte[] keyBytes = password.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor
                (
                    keyBytes,
                    initVectorBytes
                );
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream
                (
                    memoryStream,
                    decryptor,
                    CryptoStreamMode.Read
                );
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read
                (
                    plainTextBytes,
                    0,
                    plainTextBytes.Length
                );
                memoryStream.Close();
                cryptoStream.Close();
                plainText = Encoding.UTF32.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                plainText = "";
            }
            return plainText;
        }
        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
        
        public static String shorten(String str, int length)
        {
            String newstr = str;
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                int pos = rand.Next(newstr.Length);
                // The deleteCharAt() method deletes the char at the given
                // position, so we can directly use the retrieved value
                // from nextInt() as the argument to deleteCharAt().
                if (pos == 0)
                {
                    newstr = newstr.Substring( 1);
                }
                else
                {
                    newstr = newstr.Substring(0, pos) + newstr.Substring(pos + 1);
                }

                
               
            }
            return newstr;
        }
        
    }
   
   
}
