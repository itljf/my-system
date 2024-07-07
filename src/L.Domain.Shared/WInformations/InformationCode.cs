using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace L.WInformations
{
    public enum InformationCode
    {
        /// <summary>
        /// 博客信息
        /// </summary>
        blog = 0,
    }
    public enum EditorType
    {
        /// <summary>
        /// 富文本编辑器
        /// </summary>
        [Description("富文本编辑器")]
        CKEditor=0,
        /// <summary>
        /// Markdown编辑器
        /// </summary>
        [Description("Markdown编辑器")]
        Markdown =5,
    }
}
