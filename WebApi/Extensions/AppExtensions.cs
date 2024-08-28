using WebApi.Middlewares;

namespace WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void useErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
