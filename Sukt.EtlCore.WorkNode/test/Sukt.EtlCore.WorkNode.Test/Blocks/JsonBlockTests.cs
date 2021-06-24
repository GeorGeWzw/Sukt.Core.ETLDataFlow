using Sukt.TestBase;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using System.Collections.Generic;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Input;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Enum;
using Sukt.EtlCore.WorkNode.Block.DestinyCoreBlock.Input;
using System.Threading.Tasks.Dataflow;
using Sukt.EtlCore.WorkNode.BlockFLowkDataTransMission;
using Xunit.Abstractions;
using System.IO;

namespace Sukt.EtlCore.WorkNode.Test.Blocks
{
    public class JsonBlockTests : IntegratedTest<BaseTestModule>
    {
        private readonly ITestOutputHelper output;
        private readonly FtpConfigInput ftpConfig;

        public JsonBlockTests(ITestOutputHelper output)
        {
            this.output = output;
            ftpConfig = new FtpConfigInput
            {
                Host = "47.100.213.49",
                TargetFilePath = "test.json",
                UserName = "test",
                PassWord = "wzw1021..",
            };
        }


        [Fact]
        public void FTPJsonBlock_Test()
        {
            var source = new ReadFTPFileStringBlock();
            var testBlock = new ActionBlock<ReadJsonInput>(i =>
            {
                i.JsonString.Length.ShouldBeGreaterThan(10);
                output.WriteLine(i.JsonString[..100]);
            });
            source.LinkTo(testBlock, new DataflowLinkOptions { PropagateCompletion = true });
            source.Post(new ReadJsonConfiguration { FtpConfiguration = ftpConfig });
            source.Complete();
            testBlock.Completion.Wait();
        }

        [Fact]
        public void JsonInputBlock_Test()
        {
            var source = new JsonInputBlock();
            var testBlock = new ActionBlock<IDataTransMission>(d =>
            {
                var table = d.Table;
                table.Columns.Count.ShouldBe(6);
                var firstRow = table.Rows[0];
                firstRow[0].ShouldBe(11008);
                output.WriteLine(string.Join(' ', firstRow.ItemArray));
                table.Rows.Count.ShouldBeGreaterThan(10);
            });
            source.LinkTo(testBlock, new DataflowLinkOptions { PropagateCompletion = true });
            source.Post(new ReadJsonInput
            {
                JsonString = File.ReadAllText("test.json"),
                JsonReadConfig = GetJsonPathConfigurations()
            });
            source.Complete();
            testBlock.Completion.Wait();
        }

        [Fact]
        public async Task Build_JsonPipeline_Test()
        {
            var linkOption = new DataflowLinkOptions { PropagateCompletion = true };
            var source = new ReadFTPFileStringBlock();
            var readJson = new JsonInputBlock();
            var testBlock = new ActionBlock<IDataTransMission>(d =>
            {
                var table = d.Table;
                table.Columns.Count.ShouldBe(6);
                var firstRow = table.Rows[0];
                firstRow[0].ShouldBe(11008);
                output.WriteLine(string.Join(' ', firstRow.ItemArray));
                table.Rows.Count.ShouldBeGreaterThan(10);
            });
            source.LinkTo(readJson, linkOption);
            readJson.LinkTo(testBlock, linkOption);

            source.Post(new ReadJsonConfiguration
            {
                FtpConfiguration = ftpConfig,
                JsonReadConfig = GetJsonPathConfigurations()
            });
            source.Complete();
            await testBlock.Completion;
        }

        private List<JsonReadConfiguration> GetJsonPathConfigurations() => new()
        {
            new JsonReadConfiguration
            {
                FieldType = FieldTypeEnum.Integer,
                FLowField = "TeamID",
                PathField = "$.arrTeam[*].TeamID"
            },
            new JsonReadConfiguration
            {
                FieldType = FieldTypeEnum.String,
                FLowField = "Name_J",
                PathField = "$.arrTeam[*].Name_J"
            },
            new JsonReadConfiguration
            {
                FieldType = FieldTypeEnum.String,
                FLowField = "Name_Short",
                PathField = "$.arrTeam[*].Name_Short"
            },
            new JsonReadConfiguration
            {
                FieldType = FieldTypeEnum.String,
                FLowField = "Name_E",
                PathField = "$.arrTeam[*].Name_E"
            },
            new JsonReadConfiguration
            {
                FieldType = FieldTypeEnum.String,
                FLowField = "Name_F",
                PathField = "$.arrTeam[*].Name_F"
            },
            new JsonReadConfiguration
            {
                FieldType = FieldTypeEnum.String,
                FLowField = "Flag",
                PathField = "$.arrTeam[*].Flag"
            }
        };

    }
}
