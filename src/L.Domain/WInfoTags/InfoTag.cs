using System;
using System.ComponentModel.DataAnnotations.Schema;
using L.WInfoTags;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace L.WInfoTags;

[Table("TbInfoTag")]
public class InfoTag: FullAuditedAggregateRoot<long>
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public int Mark { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public int Mark2 { get; set; }
    /// <summary>
    /// 上级ID
    /// </summary>
    public int Fid { get; set; }
    /// <summary>
    /// 指向地址
    /// </summary>
    public string Url { get; set; }
    /// <summary>
    /// 对应文件
    /// </summary>
    public string Files { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public InfoTagCode Code { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string Cnt { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public DateTime? Time { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public int Top { get; set; }

}
