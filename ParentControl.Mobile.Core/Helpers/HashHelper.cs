using System.Text;

namespace ParentControl.Core.Helpers
{
    public static class HashHelper
    {
        public static string HashString(this string value)
        {
           // MD5 md5 = System.Security.Cryptography.MD5.Create();

            //byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(value);

            //byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            //for (int i = 0; i < hash.Length; i++)

            //{

            //    sb.Append(hash[i].ToString("X2"));

            //}

            return sb.ToString();
        }
    }
}
