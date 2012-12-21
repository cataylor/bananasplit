
#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;

#endregion

namespace BananaSplit.Core.Extensions.Encryption
{

    /// <summary>
    /// Encrypting and Hashing String Extension Methods
    /// </summary>
    /// 
    public static class EncryptionExtensions
    {

        #region Symmetric Encryption Methods

        /// <summary>
        /// Encrypt a string using one of the Symmetric Algorithms - Generics Version
        /// </summary>
        /// <typeparam name="T">The Symmetric Algorithm type to use</typeparam>
        /// <param name="input">The string to encrypt</param>
        /// <param name="password">The password to use to create the symmetric key</param>
        /// <param name="salt">An additional string value used to make the generated key harder to break by hackers</param>
        /// <returns></returns>
        /// 
        public static String EncryptSymmetric<T>(this String input, string password, string salt) where T : SymmetricAlgorithm, new()
        {
            SymmetricCryptography<T> sc = new SymmetricCryptography<T>(new T());
            return sc.Encrypt(input, password, salt);            
        }

        /// <summary>
        /// Encrypt a string using one of the Symmetric Algorithms - Non Generics Version
        /// </summary>        
        /// <param name="input">The string to encrypt</param>
        /// <param name="password">The password to use to create the symmetric key</param>
        /// <param name="salt">An additional string value used to make the generated key harder to break by hackers</param>
        /// <param name="encryptionType">The type of encryption algorithm you want to use to encrypt the data</param>
        /// <returns></returns>
        /// 
        public static String EncryptSymmetric(this String input, string password, string salt, SymmetricEncryptType encryptionType)
        {
            return ProcessEncryptionAlgorithm(encryptionType, input, password, salt);            
        }

        private static String ProcessEncryptionAlgorithm(SymmetricEncryptType encryptionType, string data, string password, string salt)
        {            
            switch (encryptionType)
            {
                case SymmetricEncryptType.DES:
                    return new SymmetricCryptography<DESCryptoServiceProvider>().Encrypt(data, password, salt);
                case SymmetricEncryptType.RC2:
                    return new SymmetricCryptography<RC2CryptoServiceProvider>().Encrypt(data, password, salt);                   
                case SymmetricEncryptType.RijndaelManaged:
                    return new SymmetricCryptography<RijndaelManaged>().Encrypt(data, password, salt);                   
                case SymmetricEncryptType.TripleDES:
                    return new SymmetricCryptography<TripleDESCryptoServiceProvider>().Encrypt(data, password, salt);  
                default:
                    return null;
            }            
        }

        /// <summary>
        /// Decrypts a string using one of the Symmetric Algorithms - Generics Version
        /// </summary>
        /// <typeparam name="T">The Symmetric Algorithm type to use</typeparam>
        /// <param name="input">The string to decrypt</param>
        /// <param name="password">The password that was used to encrypt the data</param>
        /// <param name="salt">The salt value used when the data was encrypted</param>
        /// <returns></returns>
        /// 
        public static String DecryptSymmetric<T>(this String input, string password, string salt) where T : SymmetricAlgorithm, new()
        {
            SymmetricCryptography<T> sc = new SymmetricCryptography<T>(new T());
            return sc.Decrypt(input, password, salt);
        }


        /// <summary>
        /// Decrypts a byte array using one of the Symmetric Algorithms - Generics Version
        /// </summary>
        /// <typeparam name="T">The Symmetric Algorithm type to use</typeparam>
        /// <param name="input">The byte array to decrypt</param>
        /// <param name="password">The password that was used to encrypt the data</param>
        /// <param name="salt">The salt value used when the data was encrypted</param>
        /// <returns></returns>
        /// 
        public static String DecryptSymmetric<T>(this Byte[] input, string password, string salt) where T : SymmetricAlgorithm, new()
        {
            SymmetricCryptography<T> sc = new SymmetricCryptography<T>(new T());
            return sc.Decrypt(input, password, salt).GetString();
        }


        /// <summary>
        /// Decrypts a string using one of the Symmetric Algorithms - Non Generics Version
        /// </summary>
        /// <param name="input">The string to decrypt</param>
        /// <param name="password">The password that was used to encrypt the data</param>
        /// <param name="salt">The salt value used when the data was encrypted</param>
        /// <param name="encryptionType">The encryption type that was used to encrypt this data</param>
        /// <returns></returns>
        /// 
        public static String DecryptSymmetric(this String input, string password, string salt, SymmetricEncryptType encryptionType)
        {            
            return ProcessDecryptionAlgorithm(encryptionType, input, password, salt);
        }


        private static String ProcessDecryptionAlgorithm(SymmetricEncryptType encryptionType, string data, string password, string salt)
        {
            switch (encryptionType)
            {
                case SymmetricEncryptType.DES:
                    return new SymmetricCryptography<DESCryptoServiceProvider>().Decrypt(data, password, salt);
                case SymmetricEncryptType.RC2:
                    return new SymmetricCryptography<RC2CryptoServiceProvider>().Decrypt(data, password, salt);
                case SymmetricEncryptType.RijndaelManaged:
                    return new SymmetricCryptography<RijndaelManaged>().Decrypt(data, password, salt);
                case SymmetricEncryptType.TripleDES:
                    return new SymmetricCryptography<TripleDESCryptoServiceProvider>().Decrypt(data, password, salt);
                default:
                    return null;
            }
        }

        #endregion


        #region Hash Methods

        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, the passed in salt
        /// or a random salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>
        /// <typeparam name="T">
        /// Name of the hash algorithm. Allowed values are: "MD5CryptoServiceProvider", "SHA1Managed",
        /// "SHA256Managed", "SHA384Managed", and "SHA512Managed" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.        
        /// </typeparam>
        /// <param name="salt">
        /// An additional value to make the hash harder to hack.This will be randomly generated if 
        /// it is null.
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        /// 
        public static String ComputeHash<T>(this String input, string salt) 
                                                where T : HashAlgorithm, new()
        {
            return Hasher.ComputeHash<T>(input, salt);       
        }

        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, the passed in salt
        /// or a random salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>
        /// <typeparam name="T">
        /// Name of the hash algorithm. Allowed values are: "MD5CryptoServiceProvider", "SHA1Managed",
        /// "SHA256Managed", "SHA384Managed", and "SHA512Managed" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.        
        /// </typeparam>
        /// <param name="salt">
        /// An additional value to make the hash harder to hack.This will be randomly generated if 
        /// it is null.
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        /// 
        public static String ComputeHash<T>(this String input, byte[] salt)
                                                where T : HashAlgorithm, new()
        {
            return Hasher.ComputeHash<T>(input, salt);
        }

        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, the passed in salt
        /// or a random salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>
        /// <typeparam name="T">
        /// Name of the hash algorithm. Allowed values are: "MD5CryptoServiceProvider", "SHA1Managed",
        /// "SHA256Managed", "SHA384Managed", and "SHA512Managed" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.    
        /// </typeparam>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        /// 
        public static String ComputeHash<T>(this String input)
                                                where T : HashAlgorithm, new()
        {
            return Hasher.ComputeHash<T>(input);
        }

        /// <summary>
        /// Non-Generic version of the Hash Algorithm.
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random 
        /// salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>             
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
        public static String ComputeHash(this String input, string salt, HashType hashType)
        {
            return Hasher.ComputeHash(input, salt, hashType);
        }

        /// <summary>
        /// Non-Generic version of the Hash Algorithm.
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random 
        /// salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>              
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
        public static String ComputeHash(this String input, byte[] salt, HashType hashType)
        {
            return Hasher.ComputeHash(input, salt, hashType);
        }

        /// <summary>
        /// Non-Generic version of the Hash Algorithm.
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random 
        /// salt is generated and appended to the plain text. 
        /// This salt is stored at the end of the hash value, so it can be 
        /// used later for hash verification.
        /// </summary>                 
        /// <param name="hashType">
        /// Specifies the type of Hash to apply to the plain text value
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        /// 
        public static String ComputeHash(this String input, HashType hashType)
        {
            return Hasher.ComputeHash(input, hashType);
        }

        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>
        /// <typeparam name="T">
        /// Name of the hash algorithm. Allowed values are: "MD5CryptoServiceProvider", "SHA1Managed",
        /// "SHA256Managed", "SHA384Managed", and "SHA512Managed" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </typeparam>           
        /// <param name="base64EncodeHashToCompare">
        /// Base64-encoded hash value produced by ComputeHash function. This value may
        /// include the original salt appended to it.
        /// </param>
        /// <returns>
        /// If computed hash mathes the specified hash the function the return
        /// value is true; otherwise, the function returns false.
        /// </returns>
        ///  
        public static bool VerifyHash<T>(this String input, string base64EncodeHashToCompare)
                                                                where T : HashAlgorithm, new()
        {
            return Hasher.VerifyHash<T>(input, base64EncodeHashToCompare);
        }

        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>
        /// <typeparam name="T">
        /// Name of the hash algorithm. Allowed values are: "MD5CryptoServiceProvider", "SHA1Managed",
        /// "SHA256Managed", "SHA384Managed", and "SHA512Managed" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </typeparam>           
        /// <param name="hashToCompare">
        /// Byte array hash value to compare the plain text to
        /// </param>
        /// <returns>
        /// If computed hash mathes the specified hash the function the return
        /// value is true; otherwise, the function returns false.
        /// </returns>
        ///
        public static bool VerifyHash<T>(this String input, byte[] hashToCompare)
                                                                where T : HashAlgorithm, new()
        {
            return Hasher.VerifyHash<T>(input, hashToCompare);
        }

        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>             
        /// <param name="base64EncodeHashToCompare">
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
        public static bool VerifyHash(this String input, string base64EncodeHashToCompare, HashType hashType)                                                                
        {
            return Hasher.VerifyHash(input, base64EncodeHashToCompare, hashType);
        }

        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>             
        /// <param name="hashToCompare">
        /// Byte array hash value produced by ComputeHash function. 
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
        public static bool VerifyHash(this String input, byte[] hashToCompare, HashType hashType)
        {
            return Hasher.VerifyHash(input, hashToCompare, hashType);
        }


        #endregion

        #region Base64 Methods

        /// <summary>
        /// Base64 Encodes a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        public static String ToBase64(this String input)
        {
            return Convert.ToBase64String(input.ToByteArray());
        }

        /// <summary>
        /// Converts a Base64 Encoded string back into its original Byte array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        public static Byte[] FromBase64(this String input)
        {
            return Convert.FromBase64String(input);
        }

        #endregion

        /// <summary>
        /// Returns a Secure String from a string
        /// </summary>
        /// <param name="input">The string to make secure</param>
        /// <returns>A secure string object</returns>
        /// 
        public static SecureString ToSecureString(this String input)
        {
            List<char> charList = input.ToCharArray().ToList();
            SecureString sec = new SecureString();
            charList.ForEach(c => sec.AppendChar(c));
            sec.MakeReadOnly();

            /* This is an example of how to display the string and then dispose of it */
            //IntPtr pBStr = Marshal.SecureStringToBSTR(sec);
            //try
            //{
            //    Console.Write(Marshal.PtrToStringBSTR(pBStr));
            //}
            //finally
            //{
            //    if (pBStr != IntPtr.Zero)
            //    {
            //        Marshal.ZeroFreeBSTR(pBStr);
            //        sec.Dispose();
            //    }
            //}

            return sec;
        }
    }
}
