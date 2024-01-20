using Application.Abstraction;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.HashProvider
{
    public class HashProvider : IHashProvider
    {
        private readonly HashOptions _options;
        public HashProvider(IOptions<HashOptions> options)
        {
            _options = options.Value;
        }
        public string GetHash(string value)
        {
            StringBuilder Sb = new StringBuilder();
            string pepper = _options.secretPepper;

            using (var hash = SHA256.Create())
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(value + pepper));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
