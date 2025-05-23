using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly TimeSpan duration;
        public RedisCacheAttribute(int seconds, int minutes = 0 , int days = 0)
        {
            duration = TimeSpan.FromSeconds(seconds);
            duration = minutes  != 0 ? duration.Add(TimeSpan.FromMinutes(minutes))  : duration;
            duration = days     != 0 ? duration.Add(TimeSpan.FromDays(days))        : duration;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService    = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;
            var cacheKey        = GenerateCacheKey(context.HttpContext.Request);
            var cachedResponse  = await cacheService.GetCahcedItem(cacheKey);

            // If the reques is cached before then return it
            if (cachedResponse != null)
            {
                context.Result = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            // call next if not cached "call the end point to get the data"
            var contextResult = await next.Invoke(); 
            // Then cach the resut from end point if it is ok result
            if (contextResult.Result is OkObjectResult okObject)
            {
                await cacheService.SetCacheValue(cacheKey, okObject.Value!, duration);
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(request.Path);

            foreach (var query in request.Query.OrderBy(q => q.Key))
            {
                stringBuilder.Append($"|{query.Key}-{query.Value}|");
            }

            return stringBuilder.ToString();
        }
    }
}
