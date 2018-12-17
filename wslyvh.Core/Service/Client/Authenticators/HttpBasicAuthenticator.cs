using System;
using System.Linq;
using System.Text;
using wslyvh.Core.Interfaces.ServiceClient.Rest;

namespace wslyvh.Core.Service.Client.Authenticators
{
    public class HttpBasicAuthenticator : IAuthenticator
    {
        private readonly string _username;
        private readonly string _password;

        public HttpBasicAuthenticator(string username, string password)
        {
            Guard.ArgumentIsNotNullOrEmpty(username, "username");
            Guard.ArgumentIsNotNullOrEmpty(password, "password");

            _username = username;
            _password = password;
        }

        public void Authenticate(IRestRequest request)
        {
            // only add the Authorization parameter if it hasn't been added by a previous Execute
            if (!request.Parameters.Any(p => p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
            {
                var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _username, _password)));
                var authHeader = string.Format("Basic {0}", token);

                request.AddHeader("Authorization", authHeader);
            }
        }
    }
}
