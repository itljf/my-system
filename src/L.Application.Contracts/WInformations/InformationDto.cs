using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;
using L.WInfoGroups;
using L.WInfoTags;

namespace L.WInformations;

public class InformationDto : EntityDto<long>
{
    #region 仅显示

    public DateTime? BindTime { get; set; }

    public List<InfoTagDto> InfoTagList { get; set; }
    public List<InfoGroupDto> InfoGroupList { get; set; }
    public int GroupNum { get; set; }
    public long InfoGroupItemId { get; set; }
    public long TagItemId { get; set; }
    public List<long> TagItemIdList
    {
        get { return JsonConvert.DeserializeObject<List<long>>(this.Tag ?? "[]") ?? new List<long>(); }
    }

    #endregion

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public InformationCode Code { get; set; }
    /// <summary>
    /// 时间
    /// </summary>
    public DateTime Time { get; set; }
    /// <summary>
    /// 是否为转载
    /// </summary>
    public bool IsReprint { get; set; }
    /// <summary>
    /// 笔记信息
    /// </summary>
    public string Info { get; set; }
    /// <summary>
    /// MD文件位置
    /// </summary>
    public string Markdown { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    public string Url { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string Cnt { get; set; }
    /// <summary>
    /// Tag
    /// </summary>
    public string Tag { get; set; }
    /// <summary>
    /// 博客首页文件
    /// </summary>
    public string BlogFiles { get; set; }
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
    /// <summary>
    /// 编辑器类型
    /// </summary>
    public EditorType EditorType { get; set; }
    public DateTime CreationTime { get; set; }
}
public class InformationBaseDto : EntityDto<long>
{
    #region 仅查看

    public bool IsSelect { get; set; }

    #endregion
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public InformationCode Code { get; set; }
    /// <summary>
    /// 时间
    /// </summary>
    public DateTime Time { get; set; }
    /// <summary>
    /// Tag
    /// </summary>
    public string Tag { get; set; }
}
