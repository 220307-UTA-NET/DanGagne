namespace myFirstServerDan.Middleware
{
    public class RequireAuthorization
    {
        private readonly RequestDelegate _next;

        public RequireAuthorization(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        //app.Use(async (context, next) =>
        {
            if (context.Request.Query["authorization"] == "true")
            {
                //authorization is good, continue on
                await _next(context);
            }
            else
            {
                //if we don't have authorization...then what
                context.Response.StatusCode = 401;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("error! not authorized");
            }
        }


        /*Logging is important
        focus on 6 levels of logging
        from least to most meaningful
        0 - Trace -every action that happens in your program
        1 - Debug
        2 - Information
        3 - Warning
        4 - Error
        5 - Critical
        */

    }
}
