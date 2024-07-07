using System;
using System.Collections.Generic;
using System.Text;

namespace L.WInfoTags;
public class GetInfoTagInput :MvcPagerDto
{
    public string Name { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public InfoTagCode? Code { get; set; }
}
