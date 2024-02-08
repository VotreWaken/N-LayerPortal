// Realisation of Abstract Interface For Hashing Algorithm
// Contain 2 Methods for ComputeSalt and ComputeHash
// Use SignUp Model to Receive Regular Client Password And Manipulate with him
// In that Case Use SHA256 Algorithm

using System.Security.Cryptography;
using System.Text;
using MusicPortal.Models.AccountModels;
namespace MusicPortal.Models.Hashing
{
    public class Sha256 : IHash
    {
        // Compute Salt Method
        public string ComputeSalt()
        {
            byte[] saltbuf = new byte[16];

            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);

            StringBuilder sb = new(16);
            for (int i = 0; i < 16; i++)
            {
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            }

            return sb.ToString();
        }

        // Compute Hash Method
        public string ComputeHash(string salt, string data)
        {
            byte[] password = Encoding.Unicode.GetBytes(salt + data);
            byte[] hashPassword = SHA256.HashData(password);

            StringBuilder hash = new(hashPassword.Length);
            for (int i = 0; i < hashPassword.Length; i++)
            {
                hash.Append(string.Format("{0:X2}", hashPassword[i]));
            }
            return hash.ToString();
        }

        // public string CompareHash(string data) { };
    }
}
