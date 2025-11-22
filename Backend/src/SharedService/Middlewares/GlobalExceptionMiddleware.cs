using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CustomLogs;
using System.Net;
using System.Text.Json;

namespace SharedService.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string message="Sorry! internal server error occured";
            int statusCode=(int)HttpStatusCode.InternalServerError;
            string title = "Error";
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                //log original exception
                CustomLogException.LogExceptions(ex);

                //timeout exception
                if (ex is TaskCanceledException || ex is TimeoutException)
                    title = "out of time"; message = "request tiimeout.... please try again";statusCode = (int)StatusCodes.Status408RequestTimeout;
                
                //if none of the expection, do the default
                await ModifyHeader(context, title, message, statusCode);
            }

            // if response hasn't started  then only modify 
            if (!context.Response.HasStarted)
            {
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests) // check if exception is too many requests
                {
                    await ModifyHeader(context, "Warning", "Too many requests. Please try again later.", StatusCodes.Status429TooManyRequests);
                }
                else if (context.Response.StatusCode == StatusCodes.Status401Unauthorized) // check if response is unauthorized/ 401 status code
                {
                    await ModifyHeader(context, "Alert", "You are not authorized to access.", StatusCodes.Status401Unauthorized);
                }
                else if (context.Response.StatusCode == StatusCodes.Status403Forbidden) // check if response is forbidden/ 403 status code
                {
                    await ModifyHeader(context, "Out of access", "You are not allowed to access.", StatusCodes.Status403Forbidden);
                }
            }
        }

        private async Task ModifyHeader(HttpContext context,string title,string message,int statusCode)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
            {
                Title = title,
                Detail = message,
                Status = statusCode
            }),CancellationToken.None);
        }
    }
}
