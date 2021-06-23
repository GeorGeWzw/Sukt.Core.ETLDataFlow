using Sukt.Module.Core;
using System.Data;
using System.Threading.Tasks;

namespace Sukt.EtlCore.WorkNode.Application.Test
{
    public interface ITestJsonBlock: IScopedDependency
    {
        Task<DataTable> TestJsonInputBlock(Block.BlockOption.Input.ReadJsonInput input);
    }
}
