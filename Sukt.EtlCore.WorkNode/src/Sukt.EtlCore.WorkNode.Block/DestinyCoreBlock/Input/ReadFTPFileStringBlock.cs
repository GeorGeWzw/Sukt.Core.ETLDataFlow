using Sukt.EtlCore.WorkNode.Block.BlockOption.Input;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Sukt.EtlCore.WorkNode.Block.DestinyCoreBlock.Input
{
    public class ReadFTPFileStringBlock : IPropagatorBlock<ReadJsonConfiguration,ReadJsonInput>
    {
        public string Content { get; init; }

        /// <summary>
        /// 目标连接块
        /// </summary>
        private readonly ITargetBlock<ReadJsonConfiguration> _mTarget;

        /// <summary>
        /// 来源块
        /// </summary>
        private readonly IReceivableSourceBlock<ReadJsonInput> _mSource;

        public ReadFTPFileStringBlock()
        {
            //传播器的源部分包含大小为ReadJsonConfiguration 的对象并将数据传播到任何连接的目标。
            var source = new BufferBlock<ReadJsonInput>();
            var target = new ActionBlock<ReadJsonConfiguration>(i =>
            {
                //连接ftp读取json
                var input = i.FtpConfiguration;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{input.Host}{(input.Port is 21 ? string.Empty : $":{input.Port}")}/{input.TargetFilePath}");
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(input.UserName, input.PassWord);
                var response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new(responseStream);
                var jsonString = reader.ReadToEnd();
                reader.Close();
                response.Close();
                source.Post(new ReadJsonInput { JsonString  = jsonString, JsonReadConfig = i.JsonReadConfig });
            });
            // 当目标设置为完成状态时，传播任何并将源设置为已完成状态。
            target.Completion.ContinueWith(delegate
            {
                source.Complete();
            });
            _mTarget = target;
            _mSource = source;
        }

        public Task Completion => _mTarget.Completion;

        public void Complete()
        {
            _mTarget.Complete();
        }


        public void Fault(Exception exception)
        {
            _mTarget.Fault(exception);
        }

        public ReadJsonInput ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<ReadJsonInput> target, out bool messageConsumed)
        {
            return _mSource.ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        public IDisposable LinkTo(ITargetBlock<ReadJsonInput> target, DataflowLinkOptions linkOptions)
        {
            return _mSource.LinkTo(target, linkOptions);
        }

        public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<ReadJsonInput> target)
        {
            _mSource.ReleaseReservation(messageHeader, target);
        }

        public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<ReadJsonInput> target)
        {
            return _mSource.ReserveMessage(messageHeader, target);
        }

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, ReadJsonConfiguration messageValue, ISourceBlock<ReadJsonConfiguration> source, bool consumeToAccept)
        {
            return _mTarget.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }
    }
}
