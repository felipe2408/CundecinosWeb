using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CundecinosWeb.Controllers
{
    [Authorize]
    public class WebScraperController : Controller
    {

        [HttpPost]
        public async Task<string?> ScrapePrice(string description)
        {
            var url = $"https://listado.mercadolibre.com.co/{description.Trim()}";

            using var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var firstResultLink = doc.DocumentNode.SelectSingleNode("//span[@class='price-tag-fraction']");
            if (firstResultLink == null)
            {
                return null;
            }
            
            var titleNode = doc.DocumentNode.SelectSingleNode("//h2[@class='ui-search-item__title shops__item-title']");

            var price = $"El precio estimado para el producto {titleNode.InnerHtml} es :"+firstResultLink.InnerHtml;


            return price;
        }
    }
}
