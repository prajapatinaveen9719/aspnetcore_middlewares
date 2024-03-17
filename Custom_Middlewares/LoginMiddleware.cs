using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace aspnetcore_middlewares.Custom_Middlewares
{
    public class LoginMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            if (context.Request.Path.Value == "/" && context.Request.Method == "POST")
            {
                context.Request.EnableBuffering();
                var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();

                //Parse the request body from string into Dictionary
                //var keyValuePairs = body.Split('&')
                //    .Select(pair => pair.Split('='))
                //    .ToDictionary(pair => pair[0], pair => pair[1]);
                Dictionary<string, StringValues> dict = QueryHelpers.ParseQuery(body);

                string? email = null;
                string? password = null;
                Int32 statusCode = 200;
                string message = "";

                if (dict.ContainsKey("email"))
                {
                    email = dict["email"].ToString();
                }


                if (dict.ContainsKey("password"))
                {
                    password = dict["password"].ToString();
                }


               
                /*example 3*/
                 if (!dict.ContainsKey("email") && !dict.ContainsKey("password"))
                {
                    statusCode = 400;
                    message = "Invalid input for email \n Invalid input for password";
                }
                else if (!dict.ContainsKey("password"))
                {
                    statusCode = 400;
                    message = "Invalid input for 'password' \n";
                }
                else if (!dict.ContainsKey("email"))
                {
                    statusCode = 400;
                    message = "Invalid input for 'email' \n";
                }

                /*example 1 successfull login */
                else if (email == "admin@example.com" && password == "admin1234")
                {
                    statusCode = 200;
                    message = "login successfull";
                }

                /*example 2*/
                else if (email != "admin@example.com" || password != "admin1234")
                {
                    statusCode = 400;
                    message = "Invalid login";
                }
                




                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(message);




            }
            else
            {
                 await next(context);
            }




        }
    }
}
