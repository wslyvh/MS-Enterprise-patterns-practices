using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Configuration.Source;
using wslyvh.Core.Interfaces.ServiceClient;

namespace wslyvh.Core.Test.ServiceClient
{
    [TestClass]
    public class ServiceClientTest
    {
        private IServiceClient _client;

        [TestInitialize]
        public void Initialize()
        {
            BootstrapperHelper.StartDefault();

            _client = ServiceLocator.Current.GetInstance<IServiceClient>("CachedServiceClient");

            //IResponse<TResponse> Get<TResponse>(Uri uri) where TResponse : class;
            //IResponse Put<TRequest>(Uri uri, IRequest<TRequest> data) where TRequest : class;
            //IResponse Post<TRequest>(Uri uri, IRequest<TRequest> data) where TRequest : class;
            //IResponse Delete(Uri uri);

            //_serializer.Setup(d => d.Get<string>(It.IsAny<string>())).Returns("Something");
            //var r = _client.Get<string>(new Uri("http://google.nl/"));
        }

        [TestMethod]
        public void StatusEntityTest()
        {
            var status = new Status {Code = 200, Message = "Success", Success = true};

            Assert.IsNotNull(status);
            Assert.IsNotNull(status.Code);
            Assert.IsNotNull(status.Message);
            Assert.IsNotNull(status.Success);
        }

        [TestMethod]
        public void ResponseEntityTest()
        {
            var status = new Status {Code = 200, Message = "Success", Success = true};
            var response = new Response {Status = status};

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Status);

            response = new Response(status);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Status);
        }

        [TestMethod]
        public void ResponseGenericEntityTest()
        {
            var status = new Status {Code = 200, Message = "Success", Success = true};
            var response = new Response<string> {Status = status, Data = "Data string"};

            Assert.IsNotNull(response.Status);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        public void RequestEntityTest()
        {
            var response = new Request<string> { Data = "Data string" };

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);
        }
    }
}
