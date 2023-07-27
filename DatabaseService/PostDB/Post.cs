using System.Diagnostics.CodeAnalysis;
public class Post{
    public int Id {get;set;}
    [NotNull]
    public string Title { get; set; } = string.Empty;
    [NotNull]
    public string Text { get; set; } = string.Empty;
    [NotNull]
    public string AuthorLogin{get;set;} = string.Empty;
}