﻿using DestinyCore.ETLDispatchCenter.Dtos.DataDictionaryDto;
using DestinyCore.ETLDispatchCenter.Dtos.DBConnResourceDto;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuktCore.Shared;
using SuktCore.Shared.Entity;
using SuktCore.Shared.Extensions.ResultExtensions;
using SuktCore.Shared.OperationResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DestinyCore.ETLDispatchCenter.Application.DBConnResource
{
    public interface IDBConnectionContract : IScopedDependency
    {
        /// <summary>
        /// 添加数据连接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateAsync(DBConnResourceInputDto input);
        /// <summary>
        /// 修改数据连接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> UpdateAsync(DBConnResourceInputDto input);
        /// <summary>
        /// 表单加载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> GetLoadAsync(Guid id);
        /// <summary>
        /// 分页加载
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IPageResult<DBConnResourcePageOutPutDto>> GetPageLoadAsync(PageRequest request);
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
        /// <summary>
        /// 导入元数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> ImportMetaDataAsync(MetaDataImportInputDto input);
        /// <summary>
        /// 获取元数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> GetLoadMetaDataAsync(Guid Id);
    }
}
