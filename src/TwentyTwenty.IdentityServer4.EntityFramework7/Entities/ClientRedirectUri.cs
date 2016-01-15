using System;

namespace TwentyTwenty.IdentityServer4.EntityFramework7.Entities
{
    public class ClientRedirectUri<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual string Uri { get; set; }

        public virtual Client<TKey> Client { get; set; }
    }
}