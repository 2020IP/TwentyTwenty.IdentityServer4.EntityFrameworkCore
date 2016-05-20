using System;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities
{
    public class ScopeClaim<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool AlwaysIncludeInIdToken { get; set; }

        public virtual Scope<TKey> Scope { get; set; }
    }
}