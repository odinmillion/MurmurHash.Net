using FluentAssertions;
using Xunit;

namespace MurmurHash.Net.Tests
{
    public class MurmurHashTests
    {
        [Theory]
        [InlineData(new byte[] {1, 2, 3}, 293U, 1971362553U)]
        [InlineData(new byte[] {1, 2, 3, 4}, 293U, 2911303516U)]
        [InlineData(new byte[] {1, 2, 3, 4, 5}, 293U, 3144920404U)]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, 293U, 3796703664U)]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6, 7}, 293U, 3433363787U)]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6, 7, 8}, 293U, 2343089733U)]
        [InlineData(new byte[0], 0U, 0U)]
        [InlineData(new byte[0], 1U, 0x514E28B7U)]
        [InlineData(new byte[0], 0xffffffffU, 0x81F16F39U)]
        [InlineData(new byte[] {0xFF, 0xFF, 0xFF, 0xFF}, 0U, 0x76293B50U)]
        [InlineData(new byte[] {0x21, 0x43, 0x65, 0x87}, 0U, 0xF55B516BU)]
        [InlineData(new byte[] {0x21, 0x43, 0x65, 0x87}, 0x5082EDEEU, 0x2362F9DEU)]
        [InlineData(new byte[] {0x21, 0x43, 0x65}, 0U, 0x7E4A8634U)]
        [InlineData(new byte[] {0x21, 0x43}, 0U, 0xA0F7B07AU)]
        [InlineData(new byte[] {0x21}, 0U, 0x72661CF4U)]
        [InlineData(new byte[] {0x0, 0x0, 0x0, 0x0}, 0U, 0x2362F9DEU)]
        [InlineData(new byte[] {0x0, 0x0, 0x0}, 0U, 0x85F0B427U)]
        [InlineData(new byte[] {0x0, 0x0}, 0U, 0x30F4C306U)]
        [InlineData(new byte[] {0x0}, 0U, 0x514E28B7U)]
        public void SmokeTest(byte[] bytes, uint seed, uint expectedHash)
        {
            MurmurHash3.Hash32(bytes, seed).Should().Be(expectedHash);
        }
    }
}