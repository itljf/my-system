using System;
using System.Linq;
using Volo.Abp.UI.Navigation;

namespace L.Web.Common;
public static class WebHelper
{
    public static bool HasActive(ApplicationMenuItem item,string name)
    {
        if (item.Name==name)
        {
            return true;
        }else if (item.Items!=null)
        {
            foreach (var item1 in item.Items)
            {
                if (HasActive(item1,name))
                {
                    return true;
                }
            }
        }{ return false; }
    }

    public static string GetQueryStr(this Microsoft.AspNetCore.Http.HttpContext context)
    {
        var queryCollection = context.Request.Query;

        return string.Join("&", queryCollection.Keys.Where(m=>!m.Equals("currentPage",StringComparison.InvariantCultureIgnoreCase)).Select(k => $"{k}={queryCollection[k]}"));
    }
}
