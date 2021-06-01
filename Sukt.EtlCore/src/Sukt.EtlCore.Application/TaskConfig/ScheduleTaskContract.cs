using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sukt.EtlCore.Application.TaskConfig;
using Sukt.EtlCore.Domain.Models.TaskConfig;
using Sukt.EtlCore.Dtos.TaskConfig;
using SuktCore.Shared.Entity;
using SuktCore.Shared.Enums;
using SuktCore.Shared.Extensions;
using SuktCore.Shared.Extensions.ResultExtensions;
using SuktCore.Shared.OperationResult;
using SuktCore.Shared.ResultMessageConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyCore.ETLDispatchCenter.Application.TaskConfig
{
    public class ScheduleTaskContract : IScheduleTaskContract
    {
        private readonly IEFCoreRepository<ScheduleTask, Guid> _scheduleTaskRepository = null;

        public ScheduleTaskContract(IEFCoreRepository<ScheduleTask, Guid> scheduleTaskRepository)
        {
            _scheduleTaskRepository = scheduleTaskRepository;
        }
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> CreateAsync(ScheduleTaskInputDto input)
        {
            return await _scheduleTaskRepository.InsertAsync(new ScheduleTask(input.TaskNumber, input.TaskName, input.TaskType, input.TaskConfig, input.Describe/*, input.SourceConnectionId, input.TargetConnectionId, input.SourceTable, input.TargetTable*/));
        }
        /// <summary>
        /// 修改任务配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> UpdateAsync(ScheduleTaskInputDto input)
        {
            ScheduleTask entity = await _scheduleTaskRepository.GetByIdAsync(input.Id);
            entity.Change(input.TaskNumber, input.TaskName, input.TaskType, input.TaskConfig, input.Describe/*, input.SourceConnectionId, input.TargetConnectionId, input.SourceTable, input.TargetTable*/);
            return await _scheduleTaskRepository.UpdateAsync(entity);
        }
        /// <summary>
        /// 表单加载任务配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> GetLoadAsync(Guid id)
        {
            ScheduleTaskLoadOutPutDto dto = await _scheduleTaskRepository.GetByIdToDtoAsync<ScheduleTaskLoadOutPutDto>(id);
            return new OperationResponse(ResultMessage.DataSuccess, dto, OperationEnumType.Success);
        }
        /// <summary>
        /// 分页加载任务配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IPageResult<ScheduleTaskPageOutPutDto>> GetLoadPageAsync(PageRequest request)
        {
            return await _scheduleTaskRepository.NoTrackEntities.ToPageAsync<ScheduleTask, ScheduleTaskPageOutPutDto>(request);
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> DeleteAsync(Guid id)
        {
            return await _scheduleTaskRepository.DeleteAsync(id);
        }
        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResponse<IEnumerable<SelectListItem>>> GetLoadSelectListItemAsync()
        {

            var list = await _scheduleTaskRepository.NoTrackEntities.Distinct()

                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.TaskName,
                    Selected = false,
                }).ToListAsync();
            return new OperationResponse<IEnumerable<SelectListItem>>(ResultMessage.DataSuccess, list, OperationEnumType.Success);
        }
    }
}
