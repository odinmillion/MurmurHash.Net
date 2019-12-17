using System;
using System.Collections.Generic;
using System.Data.HashFunction.xxHash;
using System.Text;
using BenchmarkDotNet.Attributes;
using Murmur;
using MurmurHash.Net;
using SauceControl.Blake2Fast;

namespace MurmurHash.Benchmarks
{
    [MemoryDiagnoser]
    public class BenchBytesToInt
    {
        private readonly List<byte[]> _byteArrays = new List<byte[]>();
        private int _itemsCount = 500;
        private IxxHash _xxHash = xxHashFactory.Instance.Create();
        private Murmur32 murMurDarrenkopp = Murmur.MurmurHash.Create32(seed: 293U, managed: false);

        public BenchBytesToInt()
        {
            for (var i = 0; i < _itemsCount; i++)
            {
                var item = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
                _byteArrays.Add(item);
            }
            _byteArrays.Shuffle();
        }
        
        [Benchmark]
        public long CRC32()
        {
            var result = 0L;
            unchecked
            {
                foreach (var bytes in _byteArrays)
                    result += Crc32.Crc32Algorithm.Compute(bytes);
            }

            return result;
        }
        
        [Benchmark]
        public uint XxHash()
        {
            uint result = 0;
            foreach (var bytes in _byteArrays)
            {
                var hash = _xxHash.ComputeHash(bytes).Hash;
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
                foreach (var bytes in _byteArrays)
                {
                    var hash = murMurDarrenkopp.ComputeHash(bytes);
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
                
                foreach (var bytes in _byteArrays)
                {
                    var computeHash = Blake2b.ComputeHash(4, bytes);
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
                foreach (var bytes in _byteArrays)
                {
                    result += MurmurHash3.Hash32(bytes, 293U);
                }
            }

            return result;
        }
    }
}