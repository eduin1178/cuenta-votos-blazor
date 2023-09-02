using System.Security.Cryptography;
using System.Text;

namespace CuentaVotos.Core.Shared
{
    public static class Encrypt
    {

        public static string MD5Encrypt(this string text)
        {
            MD5 md = MD5.Create();
            return GetMd5Hash(md, text);
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

    }
}
