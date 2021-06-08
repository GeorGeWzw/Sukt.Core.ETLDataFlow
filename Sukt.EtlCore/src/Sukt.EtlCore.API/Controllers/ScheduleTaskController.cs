using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.AspNetCore;
using Sukt.EtlCore.Application.TaskConfig;
using Sukt.EtlCore.Dtos.TaskConfig;
using Sukt.Module.Core.AjaxResult;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyCore.ETLDispatchCenter.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Description("任务管理")]
    public class ScheduleTaskController : Sukt.EtlCore.Shared.ApiControllerBase
    {
        private readonly IScheduleTaskContract _scheduleTaskContract;

        public ScheduleTaskController(IScheduleTaskContract scheduleTaskContract)
        {
            _scheduleTaskContract = scheduleTaskContract;
        }
        /// <summary>
        /// 加载任务下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("加载任务下拉列表")]
        public async Task<AjaxResult> GetLoadSelectListItemAsync()
        {
            return (await _scheduleTaskContract.GetLoadSelectListItemAsync()).ToAjaxResult();
        }
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加任务")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync([FromBody]ScheduleTaskInputDto input)
        {
            return (await _scheduleTaskContract.CreateAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("分页获取任务")]
        public async Task<PageList<ScheduleTaskPageOutPutDto>> GetPageLoadAsync([FromBody] PageRequest request)
        {
            return (await _scheduleTaskContract.GetLoadPageAsync(request)).PageList();
        }
    }
}
