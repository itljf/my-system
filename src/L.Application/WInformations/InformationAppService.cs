using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using L.Common;
using L.WInfoTags;

namespace L.WInformations
{
    public class InformationAppService(IRepository<Information, long> informationRepository,IRepository<InfoTagItem, long> infoTagItemRepository)
        : LAppService, IInformationAppService
    {
        public async Task<List<InformationDto>> GetList()
        {
            var query = await informationRepository.GetQueryableAsync();
            var list = query.ToList();
            return ObjectMapper.Map<List<Information>,List<InformationDto>>(list);
        }
        public async Task<PagedList<InformationDto>> GetPagedList(GetInformationInput input)
        {
            var query = await informationRepository.GetQueryableAsync();

            var list = query.WhereIf(!input.Name.IsNullOrWhiteSpace(),m=>m.Title.Contains(input.Name) || m.Author.Contains(input.Name))
                .OrderByDescending(m=>m.CreationTime).ToPagedList(input.CurrentPage??1,input.PageSize??16);
            var dto = ObjectMapper.Map<List<Information>,List<InformationDto>>(list.ToList());
            return new PagedList<InformationDto>(dto,list.CurrentPageIndex,list.PageSize,list.TotalItemCount);
        }

        public async Task<InformationDto> Get(long id)
        {
            var entity = await informationRepository.GetAsync(id);
            return ObjectMapper.Map<Information,InformationDto>(entity);
        }
        public async Task<InformationDto> Create(InformationEditDto input)
        {
            var entity = ObjectMapper.Map<InformationEditDto,Information>(input);

             entity = await informationRepository.InsertAsync(entity);
             //处理一下标签
             var tagList = JsonConvert.DeserializeObject<List<long>>(input.Tag ?? "[]") ?? new List<long>();
             var tagItemList = tagList.Select(m => new InfoTagItem()
             {
                 BlogId = entity.Id, TagId = m,Cnt = ""
             }).ToList();
             await infoTagItemRepository.InsertManyAsync(tagItemList);
            return ObjectMapper.Map<Information,InformationDto>(entity);
        }
        public async Task<InformationDto> Update(long id,InformationEditDto input)
        {
            var entity = await informationRepository.GetAsync(id);
            
            //处理一下标签
            var tagList = JsonConvert.DeserializeObject<List<long>>(input.Tag ?? "[]") ?? new List<long>();
            await infoTagItemRepository.DeleteAsync(m => m.BlogId == entity.Id,true);
            // await CurrentUnitOfWork.SaveChangesAsync();
            var tagItemList = tagList.Select(m => new InfoTagItem()
            {
                BlogId = entity.Id, TagId = m,Cnt = ""
            }).ToList();
            await infoTagItemRepository.InsertManyAsync(tagItemList);

            input.EditorType=entity.EditorType;
            input.Code=entity.Code;
            input.IsEnable=entity.IsEnable;
            input.IsEnable=entity.IsEnable;
            input.BlogFiles= input.BlogFiles ?? entity.BlogFiles;
            if (input.EditorType == EditorType.Markdown)
            {
                input.Cnt = MDHelper.ToHtml(input.Markdown);
            }

            ObjectMapper.Map(input,entity);
            await informationRepository.UpdateAsync(entity);
            return ObjectMapper.Map<Information,InformationDto>(entity);
        }
        public async Task Delete(long id)
        {
            await informationRepository.DeleteAsync(id);
        }
        /// <summary>
        /// 根据标题搜索
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<List<InformationDto>> GetListByTitle(string title)
        {
            var query = await informationRepository.GetQueryableAsync();
            var list = query
                .Where(m=>m.Title.Contains(title))
                .OrderByDescending(m=>m.Top)
                .ThenByDescending(m=>m.CreationTime)
                .Take(10).ToList();

            return ObjectMapper.Map<List<Information>,List<InformationDto>>(list);
        }

        public async Task<bool> HasByUrl(string url)
        {
            return await informationRepository.AnyAsync(m => m.Url == url);
        }

    }
}
