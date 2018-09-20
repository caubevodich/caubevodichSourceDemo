using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core.Helper
{
    public static class CryptographyHelper
    {
        private static string aes_iv = "bsxnWolsAyO7kCfWuyrnqg==";

        private static string aes_key = "AXe8YwuIn1zxt3FPWTZFlAa14EHdPAdN9FaZ9RQWihc=";

        public static string DecryptConnectionStringPassword(string cipherTextPassword)
        {
            string private_aes_key = "okeylUVXLfBAttx20ltiTa2+uAvOWV8L/QPzuQ8/M1o=";
            string private_aes_iv = "e2fIWCtVHiW5yFliLUMiyA==";
            return DecryptStringAES(cipherTextPassword, private_aes_key, private_aes_iv);
        }

        public static string DecryptStringAES(string cipherText, string aes_key, string aes_iv)
        {
            byte[] cipher = Convert.FromBase64String(cipherText);
            string decrypted = string.Empty; /// DecryptStringFromBytes(cipher, Convert.FromBase64String(aes_key), Convert.FromBase64String(aes_iv));

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Convert.FromBase64String(aes_key);
                aes.IV = Convert.FromBase64String(aes_iv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform dec = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipher))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(ms, dec, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cryptoStream))
                        {
                            decrypted = sr.ReadToEnd();
                        }
                    }
                }
            }

            return decrypted;
        }

        public static string DecryptStringAES(string cipherText)
        {
            return DecryptStringAES(cipherText, aes_key, aes_iv);
        }

        public static string EncryptConnectionStringPassword(string plainTextPassword)
        {
            string private_aes_key = "okeylUVXLfBAttx20ltiTa2+uAvOWV8L/QPzuQ8/M1o=";
            string private_aes_iv = "e2fIWCtVHiW5yFliLUMiyA==";

            return EncryptStringAES(plainTextPassword, private_aes_key, private_aes_iv);
        }

        public static string EncryptMD5(this string stringValue)
        {
            Byte[] original;
            Byte[] encode;
            MD5 md5 = new MD5CryptoServiceProvider();
            original = Encoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            return BitConverter.ToString(encode);
        }

        public static string EncryptMD5Enhance(this string stringValue)
        {
            Byte[] original, encode, encode2;
            MD5 md5 = new MD5CryptoServiceProvider();
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            original = Encoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            encode2 = sha1.ComputeHash(encode);
            var sb = new StringBuilder();
            for (int i = 0; i < encode2.Length; i++)
                sb.Append(encode2[i].ToString("x2").ToUpper());
            return sb.ToString();
        }

        public static string EncryptMD5Gravartar(this string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string EncryptSHA1(this string stringValue)
        {
            Byte[] original;
            Byte[] encode;
            SHA1 md5 = new SHA1CryptoServiceProvider();
            original = Encoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            return BitConverter.ToString(encode);
        }

        public static string EncryptSHA256(this string stringValue)
        {
            Byte[] original;
            Byte[] encode;
            SHA256 md5 = new SHA256CryptoServiceProvider();
            original = Encoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            string sha256 = BitConverter.ToString(encode);
            return sha256.Replace("-", string.Empty);
        }

        public static string EncryptSHA384(this string stringValue)
        {
            Byte[] original;
            Byte[] encode;
            SHA384 md5 = new SHA384CryptoServiceProvider();
            original = Encoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            return BitConverter.ToString(encode);
        }

        public static string EncryptSHA512(this string stringValue)
        {
            Byte[] original;
            Byte[] encode;
            SHA512 md5 = new SHA512CryptoServiceProvider();
            original = Encoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            return BitConverter.ToString(encode);
        }

        public static string EncryptStringAES(this string plainText, string aes_key, string aes_iv)
        {
            byte[] encrypted = null;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Convert.FromBase64String(aes_key);
                aes.IV = Convert.FromBase64String(aes_iv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform enc = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, enc, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        encrypted = ms.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public static string EncryptStringAES(this string plainText)
        {
            return EncryptStringAES(plainText, aes_key, aes_iv);
        }

        public static string GetHashOAuthToken(this string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            var base64 = Convert.ToBase64String(byteHash);

            return base64;
        }

        public static string PasswordHash(this string input, string salt)
        {
            var sha1 = CryptographyHelper.EncryptSHA1(input);
            var mySalt = CryptographyHelper.EncryptMD5Enhance(salt);

            string newSha1 = sha1.Replace("-", "");

            var passwordNew = newSha1 + mySalt;

            return passwordNew;
        }

        public static string StringToBase64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string StringToBase64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static Tuple<string, string> CreateAes_KeyAndAes_IV(string text, string password)
        {
            RijndaelManaged myAlg = new RijndaelManaged();
            byte[] salt = Encoding.ASCII.GetBytes(text);
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);
            myAlg.Key = key.GetBytes(myAlg.KeySize / 8);
            myAlg.IV = key.GetBytes(myAlg.BlockSize / 8);

            string aes_key = Convert.ToBase64String(myAlg.Key);
            string aes_iv = Convert.ToBase64String(myAlg.IV);

            return new Tuple<string, string>(aes_key, aes_iv);
        }
    }
}