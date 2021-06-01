﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sukt.EtlCore.WorkNode.BlockFLowkDataTransMission
{
    /// <summary>
    /// 传输对象
    /// </summary>
    public interface IDataTransMission
    {
        /// <summary>
        /// 数据存储
        /// </summary>
        DataTable Table { get; set; }
    }
}
