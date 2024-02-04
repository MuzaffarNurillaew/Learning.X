namespace AsynchronousProgramming;
internal class Bacon { }
internal class Coffee { }
internal class Egg { }
internal class Juice { }
internal class Toast { }

public static class Program
{
    public static async Task Main()
    {
        await foreach (int number in GetYearsAsync(4))
        {
            Console.WriteLine(number);
        }
    }

    public static async IAsyncEnumerable<int> GetYearsAsync(int count)
    {
        var number = new Random();
        for (int i = 0; i < count; i++)
        {
            yield return number.Next(1, 100);
            await Task.Delay(500);
        }
    }
    public static async Task CompareAllPlayGround()
    {
        // await BenchMark();
        
        // ineffective use of asynchronous functions
        // because this code will be executed like synchronous
        // await Do1Async(ConsoleColor.Red);
        // await Do2Async(ConsoleColor.Green);

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("2. -------------------------");
        // Performant way of using asynchronous functions
        var do1Task = Do1Async(ConsoleColor.Red);
        var do2Task = Do2Async(ConsoleColor.Green);
        await do1Task;
        await do2Task;
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-------------------------");
        // Performant way of using asynchronous functions
        var do1Task1 = Do1Async(ConsoleColor.Red);
        var do2Task1 = Do2Async(ConsoleColor.Green);
        await Task.WhenAll(do1Task1, do2Task1);
    }

    private static async Task BenchMark()
    {
        // var startSync = DateTime.UtcNow;
        // Synchronous.Start();
        // var endSync = DateTime.UtcNow;
        //
        // var startIA = DateTime.UtcNow;
        // await IneffectiveUseOfAsynchronousProgramming.StartAsync();
        // var endIA = DateTime.UtcNow;
        
        var startA = DateTime.UtcNow;
        await EffectiveAsynchronous.StartAsync();
        var endA = DateTime.UtcNow;

        // Console.WriteLine($"Synchronous: {(endSync - startSync).TotalMilliseconds}");
        // Console.WriteLine($"Ineffective asynchronous: {(endIA - startIA).TotalMilliseconds}");
        Console.WriteLine($"Effective asynchronous: {(endA - startA).TotalMilliseconds}");
    }
    private static async Task Do1Async(ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine("Do1: [action 1]");
        await Task.Delay(1_000);
        Console.ForegroundColor = color;
        Console.WriteLine("Do1: [action 2]");
        Console.ForegroundColor = color;
        Console.WriteLine("Do1: [action 3]");
        await Task.Delay(3_000);
        Console.ForegroundColor = color;
        Console.WriteLine("Do1: [action 4]");
        
    }
    private static async Task Do2Async(ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine("Do2: [action 1]");
        Console.ForegroundColor = color;
        Console.WriteLine("Do2: [action 2]");
        await Task.Delay(2_000);
        Console.ForegroundColor = color;
        Console.WriteLine("Do2: [action 3]");
    }
}