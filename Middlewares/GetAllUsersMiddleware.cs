public class GellAllUsersMiddleware{
    private readonly RequestDelegate _next;
    public GellAllUsersMiddleware(RequestDelegate next){
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context){
        context.Response.ContentType = "text/html; charset=utf8";
        
        if(context.Request.Path == "/getallusers" && context.Request.Method == "GET"){
            var dbContext = context.RequestServices.GetService<MyDbContext>();
            var userService = new UserService(dbContext);

            var users = userService.GetAllUsers();
            await context.Response.WriteAsJsonAsync(users);
        }
        else{
            await _next(context);
        }
    }
}