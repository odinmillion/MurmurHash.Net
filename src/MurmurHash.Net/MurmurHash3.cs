using System;

namespace MurmurHash.Net
{
    public class MurmurHash3
    {
        public static uint Hash(Span<byte> bytes, uint seed)
        {
            uint num1 = seed;
            int start;
            for (start = 0; start < bytes.Length - 4; start += 4)
            {
                Span<byte> span2 = bytes.Slice(start, 4);
                uint num2 =
                    Rotl32((uint) ((int) span2[0] | (int) span2[1] << 8 | (int) span2[2] << 16 | (int) span2[3] << 24) * 3432918353U,
                        (byte) 15) * 461845907U;
                num1 = (uint) ((int) Rotl32(num1 ^ num2, (byte) 13) * 5 - 430675100);
            }

            if (start < bytes.Length)
            {
                Span<byte> span2 = bytes.Slice(start, bytes.Length - start);
                switch (span2.Length)
                {
                    case 1:
                        uint num2 = Rotl32((uint) span2[0] * 3432918353U, (byte) 15) * 461845907U;
                        num1 ^= num2;
                        break;
                    case 2:
                        uint num3 = Rotl32(((uint) span2[0] | (uint) span2[1] << 8) * 3432918353U, (byte) 15) * 461845907U;
                        num1 ^= num3;
                        break;
                    case 3:
                        uint num4 = Rotl32((uint) ((int) span2[0] | (int) span2[1] << 8 | (int) span2[2] << 16) * 3432918353U, (byte) 15) *
                                    461845907U;
                        num1 ^= num4;
                        break;
                }
            }

            uint h = num1 ^ (uint) bytes.Length;
            h ^= h >> 16;
            h *= 2246822507U;
            h ^= h >> 13;
            h *= 3266489909U;
            h ^= h >> 16;
            return h;
        }

        private static uint Rotl32(uint x, byte r)
        {
            return x << (int) r | x >> 32 - (int) r;
        }
    }
}