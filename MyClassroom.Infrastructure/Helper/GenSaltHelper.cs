using System.Security.Cryptography;

namespace MyClassroom.Infrastructure.Helper
{
    public static class GenSaltHelper
    {
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16]; // 16 bytes = 128 bits
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
