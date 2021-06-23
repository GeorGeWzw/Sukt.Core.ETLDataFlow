using Sukt.Module.Core.Modules;
using Sukt.Module.Core.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.EtlCore.WorkNode.Test
{
    [SuktDependsOn(typeof(DependencyAppModule))]
    public class BaseTestModule : SuktAppModule
    {

    }
}
