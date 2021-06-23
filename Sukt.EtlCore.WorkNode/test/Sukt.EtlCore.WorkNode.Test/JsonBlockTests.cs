using Sukt.EtlCore.WorkNode.Application.Test;
using Sukt.TestBase;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using System.Collections.Generic;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Input;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Enum;
using System.IO;

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
            var table = await _testJsonBlock.TestJsonInputBlock(new ReadJsonInput()
            {
                JsonString = File.ReadAllText("阿丙曼特_2020-2021.json"),
                JsonReadConfig = new List<JsonReadConfiguration>
                {
                    new JsonReadConfiguration
                    {
                        FieldType= FieldTypeEnum.Integer,
                        FLowField="TeamID", 
                        PathField = "$.arrTeam[*].TeamID"
                    },
                    new JsonReadConfiguration
                    {
                        FieldType= FieldTypeEnum.String,
                        FLowField="Name_J",
                        PathField = "$.arrTeam[*].Name_J"
                    },
                    new JsonReadConfiguration
                    {
                        FieldType= FieldTypeEnum.String,
                        FLowField="Name_Short",
                        PathField = "$.arrTeam[*].Name_Short"
                    },
                    new JsonReadConfiguration
                    {
                        FieldType= FieldTypeEnum.String,
                        FLowField="Name_E",
                        PathField = "$.arrTeam[*].Name_E"
                    },
                    new JsonReadConfiguration
                    {
                        FieldType= FieldTypeEnum.String,
                        FLowField="Name_F",
                        PathField = "$.arrTeam[*].Name_F"
                    },
                    new JsonReadConfiguration
                    {
                        FieldType= FieldTypeEnum.String,
                        FLowField="Flag",
                        PathField = "$.arrTeam[*].Flag"
                    }
                }
            });

            table.Columns.Count.ShouldBe(6);
            table.Rows[0][0].ShouldBe(11008);
        }
    }
}
