using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Generic;
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

            var json = JsonConvert.SerializeObject(new { });

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

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            var json = JsonConvert.SerializeObject(new { });

            sw.Write(json);
            sw.Flush();

            ms.Position = 0;

            request.Body = ms;
            var content = new Dictionary<string, StringValues>();
            content["name"] = "Neal";

            request.Query = new QueryCollection(content);

            var result = await HelloWorld.Run(request, logger);

            Assert.NotNull(result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
        }

        [Fact]
        public async Task HelloWorld_NamePassedInBody_OkObjectResult()
        {
            var logger = A.Fake<ILogger>();
            var request = A.Fake<HttpRequest>();

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            var json = JsonConvert.SerializeObject(new { name = "Neal"});

            sw.Write(json);
            sw.Flush();

            ms.Position = 0;

            request.Body = ms;

            var result = await HelloWorld.Run(request, logger);

            Assert.NotNull(result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
            Assert.Equal("Hello, Neal", ((Microsoft.AspNetCore.Mvc.OkObjectResult)result).Value);
        }
    }
}
