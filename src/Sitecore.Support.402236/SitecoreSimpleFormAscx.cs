using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using System.Web.UI;

namespace Sitecore.Support.Form.Web.UI.Controls
{
    [ToolboxData("<div runat=\"server\"></div>"), PersistChildren(true)]
    public class SitecoreSimpleFormAscx : Sitecore.Form.Web.UI.Controls.SitecoreSimpleFormAscx
    {
        // Methods
        public new bool HasVisibleFields(ID formId)
        {
            Item item = StaticSettings.ContextDatabase.GetItem(formId);
            if (item == null)
            {
                return true;
            }
            int num = 0;
            string query = $".//*[@@templateid = '{IDs.FieldTemplateID}']";
            Item[] itemArray = item.Axes.SelectItems(query);
            if (itemArray != null)
            {
                foreach (Item item2 in itemArray)
                {
                    if (!string.IsNullOrEmpty(item2.Fields["Title"].Value))
                    {
                        num++;
                    }
                }
            }
            return (num > 0);
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            Assert.ArgumentNotNull(writer, "writer");
            if (Sitecore.Context.PageMode.IsPageEditor || this.HasVisibleFields(this.FormID))
            {
                writer.Write("<div class='{0}' id=\"{1}\"", base.CssClass ?? "scfForm", this.ID);
                base.Attributes.Render(writer);
                writer.Write(">");
                base.RenderControl(writer, base.Adapter);
                writer.Write("</div>");
            }
        }
    }



}