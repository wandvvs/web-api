public class LogoutMiddleware{
    private readonly RequestDelegate _next;
    public LogoutMiddleware(RequestDelegate next){
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context){
        context.Response.ContentType = "text/html; charset=utf8";
        
        if(context.Request.Path == "/logout" && context.Request.Method == "POST"){
            context.Response.Cookies.Delete("username");
            context.Response.Redirect("/main");
        }
        else{
            await _next(context);
        }
    }
}