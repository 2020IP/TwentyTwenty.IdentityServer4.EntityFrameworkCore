using System;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerBuilderExtensions
    {
        public static EntityFrameworkOptions ConfigureEntityFramework(this IIdentityServerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            return new EntityFrameworkOptions(builder);
        }
    }
}