public class AuthenticationMiddleware{
    private readonly RequestDelegate _next;
    public AuthenticationMiddleware(RequestDelegate next){
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context){
        var usernameCookie = context.Request.Cookies["username"];
        bool isAuthenticated = !string.IsNullOrEmpty(usernameCookie);

        switch (context.Request.Path)
        {
            case "/forum":
            case "/profile":
            case "/getusers":
            case "/getallusers":
            case "/allusers":
            case "/useroption":
                if (!isAuthenticated)
                {
                    context.Response.Redirect("/main");
                    return;
                }
                break;
        }

        await _next.Invoke(context);
    }

}