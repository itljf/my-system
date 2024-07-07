using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace L.WInfoTags;

[Table("TbInfoTagItem")]
public class InfoTagItem: FullAuditedAggregateRoot<long>
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