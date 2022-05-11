using Eshop.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try 
            {
                await next.Invoke(context);
            }
            catch(NotFoundException)
            {
                context.Response.StatusCode = 404;
            }
            catch(UnauthorizedAccessException)
            {
                context.Response.StatusCode = 401;
            }
            catch(BalanceExceededException)
            {
                context.Response.StatusCode = 400;
            }
            catch(NegativeBalanceException)
            {
                context.Response.StatusCode = 400;
            }
            catch(ForbiddenActionException)
            {
                context.Response.StatusCode = 400;
            }
        }
    }
}
