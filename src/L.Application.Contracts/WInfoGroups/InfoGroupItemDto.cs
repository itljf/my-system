using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace L.WInfoGroups
{
    public class InfoGroupItemDto: EntityDto<long>
    {
        public long InfoGroupId { get; set; }

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
        // /// <summary>
        // /// 备注
        // /// </summary>
        // public string Cnt { get; set; }
    
    }
}
