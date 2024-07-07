using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Repositories;
using L.WInformations;

namespace L.WInfoGroups;
public class InfoGroupAppService(IRepository<InfoGroup, long> infoGroupRepository, IRepository<InfoGroupItem, long> infoGroupItemRepository, IRepository<Information, long> informationRepository)
    : LAppService, IInfoGroupAppService
{
    public async Task<PagedList<InfoGroupDto>> GetPagedListAsync(GetInfoGroupInput input)
    {
        var query = await infoGroupRepository.GetQueryableAsync();
        var query2 = await infoGroupItemRepository.GetQueryableAsync();
        var list = query
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), m => m.Title.Contains(input.Name))
            .OrderByDescending(m => m.CreationTime)
            .ToPagedList(input.CurrentPage ?? 1, input.PageSize ?? 16);
        var dtoList = ObjectMapper.Map<List<InfoGroup>, List<InfoGroupDto>>(list.ToList());

        //获取下对应的博客数量
        if (dtoList.Count > 0)
        {
            var idList = dtoList.Select(m => m.Id).ToList();

            var resultList = query2.Where(m => idList.Contains(m.InfoGroupId)).GroupBy(m => m.InfoGroupId)
                .Select(m => new { InfoGroupId = m.Key, num = m.Count() }).ToList();
            dtoList.ForEach(f => f.InforNum = resultList.Where(m => m.InfoGroupId == f.Id).Select(m => m.num).FirstOrDefault());
        }

        return new PagedList<InfoGroupDto>(dtoList, list.CurrentPageIndex, list.PageSize, list.TotalItemCount);
    }

    /// <summary>
    /// 获取关联的数量
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    public async Task<List<InfoGroupNumDto>> GetNumListByIdListAsync(List<long> idList)
    {
        var query = await infoGroupItemRepository.GetQueryableAsync();
        return query
            .Where(m => idList.Contains(m.Id))
            .GroupBy(m => m.InfoGroupId)
            .Select(m => new InfoGroupNumDto() { Id = m.Key, Num = m.Count() })
            .ToList();
    }

    /// <summary>
    /// 根据博客ID获取集合
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    public async Task<List<InfoGroupDto>> GetListByBlogId(long blogId)
    {
        var query = await infoGroupItemRepository.GetQueryableAsync();
        var query2 = await infoGroupRepository.GetQueryableAsync();
        var list = query
            .Where(m => m.BlogId == blogId)
            .Join(query2, m => m.InfoGroupId, n => n.Id, (m, n) => n)
            .ToList();

        return ObjectMapper.Map<List<InfoGroup>, List<InfoGroupDto>>(list);
    }

    #region 增删改查
    /// <summary>
    /// 根据ID获取
    /// </summary>
    /// <returns></returns>
    public async Task<InfoGroupDto> GetById(long id)
    {
        var entity = await infoGroupRepository.GetAsync(id);
        return ObjectMapper.Map<InfoGroup, InfoGroupDto>(entity);
    }

    public async Task<InfoGroupDto> GetAllInfoById(long id)
    {
        var model = await infoGroupRepository.GetAsync(id);
        var query = await informationRepository.GetQueryableAsync();
        var query2 = await infoGroupItemRepository.GetQueryableAsync();

        if (model == null) return null;
        var dto = ObjectMapper.Map<InfoGroup, InfoGroupDto>(model);
        dto.Informations = query
            .Join(query2.Where(m => m.InfoGroupId == id), m => m.Id, n => n.BlogId,
                (m, n) => new InformationDto() { Id = m.Id, Title = m.Title, CreationTime = m.CreationTime, BindTime = n.CreationTime, Tag = m.Tag, Author = m.Author, Time = m.Time, InfoGroupItemId = n.Id }).ToList();
        if (dto.Informations.Count > 0)
        {
            var idList = dto.Informations.Select(m => m.Id).ToList();
            var groupList = query2.Where(m => idList.Contains(m.BlogId)).GroupBy(m => m.BlogId)
                .Select(m => new { blogId = m.Key, num = m.Count() }).ToList();
            dto.Informations.ForEach(f => f.GroupNum = groupList.FirstOrDefault(m => m.blogId == f.Id)?.num ?? 0);
        }

        return dto;
    }
    /// <summary>
    /// 根据博客信息搜索
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<List<InfoGroupItemDto>> GetAllInfoByBolgId(long id)
    {
        var query = await infoGroupItemRepository.GetQueryableAsync();
        var list = query.Where(m => m.BlogId == id).ToList();

        return ObjectMapper.Map<List<InfoGroupItem>, List<InfoGroupItemDto>>(list);
    }


    /// <summary>
    /// 根据ID获取
    /// </summary>
    /// <returns></returns>
    public async Task<List<InfoGroupDto>> GetByIdList(List<long> idList)
    {
        var query = await infoGroupRepository.GetQueryableAsync();
        var list = query.Where(m => idList.Contains(m.Id)).ToList();
        return ObjectMapper.Map<List<InfoGroup>, List<InfoGroupDto>>(list);
    }
    /// <summary>
    /// 获取所有
    /// </summary>
    /// <returns></returns>
    public async Task<List<InfoGroupDto>> GetAllList()
    {
        var query = await infoGroupRepository.GetQueryableAsync();
        var list = query.ToList();

        return ObjectMapper.Map<List<InfoGroup>, List<InfoGroupDto>>(list);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Audited]
    public async Task<InfoGroupEditDto> Create(InfoGroupEditDto input)
    {
        input.Files = "";
        input.Tag = "";
        var model = ObjectMapper.Map<InfoGroupEditDto, InfoGroup>(input);
        await infoGroupRepository.InsertAsync(model);
        return input;
    }
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    [Audited]
    public async Task Edit(InfoGroupEditDto input)
    {
        var model = await infoGroupRepository.GetAsync(input.Id);
        input.Tag = model.Tag;
        input.Files = input.Files ?? model.Files;
        ObjectMapper.Map(input, model);

        await infoGroupRepository.UpdateAsync(model);
    }
    ///// <summary>
    ///// 自定义编辑
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="updateAction"></param>
    //[Audited]
    //public void Update(long id,Action<InfoGroup> updateAction)
    //{
    //    infoGroupRepository.UpdateAsync(id,updateAction);
    //}
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    [Audited]
    public async Task Delete(long id)
    {
        await infoGroupRepository.DeleteAsync(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    [Audited]
    public async Task BatchDelete(List<long> idList)
    {
        await infoGroupRepository.DeleteAsync(m => idList.Contains(m.Id));
    }
    #endregion

    /// <summary>
    /// 给博客选择文单
    /// </summary>
    /// <param name="input"></param>
    public async Task SetGroutInfo(InfoGroupItemDto input)
    {
        var model = new InfoGroupItem();
        model.BlogId = input.BlogId;
        model.InfoGroupId = input.InfoGroupId;
        model.IsEnable = true;
        model.Cnt = "";

        await infoGroupItemRepository.InsertAsync(model);
    }

    /// <summary>
    /// 给博客移除文单关联
    /// </summary>
    /// <param name="bgiId"></param>
    public async Task DeleteSetGroutInfo(long bgiId)
    {
        await infoGroupItemRepository.DeleteAsync(bgiId);
    }

}
