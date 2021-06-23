using Sukt.EtlCore.WorkNode.Block.DestinyCoreBlock.Input;
using Sukt.EtlCore.WorkNode.BlockFLowkDataTransMission;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.EtlCore.WorkNode.Application.Test
{
    public class TestJsonBlock : ITestJsonBlock
    {
        public async Task TestJsonInputBlock()
        {
            await Task.CompletedTask;
            string file = Directory.GetFiles(@"F:/data").First();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            StreamReader rs = new StreamReader(@"F:/data/阿丙曼特_2020-2021.json", Encoding.GetEncoding("gb2312"));
            string json_str = rs.ReadToEnd();

            var json = new JsonInputBlock<IDataTransMission>(new Block.BlockOption.Input.ReadJsonInput() {JsonString= json_str });
            Console.WriteLine(json.Data);

        }
    }
}
