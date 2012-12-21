
#region Using Statements

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

#endregion

namespace BananaSplit.Core.Extensions.Encryption
{
    /// <summary>
    /// Enumeration of valid Symmetric Encryption types
    /// </summary>
    /// 
    public enum SymmetricEncryptType
    {         
         DES
        ,RC2
        ,RijndaelManaged
        ,TripleDES
    }

    /// <summary>
    /// Encrypts and Decrypts data in any one of the Symmetric encryption algorithms
    /// </summary>
    /// <typeparam name="T">The type of encryption algorithms you wish to use</typeparam>
    /// 
    public class SymmetricCryptography<T> where T : SymmetricAlgorithm, new()
    {
        #region Private Fields

        //The encryption algorithm that will be used
        private T provider;
        //private UTF8Encoding utf8 = new UTF8Encoding();
        private ASCIIEncoding ascii;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// 
        public byte[] Key
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public byte[] IV
        {
            get;
            private set;
        }    

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// 
        public SymmetricCryptography() : this(new T())
        {            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// 
        public SymmetricCryptography(T provider)
        {
            this.ascii = new ASCIIEncoding();
            this.provider = provider;
        } 

        #endregion Constructors


        #region GenerateKeys

        /// <summary>
        /// Automatically Generates keys for you
        /// </summary>
        /// 
        private void GenerateKeys()
        {
            provider.GenerateKey();
            provider.GenerateIV();
            GenerateKeys(provider.Key, provider.IV);
        }        

        /// <summary>
        /// Creates a key and initialization vector from a password and a salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// 
        private void GenerateKeys(string password, string salt)
        {
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt.ToByteArray());
            GenerateKeys(key.GetBytes(provider.KeySize / 8)
                    , key.GetBytes(provider.BlockSize / 8)
                    );
        }

        /// <summary>
        /// Overload method that set the keys you provide in byte arrays
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// 
        private void GenerateKeys(byte[] key, byte[] iv)
        {
            Key = key;
            IV = iv;
        }

        #endregion


        #region Byte Array Methods       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        public byte[] Encrypt(byte[] input)
        {
            GenerateKeys();
            return Encrypt(input, Key, IV);
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        /// 
        public byte[] Encrypt(byte[] input, string password, string salt)
        {
            GenerateKeys(password, salt);
            return Encrypt(input, Key, IV);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        /// 
        public byte[] Encrypt(byte[] input, byte[] key, byte[] iv)
        {
            GenerateKeys(key, iv);
            return Transform(input,
                   provider.CreateEncryptor(Key, IV));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        public byte[] Decrypt(byte[] input)
        {
            return Decrypt(input, Key, IV);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        /// 
        public byte[] Decrypt(byte[] input, string password, string salt)
        {
            GenerateKeys(password, salt);
            return Decrypt(input, Key, IV);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        /// 
        public byte[] Decrypt(byte[] input, byte[] key, byte[] iv)
        {
            GenerateKeys(key, iv);
            return Transform(input,
                   provider.CreateDecryptor(Key, IV));
        }

        #endregion Byte Array Methods

        #region String Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        public string Encrypt(string data)
        {
            GenerateKeys();
            return Encrypt(data, Key, IV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        /// 
        public string Encrypt(string text, string password, string salt)
        {
            GenerateKeys(password, salt);
            byte[] output = Transform(ascii.GetBytes(text),
                            provider.CreateEncryptor(Key, IV));
            return Convert.ToBase64String(output);
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        /// 
        public string Encrypt(string text, byte[] key, byte[] iv)
        {
            GenerateKeys(key, iv);
            byte[] output = Transform(ascii.GetBytes(text),
                            provider.CreateEncryptor(Key, IV));
            return Convert.ToBase64String(output);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// 
        public string Decrypt(string text)
        {
            return Decrypt(text, Key, IV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        /// 
        public string Decrypt(string text, string password, string salt)
        {
            GenerateKeys(password, salt);
            byte[] output = Transform(Convert.FromBase64String(text),
                            provider.CreateDecryptor(Key, IV));
            return ascii.GetString(output);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        /// 
        public string Decrypt(string text, byte[] key, byte[] iv)
        {
            GenerateKeys(key, iv);
            byte[] output = Transform(Convert.FromBase64String(text),
                            provider.CreateDecryptor(Key, IV));
            return ascii.GetString(output);
        }

        #endregion String Methods
        
        #region SecureString Methods
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        public byte[] Encrypt(SecureString input)
        {
            GenerateKeys();
            return Encrypt(input, Key, IV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        /// 
        public byte[] Encrypt(SecureString input, string password, string salt)
        {
            GenerateKeys(password, salt);
            return Encrypt(input, Key, IV);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// 
        public void Decrypt(byte[] input, out SecureString output, string password, string salt)
        {
            GenerateKeys(password, salt);
            Decrypt(input, out output, Key, IV);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// 
        public void Decrypt(byte[] input, out SecureString output)
        {
            Decrypt(input, out output, Key, IV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        /// 
        public byte[] Encrypt(SecureString input, byte[] key, byte[] iv)
        {
            // defensive argument checking
            if (input == null) { throw new ArgumentNullException("input"); }
            GenerateKeys(key, iv);
            IntPtr inputPtr = IntPtr.Zero;

            try
            {
                // copy the SecureString to an unmanaged BSTR and get back the pointer to the memory location
                inputPtr = Marshal.SecureStringToBSTR(input);
                if (inputPtr == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Unable to allocate necessary unmanaged resources.");
                }
                char[] inputBuffer = new char[input.Length];

                try
                {
                    // pin the buffer array so the GC doesn't move it while we are
                    // doing an unmanaged memory copy, but make sure we release the
                    // pin when we are done so that the CLR can do its thing later
                    GCHandle handle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);
                    try
                    {
                        Marshal.Copy(inputPtr, inputBuffer, 0, input.Length);
                    }
                    finally
                    {
                        handle.Free();
                    }

                    // encode the input as ascii first so that we have a
                    // way to explicitly "flush" the byte array afterwards
                    byte[] asciiBuffer = ascii.GetBytes(inputBuffer);
                    try
                    {
                        return Encrypt(asciiBuffer, key, iv);
                    }
                    finally
                    {
                        Array.Clear(asciiBuffer, 0, asciiBuffer.Length);
                    }
                }
                finally
                {
                    Array.Clear(inputBuffer, 0, inputBuffer.Length);
                }
            }
            finally
            {
                // because we are using unmanaged resources, we *must*
                // explicitly deallocate those resources ourselves
                if (inputPtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(inputPtr);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// 
        public void Decrypt(byte[] input, out SecureString output, byte[] key, byte[] iv)
        {
            byte[] decryptedBuffer = null;

            try
            {
                GenerateKeys(key, iv);
                // do our normal decryption of a byte array
                decryptedBuffer = Decrypt(input, Key, IV);
                char[] outputBuffer = null;
                
                try
                {
                    // convert the decrypted array to an explicit
                    // character array that we can "flush" later
                    outputBuffer = ascii.GetChars(decryptedBuffer);
                    // Create the result and copy the characters
                    output = new SecureString();
                    try
                    {
                        for (int i = 0; i < outputBuffer.Length; i++)
                        {
                            output.AppendChar(outputBuffer[i]);
                        }
                        return;                        
                    }
                    finally
                    {
                        output.MakeReadOnly();
                    }
                }
                finally
                {
                    if (null != outputBuffer)
                    {
                        Array.Clear(outputBuffer, 0, outputBuffer.Length);
                    }                        
                }
            }
            finally
            {
                if (null != decryptedBuffer)
                {
                    Array.Clear(decryptedBuffer, 0, decryptedBuffer.Length);
                }
            }
        }

        #endregion SecureString Methods

        #region Stream Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// 
        public void Encrypt(Stream input, Stream output)
        {
            GenerateKeys();
            Encrypt(input, output, Key, IV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// 
        public void Encrypt(Stream input, Stream output, string password, string salt)
        {
            GenerateKeys(password, salt);
            Encrypt(input, output, Key, IV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// 
        public void Encrypt(Stream input, Stream output, byte[] key, byte[] iv)
        {
            GenerateKeys(key, iv);
            TransformStream(true, ref input, ref output, Key, IV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// 
        public void Decrypt(Stream input, Stream output)
        {
            Decrypt(input, output, Key, IV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// 
        public void Decrypt(Stream input, Stream output, string password, string salt)
        {
            GenerateKeys(password, salt);
            Decrypt(input, output, Key, IV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// 
        public void Decrypt(Stream input, Stream output, byte[] key, byte[] iv)
        {
            GenerateKeys(key, iv);
            TransformStream(false, ref input, ref output, key, iv);
        }

        #endregion Stream Methods

        #region Private Methods

        private byte[] Transform(byte[] input, 
                       ICryptoTransform CryptoTransform)
        {
            byte[] result = null;
            // create the necessary streams
            using (MemoryStream memStream = new MemoryStream())
            {
                using (CryptoStream cryptStream = new CryptoStream(memStream,
                         CryptoTransform, CryptoStreamMode.Write))
                {
                    // transform the bytes as requested
                    cryptStream.Write(input, 0, input.Length);
                    cryptStream.FlushFinalBlock();
                    // Read the memory stream and convert it back into byte array
                    memStream.Position = 0;
                    result = memStream.ToArray();                    
                }
            }            
            return result;
        }

        private void TransformStream(bool encrypt, ref Stream input, ref Stream output, byte[] key, byte[] iv)
        {
            GenerateKeys(key, iv);
            // defensive argument checking
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }
            if (!input.CanRead)
            {
                throw new ArgumentException("Unable to read from the input Stream.", "input");
            }
            if (!output.CanWrite)
            {
                throw new ArgumentException("Unable to write to the output Stream.", "output");
            }
            // make the buffer just large enough for the portion of the stream to be processed
            byte[] inputBuffer = new byte[input.Length - input.Position];
            // read the stream into the buffer
            input.Read(inputBuffer, 0, inputBuffer.Length);
            // transform the buffer
            byte[] outputBuffer = encrypt ? Encrypt(inputBuffer, key, iv) 
                                          : Decrypt(inputBuffer, key, iv);
            // write the transformed buffer to our output stream 
            output.Write(outputBuffer, 0, outputBuffer.Length);
        }

        #endregion Private Methods
    }
}