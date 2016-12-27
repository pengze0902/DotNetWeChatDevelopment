using System;
using System.Security.Cryptography;
using System.Text;

namespace WeChatHelper
{
    public class Sha1UtilHelper
    {
        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetSha1(string str)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            var enc = new ASCIIEncoding();
            var dataToHash = enc.GetBytes(str);
            var dataHashed = sha.ComputeHash(dataToHash);
            var hash = BitConverter.ToString(dataHashed).Replace("-", "");
            return hash;
        }
    }
}