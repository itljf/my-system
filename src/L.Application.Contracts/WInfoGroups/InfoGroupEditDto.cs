using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace L.WInfoGroups
{
    public class InfoGroupEditDto: EntityDto<long>
    {
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

}
