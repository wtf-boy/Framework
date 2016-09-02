namespace WTF.Framework
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class RSACryptographyHelper
    {
        public static string DecryptMaxRSA(this string source, string xmlPrivateKey)
        {
            return Convert.FromBase64String(source).DecryptMaxRSA(xmlPrivateKey);
        }

        public static string DecryptMaxRSA(this byte[] source, string xmlPrivateKey)
        {
            string str;
            try
            {
                using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
                {
                    provider.FromXmlString(xmlPrivateKey);
                    int count = provider.KeySize / 8;
                    if (source.Length <= count)
                    {
                        str = new UnicodeEncoding().GetString(provider.Decrypt(source, false));
                    }
                    else
                    {
                        using (MemoryStream stream = new MemoryStream(source))
                        {
                            using (MemoryStream stream2 = new MemoryStream())
                            {
                                byte[] buffer = new byte[count];
                                for (int i = stream.Read(buffer, 0, count); i > 0; i = stream.Read(buffer, 0, count))
                                {
                                    byte[] destinationArray = new byte[i];
                                    Array.Copy(buffer, 0, destinationArray, 0, i);
                                    byte[] buffer3 = provider.Decrypt(destinationArray, false);
                                    stream2.Write(buffer3, 0, buffer3.Length);
                                }
                                str = new UnicodeEncoding().GetString(stream2.ToArray());
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        public static string DecryptRSA(this string source, string xmlPrivateKey)
        {
            return Convert.FromBase64String(source).DecryptRSA(xmlPrivateKey);
        }

        public static string DecryptRSA(this byte[] source, string xmlPrivateKey)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPrivateKey);
                byte[] bytes = provider.Decrypt(source, false);
                str2 = new UnicodeEncoding().GetString(bytes);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public static bool DecryptSignature(this string source, string signature, string publicKey)
        {
            return signature.DecryptSignatureHash(source.GetSignatureHash(), publicKey);
        }

        public static bool DecryptSignatureHash(this byte[] signatureByte, byte[] hashSignatureByteSource, string publicKey)
        {
            bool flag;
            try
            {
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(publicKey);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("MD5");
                if (deformatter.VerifySignature(hashSignatureByteSource, signatureByte))
                {
                    return true;
                }
                flag = false;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool DecryptSignatureHash(this byte[] signatureByte, string hashSignatureSource, string publicKey)
        {
            return signatureByte.DecryptSignatureHash(Convert.FromBase64String(hashSignatureSource), publicKey);
        }

        public static bool DecryptSignatureHash(this string signature, byte[] hashSignatureByteSource, string publicKey)
        {
            return Convert.FromBase64String(signature).DecryptSignatureHash(hashSignatureByteSource, publicKey);
        }

        public static bool DecryptSignatureHash(this string signature, string hashSignatureSource, string publicKey)
        {
            return signature.DecryptSignatureHash(Convert.FromBase64String(hashSignatureSource), publicKey);
        }

        public static string EncryptMaxRSA(this byte[] source, string xmlPublicKey)
        {
            string str;
            try
            {
                using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
                {
                    provider.FromXmlString(xmlPublicKey);
                    int count = (provider.KeySize / 8) - 11;
                    if (source.Length <= count)
                    {
                        str = Convert.ToBase64String(provider.Encrypt(source, false));
                    }
                    else
                    {
                        using (MemoryStream stream = new MemoryStream(source))
                        {
                            using (MemoryStream stream2 = new MemoryStream())
                            {
                                byte[] buffer = new byte[count];
                                for (int i = stream.Read(buffer, 0, count); i > 0; i = stream.Read(buffer, 0, count))
                                {
                                    byte[] destinationArray = new byte[i];
                                    Array.Copy(buffer, 0, destinationArray, 0, i);
                                    byte[] buffer3 = provider.Encrypt(destinationArray, false);
                                    stream2.Write(buffer3, 0, buffer3.Length);
                                }
                                str = Convert.ToBase64String(stream2.ToArray(), Base64FormattingOptions.None);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        public static string EncryptMaxRSA(this string source, string xmlPublicKey)
        {
            return new UnicodeEncoding().GetBytes(source).EncryptMaxRSA(xmlPublicKey);
        }

        public static string EncryptRSA(this byte[] source, string xmlPublicKey)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPublicKey);
                str2 = Convert.ToBase64String(provider.Encrypt(source, false));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public static string EncryptRSA(this string source, string xmlPublicKey)
        {
            return new UnicodeEncoding().GetBytes(source).EncryptRSA(xmlPublicKey);
        }

        public static string EncryptSignature(this string source, string privateKey)
        {
            return source.GetSignatureHash().EncryptSignatureHash(privateKey);
        }

        public static string EncryptSignatureHash(this byte[] hashSignatureSignatureByte, string privateKey)
        {
            return Convert.ToBase64String(hashSignatureSignatureByte.EncryptSignatureHashByte(privateKey));
        }

        public static string EncryptSignatureHash(this string hashSignatureSource, string privateKey)
        {
            return Convert.FromBase64String(hashSignatureSource).EncryptSignatureHash(privateKey);
        }

        public static byte[] EncryptSignatureHashByte(this byte[] hashSignatureSignatureByte, string privateKey)
        {
            byte[] buffer;
            try
            {
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(privateKey);
                RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
                formatter.SetHashAlgorithm("MD5");
                buffer = formatter.CreateSignature(hashSignatureSignatureByte);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return buffer;
        }

        public static byte[] EncryptSignatureHashByte(this string hashSignatureSource, string privateKey)
        {
            return Convert.FromBase64String(hashSignatureSource).EncryptSignatureHashByte(privateKey);
        }

        public static string GetSignatureHash(this FileStream file)
        {
            return Convert.ToBase64String(file.GetSignatureHashByte());
        }

        public static string GetSignatureHash(this string source)
        {
            return Convert.ToBase64String(source.GetSignatureHashByte());
        }

        public static byte[] GetSignatureHashByte(this FileStream file)
        {
            byte[] buffer2;
            try
            {
                byte[] buffer = HashAlgorithm.Create("MD5").ComputeHash(file);
                file.Close();
                buffer2 = buffer;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return buffer2;
        }

        public static byte[] GetSignatureHashByte(this string source)
        {
            byte[] buffer2;
            try
            {
                HashAlgorithm algorithm = HashAlgorithm.Create("MD5");
                byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(source);
                buffer2 = algorithm.ComputeHash(bytes);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return buffer2;
        }

        public static void RSAKey(out string xmlPrivateKeys, out string xmlPublicKey)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                xmlPrivateKeys = provider.ToXmlString(true);
                xmlPublicKey = provider.ToXmlString(false);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

