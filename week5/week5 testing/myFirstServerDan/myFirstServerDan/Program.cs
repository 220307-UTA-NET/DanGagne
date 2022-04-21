using myFirstServerDan.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//WebApplication is the thing we're builidng, all packaged up into a single object

//Before we "build" we can set up things call Middleware
//These are all things that determine the functionality and behavior of the web application.

var app = builder.Build();

app.UseMiddleware<RequireAuthorization>();

//Moved to middleware folder
//app.Use(async (context, next) =>
//{
//    if (context.Request.Query["authorization"] == "true")
//    {
//        //authorization is good, continue on
//        await next(context);
//    }
//    else
//    {
//        //if we don't have authorization...then what
//        context.Response.StatusCode = 401;
//        context.Response.ContentType = "text/plain";
//        await context.Response.WriteAsync("error! not authorized");
//    }
//});



app.UseRouting();
app.UseEndpoints(routeBuilder =>
{
    routeBuilder.MapControllers();
});

//after we have a WebApplication (the app) we need to construct its request processing pipelin, using the middleware


app.MapGet("/", () => "Hello World!");

app.MapGet("/map1", () => "Hi there!");

app.MapGet("/map2", () => "Hello Again!");


app.Run();
