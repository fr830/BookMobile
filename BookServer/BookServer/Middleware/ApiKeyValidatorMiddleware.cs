using BookServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BookServer.Middleware
{
    // ContactsApi
    // https://github.com/mithunvp/ContactsAPI
    public class ApiKeyValidatorMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyValidatorMiddleware(RequestDelegate next)
        {
            _next = next;
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

    public static class UserKeyValidatorExtension
    {
        public static IApplicationBuilder ApplyApiKeyValidation(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiKeyValidatorMiddleware>();
        }
    }
}
