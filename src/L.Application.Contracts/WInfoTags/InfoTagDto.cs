using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using L.WInformations;

namespace L.WInfoTags;


public class InfoTagDto: EntityDto<long>
{
    
    #region 仅显示
    /// <summary>
    /// 子集
    /// </summary>
    public List<InfoTagDto> sub { get; set; }
    public List<InformationDto> Informations { get; set; }
    public int InforNum { get; set; }

    #endregion
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
    [CanBeNull]
    public string Cnt { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public int Top { get; set; }
    public DateTime CreationTime { get; set; }
    
}
/// <summary>
/// 获取数量使用
/// </summary>
public class InfoTagNumDto: EntityDto<long>
{
    public int Num { get; set; }
}