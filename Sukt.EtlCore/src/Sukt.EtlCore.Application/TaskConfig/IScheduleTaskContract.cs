using Microsoft.AspNetCore.Mvc.Rendering;
using Sukt.EtlCore.Dtos.TaskConfig;
using Sukt.Module.Core;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sukt.EtlCore.Application.TaskConfig
{
    public interface IScheduleTaskContract : IScopedDependency
    {
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateAsync(ScheduleTaskInputDto input);

        /// <summary>
        /// 修改任务配置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> UpdateAsync(Guid id, ScheduleTaskInputDto input);
        /// <summary>
        /// 表单加载任务配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> GetLoadAsync(Guid id);
        /// <summary>
        /// 分页加载任务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IPageResult<ScheduleTaskPageOutPutDto>> GetLoadPageAsync(PageRequest request);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> DeleteAsync(Guid id);
        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <returns></returns>
        Task<OperationResponse<IEnumerable<SelectListItem>>> GetLoadSelectListItemAsync();
    }
}
