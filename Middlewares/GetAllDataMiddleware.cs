public class GetAllDataMiddleware{
    private readonly RequestDelegate _next;
    public GetAllDataMiddleware(RequestDelegate next){
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context){
        context.Response.ContentType = "text/html; charset=utf8";
        
        if(context.Request.Path == "/getallposts" && context.Request.Method == "POST"){
            var dbContext = context.RequestServices.GetService<MyDbContext>();
            var postService = new PostService(dbContext);

            var posts = postService.GetAllPosts();
            await context.Response.WriteAsJsonAsync(posts);
        }
        else{
            await _next(context);
        }
    }
}