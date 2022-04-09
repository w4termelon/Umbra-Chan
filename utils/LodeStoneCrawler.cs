using ScrapySharp.Extensions;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Umbra_C.utils
{
    public static class LodeStoneCrawler
    {
        public static string getLodestoneID(string charaktername)
        {
            var rg = new Regex(@"\d+");
            var web = new HtmlWeb();
            var doc = web.Load($"https://de.finalfantasyxiv.com/lodestone/character/?q={charaktername}&worldname=Omega");
            HtmlNode[] result = doc.DocumentNode.CssSelect("div.entry > a.entry__link").ToArray();
            var link = result.First().GetAttributes("href").First().Value;
            return rg.Match(link).Value;
        }
    }
}