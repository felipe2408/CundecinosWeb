using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace CundecinosWeb.Controllers
{
    [Authorize]
    public class WebScraperController : Controller
    {

        [HttpPost]
        public async Task<string?> ScrapePrice(string description)
        {
            string result = string.Empty;
            float dollar = await GetDollarToCop();
            string msj = "";
            int cont = 0;
            float sum = 0;

            string url = $"https://listado.mercadolibre.com.co/{description.Trim()}";
            string xpath = "//span[@class='andes-money-amount ui-search-price__part shops__price-part andes-money-amount--cents-superscript']//span[@class='andes-money-amount__fraction']";
            List<float> pricesML = await GetPricesAsync(url, xpath, dot: true);
            if (pricesML.IsNullOrEmpty())
            {
                msj = "No se encontró su producto en MercadoLibre";
            }
            else
            {
                float medianPriceML = Median(pricesML);
                msj = $"Precio estimado en MercadoLibre: {medianPriceML.ToString("C")}";
                sum += medianPriceML;
                cont++;
            }
            result = @$"{{""description"":""{description}"",""priceML"":""{msj}"",";

            url = $"https://www.amazon.com/s?k={description.Trim()}";
            xpath = "//span[@class='a-price']//span[@class='a-offscreen']";
            List<float> pricesAm = await GetPricesAsync(url, xpath);
            if (pricesAm.IsNullOrEmpty())
            {
                msj = "No se encontró su producto en amazon";
            }
            else
            {
                float medianPriceAm = Median(pricesAm);
                msj = $"Precio estimado en amazon: {(medianPriceAm * dollar).ToString("C")}";
                sum += medianPriceAm;
                cont++;
            }
            result += @$"""priceAm"":""{msj}"",";

            url = $"https://www.ebay.com/sch/i.html?_nkw={description}";
            xpath = "//div[@class='s-item__detail s-item__detail--primary']//span[@class='s-item__price']";
            List<float> pricesEbay = await GetPricesAsync(url, xpath);
            if (pricesAm.IsNullOrEmpty())
            {
                msj = "No se encontró su producto en ebay";
            }
            else
            {
                float medianPriceEbay = Median(pricesEbay);
                msj = $"Precio estimado en ebay: {medianPriceEbay.ToString("C")}";
                sum += medianPriceEbay;
                cont++;
            }
            result += @$"""priceEbay"":""{msj}"",""averagePrice"":""Precio promedio: {(sum/cont).ToString("C")}""}}";

            return result;
        }
        private async Task<float> GetDollarToCop()
        {
            string dollar = "";
            using (var httpClient = new HttpClient())
            {
                var url = $"https://www.exchange-rates.org/es/conversor/usd-cop";
                var html = await httpClient.GetStringAsync(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                dollar = doc.DocumentNode.SelectSingleNode("//div[@class='main-results']//span[@class='to-cnt']//span[@class='to-rate']").InnerText;
            }
            return float.Parse(dollar);
        }
        private async Task<List<float>> GetPricesAsync(string url, string xpath, bool dot = false)//dot -> false:se maneja , como separador decimal true: no hay separador decimal
        {
            using var httpClient = new HttpClient();
            //url = $"https://www.ebay.com/sch/i.html?_nkw={description}";
            var html = await httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var result = doc.DocumentNode.SelectNodes(xpath);
            if (result == null)
            {
                return null;
            }
            var priceText = dot ? result.Select(x => x.InnerText.Replace(".", " ")) : result.Select(x => x.InnerText);
            List<float> prices = new List<float>();
            foreach (var item in priceText)
            {
                var precios = ExtractPrices(item);
                foreach (var value in precios)
                {
                    prices.Add(value);
                }
            }
            return prices;
        }
        private float[] ExtractPrices(string input)
        {
            string pattern = @"(?:\b|\$)(\d{1,3}(?:\s\d{3})*(?:\.\d{2})?)\b";
            MatchCollection matches = Regex.Matches(input, pattern);
            float[] prices = new float[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                string price = matches[i].Groups[1].Value.Replace("$", "").Replace(".", ",");
                price = Regex.Replace(price, @"\s", ".");
                prices[i] = float.Parse(price);
            }

            return prices;
        }
        private float Median(List<float> source)
        {
            var sortedList = source.OrderBy(number => number).ToList();

            int itemIndex = sortedList.Count / 2;

            if (sortedList.Count % 2 == 0)
            {
                // Even number of items.
                return (sortedList[itemIndex] + sortedList[itemIndex - 1]) / 2;
            }
            else
            {
                // Odd number of items.
                return sortedList[itemIndex];
            }
        }
    }
}
