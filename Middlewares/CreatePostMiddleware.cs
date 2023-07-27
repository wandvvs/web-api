public class CreatePostMiddleware{
    public readonly RequestDelegate _next;
    public CreatePostMiddleware(RequestDelegate next){
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context){
        context.Response.ContentType = "text/html; charset=utf8";
        
        if(context.Request.Path == "/createPost" && context.Request.Method == "POST"){
            var dbContext = context.RequestServices.GetService<MyDbContext>();
            var postService = new PostService(dbContext);
            var userService = new UserService(dbContext);

            string title = context.Request.Form["title"];
            string text = context.Request.Form["mainText"];
            string username = context.Request.Cookies["username"];

            try{
                postService.Create(title, text, username);
                context.Response.Redirect("/forum");
            }
            catch(ArgumentException ex) {
            await context.Response.WriteAsync($"Ошибка: {ex.Message}");
            }
        }
        else{
            await _next(context);
        }
    }
}