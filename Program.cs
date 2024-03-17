using aspnetcore_middlewares.Custom_Middlewares;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDataProtection();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
//builder.Services.AddTransient<LoginMiddleware>();
var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.UseSession();

//app.UseMiddleware<LoginMiddleware>();





//app.Use(async (HttpContext context, RequestDelegate next) =>
//{
//    await context.Response.WriteAsync("Har Har Mahadev \n");
//    await context.Response.WriteAsync("Request Type : " + context.Request.Method + "\n");
//    await context.Response.WriteAsync("Request Path : " + context.Request.Path + "\n");

//    /*Request Header*/
//    await context.Response.WriteAsync("Request Header : " + context.Request.Headers["PC"].ToString() + "\n");


//    /*Accessing Route Values
//     *https://localhost:7268/om/namah/shivaay
//     *
//     */
//    var path = context.Request.Path.Value;
//    var segments = path?.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

//    if (segments?.Length > 0)
//    {
//        await context.Response.WriteAsync("Request GetRouteData : " + segments?[0] + segments?[1] + segments?[2] + "\n");
//    }

//    /*Accessing query string values 
//     * https://localhost:7268/?name=Har Har Mahadev&age=0
//     */
//    await context.Response.WriteAsync("query string name values  : " + context.Request.Query["name"].ToString() + "\n");
//    await context.Response.WriteAsync("query string age values  : " + context.Request.Query["age"].ToString() + "\n");


//    /*Accessing  Body parameters 
//      * {
//      *  "Om":"Ram Ram G"
//      * }
//     */
//    context.Request.EnableBuffering();
//    var bodystream = new StreamReader(context.Request.Body);
//    var bodyText = bodystream.ReadToEndAsync();
//    context.Request.Body.Position = 0;
//    await context.Response.WriteAsync("bodyText  : " + bodyText.Result + "\n");


//    context.Session.SetString("Name", "Har Har Mahadev");
//    await next(context);


//});



//app.Use(async (HttpContext context, RequestDelegate next) =>
//{
//    context.Session.SetInt32("Age", 0);
//    await next(context);
//});

//app.Use(async (HttpContext context,RequestDelegate next) =>
//{
//   var name=  context.Session.GetString("Name");
//    var age = context.Session.GetInt32("Age");

//    await context.Response.WriteAsync("session name  : " + name + "\n");
//    await context.Response.WriteAsync("session age  : " + age + "\n");
//});


app.UseHelloMiddleware();

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("No response");
});


app.Run();