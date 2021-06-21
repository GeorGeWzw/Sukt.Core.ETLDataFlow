using System;
using System.Collections.Generic;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Enum;

namespace Sukt.EtlCore.WorkNode.Block.BlockOption.Input
{
    /// <summary>
    /// 通过jsonPath读取json字符串内容
    /// </summary>
    public class ReadJsonInput
    {
        /// <summary>
        /// Json字符串
        /// </summary>
        public string JsonString { get; set; }
        /// <summary>
        /// Json读取配置
        /// </summary>
        public List<JsonReadConfiguration> JsonReadConfig { get; set; }
    }
    
    /// <summary>
    /// FTP配置项
    /// </summary>
    public class FtpConfigInput
    {
        /// <summary>
        /// FTP主机地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 服务器密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// FTP文件路径
        /// </summary>
        public string TargetFilePath { get; set; }
        /// <summary>
        /// FTP端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 是否忽略空文件
        /// </summary>
        public bool IsIgnoreEmptyFile { get; set; }
    }
    
    /// <summary>
    /// Json读取配置
    /// </summary>
    public class JsonReadConfiguration
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Json字段路径
        /// </summary>
        public string PathField { get; set; }
        /// <summary>
        /// 数据流内字段名称
        /// </summary>
        public string FLowField { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public FieldTypeEnum FieldType { get; set; }
    }
}
