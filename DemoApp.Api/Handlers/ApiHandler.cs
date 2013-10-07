using DemoApp.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace DemoApp.Api.Handlers
{
    public class ApiHandler : DelegatingHandler
    {
        private const string AuthorizationScheme = "DemoApp";

        public ApiHandler(HttpConfiguration httpConfiguration)
        {
            InnerHandler = new HttpControllerDispatcher(httpConfiguration);
        }

        async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if (request.Headers.Authorization == null || string.IsNullOrEmpty(request.Headers.Authorization.Scheme) || request.Headers.Authorization.Scheme != AuthorizationScheme)
            //    return new HttpResponseMessage(HttpStatusCode.Unauthorized);

            //if (!ValidateAuthorizationHeader(request.Headers.Authorization.Parameter))
            //    return new HttpResponseMessage(HttpStatusCode.Unauthorized);

            return await base.SendAsync(request, cancellationToken);
        }

        private bool ValidateAuthorizationHeader(string authHeader)
        {
            var credentials = authHeader.Split(new[] { ':' });
            if (credentials.Length != 2 || string.IsNullOrEmpty(credentials[0]) || string.IsNullOrEmpty(credentials[1]))
                return false;
            var username = credentials[0];
            //var user = new UserService().GetOne(x => x.Username == username);
            //return user != null && user.AuthToken == credentials[1];
            return true;
        }
    }
}
