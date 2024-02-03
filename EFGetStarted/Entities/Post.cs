namespace EFGetStarted.Entities;

public class Post
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    
    public int BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
}