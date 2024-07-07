using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using L.WInformations;

namespace L.WInfoTags;


public class InfoTagItemDto: EntityDto<long>
{
    /// <summary>
    /// 博客ID
    /// </summary>
    public long BlogId { get; set; }
    public long TagId { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public int Top { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Cnt { get; set; }
    
}