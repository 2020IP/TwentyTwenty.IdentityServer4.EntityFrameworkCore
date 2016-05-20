using System;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities
{
    public class ClientSecret<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual string Value { get; set; }

        public string Type { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime? Expiration { get; set; }

        public virtual Client<TKey> Client { get; set; }
    }
}