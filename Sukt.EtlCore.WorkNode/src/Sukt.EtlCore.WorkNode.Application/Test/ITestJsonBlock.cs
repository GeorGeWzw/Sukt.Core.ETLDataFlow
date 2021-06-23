using Sukt.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.EtlCore.WorkNode.Application.Test
{
    public interface ITestJsonBlock: IScopedDependency
    {
        Task TestJsonInputBlock();
    }
}
