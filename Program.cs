using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MyDbContext>(options => options.UseNpgsql("Server=localhost;Port=5432;Database=webapi;User Id=postgres;Password=ranerran;"));
var app = builder.Build();

app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<RegisterMiddleware>();
app.UseMiddleware<LoginMiddleware>();
app.UseMiddleware<LogoutMiddleware>();
app.UseMiddleware<GellAllUsersMiddleware>();
app.UseMiddleware<GetAllDataMiddleware>();
app.UseMiddleware<DeleteAccountMiddleware>();
app.UseMiddleware<ChangeNameMiddleware>();
app.UseMiddleware<CreatePostMiddleware>();
app.UseMiddleware<StaticFileMiddleware>();


app.Map("/main", appBuilder => {
    appBuilder.Run(async context => {
        var response = context.Response;

        response.ContentType = "text/html; charset=utf8";

        var usernameCookie = context.Request.Cookies["username"];
        if(!string.IsNullOrEmpty(usernameCookie)){
            response.Redirect("/profile");
            return;
        }

        await response.SendFileAsync("resources/html/main.html");
    });
});

app.Run(async (context) =>{
    context.Response.Redirect("/main");
});

app.Run();
