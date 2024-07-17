using Examine;
using L.Common;
using L.WInfoTags;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examine.Lucene.Search;
using Examine.Search;
using HtmlAgilityPack;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Entities;

namespace L.WInformations
{
    public class InformationAppService(IRepository<Information, long> informationRepository,IRepository<InfoTagItem, long> infoTagItemRepository,IExamineManager examineManager)
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
                .OrderByDescending(m=>m.CreationTime).ToPagedList(input.CurrentPage??1,input.PageSize??10);
            var dto = ObjectMapper.Map<List<Information>,List<InformationDto>>(list.ToList());
            return new PagedList<InformationDto>(dto,list.CurrentPageIndex,list.PageSize,list.TotalItemCount);
        }

        public async Task<InformationDto> Get(long id)
        {
            var entity = await informationRepository.GetAsync(id);
            var dto = ObjectMapper.Map<Information,InformationDto>(entity);
            return dto;
        }
        public async Task<InformationDto> Create(InformationEditDto input)
        {
            var entity = ObjectMapper.Map<InformationEditDto, Information>(input);

            entity = await informationRepository.InsertAsync(entity);
            //处理一下标签
            var tagList = JsonConvert.DeserializeObject<List<long>>(input.Tag ?? "[]") ?? new List<long>();
            var tagItemList = tagList.Select(m => new InfoTagItem()
            {
                BlogId = entity.Id,
                TagId = m,
                Cnt = ""
            }).ToList();
            await infoTagItemRepository.InsertManyAsync(tagItemList);

            UpdateSearch(entity);
            return ObjectMapper.Map<Information, InformationDto>(entity);
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
            UpdateSearch(entity);
            return ObjectMapper.Map<Information,InformationDto>(entity);
        }
        public async Task Delete(long id)
        {
            await informationRepository.DeleteAsync(id);
            DeleteSearch(id);
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
        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<InformationDto> UpdateTag(long id,InformationEditInfoDto input)
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
            entity.Tag = input.Tag;
            await informationRepository.UpdateAsync(entity);
            return ObjectMapper.Map<Information,InformationDto>(entity);
        }
        /// <summary>
        /// 修改内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<InformationDto> UpdateInfo(long id,InformationEditInfoDto input)
        {
            var entity = await informationRepository.GetAsync(id);
            entity.Info = input.Info;
            await informationRepository.UpdateAsync(entity);
            UpdateSearch(entity);
            return ObjectMapper.Map<Information,InformationDto>(entity);
        }

        /// <summary>
        /// 根据标签搜索
        /// </summary>
        /// <param name="tagIdList"></param>
        /// <returns></returns>
        public async Task<List<InformationDto>> GetListByTags(List<long> tagIdList)
        {
            var query = await informationRepository.GetQueryableAsync();
            var query2 = await infoTagItemRepository.GetQueryableAsync();

            var list = query.Join(query2.Where(m => tagIdList.Contains(m.TagId)), m => m.Id, n => n.BlogId, (m, n) => m)
                .Distinct()
                .OrderByDescending(m => m.CreationTime)
                .ToList();
            return ObjectMapper.Map<List<Information>, List<InformationDto>>(list);

        }

        #region 全文检索

        public async Task ResetLucene()
        {
            var list = await informationRepository.GetListAsync();
            foreach (var item in list)
            {
                UpdateSearch(item);
            }
        }

        /// <summary>
        /// 全文检索
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedList<InformationDto>> SearchByLucene(GetInformationInput input)
        {
            var myIndex = examineManager.GetIndex("MyIndex");
            var searcher = myIndex.Searcher; // 获取收件箱
            int take = input.PageSize??10;
            int skip = (input.CurrentPage ?? 1) * take - take;
            
            var results = searcher.CreateQuery("content")  // Create a query
                .ManagedQuery(input.Name)
                .Execute(QueryOptions.SkipTake(skip,take));              // 执行搜索
            List<long> idList=new List<long>();
            foreach (var item in results)
            {
                idList.Add(Convert.ToInt64(item.Id.Split('_').LastOrDefault()));
            }
            var query = await informationRepository.GetQueryableAsync();
            var list = query.Where(m => idList.Contains(m.Id)).ToList();
            
            var dto = ObjectMapper.Map<List<Information>,List<InformationDto>>(list.ToList());
            dto = dto.OrderBy(m => idList.IndexOf(m.Id)).ToList();

            return new PagedList<InformationDto>(dto,input.CurrentPage??1,take,(int)results.TotalItemCount);

        }

        /// <summary>
        /// 更新缓存的数据
        /// </summary>
        /// <param name="info"></param>
        public void UpdateSearch(Information info)
        {
            var myIndex = examineManager.GetIndex("MyIndex");
            
            myIndex.IndexItem(new ValueSet(
                "information_"+info.Id,  //向医生提供您选择的ID
                "content",               //每个文档都有一个“类别”
                new Dictionary<string, object>()
                {
                    {"Title", info.Title },
                    {"Info", RemoveHtml(info.Info) },
                    {"Time", info.Time },
                    {"Cnt", RemoveHtml(info.Cnt) },
                }));
        }
        /// <summary>
        /// 删除缓存的数据
        /// </summary>
        /// <param name="id">根据ID删除</param>
        public void DeleteSearch(long id)
        {
            var myIndex = examineManager.GetIndex("MyIndex");
            myIndex.DeleteFromIndex("information_"+id);
        }

        public string RemoveHtml(string html)
        {
            if (html.IsNullOrWhiteSpace())
            {
                return "";
            }
            if (html.Contains('<'))
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                return doc.DocumentNode.InnerText;
            }
            else
            {
                return html;
            }
        }

        #endregion
    }
}
