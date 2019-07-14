using System;
namespace Core
{
    public static class NetworkHelper
    {
        public static string[] UrisFromDomainNames(string[] urls, Func<string, string> uriFromUrlConverter)
        {
            string[] uris = new string[urls.Length];
            for (int i = 0; i < uris.Length; i++)
            {
                uris[i] = uriFromUrlConverter(urls[i]);
            }
            return uris;
        }

        public static string HttpsUriFromDomainNameConverter(string domainName)
        {
            return "https://" + domainName;
        }

        public static string HttpUriFromDomainNameConverter(string domainName)
        {
            return "http://" + domainName;
        }
    }
}
