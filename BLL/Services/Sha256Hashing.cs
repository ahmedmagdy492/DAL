using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class Sha256Hashing : IPasswordHashing
    {
        private readonly SHA256 _sha256;
        public Sha256Hashing()
        {
            _sha256 = SHA256.Create();
        }

        private string ConvertHashToHexa(byte[] buffer)
        {
            string result = string.Empty;

            foreach (byte b in buffer)
            {
                result += b.ToString("x2");
            }
            return result;
        }

        public string Hash(string input)
        {
            byte[] output = _sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return ConvertHashToHexa(output);
        }
    }
}
