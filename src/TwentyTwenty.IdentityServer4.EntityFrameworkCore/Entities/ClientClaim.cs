using System;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities
{
    public class ClientClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual string Type { get; set; }

        public virtual string Value { get; set; }

        public virtual Client<TKey> Client { get; set; }
    }
}