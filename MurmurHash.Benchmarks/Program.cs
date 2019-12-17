using BenchmarkDotNet.Running;

namespace MurmurHash.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchStringToInt>();
            BenchmarkRunner.Run<BenchBytesToInt>();
        }
    }
}