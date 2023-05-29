using System;
using System.Text;
using System.Security.Cryptography;

namespace MSys.Core.Util
{
    /// <summary>
    /// Generate hash for given source and salt using SHA256.
    /// </summary>
    public class ComputeHashUtil
    {
        public static string ComputeHash(string source, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            string hash = GetHash(source, key);

            if (VerifyHash(source, key, hash))
            {
                return hash;
            }
            else
            {
                return null;
            }

        }
        
        // Hash an input string and return the hash
        public static string GetHash(string input, string key)
        {
            string sysValue = System.Configuration.ConfigurationManager.AppSettings["HashRamdomValue"].ToString();

            //Create ByteArray for input
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            //Create ByteArray for salt
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] sysBytes = Encoding.UTF8.GetBytes(sysValue);

            // Allocate array, which will hold input and salt.
            byte[] inputWithSaltBytes = new byte[inputBytes.Length + keyBytes.Length + sysBytes.Length];

            // Copy input bytes into resulting array.
            int i = 0;
            for (i = 0; i <= inputBytes.Length - 1; i++)
            {
                inputWithSaltBytes[i] = inputBytes[i];
            }

            // Append salt bytes to the resulting array.
            for (i = 0; i <= keyBytes.Length - 1; i++)
            {
                inputWithSaltBytes[inputBytes.Length + i] = keyBytes[i];
            }
            for (i = 0; i <= sysBytes.Length - 1; i++)
            {
                //inputWithSaltBytes(keyBytes.Length + i) = sysBytes(i)
                inputWithSaltBytes[inputBytes.Length + keyBytes.Length + i] = sysBytes[i];
            }

            // Create a new instance of the SHA256 object.
            SHA256 crypto = new SHA256CryptoServiceProvider();
            byte[] hashValue = crypto.ComputeHash(inputWithSaltBytes);

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            StringBuilder hashedText = new StringBuilder();
            for (i = 0; i <= hashValue.Length - 1; i++)
            {
                hashedText.AppendFormat("{0:x2}", hashValue[i]);
            }
            return hashedText.ToString();
        }
        // Verify a hash against a string.
        public static bool VerifyHash(string input, string key, string hash)
        {
            // Hash the input.
            string hashOfInput = GetHash(input, key);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}