using System;
using System.Collections.Generic;
using System.Data.HashFunction;
using System.Data.HashFunction.xxHash;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using Murmur;
using MurmurHash.Net;
using SauceControl.Blake2Fast;

namespace MurmurHash.Benchmarks
{
    [MemoryDiagnoser]
    public class BenchStringToInt
    {
        private readonly List<string> _strings = new List<string>();
        private int _itemsCount = 500;
        private IxxHash _xxHash = xxHashFactory.Instance.Create();
        private Murmur32 murMurDarrenkopp = Murmur.MurmurHash.Create32(seed: 293U, managed: false);
        private int maxCharsCount;

        public BenchStringToInt()
        {
            for (var i = 0; i < _itemsCount; i++)
            {
                var item = Guid.NewGuid().ToString();
                _strings.Add(item);
            }
            _strings.Shuffle();
            maxCharsCount = _strings.Max(x => x.Length);
        }
        
        [Benchmark]
        public long CRC32()
        {
            var result = 0L;
            unchecked
            {
                foreach (var str in _strings)
                    result += Crc32.Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(str));
            }

            return result;
        }
        
        [Benchmark]
        public uint XxHash()
        {
            uint result = 0;
            foreach (var str in _strings)
            {
                var hash = _xxHash.ComputeHash(str).Hash;
                result += BitConverter.ToUInt32(hash);
            }

            return result;
        }

        [Benchmark]
        public long MurMurHashByDarrenkopp()
        {
            long result = 0;
            unchecked
            {
                foreach (var str in _strings)
                {
                    var hash = murMurDarrenkopp.ComputeHash(Encoding.UTF8.GetBytes(str));
                    result += BitConverter.ToUInt32(hash);
                }
            }

            return result;
        }
        
        [Benchmark]
        public int Blake2B()
        {
            var result = 0;
            unchecked
            {
                
                foreach (var str in _strings)
                {
                    var computeHash = Blake2b.ComputeHash(4, Encoding.UTF8.GetBytes(str));
                    result += BitConverter.ToInt32(computeHash, 0);
                }
            }

            return result;
        }
        
        [Benchmark(Baseline = true)]
        public long MurMurHashNet()
        {
            long result = 0;
            unchecked
            {
                Span<byte> inputBuffer = stackalloc byte[maxCharsCount * 4];
                foreach (var str in _strings)
                {
                    var count = Encoding.UTF8.GetBytes(str.AsSpan(), inputBuffer);
                    result += MurmurHash3.Hash32(inputBuffer.Slice(0, count), 293U);
                }
            }

            return result;
        }
    }
}