using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace HelloWorld.Tests
{
    public class HelloWorldTests
    {
        [Fact]
        public async Task HelloWorld_NoNamePassed_ReturnBadRequestObjectResult()
        {
            var logger = A.Fake<ILogger>();
            var request = A.Fake<HttpRequest>();

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            var json = JsonConvert.SerializeObject(new object { });

            sw.Write(json);
            sw.Flush();

            ms.Position = 0;

            request.Body = ms;

            var result = await HelloWorld.Run(request, logger);

            Assert.NotNull(result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task HelloWorld_NamePassedInQueryString_OkObjectResult()
        {
            var logger = A.Fake<ILogger>();
            var request = A.Fake<HttpRequest>();

            var result = await HelloWorld.Run(request, logger);

            Assert.NotNull(result);
            //TODO
        }
    }
}
