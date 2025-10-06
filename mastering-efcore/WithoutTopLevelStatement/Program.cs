namespace WithoutTopLevelStatement
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("1. Hello, World!");
            await Delay();
            Console.WriteLine("4.done");
            Console.ReadKey();
        }

        private static async Task Delay()
        {
            Console.WriteLine("2. enter");
            await Task.Delay(10000);
            Console.WriteLine("3. exit");

        }
    }
}
