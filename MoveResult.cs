using System;
using System.Security.Cryptography;
using System.Text;

namespace _3
{
    internal class MoveResult
    {
        public string Name { get; set; }

        public uint MoveIndex { get; }

        public string Move { get; }

        public byte[] HmacKey { get; }

        public HMAC Hmac { get; }

        public MoveResult(string name, string[] move, uint choice)
        {
            Name = name;
            MoveIndex = choice;
            Move = move[MoveIndex];
            HmacKey = GetRandomKey();
            Hmac = HMAC.Create("HMACSHA256");
            Hmac.Key = HmacKey;
        }

        public string GetHMAC()
            => BitConverter.ToString(Hmac.ComputeHash(Encoding.UTF8.GetBytes(Move)))
            .Replace("-", "").ToLower();

        public string GetHMACKey()
            => BitConverter.ToString(HmacKey)
            .Replace("-", "");

        private static byte[] GetRandomKey()
        {
            var bytes = new byte[64];
            RandomNumberGenerator.Create()
                .GetBytes(bytes);

            return bytes;
        }
    }
}