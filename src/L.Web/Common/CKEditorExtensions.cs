using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using System;

namespace L.Web.Common
{
    public static class CKEditorExtensions
    {
        // public static MvcHtmlString CropperUploader(this HtmlHelper helper,
        //     string name, CropperUploader model)
        // {
        //     return GenerateCropperUploaderHtml(helper, name, model);
        // }
        // public static MvcHtmlString CropperUploaderFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, CropperUploader model)
        // {
        //     if (model == null) { model = new CropperUploader(); }
        //     string name = ExpressionHelper.GetExpressionText(expression);
        //     var val = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData).Model;
        //     if (val != null) { model.Files = val.ToString(); }
        //     return GenerateCropperUploaderHtml(helper, name, model);
        // }
        // private static MvcHtmlString GenerateCropperUploaderHtml(this HtmlHelper html, string name, CropperUploader model)
        // {
        //     var map = new ViewDataDictionary();
        //     map["FileUploaderName"] = name;
        //     
        //     return html.Partial("_CropperUploader", model, map);
        //
        //
        // }
    }

    public class CKEditorHelper
    {
        /// <summary>
        /// 按钮文字
        /// </summary>
        public string Button { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Cnt { get; set; }
        
        /// <summary>
        /// 编辑器宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 编辑器高
        /// </summary>
        public int Height { get; set; }
    }
}
