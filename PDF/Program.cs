static class Program
{
    static async Task Main()
    {
        string filePath = "../../../test.pdf";
        string resultFilePath = "../../../result.pdf";
        var stream = new FileStream(path: filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        var resultArray = new byte[stream.Length];
        int length = await stream.ReadAsync(resultArray);
        stream.Close();
        FileStream resultStream = new FileStream(resultFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
            FileShare.ReadWrite);
        resultArray[0] = (byte)'M';
        await resultStream.WriteAsync(resultArray);
        resultStream.Close();
        Console.WriteLine(length);
        for (int i = 0; resultArray[i] != '\n'; i++)
        {
            if (resultArray[i + 1] == '\n')
            {
                Console.Write("\n\n--------------------press enter to continue: \n\n");
                Console.ReadKey();
                i++;
            }

            Console.Write($"{resultArray[i]} ");
        }
    }
}