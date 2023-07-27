public class RegisterMiddleware{
    private readonly RequestDelegate _next;

    public RegisterMiddleware(RequestDelegate next){
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context){
        context.Response.ContentType = "text/html; charset=utf8";
        
        if(context.Request.Path == "/registerUser" && context.Request.Method == "POST"){
        var form = context.Request.Form;

        string login = form["loginRegister"];
        string password = form["passwordRegister"];
        string confirmPassword = form["passwordConfirm"];

        var dbContext = context.RequestServices.GetService<MyDbContext>();
        var userService = new UserService(dbContext);

        try{
            if(password != confirmPassword){
                await context.Response.WriteAsync($"<h2>Пароли не совпадают</h2>");
            }
            userService.Register(login, password);
            context.Response.Cookies.Append("username", login);
            context.Response.Redirect("/profile");
            
        } catch(ArgumentException ex){
            await context.Response.WriteAsync($"<h2>Ошибка: {ex.Message}</h2>");
        }
    }
    else{
        await _next(context);
    }
    }
}