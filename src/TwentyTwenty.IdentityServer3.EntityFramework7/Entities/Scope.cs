using System;
using System.Collections.Generic;

namespace TwentyTwenty.IdentityServer3.EntityFramework7.Entities
{
    public class Scope<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Required { get; set; }

        public virtual bool Emphasize { get; set; }

        public virtual int Type { get; set; }

        public virtual ICollection<ScopeClaim<TKey>> ScopeClaims { get; set; }

        public virtual ICollection<ScopeSecret<TKey>> ScopeSecrets { get; set; }

        public virtual bool IncludeAllClaimsForUser { get; set; }

        public virtual string ClaimsRule { get; set; }

        public virtual bool ShowInDiscoveryDocument { get; set; }
    }
}