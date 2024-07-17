using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace L.WInfoTags;
public interface IInfoTagAppService:IApplicationService
{
    Task<PagedList<InfoTagDto>> GetPagedList(GetInfoTagInput input);

    /// <summary>
    /// 获取关联的数量
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    Task<List<InfoTagNumDto>> GetNumListByIdList(List<long> idList);

    Task<InfoTagDto> GetAllInfoById(long id);

    /// <summary>
    /// 根据博客ID获取TagId
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    Task<List<long>> GetTagIdListByBlogId(long blogId);

    #region 增删改查

    /// <summary>
    /// 根据ID获取
    /// </summary>
    /// <returns></returns>
    Task<InfoTagDto> GetById(long id);

    /// <summary>
    /// 根据ID获取
    /// </summary>
    /// <returns></returns>
    Task<List<InfoTagDto>> GetByIdList(List<long> idList);

    /// <summary>
    /// 获取所有
    /// </summary>
    /// <returns></returns>
    Task<List<InfoTagDto>> GetAllList();

    Task<List<InfoTagDto>> GetListByCode(InfoTagCode code);

    /// <summary>
    /// 根据博客ID获取集合
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    Task<List<InfoTagDto>> GetListByBlogId(long blogId);
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<InfoTagEditDto> Create(InfoTagEditDto input);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    Task Edit(InfoTagEditDto input);
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
    Task SetGroutInfo(InfoTagItemDto input);

    /// <summary>
    /// 给博客移除文单关联
    /// </summary>
    /// <param name="bgiId"></param>
    Task DeleteSetGroutInfo(long bgiId);

    #region 标签处理

    /// <summary>
    /// 获取所有待办信息
    /// </summary>
    /// <returns></returns>
    Task<List<InfoTagDto>> GetAllTag();

        #endregion

    #region 置顶

    /// <summary>
    /// 设置置顶
    /// </summary>
    /// <param name="id"></param>
    Task SetTop(long id);

    /// <summary>
    /// 取消置顶
    /// </summary>
    /// <param name="id"></param>
    Task CloseTop(long id);

    #endregion


    
    #region 导航目录

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<InfoTagEditDto> CreateNavigation(InfoTagEditDto input);

    /// <summary>
    /// 获取目录导航
    /// </summary>
    /// <returns></returns>
    Task<List<InfoTagDto>> GetNavigation();

    /// <summary>
    /// 删除导航信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="num">1:1级导航，2:2级导航，3:3级导航</param>
    Task DeleteNavigation(long id, int num);

    #endregion

    #region 待办信息

    /// <summary>
    /// 获取所有待办信息
    /// </summary>
    /// <returns></returns>
    Task<List<InfoTagDto>> GetAllTodo();

    #endregion
}
