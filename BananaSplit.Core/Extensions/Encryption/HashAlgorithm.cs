
#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;

#endregion

namespace BananaSplit.Core.Extensions.Encryption{

    /// <summary>
    /// An enumeration of possible Hash algorithms
    /// to use in hashing a plain text value
    /// </summary>
    /// 
    public enum HashType
    {
         MD5
        ,SHA1
        ,SHA256
        ,SHA384
        ,SHA512
    }

    /// <summary>
    /// This class generates and compares hashes using MD5, SHA1, SHA256, SHA384, 
    /// and SHA512 hashing algorithms. 
    /// author: Charles Taylor (internetmandude1@hotmail.com)
    /// </summary>
    public static class Hasher
    {
        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, the passed in salt
        /// or a random salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>
        /// <param name="T">
        /// Name of the hash algorithm. Allowed values are: "MD5CryptoServiceProvider", "SHA1Managed",
        /// "SHA256Managed", "SHA384Managed", and "SHA512Managed" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </param>
        /// <param name="plainText">
        /// Plaintext value to be hashed. The method assumes the parameter is not null
        /// </param>        
        /// <param name="salt">
        /// An additional value to make the hash harder to hack.This will be randomly generated if 
        /// it is null.
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        public static string ComputeHash<T>(string plainText, string salt) where T : HashAlgorithm, new()
        {
            byte[] saltBytes = (!String.IsNullOrEmpty(salt)) ? salt.ToByteArray() : CreateRandomSalt();
            return ComputeHash<T>(plainText, saltBytes);
        }


        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random 
        /// salt is generated and appended to the plain text.This salt is
        /// stored at the end of the hash value, so it can be used later 
        /// for hash verification.
        /// </summary>
        /// <param name="T">
        /// Name of the hash algorithm. Allowed values are: "MD5CryptoServiceProvider", "SHA1Managed",
        /// "SHA256Managed", "SHA384Managed", and "SHA512Managed" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </param>
        /// <param name="plainText">
        /// Plaintext value to be hashed. The method assumes the parameter is not null
        /// </param>                
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        /// 
        public static string ComputeHash<T>(string plainText) where T : HashAlgorithm, new()
        {
            return ComputeHash<T>(plainText, CreateRandomSalt());
        }


        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, the passed in salt
        /// or a random salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>
        /// <param name="T">
        /// Name of the hash algorithm. Allowed values are: "MD5CryptoServiceProvider", "SHA1Managed",
        /// "SHA256Managed", "SHA384Managed", and "SHA512Managed" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </param>
        /// <param name="plainText">
        /// Plaintext value to be hashed. The method assumes the parameter is not null
        /// </param>        
        /// <param name="salt">
        /// An additional value to make the hash harder to hack. This will be randomly generated if 
        /// it is null.
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        /// 
        public static string ComputeHash<T>(string plainText, byte[] salt) where T : HashAlgorithm, new()
        {
            byte[] saltBytes = (null != salt) ? salt : CreateRandomSalt();

            // Convert plain text into a byte array.
            byte[] plainTextBytes = plainText.ToByteArray(); 

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            plainTextBytes.CopyTo(plainTextWithSaltBytes, 0); // Copy plain text bytes into resulting array.
            saltBytes.CopyTo(plainTextWithSaltBytes, plainTextBytes.Length); // Append salt bytes to the resulting array.   

            T hash = new T(); // Supports any hashing algorithm
            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

            hashBytes.CopyTo(hashWithSaltBytes, 0); // Copy hash bytes into resulting array.
            saltBytes.CopyTo(hashWithSaltBytes, hashBytes.Length); // Append salt bytes to the result.            

            // Convert result into a base64-encoded string.
            return Convert.ToBase64String(hashWithSaltBytes);
        }


        /// <summary>
        /// Non-Generic version of the Hash Algorithm.
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random 
        /// salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be hashed. The method assumes the parameter is not null
        /// </param>              
        /// <param name="hashType">
        /// Specifies the type of Hash to apply to the plain text value
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        /// 
        public static string ComputeHash(string plainText, HashType hashType)
        {
            return ComputeHash(plainText, CreateRandomSalt(), hashType);
        }


        /// <summary>
        /// Non-Generic version of the Hash Algorithm.
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, the passed in salt
        /// or a random salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be hashed. The method assumes the parameter is not null
        /// </param>        
        /// <param name="salt">
        /// An additional value to make the hash harder to hack. This will be randomly generated if 
        /// it is null.
        /// </param>
        /// <param name="hashType">
        /// Specifies the type of Hash to apply to the plain text value
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        /// 
        public static string ComputeHash(string plainText, string salt, HashType hashType)
        {
            byte[] saltBytes = (!String.IsNullOrEmpty(salt)) ? salt.ToByteArray() : CreateRandomSalt();
            return ComputeHash(plainText, saltBytes, hashType);
        }


        /// <summary>
        /// Non-Generic version of the Hash Algorithm.
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, the passed in salt
        /// or a random salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be hashed. The method assumes the parameter is not null
        /// </param>        
        /// <param name="salt">
        /// An additional value to make the hash harder to hack. This will be randomly generated if 
        /// it is null.
        /// </param>
        /// <param name="hashType">
        /// Specifies the type of Hash to apply to the plain text value
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        /// 
        public static string ComputeHash(string plainText, byte[] salt, HashType hashType)
        {
            var hashString = String.Empty;
            switch (hashType)
            {
                case HashType.SHA1:
                    hashString = ComputeHash<SHA1Managed>(plainText, salt);
                    break;
                case HashType.SHA256:
                    hashString = ComputeHash<SHA256Managed>(plainText, salt);
                    break;
                case HashType.SHA384:
                    hashString = ComputeHash<SHA384Managed>(plainText, salt);
                    break;
                case HashType.SHA512:
                    hashString = ComputeHash<SHA512Managed>(plainText, salt);
                    break;
                default:
                    hashString = ComputeHash<MD5CryptoServiceProvider>(plainText, salt);
                    break;
            }
            return hashString;
        }


        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>
        /// <param name="T">
        /// Name of the hash algorithm. Allowed values are: "MD5CryptoServiceProvider", "SHA1Managed",
        /// "SHA256Managed", "SHA384Managed", and "SHA512Managed" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </param>
        /// <param name="plainText">
        /// Plain text to be verified against the specified hash. The method
        /// does not check whether this parameter is null.
        /// </param>      
        /// <param name="base64EncodedHash">
        /// Base64-encoded hash value produced by ComputeHash function. This value
        /// includes the original salt appended to it.
        /// </param>
        /// <returns>
        /// If computed hash mathes the specified hash the function the return
        /// value is true; otherwise, the function returns false.
        /// </returns>
        /// 
        public static bool VerifyHash<T>(string plainText, string base64EncodedHash) where T : HashAlgorithm, new()
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = base64EncodedHash.FromBase64();

            string salt = null;

            // We must know size of hash (without salt).
            int hashSizeInBytes;

            T hash = new T(); //Create the concrete hash            

            // Convert size of hash from bits to bytes.
            hashSizeInBytes = GetHashSize(hash) / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
            {
                return false;
            }

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
            {
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];
            }

            salt = new ASCIIEncoding().GetString(saltBytes);

            // Compute a new hash string.
            string expectedHashString = ComputeHash<T>(plainText, salt);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return HashesAreEqual(base64EncodedHash, expectedHashString);                       
        }


        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>
        /// <param name="T">
        /// Name of the hash algorithm. Allowed values are: "MD5CryptoServiceProvider", "SHA1Managed",
        /// "SHA256Managed", "SHA384Managed", and "SHA512Managed" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </param>
        /// <param name="plainText">
        /// Plain text to be verified against the specified hash. The method
        /// does not check whether this parameter is null.
        /// </param>      
        /// <param name="hashToCompare">
        /// The byte array hash to compare
        /// </param>
        /// <returns>
        /// If computed hash mathes the specified hash the function the return
        /// value is true; otherwise, the function returns false.
        /// </returns>
        /// 
        public static bool VerifyHash<T>(string plainText, byte[] hashToCompare) where T : HashAlgorithm, new()
        {
            string base64EncodedHash = hashToCompare.GetString().ToBase64();
            return VerifyHash<T>(plainText, base64EncodedHash);
        }


        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>
        /// <param name="plainText">
        /// Plain text to be verified against the specified hash. The method
        /// does not check whether this parameter is null.  
        /// </param>
        /// <param name="base64EncodedHash">
        /// Base64-encoded hash value produced by ComputeHash function. This value
        /// includes the original salt appended to it.
        /// </param>
        /// </param>
        /// <param name="hashType">
        /// Name of the hash algorithm. Allowed values are: "MD5", "SHA1", 
        /// "SHA256", "SHA384", and "SHA512" (if any other value is specified,
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </param>
        /// <returns>
        /// If computed hash mathes the specified hash the function the return
        /// value is true; otherwise, the function returns false.
        /// </returns>
        /// 
        public static bool VerifyHash(string plainText, string base64EncodedHash, HashType hashType)
        {
            return VerifyHash(plainText, base64EncodedHash.FromBase64(), hashType);            
        }


        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>
        /// <param name="plainText">
        /// Plain text to be verified against the specified hash. The method
        /// does not check whether this parameter is null.    
        /// </param>
        /// <param name="base64EncodedHash">
        /// Base64-encoded hash value produced by ComputeHash function. This value
        /// includes the original salt appended to it.
        /// </param>
        /// </param>
        /// <param name="hashType">
        /// Name of the hash algorithm. Allowed values are: "MD5", "SHA1", 
        /// "SHA256", "SHA384", and "SHA512" (if any other value is specified,
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </param>
        /// <returns>
        /// If computed hash mathes the specified hash the function the return
        /// value is true; otherwise, the function returns false.
        /// </returns>
        /// 
        public static bool VerifyHash(string plainText, byte[] hashToCompare, HashType hashType)
        {
            var hasMatch = false;
            switch (hashType)
            {
                case HashType.SHA1:
                    hasMatch = VerifyHash<SHA1Managed>(plainText, hashToCompare);
                    break;
                case HashType.SHA256:
                    hasMatch = VerifyHash<SHA256Managed>(plainText, hashToCompare);
                    break;
                case HashType.SHA384:
                    hasMatch = VerifyHash<SHA384Managed>(plainText, hashToCompare);
                    break;
                case HashType.SHA512:
                    hasMatch = VerifyHash<SHA512Managed>(plainText, hashToCompare);
                    break;
                default:
                    hasMatch = VerifyHash<MD5CryptoServiceProvider>(plainText, hashToCompare);
                    break;
            }
            return hasMatch;
        }

        #region Private Helper Methods

        //Method used to generate a random salt
        private static byte[] CreateRandomSalt()
        {
            // Define min and max salt sizes.
            int minSaltSize = 4;
            int maxSaltSize = 8;

            // Generate a random number for the size of the salt.
            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            // Allocate a byte array, which will hold the salt.
            byte[] saltBytes = new byte[saltSize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(saltBytes);

            return saltBytes;
        }


        //Returns the expected size of the Hash
        private static int GetHashSize(HashAlgorithm hash)
        {
            int hashSizeInBits;
            if (hash is SHA1Managed)
            {
                hashSizeInBits = 160;
            }
            else if (hash is SHA256Managed)
            {
                hashSizeInBits = 256;
            }
            else if (hash is SHA384Managed)
            {
                hashSizeInBits = 384;
            }
            else if (hash is SHA512Managed)
            {
                hashSizeInBits = 512;
            }
            else
            {
                hashSizeInBits = 128;
            }
            return hashSizeInBits;
        }

        // Gets the concrete algorithm to use
        private static HashAlgorithm GetHashAlgorithm(HashType hashType)
        {
            HashAlgorithm h = null;
            switch (hashType)
            {
                case HashType.SHA1:
                    h = new SHA1Managed();
                    break;
                case HashType.SHA256:
                    h = new SHA256Managed();
                    break;
                case HashType.SHA384:
                    h = new SHA384Managed();
                    break;
                case HashType.SHA512:
                    h = new SHA512Managed();
                    break;
                default:
                    h = new MD5CryptoServiceProvider();
                    break;
            }
            return h;
        }

        //Compare the 2 hashes for equality
        private static bool HashesAreEqual(string hashOne, string hashTwo)
        {            
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return (0 == comparer.Compare(hashOne, hashTwo));
        }

        #endregion
    }

}