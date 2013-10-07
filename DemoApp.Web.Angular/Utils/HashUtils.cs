using System;
using System.Security.Cryptography;
using System.Text;

namespace DemoApp.Web.Angular.Utils
{
    public static class HashUtils
    {

        public static string GenerateSalt()
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                var saltBuffer = new byte[0x10];
                provider.GetBytes(saltBuffer);
                return Convert.ToBase64String(saltBuffer);
            }
        }

        public static string GetHash(string password, string salt)
        {
            using (var sha256 = new SHA256Managed())
            using (var rfc289 = new Rfc2898DeriveBytes(sha256.ComputeHash(Encoding.Default.GetBytes(password)), Convert.FromBase64String(salt), 10000))
                return Convert.ToBase64String(rfc289.GetBytes(100));
        }

        public static bool CompareHash(string attemptedPassword, string hash, string salt)
        {
            return hash == GetHash(attemptedPassword, salt);
        }
    }
}