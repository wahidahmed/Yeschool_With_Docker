using School.AdminService.Errors;
using System.Net;

namespace School.AdminService.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IWebHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IWebHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // Log full exception including inner exceptions
                var innerEx = ex;
                while (innerEx != null)
                {
                    Console.WriteLine(innerEx.Message);
                    Console.WriteLine(innerEx.StackTrace);
                    innerEx = innerEx.InnerException;
                }

                // Or just capture the deepest inner exception
                var deepestEx = ex;
                while (deepestEx.InnerException != null) deepestEx = deepestEx.InnerException;
                ApiError response;
                string message;
                var exceptionType=ex.GetType();
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    //statusCode = HttpStatusCode.Unauthorized;
                    statusCode = HttpStatusCode.Forbidden;
                    message = "You are not authorizd";
                }
                else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                    //message = "Unknown Error";
                    //message =ex.Message == null? "Unknown Error":ex.Message;
                    message = deepestEx.Message;
                }
              
                if(env.IsDevelopment())
                {
                    //response = new ApiError((int)statusCode, ex.Message,ex.StackTrace.ToString());
                    response = new ApiError((int)statusCode, message, ex.StackTrace.ToString());
                }
                else
                {
                    response = new ApiError((int)statusCode, message);
                }
                logger.LogError(ex.Message,ex);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType="application/json";
                await context.Response.WriteAsync(response.ToString());

            }
        }
    }
}
