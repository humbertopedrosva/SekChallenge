using SekChallenge.API.Infra;

namespace SekChallenge.API
{
    public class ContextMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var dbContext = context.RequestServices.GetRequiredService<SekContext>();

            await next.Invoke(context);

            await dbContext.SaveChangesAsync();
        }
    }
}
