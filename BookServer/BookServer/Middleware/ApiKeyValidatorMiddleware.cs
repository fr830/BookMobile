/**
 * Use ASP.NET Core Middleware to check for API key
 * Reference:
 *   ContactsApi
 *   https://mithunvp.com/create-aspnet-mvc-6-web-api-visual-studio-2017/
 *   https://github.com/mithunvp/ContactsAPI
 */
using BookServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BookServer.Middleware
{
    public class ApiKeyValidatorMiddleware
    {
        private readonly RequestDelegate _next;
        private IBookRepository BookRepo { get; set; }
        public ApiKeyValidatorMiddleware(RequestDelegate next, IBookRepository repo)
        {
            _next = next;
            BookRepo = repo;
        }

        public async Task Invoke(HttpContext context, IBookRepository repo)
        {
            if (!context.Request.Headers.Keys.Contains("Authorization"))
            {
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("Api Key is missing");
                return;
            }
            else
            {
                if (!repo.CheckValidApiKey(context.Request.Headers["Authorization"]))
                {
                    context.Response.StatusCode = 401; // UnAuthorized
                    await context.Response.WriteAsync("Invalid Api Key");
                    return;
                }
            }

            await _next.Invoke(context);
        }

    }

    // Extension Method
    public static class UserKeyValidatorExtension
    {
        public static IApplicationBuilder ApplyApiKeyValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiKeyValidatorMiddleware>();
            return app;
        }
    }
}
