using Microsoft.AspNetCore.Mvc;
using Sukt.EtlCore.Application.TaskConfig;
using Sukt.EtlCore.Dtos.TaskConfig;
using Sukt.Module.Core.AjaxResult;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.ComponentModel;
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
        public async Task<AjaxResult> CreateAsync([FromBody] ScheduleTaskInputDto input)
        {
            return (await _scheduleTaskContract.CreateAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("获取任务")]
        public async Task<AjaxResult> GetLoadAsync([FromQuery] Guid? id)
        {
            return (await _scheduleTaskContract.GetLoadAsync(id.Value)).ToAjaxResult();
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
        /// <summary>
        /// 编辑任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Description("编辑任务")]
        public async Task<AjaxResult> UpdateAsync(Guid id, [FromBody] ScheduleTaskInputDto input)
        {
            return (await _scheduleTaskContract.UpdateAsync(id,input)).ToAjaxResult();
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Description("删除任务")]
        public async Task<AjaxResult> DeleteAsync(Guid id)
        {
            return (await _scheduleTaskContract.DeleteAsync(id)).ToAjaxResult();
        }
    }
}
