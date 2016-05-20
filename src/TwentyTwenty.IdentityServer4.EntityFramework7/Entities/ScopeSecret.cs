using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities
{
    public class ScopeSecret<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public string Description { get; set; }

        public DateTime? Expiration { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public virtual Scope<TKey> Scope { get; set; }
    }
}