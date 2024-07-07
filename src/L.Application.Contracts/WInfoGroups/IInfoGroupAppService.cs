using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace L.WInfoGroups
{
    public interface IInfoGroupAppService:IApplicationService
{
    Task<PagedList<InfoGroupDto>> GetPagedListAsync(GetInfoGroupInput input);

    /// <summary>
    /// 获取关联的数量
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    Task<List<InfoGroupNumDto>> GetNumListByIdListAsync(List<long> idList);

    /// <summary>
    /// 根据博客ID获取集合
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    Task<List<InfoGroupDto>> GetListByBlogId(long blogId);
    #region 增删改查

    /// <summary>
    /// 根据ID获取
    /// </summary>
    /// <returns></returns>
    Task<InfoGroupDto> GetById(long id);

    Task<InfoGroupDto> GetAllInfoById(long id);

    /// <summary>
    /// 根据博客信息搜索
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<InfoGroupItemDto>> GetAllInfoByBolgId(long id);

    /// <summary>
    /// 根据ID获取
    /// </summary>
    /// <returns></returns>
    Task<List<InfoGroupDto>> GetByIdList(List<long> idList);

    /// <summary>
    /// 获取所有
    /// </summary>
    /// <returns></returns>
    Task<List<InfoGroupDto>> GetAllList();

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<InfoGroupEditDto> Create(InfoGroupEditDto input);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    Task Edit(InfoGroupEditDto input);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    Task Delete(long id);

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    Task BatchDelete(List<long> idList);

    #endregion

    /// <summary>
    /// 给博客选择文单
    /// </summary>
    /// <param name="input"></param>
    Task SetGroutInfo(InfoGroupItemDto input);

    /// <summary>
    /// 给博客选择文单
    /// </summary>
    /// <param name="bgiId"></param>
    Task DeleteSetGroutInfo(long bgiId);

}
}
