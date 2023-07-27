using System.Diagnostics.CodeAnalysis;
public class User{
    public int Id {get;set;}
    [NotNull]
    public string Login { get; set; } = string.Empty;
    [NotNull]
    public string Password { get; set; } = string.Empty;
    public bool isAdmin {get;set;} = false;
}