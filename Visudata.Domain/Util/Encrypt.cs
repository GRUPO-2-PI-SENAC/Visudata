using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Util
{
    public class Encrypt
    {
        public static string EncryptPassword(string password)
        {
            byte[] sotrePasswordAsByteArray = Encoding.UTF8.GetBytes(password);
            string encryptedPassword = Convert.ToBase64String(sotrePasswordAsByteArray);
            return encryptedPassword;
        }

        public static string DecryptPassword(string password)
        {
            byte[] encryptedPassword = Convert.FromBase64String(password);
            string decryptedPasswordAsString = ASCIIEncoding.UTF8.GetString(encryptedPassword);
            return decryptedPasswordAsString;
        }
    }
}
