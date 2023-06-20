namespace CodeFirst.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public ErrorHandlingMiddleware() { }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);

            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(ex.Message);
            }
            catch
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Unexpected error");
            }
        }
    }
}
