using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sukt.EtlCore.WorkNode.BlockFLowkDataTransMission
{
    public class DataTransMission : IDataTransMission
    {
        public DataTable Table { get; set; } = new DataTable();
    }
}
