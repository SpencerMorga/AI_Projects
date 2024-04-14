using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;

namespace BenchMarkDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            BenchmarkRunner.Run<LoopBenchmark>();

            Console.ReadKey();
            while (true) ;

        }
    }

    [MemoryDiagnoser]
    public class LoopBenchmark
    {
        IEnumerable<int> listy;
        List<int> list = new();


        [GlobalSetup]
        public void Setup()
        {
            listy = list = new List<int>(100);
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = i;
            }
        }
        [Benchmark]
        public void ForExample()
        {
            int sum = 0;
            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }
            Console.WriteLine(sum);
        }

        [Benchmark]
        public void ForEachExample()
        {
            int sum = 0;
            foreach (var item in listy)
            {
                sum += item;
            }
            Console.WriteLine(sum);
        }

    }
}
