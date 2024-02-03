namespace AsynchronousProgramming;

public static class EffectiveAsynchronous
{
    private static int step = 0;
    public static async Task StartAsync()
    {
        Coffee cup = PourCoffee();
        Console.WriteLine($"{step++}. coffee is ready");
        
        Task<Egg> eggsTask = FryEggsAsync(2);
        Task<Bacon> baconTask = FryBaconAsync(3);
        Task<Toast> toastTask = MakeToastWithButterAndJamAsync(2);
        
        Toast toast = await toastTask;
        Console.WriteLine($"{step++}. toast is ready");
        
        Juice oj = PourJuice();
        Console.WriteLine($"{step++}. oj is ready");

        Egg eggs = await eggsTask;
        Console.WriteLine($"{step++}. eggs are ready");

        Bacon bacon = await baconTask;
        Console.WriteLine($"{step++}. bacon is ready");
        
        Console.WriteLine("BREAKFAST IS READY!");
    }

    private static Juice PourJuice()
    {
        Console.WriteLine("Pouring juice...");
        return new ();
    }
    private static void ApplyJam(Toast toast)
    {
        Console.WriteLine($"{step++}. Applying jam on the toast...");
    }
    private static void ApplyButter(Toast toast)
    {
        Console.WriteLine($"{step++}. Applying butter on the toast...");
    }
    private static async Task<Toast> ToastBreadAsync(int slices)
    {
        for (int slice = 0; slice < slices; slice++)
        {
            Console.WriteLine($"{step++}. Putting a slice of bread in the toaster");
        }

        Console.WriteLine($"{step++}. Start toasting...");
        await Task.Delay(3_000);
        Console.WriteLine($"{step++}. Remove toast from toaster");

        return new Toast();
    }
    private static async Task<Bacon> FryBaconAsync(int slices)
    {
        Console.WriteLine($"{step++}. putting {slices} slices of bacon in the pan");
        Console.WriteLine($"{step++}. cooking first side of bacon...");
        Task.Delay(3000).Wait();
        for (int slice = 0; slice < slices; slice++)
        {
            Console.WriteLine($"{step++}. flipping a slice of bacon");
        }
        Console.WriteLine($"{step++}. cooking the second side of bacon...");
        await Task.Delay(3000);
        Console.WriteLine("Put bacon on plate");

        return new Bacon();
    }
    private static async Task<Egg> FryEggsAsync(int howMany)
    {
        Console.WriteLine($"{step++}. Warming the egg pan...");
        Task.Delay(3000).Wait();
        Console.WriteLine($"{step++}. cracking {howMany} eggs");
        Console.WriteLine($"{step++}. cooking the eggs ...");
        await Task.Delay(3000);
        Console.WriteLine($"{step++}. Put eggs on plate");
        return new Egg();
    }
    private static Coffee PourCoffee()
    {
        Console.WriteLine($"{step++}. Pouring coffee");
        return new Coffee();
    }
    private static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
    {
        var toast = await ToastBreadAsync(number);
        ApplyButter(toast);
        ApplyJam(toast);
        return toast;
    }
}