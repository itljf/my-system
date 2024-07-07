using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace L.WInfoGroups;
/// <summary>
/// 组 类似网易云的歌单
/// </summary>
[Table("TbInfoGroupItem")]
public class InfoGroupItem: FullAuditedAggregateRoot<long>
{
    public long InfoGroupId { get; set; }
    // public InfoGroup InfoGroup { get; set; }

    /// <summary>
    /// 博客ID
    /// </summary>
    public long BlogId { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public int Top { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Cnt { get; set; }

}