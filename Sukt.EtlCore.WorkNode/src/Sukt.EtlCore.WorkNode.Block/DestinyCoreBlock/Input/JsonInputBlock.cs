using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using JetBrains.Annotations;
using Json.Path;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Enum;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Input;
using Sukt.EtlCore.WorkNode.BlockFLowkDataTransMission;
using Sukt.Module.Core.Extensions;

namespace Sukt.EtlCore.WorkNode.Block.DestinyCoreBlock.Input
{
    public class JsonInputBlock<TDataTransMission> : IPropagatorBlock<TDataTransMission, TDataTransMission>, IReceivableSourceBlock<TDataTransMission>
    {
        public readonly DataTransMission DataTransMission;
        /// <summary>
        /// 私有来源表变量
        /// </summary>
        private readonly IReceivableSourceBlock<TDataTransMission> _mSource;
        /// <summary>
        /// 目标连接块
        /// </summary>
        private readonly ITargetBlock<TDataTransMission> _mTarget;
        public JsonInputBlock(ReadJsonInput readJsonInput)
        {
            // 创建一个队列来保存消息。
            //var queue = new Queue<TDataTransMission>();
            //传播器的源部分包含大小为readJsonConfig的对象并将数据传播到任何连接的目标。
            var source = new BufferBlock<TDataTransMission>();
            // 目标部件接收数据并将其添加到队列中。
            var target = new ActionBlock<TDataTransMission>(item =>
            {
               
            });

            //读取json到dataTable
            var reader =  new Utf8JsonReader(Encoding.UTF8.GetBytes(readJsonInput.JsonString));
            if (JsonDocument.TryParseValue(ref reader, out var doc))//非json字符串会在此处抛出JsonReaderException异常 Expected a value, but instead reached end of data.
            {
                DataTransMission = new DataTransMission{Table = ReadJson(doc, readJsonInput.JsonReadConfig)};
            }
            
            // 当目标设置为完成状态时，传播任何并将源设置为已完成状态。
            target.Completion.ContinueWith(delegate
            {
                source.Complete();
            });
            _mTarget = target;
            _mSource = source;
        }
        // 获取数据
        public DataTransMission Data => DataTransMission;


        #region IDataflowBlock 数据流块成员
        /// <summary>
        /// 获取表示当前数据流块完成的任务。
        /// </summary>
        public Task Completion => _mSource.Completion;

        /// <summary>
        /// 向此目标块发出信号，表示它不应再接受任何消息，也不使用延迟的消息。
        /// </summary>
        public void Complete()
        {
            _mTarget.Complete();
        }
        
        /// <summary>
        /// 断层故障处理
        /// </summary>
        /// <param name="exception"></param>
        public void Fault(Exception exception)
        {
            _mTarget.Fault(exception);
        }
        #endregion

        #region ISourceBlock 源块成员
        /// <summary>
        /// 将此数据流块链接到下一目标。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="linkOptions"></param>
        /// <returns></returns>
        public IDisposable LinkTo(ITargetBlock<TDataTransMission> target, DataflowLinkOptions linkOptions)
        {
            return _mSource.LinkTo(_mTarget, linkOptions);
        }
        /// <summary>
        /// 由目标调用以使用来自源的先前提供的消息。
        /// </summary>
        /// <param name="messageHeader"></param>
        /// <param name="target"></param>
        /// <param name="messageConsumed"></param>
        /// <returns></returns>
        TDataTransMission ISourceBlock<TDataTransMission>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TDataTransMission> target, out bool messageConsumed)
        {
            return _mSource.ConsumeMessage(messageHeader,
               target, out messageConsumed);
        }
        /// <summary>
        /// 由目标调用以保留源以前提供的消息,但还没有被这个目标消耗掉。
        /// </summary>
        /// <param name="messageHeader"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        bool ISourceBlock<TDataTransMission>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TDataTransMission> target)
        {
            return _mSource.ReserveMessage(messageHeader, target);
        }
        /// <summary>
        /// 由目标调用以从源释放先前保留的消息。
        /// </summary>
        /// <param name="messageHeader"></param>
        /// <param name="target"></param>
        void ISourceBlock<TDataTransMission>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TDataTransMission> target)
        {
            _mSource.ReleaseReservation(messageHeader, target);
        }
        #endregion

        #region  IReceivableSourceBlock 可接收源块<TOutput>成员
        /// <summary>
        /// 尝试从源同步接收项目。
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryReceive(Predicate<TDataTransMission> filter, out TDataTransMission item)
        {
            return _mSource.TryReceive(filter, out item);
        }
        /// <summary>
        /// 尝试将源中的所有可用元素删除到新的返回的数组。
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool TryReceiveAll(out IList<TDataTransMission> items)
        {
            return _mSource.TryReceiveAll(out items);
        }
        #endregion

        #region  ITargetBlock 目标数成员处理
        /// <summary>
        /// 异步地将消息传递给目标块，给目标消费信息的机会。
        /// </summary>
        /// <param name="messageHeader"></param>
        /// <param name="messageValue"></param>
        /// <param name="source"></param>
        /// <param name="consumeToAccept"></param>
        /// <returns></returns>
        DataflowMessageStatus ITargetBlock<TDataTransMission>.OfferMessage(DataflowMessageHeader messageHeader, TDataTransMission messageValue, ISourceBlock<TDataTransMission> source, bool consumeToAccept)
        {
            return _mTarget.OfferMessage(messageHeader,
               messageValue, source, consumeToAccept);
        }
        #endregion

        private DataTable ReadJson(JsonDocument doc, [ItemNotNull] List<JsonReadConfiguration> readConfigurations)
        {
            var table = new DataTable();
            table.Columns.AddRange(ReadColumns(readConfigurations));
            // 判断是否为数组
            switch (doc.RootElement.ValueKind)
            {
                case JsonValueKind.Object:
                case JsonValueKind.Array:
                case JsonValueKind.String:
                case JsonValueKind.Number:
                case JsonValueKind.True:
                case JsonValueKind.False:
                case JsonValueKind.Null:
                    ReadAll(table, doc.RootElement, readConfigurations);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return table;
        }

        private void ReadAll(DataTable table, JsonElement element, List<JsonReadConfiguration> readConfigurations)
        {
            List<object[]> data = new(readConfigurations.Count());
            foreach (var c in readConfigurations)
            {
                if (!JsonPath.TryParse(c.PathField, out var path)) continue;
                
                Debug.Assert(path != null, nameof(path) + " != null");
                var results = path.Evaluate(element);
                if (results.Matches.IsNull())
                {
                    throw new InvalidOperationException();
                }

                object[] columnValue = new object[results.Matches.Count];
                for (int i = 0; i < results.Matches.Count; i++)
                {
                    PathMatch valueNode = results.Matches[i];
                    columnValue[i] = valueNode.Value.ValueKind switch
                    {
                        JsonValueKind.String => valueNode.Value.GetReferenceValue(c.FieldType),
                        JsonValueKind.Number => valueNode.Value.GetValue(c.FieldType),
                        JsonValueKind.True => valueNode.Value.GetValue(c.FieldType),
                        JsonValueKind.False => valueNode.Value.GetValue(c.FieldType),
                        JsonValueKind.Null => valueNode.Value.GetReferenceValue(c.FieldType),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                data.Add(columnValue);
            }

            for (int i = 0; i < data[0].Length; i++)
            {
                var r = table.NewRow();
                for (int j = 0; j < readConfigurations.Count; j++)
                {
                    r[readConfigurations[j].FLowField] = data[j][i];
                }
                table.Rows.Add(r);
            }
        }

        private DataColumn[] ReadColumns(IEnumerable<JsonReadConfiguration> readConfigurations) => readConfigurations
            .Select(c =>
                new DataColumn(c.FLowField, GetProperType(c.FieldType))).ToArray();

        private Type GetProperType(FieldTypeEnum fieldType)
        {
            return fieldType switch
            {
                FieldTypeEnum.String => typeof(string),
                FieldTypeEnum.Integer => typeof(int),
                FieldTypeEnum.Double => typeof(double),
                _ => throw new ArgumentOutOfRangeException(nameof(fieldType), fieldType, null)
            };
        }
    }
}
