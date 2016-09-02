namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class DES3CryptographyHelper
    {
        public static string Decrypt3DESFromBase64(this string DecryptString, string Key, string IV)
        {
            return DecryptString.Decrypt3DESFromBase64(Key.HexStringToByteArray(), IV.HexStringToByteArray());
        }

        public static string Decrypt3DESFromBase64(this string DecryptString, byte[] Key, byte[] IV)
        {
            string str;
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(DecryptString);
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = provider.CreateDecryptor(Key, IV);
                str = Encoding.UTF8.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
                str = string.Empty;
            }
            finally
            {
                provider.Clear();
            }
            return str;
        }

        public static string Encrypt3DESToBase64(this string CryptString, string Key, string IV)
        {
            return CryptString.Encrypt3DESToBase64(Key.HexStringToByteArray(), IV.HexStringToByteArray());
        }

        public static string Encrypt3DESToBase64(this string CryptString, byte[] Key, byte[] IV)
        {
            string str;
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(CryptString);
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                str = Convert.ToBase64String(provider.CreateEncryptor(Key, IV).TransformFinalBlock(bytes, 0, bytes.Length));
            }
            catch
            {
                str = string.Empty;
            }
            finally
            {
                provider.Clear();
            }
            return str;
        }

        public static string GenerateIV()
        {
            return CryptogramHelper.ByteArrayToHexString(GetLegalIV());
        }

        public static string GenerateKey()
        {
            return CryptogramHelper.ByteArrayToHexString(GetLegalKey());
        }

        public static byte[] GetLegalIV()
        {
            byte[] iV = null;
            try
            {
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.GenerateIV();
                iV = provider.IV;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return iV;
        }

        public static byte[] GetLegalKey()
        {
            byte[] key = null;
            try
            {
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.GenerateKey();
                key = provider.Key;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return key;
        }
    }
}

