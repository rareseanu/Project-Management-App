using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectManagementApp.Models.Exceptions;
using ProjectManagementApp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NLog;
using Serilog;
using ILogger = NLog.ILogger;

namespace ProjectManagementApp.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
            logger = LogManager.GetCurrentClassLogger();
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value!.Contains("/api/"))
            {
                try
                {                   
                    await next(context);
                    if (context.Request.Query["hide"] == "true")
                    {
                        throw new CustomException("Error", HttpStatusCode.NoContent);
                    }
                }
                catch (NotFoundException ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    var response = JsonConvert.SerializeObject(new ExceptionResponse
                    {
                        Message = ex.Message,
                        Trace = ex.StackTrace,
                        Type = ex.GetType().Name
                    });

                    await context.Response.WriteAsync(response);
                    logger.Error(response);
                    Log.Error(ex, ex.Message);
                }
                catch (CustomException ex)
                {
                    context.Response.StatusCode = (int)ex.Status;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(ex.Value));
                    Log.Error(ex, "Custom exception.");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ExceptionResponse
                    {
                        Message = ex.Message,
                        Trace = ex.StackTrace,
                        Type = ex.GetType().Name
                    }));
                    Log.Error(ex, ex.Message);
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
