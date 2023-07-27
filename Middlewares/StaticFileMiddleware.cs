public class StaticFileMiddleware{
    private readonly RequestDelegate _next;
    public StaticFileMiddleware(RequestDelegate next){
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context){
        context.Response.ContentType = "text/html; charset=utf8";
        
        if(context.Request.Path.StartsWithSegments("/register")){
            await context.Response.SendFileAsync("resources/html/register.html");
        }
        else if(context.Request.Path.StartsWithSegments("/useroption")){
            await context.Response.SendFileAsync("resources/html/useroption.html");
        }
        else if(context.Request.Path.StartsWithSegments("/profile")){
            await context.Response.SendFileAsync("resources/html/profile.html");
        }
        else if(context.Request.Path.StartsWithSegments("/forum")){
            await context.Response.SendFileAsync("resources/html/forum.html");
        }
        else if (context.Request.Path.StartsWithSegments("/allusers"))
        {
            if (context.Request.Method == "GET")
            {
                await context.Response.SendFileAsync("resources/html/allusers.html");
            }
            else if (context.Request.Method == "POST")
            {
                await RedirectToAllUsers(context);
            }
        }
        else{
            await _next(context);
        }
    }
    private async Task RedirectToAllUsers(HttpContext context){
        context.Response.Redirect("/allusers");
        await Task.CompletedTask;
    }
}