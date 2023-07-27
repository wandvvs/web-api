public class DeleteAccountMiddleware{
    private readonly RequestDelegate _next;
    public DeleteAccountMiddleware(RequestDelegate next){
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context){
        context.Response.ContentType = "text/html; charset=utf8";
        
        if(context.Request.Path == "/deleteAcc" && context.Request.Method == "GET"){
            var dbContext = context.RequestServices.GetService<MyDbContext>();
            var userService = new UserService(dbContext);
            string accountLogin = context.Request.Cookies["username"];

            userService.Delete(accountLogin);
            context.Response.Cookies.Delete("username");
            context.Response.Redirect("/main");
        }
        else{
            await _next(context);
        }
    }
}