using Sukt.EtlCore.WorkNode.Block.DestinyCoreBlock.Input;
using Sukt.EtlCore.WorkNode.BlockFLowkDataTransMission;
using System.Threading.Tasks;
using System.Data;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Input;

namespace Sukt.EtlCore.WorkNode.Application.Test
{
    public class TestJsonBlock : ITestJsonBlock
    {
        public async Task<DataTable> TestJsonInputBlock(ReadJsonInput input)
        {
            //await Task.CompletedTask;
            //string file = Directory.GetFiles(@"F:/data").First();
            //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //StreamReader rs = new StreamReader(@"F:/data/阿丙曼特_2020-2021.json", Encoding.GetEncoding("gb2312"));
            //string json_str = rs.ReadToEnd();

            var json = new JsonInputBlock<IDataTransMission>(input);
            return json.Data.Table;
        }
    }
}
