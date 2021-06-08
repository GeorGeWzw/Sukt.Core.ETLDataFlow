using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Sukt.EtlCore.WorkNode.Block.BlockOption.Input;
using Sukt.EtlCore.WorkNode.BlockFLowkDataTransMission;

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
        public JsonInputBlock(ReadJsonConfig readJsonConfig)
        {
            // 创建一个队列来保存消息。
            var queue = new Queue<TDataTransMission>();
            //传播器的源部分包含大小为readJsonConfig的对象并将数据传播到任何连接的目标。
            var source = new BufferBlock<TDataTransMission>();
            // 目标部件接收数据并将其添加到队列中。
            var target = new ActionBlock<TDataTransMission>(item =>
            {
               
            });
            
            

            // 当目标设置为完成状态时，传播任何并将源设置为已完成状态。
            target.Completion.ContinueWith(delegate
            {
                source.Complete();
            });
            DataTransMission = new DataTransMission();
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


    }
}
