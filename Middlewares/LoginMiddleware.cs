public class LoginMiddleware{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next){
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context){
        context.Response.ContentType = "text/html; charset=utf8";
        
        if(context.Request.Path == "/Ilogin"){
        var form = context.Request.Form;

        string login = form["nameLogin"];
        string password = form["passwordLogin"];

        var dbContext = context.RequestServices.GetService<MyDbContext>();
        var userService = new UserService(dbContext);

        try{
            userService.Login(login, password);
            context.Response.Cookies.Append("username", login);
            context.Response.Redirect("/profile");
        } catch(ArgumentException ex){
            context.Response.WriteAsync($"Ошибка: {ex.Message}");
        }
    }
    else{
        await _next(context);
    }
    }
}
