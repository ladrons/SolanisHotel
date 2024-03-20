using System.Security.Cryptography;
using System.Text;

namespace SolanisHotel.COMMON.Tools
{
    public static class EncryptionService
    {
        /// <summary>
        /// Verilen string değeri SHA-256 algoritmasını kullanarak şifreler.
        /// </summary>
        /// <param name="toBeEncrypted">Şifrelenmek istenen metin.</param>
        /// <returns>SHA-256 algoritmasıyla şifrelenmiş metin.</returns>
        public static string SHA256Encrypt(string toBeEncrypted)
        {
            SHA256 sha256 = SHA256.Create();

            byte[] sourceByte = Encoding.UTF8.GetBytes(toBeEncrypted);
            byte[] hashByte = sha256.ComputeHash(sourceByte);

            return BitConverter.ToString(hashByte).Replace("-", String.Empty);
        }
    }
}