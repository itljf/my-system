using L.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace L.Permissions;

public class LPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(LPermissions.MyPermission1, L("Permission:MyPermission1"));
        var booksPermission = myGroup.AddPermission(LPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(LPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(LPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(LPermissions.Books.Delete, L("Permission:Books.Delete"));

        myGroup.AddPermission(LPermissions.Blogs.Default, L(LPermissions.Blogs.Default));
        myGroup.AddPermission(LPermissions.BlogMgr.Default, L(LPermissions.BlogMgr.Default));
        myGroup.AddPermission(LPermissions.TagMgr.Default, L(LPermissions.TagMgr.Default));
        myGroup.AddPermission(LPermissions.GroupMgr.Default, L(LPermissions.GroupMgr.Default));
        myGroup.AddPermission(LPermissions.NavigationMgr.Default, L(LPermissions.NavigationMgr.Default));
        myGroup.AddPermission(LPermissions.Nav.Default, L(LPermissions.Nav.Default));
        myGroup.AddPermission(LPermissions.Settings.Default, L(LPermissions.Settings.Default));

        myGroup.AddPermission(LPermissions.Setting.SystemName, L(LPermissions.Setting.SystemName));
        myGroup.AddPermission(LPermissions.BlogAnalysis, L(LPermissions.BlogAnalysis));
        myGroup.AddPermission(LPermissions.Todo, L(LPermissions.Todo));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LResource>(name);
    }
}
