namespace EFGetStarted.Entities;

public class Blog
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public List<Post> Posts { get; set; } = new();
}