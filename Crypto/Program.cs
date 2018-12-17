using System;
using System.Text;
using System.Net;
using System.Security;
using System.Security.Cryptography;

namespace Crypto
{
    class Program
    {
        static void Main(string[] args)
        {
            SecureString pass = cryptoPass();
            string cryptoSalt = cryptoString(), saltedStr = cryptoHash(pass, cryptoSalt);
            string db = "i88fIf3W5Chx9SOhQESJTM0VXXHcUx8rn3igPp0RLgo9wKv4pJ8JJt+qJ2Xu4Q4tRVE0yevW1Hz+gvpczVFYDg==";
            string salt = "epUkKz2u5YTd58cA+lYOJDEdWVE=";

            Console.WriteLine("Acceso " + (validateString(salt, pass, db) ? "autorizado" : "denegado"));
            Console.ReadKey();
            pass.Dispose();
        }

        static bool validateString(string salted, SecureString usrString, string storaged)
        {
            string newUsrStr = cryptoHash(usrString, salted);

            return (((object)newUsrStr).Equals((object)storaged));
        }

        static string cryptoHash(SecureString pass, string toHash)
        {
            using (var sha512 = SHA512CryptoServiceProvider.Create())
            {
                return Convert.ToBase64String(sha512.ComputeHash(
                        Encoding.ASCII.GetBytes(new NetworkCredential("", pass).Password + toHash)));
            }
        }

        static string cryptoString()
        {
            byte[] tokenData = new byte[32];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(tokenData);

                using (var crypt = RIPEMD160.Create())
                {
                    return Convert.ToBase64String(crypt.ComputeHash(tokenData));
                }
            }
        }

        static SecureString cryptoPass()
        {
            SecureString ss = new NetworkCredential("","Soy basura :v").SecurePassword;
            ss.MakeReadOnly();

            return ss;
        }
    }
}
