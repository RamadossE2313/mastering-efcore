// See https://aka.ms/new-console-template for more information

Console.WriteLine("1.Starting");

static async Task ParallelWithMaxDegree()
{
  await Task.Run(() =>  Parallel.ForEach(Enumerable.Range(0, 10), new ParallelOptions { MaxDegreeOfParallelism = 2 }, item =>
    {
        Thread.Sleep(10000);
        Console.WriteLine($"2. ---{item}");
    }));

    //await Parallel.ForEachAsync(Enumerable.Range(1, 10), (i, cts) => {  });
}

ParallelExecution();
Console.WriteLine("4. exiting method");
Console.ReadLine();
void ParallelExecution()
{
    //ParallelWithMaxDegree();

    Parallel.ForEach(Enumerable.Range(0, 10),  item =>
    {
        Thread.Sleep(10000);
        Console.WriteLine($"2. ---{item}");
    });

    Console.WriteLine("3. exiting parallel method");
}

Console.WriteLine("Hello, World!");
