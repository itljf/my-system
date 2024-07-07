using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace L.WInformations
{
    [Table("TbInformation")]
    public class Information:FullAuditedAggregateRoot<long>
    {
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
        /// 简介
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
    }
}
