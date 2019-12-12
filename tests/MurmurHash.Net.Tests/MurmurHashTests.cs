using System.Linq;
using FluentAssertions;
using Xunit;

namespace MurmurHash.Net.Tests
{
    public class MurmurHashTests
    {
        [Fact]
        public void SmokeTest()
        {
            MurmurHash3.Hash(new byte[] {1, 2, 3}, 293U).Should().Be(1971362553u);
        }
    }
}