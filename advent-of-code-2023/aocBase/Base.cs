using System.Diagnostics;

namespace advent_of_code_2023.aocBase
{
    public abstract class Base
    {
        protected Stopwatch Timer = Stopwatch.StartNew();

        protected abstract void Start(bool isTestInput = false);

        protected string[] ReadAllLines(bool isTestInput = false)
        {
            return File.ReadAllLines($"{this.GetType().Name}\\{(isTestInput ? "input_test.txt" : "input.txt")}");
        }

        public void Run(bool isTestInput = false)
        {
            Timer.Start();
            Start(isTestInput);
            Timer.Stop();

            Console.WriteLine($"Method Execution time: {Timer.ElapsedMilliseconds} {Timer.Elapsed}");
        }
    }
}
