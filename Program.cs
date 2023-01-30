class MyClass
{
    static async Task Main(string[] args)
    {
        await using var producer=new Producer();
        //var numbers=await producer.GetNumbersAsync();
        //foreach (var num in numbers)
        //{
        //    Console.WriteLine(num);
        //}

        await foreach(var num in producer.EnumerateNumbersAsync())
        {
            Console.WriteLine(num);
        }
    }
}

public class Producer : IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        await Task.Delay(2000);
        Console.WriteLine("methods disposed!!!");
    }

    public async Task<IEnumerable<int>> GetNumbersAsync()
    {
        List<int> numbers = new List<int>();
        for(int i=0; i<10; i++)
        {
            Console.WriteLine("Get some numbers");
            for(int j=0; j<10; j++)
            {
                numbers.Add(i*10+j);
            }
            Console.WriteLine("Making next request....");
            await Task.Delay(1500);
        }
        return numbers;
    }

    internal async IAsyncEnumerable<int> EnumerateNumbersAsync()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Get some numbers");
            for (int j = 0; j < 10; j++)
            {
                yield return i*10+j;
            }
            Console.WriteLine("Making next request....");
            await Task.Delay(1500);
        }
    }
}