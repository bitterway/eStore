using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Text;
using eStore.ViewModels;
using eStore.Utils;

namespace eStore.TagHelpers
{
    // You may need to install the Microsoft.AspNet.Razor.Runtime package into your project
    [HtmlTargetElement("catalogue", Attributes = BrandIdAttribute)]
    public class CatalogueHelper : TagHelper
    {
        private const string BrandIdAttribute = "brand";
        [HtmlAttributeName(BrandIdAttribute)]
        public string BrandId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CatalogueHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_session.GetObject<ProductViewModel[]>(SessionVars.Menu) != null && Convert.ToInt32(BrandId) > 0)
            {
                var innerHtml = new StringBuilder();
                ProductViewModel[] menu = _session.GetObject<ProductViewModel[]>(SessionVars.Menu);
                innerHtml.Append("<div class=\"col-xs-12\" style=\"font-size:x-large;\"><span>Catalogue</span></div>");
                foreach (ProductViewModel item in menu)
                {
                    if (item.BrandId == Convert.ToInt32(BrandId))
                    {
                        var img_src = "img/" + item.GraphicName;
                        innerHtml.Append("<div id =\"item" + item.Id + "\"class=\"col-sm-3 col-xs-12 text-center\">");
                        //product view under brand
                        innerHtml.Append("<span class=\"col-xs-12\"><img src=\"" + img_src + "\" class=\"img-responsive center-block\" /></span>");
                        innerHtml.Append("<p id = name" + item.Id + " data-description=\"" + item.ProductName + "\">");
                        innerHtml.Append("<span style =\"font-size:large;\">" + item.ProductName.Substring(0, 10) + "...</span>");
                        innerHtml.Append("</p>");
                        innerHtml.Append("<div><span>More Beauty Info.<br />Click Details</span></div>");
                        innerHtml.Append("<div style =\"padding-bottom: 10px;\">");
                        innerHtml.Append("<a href =\"#product-details-popup\" data-toggle=\"modal\" class=\"btn btn-default\" id=\"modalbtn\" data-id=\"" + item.Id + "\">Details</a>");
                        innerHtml.Append("</div>");
                        // product view in modal
                        innerHtml.Append("<input type =\"hidden\" id=\"h-name" + item.Id + "\" value=\"" + item.ProductName + "\"/>");
                        innerHtml.Append("<input type =\"hidden\" id=\"h-graphic" + item.Id + "\" value=\"" + item.GraphicName + "\"/>");
                        innerHtml.Append("<input type =\"hidden\" id=\"h-descr" + item.Id + "\" value=\"" + item.Description + "\"/>");
                        innerHtml.Append("<input type =\"hidden\" id=\"h-on-hand" + item.Id + "\" value=\"" + item.QtyOnHand + "\"/>");
                        innerHtml.Append("<input type =\"hidden\" id=\"h-msrp" + item.Id + "\" value=\"" + item.MSRP.ToString("C") + "\"/>");
                        innerHtml.Append("</div>");
                    }
}
output.Content.SetHtmlContent(innerHtml.ToString());
}
}
}
}
