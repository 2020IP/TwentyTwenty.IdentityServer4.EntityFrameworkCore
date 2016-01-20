using System.Collections.Generic;
using System.Linq;

namespace TwentyTwenty.IdentityServer4.EntityFramework7.Extensions
{
    internal static class StringExtensions
    {
        public static string GetOrigin(this string url)
        {
            if (url != null && (url.StartsWith("http://") || url.StartsWith("https://")))
            {
                var idx = url.IndexOf("//");
                if (idx > 0)
                {
                    idx = url.IndexOf("/", idx + 2);
                    if (idx >= 0)
                    {
                        url = url.Substring(0, idx);
                    }
                    return url;
                }
            }

            return null;
        }

        public static IEnumerable<string> ParseScopes(this string scopes)
        {
            if (scopes == null || string.IsNullOrWhiteSpace(scopes))
            {
                return Enumerable.Empty<string>();
            }

            return scopes.Split(',');
        }

        public static string StringifyScopes(this IEnumerable<string> scopes)
        {
            if (scopes == null || !scopes.Any())
            {
                return null;
            }

            return scopes.Aggregate((s1, s2) => s1 + "," + s2);
        }
    }
}