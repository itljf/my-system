using System.Threading.Tasks;
using L.Localization;
using L.MultiTenancy;
using L.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace L.Web.Menus;

public class LMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<LResource>();

        var list = new System.Collections.Generic.List<ApplicationMenuItem>();
        list.Add(new ApplicationMenuItem(
            LMenus.Home,
            l["Menu:Home"],
            "~/",
            icon: "fas fa-home",
            order: 0
        ));
        //图书相关
        // list.Add(new ApplicationMenuItem(
        //     "WP.Books",
        //     l["Menu:Books"],
        //     url: "/Books",
        //     icon:"fas fa-address-book"
        // ));
        list.Add(new ApplicationMenuItem(
            LPermissions.Blogs.Default,
            l[LPermissions.Blogs.Default],
            url: "/Blog",
            icon:"fab fa-blogger"
        ));
        list.Add(new ApplicationMenuItem(
            LPermissions.Nav.Default,
            l[LPermissions.Nav.Default],
            url: "/Nav",
            icon:"far fa-bookmark"
        ));
        list.Add(new ApplicationMenuItem(
            LPermissions.BlogMgr.Default,
            l[LPermissions.BlogMgr.Default],
            url: "/BlogMgr",
            icon:"fab fa-blogger-b"
        ));
        list.Add(new ApplicationMenuItem(
            LPermissions.BlogAnalysis,
            l[LPermissions.BlogAnalysis],
            url: "/BlogAnalysis",
            icon:"fab fa-blogger-b"
        ));
        list.Add(new ApplicationMenuItem(
            LPermissions.TagMgr.Default,
            l[LPermissions.TagMgr.Default],
            url: "/TagMgr",
            icon:"fas fa-tags"
        ));
        list.Add(new ApplicationMenuItem(
            LPermissions.GroupMgr.Default,
            l[LPermissions.GroupMgr.Default],
            url: "/GroupMgr",
            icon:"far fa-object-group"
        ));
        list.Add(new ApplicationMenuItem(
            LPermissions.NavigationMgr.Default,
            l[LPermissions.NavigationMgr.Default],
            url: "/NavigationMgr",
            icon:"far fa-building"
        ));
        list.Add(new ApplicationMenuItem(
            LPermissions.Settings.Default,
            l[LPermissions.Settings.Default],
            url: "/Setting",
            icon:"fas fa-briefcase"
        ));
        list.Add(new ApplicationMenuItem(
            LPermissions.Todo,
            l[LPermissions.Todo],
            url: "/Todo",
            icon:"fas fa-briefcase"
        ));
        context.Menu.Items.InsertRange(0,list);


        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
