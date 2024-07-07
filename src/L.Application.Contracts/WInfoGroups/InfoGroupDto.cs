using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using L.WInformations;

namespace L.WInfoGroups;
public class InfoGroupDto: EntityDto<long>
{
    #region 仅显示

    public List<InformationDto> Informations { get; set; }
    /// <summary>
    /// 关联的博客数量
    /// </summary>
    public int InforNum { get; set; }

    

    #endregion

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
    public DateTime CreationTime { get; set; }
    
}
/// <summary>
/// 获取数量使用
/// </summary>
public class InfoGroupNumDto: EntityDto<long>
{
    public int Num { get; set; }
}
