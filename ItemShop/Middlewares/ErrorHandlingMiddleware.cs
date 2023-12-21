using ItemShop.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ItemShop.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception error)
            {

                var response = httpContext.Response;
                response.ContentType = "application/json";
                switch (error)
                {
                    case ItemNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case NoItemsFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }

    //// Extension method used to add the middleware to the HTTP request pipeline.
    //public static class ErrorHandlingMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    //    }
    //}
}
