namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class SymmetricCryptographyHelper
    {
        public static string DecryptSymmetric(this string DecryptString, SymmetricProvider netSelected, string Key, string IV)
        {
            return DecryptString.DecryptSymmetric(netSelected, Key.HexStringToByteArray(), IV.HexStringToByteArray());
        }

        public static string DecryptSymmetric(this string DecryptString, SymmetricProvider netSelected, byte[] Key, byte[] IV)
        {
            string str;
            SymmetricAlgorithm symmetricCryptoService = GetSymmetricCryptoService(netSelected);
            try
            {
                symmetricCryptoService.Key = Key;
                symmetricCryptoService.IV = IV;
                byte[] inputBuffer = Convert.FromBase64String(DecryptString);
                ICryptoTransform transform = symmetricCryptoService.CreateDecryptor(Key, IV);
                return Encoding.UTF8.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
                str = string.Empty;
            }
            finally
            {
                symmetricCryptoService.Clear();
            }
            return str;
        }

        public static string EncryptSymmetric(this string CryptString, SymmetricProvider netSelected, string Key, string IV)
        {
            return CryptString.EncryptSymmetric(netSelected, Key.HexStringToByteArray(), IV.HexStringToByteArray());
        }

        public static string EncryptSymmetric(this string CryptString, SymmetricProvider netSelected, byte[] Key, byte[] IV)
        {
            string str;
            SymmetricAlgorithm symmetricCryptoService = GetSymmetricCryptoService(netSelected);
            try
            {
                symmetricCryptoService.Key = Key;
                symmetricCryptoService.IV = IV;
                byte[] bytes = Encoding.UTF8.GetBytes(CryptString);
                return Convert.ToBase64String(symmetricCryptoService.CreateEncryptor(Key, IV).TransformFinalBlock(bytes, 0, bytes.Length));
            }
            catch
            {
                str = string.Empty;
            }
            finally
            {
                symmetricCryptoService.Clear();
            }
            return str;
        }

        public static string GenerateIV(SymmetricProvider netSelected)
        {
            return CryptogramHelper.ByteArrayToHexString(GetLegalIV(netSelected));
        }

        public static string GenerateKey(SymmetricProvider netSelected)
        {
            return CryptogramHelper.ByteArrayToHexString(GetLegalKey(netSelected));
        }

        public static byte[] GetLegalIV(SymmetricProvider netSelected)
        {
            SymmetricAlgorithm symmetricCryptoService = GetSymmetricCryptoService(netSelected);
            symmetricCryptoService.GenerateIV();
            return symmetricCryptoService.IV;
        }

        public static byte[] GetLegalKey(SymmetricProvider netSelected)
        {
            SymmetricAlgorithm symmetricCryptoService = GetSymmetricCryptoService(netSelected);
            symmetricCryptoService.GenerateKey();
            return symmetricCryptoService.Key;
        }

        public static SymmetricAlgorithm GetSymmetricCryptoService(SymmetricProvider netSelected)
        {
            SymmetricAlgorithm algorithm = new DESCryptoServiceProvider();
            switch (netSelected)
            {
                case SymmetricProvider.Default:
                    return SymmetricAlgorithm.Create();

                case SymmetricProvider.DES:
                    return new DESCryptoServiceProvider();

                case SymmetricProvider.RC2:
                    return new RC2CryptoServiceProvider();

                case SymmetricProvider.Rijndael:
                    return new RijndaelManaged();
            }
            return algorithm;
        }
    }
}

