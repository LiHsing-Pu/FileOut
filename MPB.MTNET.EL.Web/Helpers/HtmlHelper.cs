using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MPB.MTNET.EL.Parameter.ViewModel;
using MPB.MTNET.EL.Parameter.SearchModel;


namespace MPB.MTNET.EL.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        #region 下拉式選單
        public static MvcHtmlString MicsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes = null)
        {
            var exp = expression.Body as MemberExpression;
            string id = exp.Member.Name;
            var radioVaule = (string)ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;

            return MicsDropDownList(htmlHelper, id, selectList, optionLabel, htmlAttributes);
        }

        public static MvcHtmlString MicsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes = null)
        {
            return MicsDropDownListFor(htmlHelper, expression, selectList, null, htmlAttributes);
        }

        public static MvcHtmlString MicsDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes = null)
        {
            var attrs = ConvertToDictionary(htmlAttributes);

            return MicsDropDownList(htmlHelper, name, selectList, optionLabel, attrs);
        }

        public static MvcHtmlString MicsDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("div");
            var dropdownHtml = htmlHelper.DropDownList(name, selectList, optionLabel, htmlAttributes).ToString();

            //tag.AddCssClass("selectbox");
            tag.InnerHtml = dropdownHtml;

            return new MvcHtmlString(tag.ToString());
        }









        #endregion

        #region 文字方塊
        public static MvcHtmlString MicsTextBox(this HtmlHelper htmlHelper, string name, object value, string format, object htmlAttributes = null)
        {
            TagBuilder tag = new TagBuilder("div");
            var textBoxHtml = htmlHelper.TextBox(name, value, format, htmlAttributes).ToHtmlString();

            tag.AddCssClass("inputbox");
            tag.InnerHtml = textBoxHtml;

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString MicsTextBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null)
        {
            return MicsTextBox(htmlHelper, name, value, string.Empty, htmlAttributes);
        }

        public static MvcHtmlString MicsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, object htmlAttributes = null)
        {
            var exp = expression.Body as MemberExpression;
            string name = exp.Member.Name;
            var value = (string)ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;

            return MicsTextBox(htmlHelper, name, value, format, htmlAttributes);
        }

        public static MvcHtmlString MicsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return MicsTextBoxFor(htmlHelper, expression, string.Empty, htmlAttributes);
        }
        #endregion

        #region RadioButton
        public static MvcHtmlString MicsRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var html = string.Empty;

            if (items == null)
            {
                throw new Exception("Radio Button至少須有一個選項值!");
            }

            foreach (var item in items)
            {
                html += MicsRadioButtonFor(htmlHelper, expression, item.Value, item.Text, htmlAttributes).ToHtmlString();
            }

            return new MvcHtmlString(html);
        }

        public static MvcHtmlString MicsRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string value, string text, object htmlAttributes = null)
        {
            var attrs = ConvertToDictionary(htmlAttributes);

            return MicsRadioButtonFor(htmlHelper, expression, value, text, attrs);
        }

        public static MvcHtmlString MicsRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string value, string text, IDictionary<string, object> htmlAttributes)
        {
            var exp = expression.Body as MemberExpression;
            string id = exp.Member.Name;
            var radioVaule = (string)ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;

            return MicsRadioButton(htmlHelper, id, value, text, radioVaule == value, htmlAttributes);
        }

        public static MvcHtmlString MicsRadioButton(this HtmlHelper htmlHelper, string name, string value, string text, object htmlAttributes = null)
        {
            return MicsRadioButton(htmlHelper, name, value, text, false, htmlAttributes);
        }

        public static MvcHtmlString MicsRadioButton(this HtmlHelper htmlHelper, string name, string value, string text, IDictionary<string, object> htmlAttributes)
        {
            return MicsRadioButton(htmlHelper, name, value, text, false, htmlAttributes);
        }

        public static MvcHtmlString MicsRadioButton(this HtmlHelper htmlHelper, string name, string value, string text, bool isChecked, object htmlAttributes = null)
        {
            var attrs = ConvertToDictionary(htmlAttributes);

            return MicsRadioButton(htmlHelper, name, value, text, false, attrs);
        }

        public static MvcHtmlString MicsRadioButton(this HtmlHelper htmlHelper, string name, string value, string text, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder labelTag = new TagBuilder("label");
            TagBuilder spanTag = new TagBuilder("span");

            if (HasClassAttrs(htmlAttributes))
            {
                htmlAttributes["class"] += " radio";
            }
            else
            {
                htmlAttributes.Add("class", "radio");
            }

            var radio = htmlHelper.RadioButton(name, value, isChecked, htmlAttributes);

            spanTag.AddCssClass("radioInput");
            labelTag.InnerHtml = radio.ToHtmlString() + spanTag.ToString() + text;

            return new MvcHtmlString(labelTag.ToString());
        }

        #endregion

        #region 核取方塊

        public static MvcHtmlString MicsCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, string text, object htmlAttributes = null)
        {
            var attrs = ConvertToDictionary(htmlAttributes);

            return MicsCheckBoxFor(htmlHelper, expression, text, attrs);
        }

        public static MvcHtmlString MicsCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, string value, string text, IDictionary<string, object> htmlAttributes)
        {
            var exp = expression.Body as MemberExpression;
            string name = exp.Member.Name;
            var checkBoxVaule = (string)ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;

            return MicsCheckBox(htmlHelper, name, value, text, checkBoxVaule == value, htmlAttributes);
        }

        public static MvcHtmlString MicsCheckBox(this HtmlHelper htmlHelper, string name, string value, string text, bool isChecked, object htmlAttributes = null)
        {
            var attrs = ConvertToDictionary(htmlAttributes);

            return MicsCheckBox(htmlHelper, name, value, text, isChecked, attrs);
        }

        public static MvcHtmlString MicsCheckBox(this HtmlHelper htmlHelper, string name, string value, string text, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            var labelTag = new TagBuilder("label");
            var spanTag = new TagBuilder("span");
            var inputTag = new TagBuilder("input");

            if (HasClassAttrs(htmlAttributes))
            {
                htmlAttributes["class"] += " checkbox";
            }
            else
            {
                htmlAttributes.Add("class", "checkbox");
            }

            inputTag.MergeAttributes(htmlAttributes);
            inputTag.MergeAttribute("name", name);
            inputTag.MergeAttribute("value", value);
            inputTag.MergeAttribute("type", "checkbox");
            inputTag.GenerateId(name);

            if (isChecked)
            {
                inputTag.MergeAttribute("checked", "checked");
            }

            spanTag.AddCssClass("checkboxInput");
            labelTag.InnerHtml = inputTag.ToString() + spanTag.ToString() + text;

            return new MvcHtmlString(labelTag.ToString());
        }

        #endregion

        #region 日期輸入框
        public static MvcHtmlString MicsDateTimeBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return MicsDateTimeBoxFor(htmlHelper, expression, string.Empty, htmlAttributes);
        }

        public static MvcHtmlString MicsDateTimeBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, object htmlAttributes = null)
        {
            var exp = expression.Body as MemberExpression;
            string name = exp.Member.Name;
            var value = (DateTime?)ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;

            return MicsDateTimeBox(htmlHelper, name, value, format, htmlAttributes);
        }

        public static MvcHtmlString MicsDateTimeBox(this HtmlHelper htmlHelper, string name, DateTime? value, object htmlAttributes = null)
        {
            return MicsDateTimeBox(htmlHelper, name, value, string.Empty, htmlAttributes);
        }

        public static MvcHtmlString MicsDateTimeBox(this HtmlHelper htmlHelper, string name, DateTime? value, string format, object htmlAttributes = null)
        {
            var attrs = ConvertToDictionary(htmlAttributes);

            return MicsDateTimeBox(htmlHelper, name, value, format, attrs);
        }

        public static MvcHtmlString MicsDateTimeBox(this HtmlHelper htmlHelper, string name, DateTime? value, string format, IDictionary<string, object> htmlAttributes = null)
        {
            format = !string.IsNullOrEmpty(format) ? format : "yyyy/MM/dd";
            var textValue = value.HasValue ? value.Value.ToString(format) : string.Empty;

            if (HasClassAttrs(htmlAttributes))
            {
                htmlAttributes["class"] += " inputbox date-picker";
            }
            else
            {
                htmlAttributes.Add("class", "inputbox date-picker");
            }

            return htmlHelper.TextBox(name, textValue, htmlAttributes);
        }
        #endregion

        private static bool HasClassAttrs(IDictionary<string, object> htmlAttributes)
        {
            return htmlAttributes.Keys.Any(o => o == "class");
        }

        private static IDictionary<string, object> ConvertToDictionary(object htmlAttributes)
        {
            var attrs = new Dictionary<string, object>();

            if (htmlAttributes != null)
            {
                foreach (var attr in htmlAttributes.GetType().GetProperties())
                {
                    attrs.Add(attr.Name, (string)attr.GetValue(htmlAttributes));
                }
            }

            return attrs;
        }
    }
}