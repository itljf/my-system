using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Repositories;
using L.Common;
using L.WInformations;

namespace L.WInfoTags;

public class InfoTagAppService(IRepository<InfoTag, long> infoTagRepository, IRepository<InfoTagItem, long> infoTagItemRepository, IRepository<Information, long> informationRepository)
    : LAppService, IInfoTagAppService
{


    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<InfoTagDto>> GetPagedList(GetInfoTagInput input)
    {
        var query = await infoTagRepository.GetQueryableAsync();
        var query2 = await infoTagItemRepository.GetQueryableAsync();

        var list = query
            .WhereIf(!input.Name.IsNullOrWhiteSpace(),m=>m.Title.Contains(input.Name))
            .WhereIf(input.Code.HasValue,m=>m.Code == input.Code)
            .OrderByDescending(m => m.Top)
            .ThenBy(m=>m.CreationTime)
            .ToPagedList(input.CurrentPage??1,input.PageSize??16);
        
        var dtoList = ObjectMapper.Map<List<InfoTag>,List<InfoTagDto>>(list.ToList());
        
        //获取下对应的博客数量
        if (dtoList.Count > 0)
        {
            var idList = dtoList.Select(m => m.Id).ToList();
            var resultList = query2.Where(m => idList.Contains(m.TagId)).GroupBy(m => m.TagId)
                .Select(m => new { TagId = m.Key, num = m.Count() }).ToList();
            dtoList.ForEach(f=>f.InforNum=resultList.Where(m=>m.TagId==f.Id).Select(m=>m.num).FirstOrDefault());
        }
        return new PagedList<InfoTagDto>(dtoList,list.CurrentPageIndex,list.PageSize,list.TotalItemCount);
    }

    /// <summary>
    /// 获取关联的数量
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    public async Task<List<InfoTagNumDto>> GetNumListByIdList(List<long> idList)
    {
        var query = await infoTagItemRepository.GetQueryableAsync();
        return query
            .Where(m => idList.Contains(m.TagId))
            .GroupBy(m => m.TagId)
            .Select(m => new InfoTagNumDto() { Id = m.Key, Num = m.Count() })
            .ToList();
    }
    
    public async Task<InfoTagDto> GetAllInfoById(long id)
    {
        var query = await informationRepository.GetQueryableAsync();
        var query2 = await infoTagItemRepository.GetQueryableAsync();

        var model = await infoTagRepository.FirstOrDefaultAsync(m=>m.Id == id);
        if (model == null) return null;
        var dto = ObjectMapper.Map<InfoTag,InfoTagDto>(model);
        dto.Informations = query
            .Join(query2.Where(m => m.TagId == id), m => m.Id, n => n.BlogId,
                (m, n) => new InformationDto() { Id = m.Id, Time = m.Time,Title = m.Title, CreationTime = m.CreationTime,BindTime=n.CreationTime,TagItemId=n.Id,Tag=m.Tag }).ToList();

        return dto;
    }

    /// <summary>
    /// 根据博客ID获取TagId
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    public async Task<List<long>> GetTagIdListByBlogId(long blogId)
    {
        var query = await infoTagItemRepository.GetQueryableAsync();
        return query.Where(m => m.BlogId == blogId).Select(m => m.TagId).ToList();
    }

    #region 增删改查
    /// <summary>
    /// 根据ID获取
    /// </summary>
    /// <returns></returns>
    public async Task<InfoTagDto> GetById(long id)
    {
        var model = await infoTagRepository.FirstOrDefaultAsync(m=>m.Id==id);
        return ObjectMapper.Map<InfoTag,InfoTagDto>(model);

    }
    /// <summary>
    /// 根据ID获取
    /// </summary>
    /// <returns></returns>
    public async Task<List<InfoTagDto>> GetByIdList(List<long> idList)
    {
        var query = await infoTagRepository.GetQueryableAsync();
        var list = query.Where(m=>idList.Contains(m.Id)).ToList();
        return ObjectMapper.Map<List<InfoTag>,List<InfoTagDto>>(list);

    }
    /// <summary>
    /// 获取所有
    /// </summary>
    /// <returns></returns>
    public async Task<List<InfoTagDto>> GetAllList()
    {
        var query = await infoTagRepository.GetQueryableAsync();
        var list = query.ToList();

        return ObjectMapper.Map<List<InfoTag>,List<InfoTagDto>>(list);
    }
    public async Task<List<InfoTagDto>> GetListByCode(InfoTagCode code)
    {
        var query = await infoTagRepository.GetQueryableAsync();
        var list = query.Where(m=>m.Code==code).ToList();

        return ObjectMapper.Map<List<InfoTag>,List<InfoTagDto>>(list);
    }
    /// <summary>
    /// 根据博客ID获取集合
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    public async Task<List<InfoTagDto>> GetListByBlogId(long blogId)
    {
        var query = await infoTagRepository.GetQueryableAsync();
        var query2 = await infoTagItemRepository.GetQueryableAsync();
        var list = query2.Where(m=>m.BlogId==blogId)
            .Join(query,m=>m.TagId,n=>n.Id,(m,n)=>n)
            .ToList();

        return ObjectMapper.Map<List<InfoTag>,List<InfoTagDto>>(list);
    } 

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Audited]
    public async Task<InfoTagEditDto> Create(InfoTagEditDto input)
    {
        var model = ObjectMapper.Map<InfoTagEditDto,InfoTag>(input);
        await infoTagRepository.InsertAsync(model);
        return input;
    }
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    [Audited]
    public async Task Edit(InfoTagEditDto input)
    {
        var model = await infoTagRepository.GetAsync(input.Id);

        ObjectMapper.Map(input, model);

        await infoTagRepository.UpdateAsync(model);
    }
    ///// <summary>
    ///// 自定义编辑
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="updateAction"></param>
    //[Audited]
    //public void Update(long id,Action<InfoTag> updateAction)
    //{
    //    _infoTagRepository.UpdateAsync(id,updateAction);
    //}
    /// <summary>
    /// 删除标签类
    /// </summary>
    /// <param name="id"></param>
    [Audited]
    public async Task Delete(long id)
    {
        await infoTagRepository.DeleteAsync(m=>m.Id==id && m.Code == InfoTagCode.blog);
        await infoTagItemRepository.DeleteAsync(m => m.TagId==id);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    [Audited]
    public async Task BatchDelete(List<long> idList)
    {
        await infoTagRepository.DeleteAsync(m => idList.Contains(m.Id));
    }


    #endregion

    /// <summary>
    /// 给博客选择文单
    /// </summary>
    /// <param name="input"></param>
    public async Task SetGroutInfo(InfoTagItemDto input)
    {
        var model = new InfoTagItem();
        model.BlogId = input.BlogId;
        model.TagId = input.TagId;
        
        await infoTagItemRepository.InsertAsync(model);
    }

    /// <summary>
    /// 给博客移除文单关联
    /// </summary>
    /// <param name="bgiId"></param>
    public async Task DeleteSetGroutInfo(long bgiId)
    {
        await infoTagItemRepository.DeleteAsync(bgiId);
    }

    #region 置顶

    /// <summary>
    /// 设置置顶
    /// </summary>
    /// <param name="id"></param>
    public async Task SetTop(long id)
    {
        var model = await infoTagRepository.GetAsync(id);
        model.Top = TimeHelper.ConvertDateTimeInt(DateTime.Now);

        await infoTagRepository.UpdateAsync(model);
    }

    /// <summary>
    /// 取消置顶
    /// </summary>
    /// <param name="id"></param>
    public async Task CloseTop(long id)
    {
        var model = await infoTagRepository.GetAsync(id);
        model.Top = 0;

        await infoTagRepository.UpdateAsync(model);
    }

    #endregion


    #region 导航目录
    
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Audited]
    public async Task<InfoTagEditDto> CreateNavigation(InfoTagEditDto input)
    {
        var model = ObjectMapper.Map<InfoTagEditDto,InfoTag>(input);
        model.Code = InfoTagCode.navigation;
        await infoTagRepository.InsertAsync(model);
        return input;
    }
    /// <summary>
    /// 获取目录导航
    /// </summary>
    /// <returns></returns>
    public async Task<List<InfoTagDto>> GetNavigation()
    {
        var query = await infoTagRepository.GetQueryableAsync();
        var list = query
            .Where(m => m.Code == InfoTagCode.navigation)
            .ToList();
        var list2 = GetSub(ObjectMapper.Map<List<InfoTag>,List<InfoTagDto>>(list), 0);
        return list2;
    }

    public List<InfoTagDto> GetSub(List<InfoTagDto> list, long fid)
    {
        var list2 = list.Where(m => m.Fid == fid).ToList();
        foreach (var item in list2)
        {
            if (list.Any(m=>m.Fid==item.Id))
            {
                item.sub = GetSub(list, item.Id);
            }
        }

        return list2;
    }

    /// <summary>
    /// 删除导航信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="num">1:1级导航，2:2级导航，3:3级导航</param>
    public async Task DeleteNavigation(long id, int num)
    {
        await infoTagRepository.DeleteAsync(m=>m.Id == id && m.Code == InfoTagCode.navigation);
        if (num==1 || num==2)
        {
            var query = await infoTagRepository.GetQueryableAsync();
            var itemIdList = query.Where(m => m.Fid == id && m.Code == InfoTagCode.navigation).Select(m => m.Id).ToList();
            await infoTagRepository.DeleteAsync(m => itemIdList.Contains(m.Id) || itemIdList.Contains(m.Fid));
        }
    }


    #endregion


    #region 待办信息
    /// <summary>
    /// 获取所有待办信息
    /// </summary>
    /// <returns></returns>
    public async Task<List<InfoTagDto>> GetAllTodo()
    {
        var query = await infoTagRepository.GetQueryableAsync();
        var list = query.Where(m => m.Code == InfoTagCode.todo).OrderByDescending(m => m.Time)
            .ThenByDescending(m => m.CreationTime).ToList();
        return ObjectMapper.Map<List<InfoTag>, List<InfoTagDto>>(list);
    }

    #endregion
}
