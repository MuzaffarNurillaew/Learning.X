using System.Diagnostics;

namespace CancellingTasks;

static class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Application started...");
        await Task.Delay(100);
        Console.WriteLine("Press ENTER to cancel...");
        
        // "cancelling task" task
        Task cancelTask = Task.Run(() =>
        {
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine("Press ENTER to cancel...");
            }

            Console.WriteLine("Enter pressed, cancelling all tasks...");
            STokenSource.Cancel();
        });
        var sumPagesTask = SumPagesAsync();

        var tasks = new[] { cancelTask };
        var finishedTask = await Task.WhenAny(tasks);
        if (finishedTask == cancelTask)
        {
            try
            {
                await sumPagesTask;
                Console.WriteLine("Downloaded successfully...");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Download task has been cancelled...");
            }
        }

        Console.WriteLine("App finished... ");
     }
    
    
    private static async Task SumPagesAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        IEnumerable<Task<int>> downloadTasksQuery =
            from url in SUrlList
            select ProcessUrlAsync(url, SClient, STokenSource.Token);
        List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

        int total = 0;
        while (downloadTasks.Any())
        {
            Task<int> finishedTask = await Task.WhenAny(downloadTasks);
            downloadTasks.Remove(finishedTask);
            int result = await finishedTask;
            total += result;
        }
        stopwatch.Stop();
        await Task.Delay(5_000); 
        Console.WriteLine($"Total bytes: {total:#,#}");
        Console.WriteLine($"Elapsed time: {stopwatch.Elapsed:g}");
        await Task.Delay(5_000); 
    }

    private static async Task<int> ProcessUrlAsync(string url, HttpClient client, CancellationToken token)
    {
        var response = await client.GetAsync(url, token);
        var repsonseStream = await response.Content.ReadAsStreamAsync(token);
        int len = (int)repsonseStream.Length;
        Console.WriteLine($"Size: {url,-60} {len,10:##,###}");

        return len;
    }
    
    private static CancellationTokenSource STokenSource = new();
    static readonly HttpClient SClient = new HttpClient
    {
        MaxResponseContentBufferSize = 1_000_000
    };
    static readonly IEnumerable<string> SUrlList = new string[]
    {
        "https://learn.microsoft.com",
        "https://learn.microsoft.com/aspnet/core",
        "https://learn.microsoft.com/azure",
        "https://learn.microsoft.com/azure/devops",
        "https://learn.microsoft.com/dotnet",
        "https://learn.microsoft.com/dynamics365",
        "https://learn.microsoft.com/education",
        "https://learn.microsoft.com/enterprise-mobility-security",
        "https://learn.microsoft.com/gaming",
        "https://learn.microsoft.com/graph",
        "https://learn.microsoft.com/microsoft-365",
        "https://learn.microsoft.com/office",
        "https://learn.microsoft.com/powershell",
        "https://learn.microsoft.com/sql",
        "https://learn.microsoft.com/surface",
        "https://learn.microsoft.com/system-center",
        "https://learn.microsoft.com/visualstudio",
        "https://learn.microsoft.com/windows",
        "https://learn.microsoft.com/xamarin"
    };
}