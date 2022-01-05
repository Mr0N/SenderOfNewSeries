using HtmlAgilityPack;
using Fizzler.Systems;
using Fizzler.Systems.HtmlAgilityPack;
using Message.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Implementer;

namespace Message.Service
{
    internal class Parser: BaseFactory<Parser>
    {
        public async Task<IEnumerable<InfoDto>> Get()
        {
            string html = await (await client.GetAsync(config.UriParsing))
                 .Content
                 .ReadAsStringAsync();
            var node = HtmlNode.CreateNode(html);
            return node.QuerySelectorAll("li > .item")
                .Select(Extract);
        }
        private InfoDto Extract(HtmlNode a)
        {
            var dto = new InfoDto();
            dto.NameSerial = a.QuerySelector(".field-title")?.InnerText ?? "";
            dto.SezonAndSeria = a.QuerySelector(".field-description")?.InnerText ?? "";
            string text = a.QuerySelector(".field-img")
            ?.Attributes
            ?.FirstOrDefault(x => x.Name.Equals("style"))
            ?.Value;
            dto.UriImagePrevies = Regex.Match(text ?? "", "url\\('(?<uri>[^']+)'\\);")
            ?.Groups["uri"]
            ?.Value;
            dto.UriViews = a.QuerySelector(".field-img a")
            ?.Attributes
            ?.FirstOrDefault(a => a.Name.Equals("href"))
            ?.Value;
            return dto;
        }
      
        HttpClient client;
        Config config;
        public Parser( HttpClient client, Config config)
        {
  
            this.client = client;
            this.config = config;
        }
    }
}
