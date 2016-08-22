using System.Collections.Generic;

namespace HoS_AP.Web.Tests
{
    internal static class UrlManager
    {
        private static readonly IDictionary<PageTypes, string> knownPages = new Dictionary<PageTypes, string>
        {
            {PageTypes.Login, GetAbsoluteUrl("/")},
            {PageTypes.Listing, GetAbsoluteUrl("/characters")}
        };

        internal static string GetPage(PageTypes type)
        {
            return knownPages[type];
        }

        private static string GetAbsoluteUrl(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
            {
                relativeUrl = "/" + relativeUrl;
            }
            return string.Format("http://localhost:{0}{1}", Constants.IisPort, relativeUrl);
        }
    }
}