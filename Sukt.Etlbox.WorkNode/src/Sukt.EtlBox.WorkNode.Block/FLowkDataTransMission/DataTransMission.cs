﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sukt.EtlBox.WorkNode.Block.FLowkDataTransMission
{
    public class DataTransMission : IDataTransMission
    {
        public DataTable Table { get; set; } = new DataTable();
    }
}
