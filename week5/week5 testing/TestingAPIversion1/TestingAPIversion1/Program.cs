using TestingAPIDatabases;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


var app = builder.Build();

//string connectString = File.ReadAllText(@"\Revature\DanGagne\ConnectionString\SchoolStringDB.txt");
//IRepo repo = new SqlRepo(connectString);
//Store store = new Store(repo);

//app.UseMiddleware<RequireAuthorization>();


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