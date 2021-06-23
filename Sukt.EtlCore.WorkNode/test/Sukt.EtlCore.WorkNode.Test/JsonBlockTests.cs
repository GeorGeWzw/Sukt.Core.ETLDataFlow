using Sukt.EtlCore.WorkNode.Application.Test;
using Sukt.Module.Core.Modules;
using Sukt.Module.Core.SuktDependencyAppModule;
using Sukt.TestBase;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Sukt.EtlCore.WorkNode.Test
{
    public class JsonBlockTests : IntegratedTest<BaseTestModule>
    {
        private readonly ITestJsonBlock _testJsonBlock;

        public JsonBlockTests()
        {
            _testJsonBlock = ServiceProvider.GetService<ITestJsonBlock>();
        }
        [Fact]
        public async Task String_Test()
        {
            await _testJsonBlock.TestJsonInputBlock();
        }
    }
}
