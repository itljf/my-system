using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Auditing;

namespace L.WInfoGroups;
/// <summary>
/// 组 类似网易云的歌单
/// </summary>
[Table("TbInfoGroup")]
public class InfoGroup: FullAuditedAggregateRoot<long>
{
    // // 导航属性
    // public List<InfoGroupItem> InfoGroupItems { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public InfoGroupCode Code { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string Cnt { get; set; }
    /// <summary>
    /// Tag
    /// </summary>
    public string Tag { get; set; }
    /// <summary>
    /// 文件
    /// </summary>
    public string Files { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public int Top { get; set; }

}
