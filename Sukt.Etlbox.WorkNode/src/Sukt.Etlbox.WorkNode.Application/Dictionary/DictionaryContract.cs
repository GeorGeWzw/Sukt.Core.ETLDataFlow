﻿using Microsoft.Extensions.DependencyInjection;
using Sukt.Etlbox.WorkNode.Domain.Models.SystemFoundation.DataDictionary;
using Sukt.Etlbox.WorkNode.Domain.Repository.DomainRepository;
using Sukt.Etlbox.WorkNode.Dtos.DataDictionaryDto;
using SuktCore.Shared.Attributes.Dependency;
using SuktCore.Shared.Entity;
using SuktCore.Shared.Enums;
using SuktCore.Shared.Extensions;
using SuktCore.Shared.Extensions.ResultExtensions;
using SuktCore.Shared.OperationResult;
using SuktCore.Shared.ResultMessageConst;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sukt.Etlbox.WorkNode.Application
{
    /// <summary>
    /// 数据字典应用实现层
    /// </summary>
    [Dependency(ServiceLifetime.Scoped)]
    public class DictionaryContract : IDictionaryContract
    {
        private readonly IDataDictionaryRepository _dataDictionary;

        public DictionaryContract(IDataDictionaryRepository dataDictionary)
        {
            _dataDictionary = dataDictionary;
        }

        //[NonGlobalAopTran]
        public async Task<OperationResponse> InsertAsync(DataDictionaryInputDto input)
        {
            input.NotNull(nameof(input));
            return await _dataDictionary.InsertAsync(input);
        }

        public async Task<OperationResponse> UpdateAsync(DataDictionaryInputDto input)
        {
            var result = await _dataDictionary.UpdateAsync(input);
            return result;
        }

        public async Task<OperationResponse> DeleteAsync(Guid Id)
        {
            return await _dataDictionary.DeleteAsync(Id);
        }

        public async Task<IPageResult<DataDictionaryOutDto>> GetResultAsync(PageRequest query)
        {
            var result = await _dataDictionary.NoTrackEntities.ToPageAsync<DataDictionaryEntity, DataDictionaryOutDto>(query);
            return result;
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<OperationResponse> GetTreeAsync()
        {
            var list = await _dataDictionary.NoTrackEntities.ToTreeResultAsync<DataDictionaryEntity, TreeDictionaryOutDto>(
                (p, c) =>
                {
                    return c.ParentId == null || c.ParentId == Guid.Empty;
                },
                (p, c) =>
                {
                    return p.Id == c.ParentId;
                },
                (p, datalist) =>
                {
                    if (p.Children == null)
                    {
                        p.Children = new List<TreeDictionaryOutDto>();
                    }
                    p.Children.AddRange(datalist);
                }
                );
            OperationResponse operationResponse = new OperationResponse(ResultMessage.DataSuccess, list, OperationEnumType.Success);
            return operationResponse;
        }
    }
}