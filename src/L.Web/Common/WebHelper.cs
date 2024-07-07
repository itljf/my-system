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
}
