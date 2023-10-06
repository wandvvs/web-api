using System.Diagnostics.CodeAnalysis;

public class PostService{
    private readonly MyDbContext _dbContext;

    public PostService(MyDbContext dbContext){
        _dbContext = dbContext;
    }

    public Post Create(string title, string text, string authorLogin){
        if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(text)){
            throw new ArgumentException("Заполните все поля формы");
        }
            
        if(title.Length < 5 || text.Length < 5){
            throw new ArgumentException("Заголовок или текст не может быть меньше пяти символов");
        }
            

        var post = new Post {Title = title, Text = text, AuthorLogin = authorLogin};
        _dbContext.Add(post);
        _dbContext.SaveChanges();
        return post;
    }
    public List<Post> GetAllPosts()
    {
        var posts = _dbContext.posts.OrderByDescending(x=> x.Id).ToList();

        return posts;
    }
}
