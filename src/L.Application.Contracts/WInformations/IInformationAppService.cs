using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace L.WInformations
{
    public interface IInformationAppService: IApplicationService
    {
        Task<List<InformationDto>> GetList();

        Task<PagedList<InformationDto>> GetPagedList(GetInformationInput input);

        Task<InformationDto> Get(long id);
        Task<InformationDto> Create(InformationEditDto input);
        Task<InformationDto> Update(long id, InformationEditDto input);
        Task Delete(long id);

        /// <summary>
        /// 根据标题搜索
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<List<InformationDto>> GetListByTitle(string title);

        Task<bool> HasByUrl(string url);

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<InformationDto> UpdateTag(long id, InformationEditInfoDto input);

        /// <summary>
        /// 修改内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<InformationDto> UpdateInfo(long id, InformationEditInfoDto input);

        /// <summary>
        /// 根据标签搜索
        /// </summary>
        /// <param name="tagIdList"></param>
        /// <returns></returns>
        Task<List<InformationDto>> GetListByTags(List<long> tagIdList);

        #region 全文检索
        
        Task ResetLucene();
        /// <summary>
        /// 全文检索
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedList<InformationDto>> SearchByLucene(GetInformationInput input);
        
        #endregion
    }
}
