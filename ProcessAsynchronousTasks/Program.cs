using System.Diagnostics;

class Program
{
    static async Task Main()
    {
        await SumPagesAsync();
        await SumPagesInsufficientAsync();
    }

    private static async Task WriteAsync(string content, int delay = 0)
    {
        Console.WriteLine(content);
        await Task.Delay(delay);
    }

    private static async Task SumPagesInsufficientAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        int total = 0;
        foreach (var url in SUrlList)
        {
            total += await ProcessUrlAsync(url, SClient);
        }
        
        stopwatch.Stop();
        Console.WriteLine($"Total bytes: {total:##,###}");
        Console.WriteLine($"Elapsed time: {stopwatch.Elapsed:g}");
    }
    private static async Task SumPagesAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        IEnumerable<Task<int>> downloadTasksQuery =
            from url in SUrlList
            select ProcessUrlAsync(url, SClient);
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
        Console.WriteLine($"Total bytes: {total:#,#}");
        Console.WriteLine($"Elapsed time: {stopwatch.Elapsed:g}");
    }

    private static async Task<int> ProcessUrlAsync(string url, HttpClient client)
    {
        var response = await client.GetAsync(url);
        var repsonseStream = await response.Content.ReadAsStreamAsync();
        int len = (int)repsonseStream.Length;
        Console.WriteLine($"Size: {url,-60} {len,10:##,###}");

        return len;
    }

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