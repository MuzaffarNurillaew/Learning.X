namespace AsynchronousProgramming;

public static class IneffectiveUseOfAsynchronousProgramming
{
    public static async Task StartAsync()
    {
        Coffee cup = PourCoffee();
        Console.WriteLine("1. coffee is ready");
        
        Task<Egg> eggsTask = FryEggsAsync(2);
        Console.WriteLine("2. eggs are ready");
        
        Task<Bacon> baconTask = FryBaconAsync(3);
        Console.WriteLine("3. bacon is ready");
        
        Task<Toast> toastTask = ToastBreadAsync(2);
        Toast toast = await toastTask;
        ApplyButter(toast);
        ApplyJam(toast);
        Console.WriteLine("4. toast is ready");
        
        Juice oj = PourJuice();
        Console.WriteLine("5. oj is ready");
        Console.WriteLine("BREAKFAST IS READY!");
    }

    private static Juice PourJuice()
    {
        Console.WriteLine("Pouring juice...");
        return new ();
    }
    private static void ApplyJam(Toast toast)
    {
        Console.WriteLine("Applying jam on the toast...");
    }
    private static void ApplyButter(Toast toast)
    {
        Console.WriteLine("Applying butter on the toast...");
    }
    private static async Task<Toast> ToastBreadAsync(int slices)
    {
        for (int slice = 0; slice < slices; slice++)
        {
            Console.WriteLine("Putting a slice of bread in the toaster");
        }

        Console.WriteLine("Start toasting...");
        await Task.Delay(3_000);
        Console.WriteLine("Remove toast from toaster");

        return new Toast();
    }
    private static async Task<Bacon> FryBaconAsync(int slices)
    {
        Console.WriteLine($"putting {slices} slices of bacon in the pan");
        Console.WriteLine("cooking first side of bacon...");
        Task.Delay(3000).Wait();
        for (int slice = 0; slice < slices; slice++)
        {
            Console.WriteLine("flipping a slice of bacon");
        }
        Console.WriteLine("cooking the second side of bacon...");
        await Task.Delay(3000);
        Console.WriteLine("Put bacon on plate");

        return new Bacon();
    }
    private static async Task<Egg> FryEggsAsync(int howMany)
    {
        Console.WriteLine("Warming the egg pan...");
        Task.Delay(3000).Wait();
        Console.WriteLine($"cracking {howMany} eggs");
        Console.WriteLine("cooking the eggs ...");
        await Task.Delay(3000);
        Console.WriteLine("Put eggs on plate");
        return new Egg();
    }
    private static Coffee PourCoffee()
    {
        Console.WriteLine("Pouring coffee");
        return new Coffee();
    }
}