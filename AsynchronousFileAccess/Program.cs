using System.Diagnostics;
using System.Text;

internal class Program
{
    public static async Task Main(string[] args)
    {
        long length = 1024 * 500_000;
        var sbText = new StringBuilder("Muzaffar");
        while (sbText.Length < length)
        {
            sbText.Append(sbText);
        }

        string text = sbText.ToString();

        var tasks = new List<Task>()
        {
            SufficientWriteAsync("sufficient.text", text),
            InefficientWriteAsync("inefficient.txt", text),
        };

        await Task.WhenAll(tasks);
    }

    static async Task InefficientWriteAsync(string filePath, string text)
    {
        await File.WriteAllTextAsync(filePath, text);
        Console.WriteLine("inefficient finished.");
    }

    static async Task SufficientWriteAsync(string filePath, string text)
    {
        // converting string to byte array (using encoding)
        byte[] encodedText = Encoding.Unicode.GetBytes(text);
        
        // creating a new FileStream
        using var fileStream =
            new FileStream(
                path: filePath,
                mode: FileMode.Create,
                access: FileAccess.Write,
                share: FileShare.None,
                bufferSize: 4096,
                useAsync: true);
        
        // writing the text inside the stream
        await fileStream.WriteAsync(encodedText, 0, encodedText.Length);
        Console.WriteLine("efficient finished.");
    }
}
