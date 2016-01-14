using System;

namespace TwentyTwenty.IdentityServer3.EntityFramework7.Entities
{
    public class ClientProviderRestriction<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual string Provider { get; set; }

        public virtual Client<TKey> Client { get; set; }
    }
}