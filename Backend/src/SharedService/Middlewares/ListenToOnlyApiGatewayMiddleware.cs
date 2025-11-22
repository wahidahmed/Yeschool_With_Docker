using Microsoft.AspNetCore.Http;

namespace SharedService.Middlewares
{
    public class ListenToOnlyApiGatewayMiddleware
    {
        private readonly RequestDelegate next;

        public ListenToOnlyApiGatewayMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //extract specific request from header
            var signedHeader = context.Request.Headers["Api-Gateway"];

            //null means the request is not coming from api gateway // 503 service unavailable
            if (signedHeader.FirstOrDefault() is null)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Sorry! service is unavailable");
                return;
            }
            await next(context);
        }
    }
}
