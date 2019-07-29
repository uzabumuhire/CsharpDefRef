using System;
namespace Core
{
    public static class NetworkHelper
    {
        /// <summary>
        /// Uses a given converter to transform the given domain names into URIs.
        /// </summary>
        /// <param name="domainNames">The given domain names</param>
        /// <param name="uriFromUrlConverter">A given converter</param>
        /// <returns>URIs</returns>
        public static string[] UrisFromDomainNames(string[] domainNames, Func<string, string> uriFromUrlConverter)
        {
            string[] uris = new string[domainNames.Length];
            for (int i = 0; i < uris.Length; i++)
            {
                uris[i] = uriFromUrlConverter(domainNames[i]);
            }
            return uris;
        }

        /// <summary>
        /// Converts a givne domain name into an https URI.
        /// </summary>
        /// <param name="domainName">A given domain name.</param>
        /// <returns>An https URI.</returns>
        public static string HttpsUriFromDomainNameConverter(string domainName)
        {
            return "https://" + domainName;
        }

        /// <summary>
        /// Converts a givne domain name into an http URI.
        /// </summary>
        /// <param name="domainName">A given domain name.</param>
        /// <returns>An http URI.</returns>
        public static string HttpUriFromDomainNameConverter(string domainName)
        {
            return "http://" + domainName;
        }
    }
}
