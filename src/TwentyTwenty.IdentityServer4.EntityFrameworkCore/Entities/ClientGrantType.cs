using System;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities
{
    public class ClientGrantType<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual string GrantType { get; set; }

        public virtual Client<TKey> Client { get; set; }
    }
}