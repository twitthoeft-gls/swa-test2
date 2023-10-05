using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace Company.Function
{
    public static class HttpTrigger1
    {
        [FunctionName("HttpTrigger1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            
            //Learn more about dotnet core and SameSite cookies = https://learn.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-7.0

            var cookieOptions = new CookieOptions
            {
                // Set the secure flag, which Chrome's changes will require for SameSite none.
                // Note this will also require you to be running on HTTPS.
                Secure = true,

                // Set the cookie to HTTP only which is good practice unless you really do need
                // to access it client side in scripts.
                //HttpOnly = true,

                // Add the SameSite attribute, this will emit the attribute with a value of none.
                SameSite = SameSiteMode.None

                // The client should follow its default cookie policy.
                // SameSite = SameSiteMode.Unspecified
            };

            var guid = Guid.NewGuid();

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Moved);
            response.Headers.Location = new Uri("https://httpstat.us/200");

            // Get the httpContext from the incoming request, append cookies onto the outgoing response.  More info = https://www.jacobmohl.dk/how-to-create-custom-cookies-in-http-triggered-azure-functions
            req.HttpContext.Response.Cookies.Append("glsauto-guid", guid.ToString(), cookieOptions);
            return response;
        }
    }
}
