using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectManagementApp.Models.Exceptions;
using ProjectManagementApp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectManagementApp.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value!.Contains("/api/"))
            {
                try
                {
                    await next(context);
                }
                catch (NotFoundException ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ExceptionResponse
                    {
                        Message = ex.Message,
                        Trace = ex.StackTrace,
                        Type = ex.GetType().Name
                    }));
                }
                catch (CustomException ex)
                {
                    context.Response.StatusCode = (int)ex.Status;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(ex.Value));
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
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
