public class ChangeNameMiddleware{
    private readonly RequestDelegate _next;
    public ChangeNameMiddleware(RequestDelegate next){
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context){
        if (context.Request.Path == "/changeName" && context.Request.Method == "GET") {
            context.Response.ContentType = "text/html; charset=utf8";
            
            var dbContext = context.RequestServices.GetService<MyDbContext>();
            var userService = new UserService(dbContext);
            string accountLogin = context.Request.Cookies["username"];
            string newLogin = context.Request.Query["newNameInput"];

            try {
                userService.NameChange(accountLogin, newLogin);
                context.Response.Cookies.Delete("username");
                context.Response.Cookies.Append("username", newLogin);
                context.Response.Redirect("/main");
            } catch (ArgumentException ex) {
                Console.WriteLine($"Ошибка: {ex.Message}");

        context.Response.StatusCode = 400;
        await context.Response.WriteAsync($"Ошибка: {ex.Message}");
            }
        }
        else{
            await _next(context);
        }
    }
}