using Microsoft.Extensions.DependencyInjection;
using System;

namespace TwentyTwenty.IdentityServer3.EntityFramework7.Extensions
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