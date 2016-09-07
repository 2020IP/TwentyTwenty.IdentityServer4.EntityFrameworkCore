using Microsoft.EntityFrameworkCore;
using System;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Interfaces
{
    public interface IScopeConfigurationContext<TKey>
        where TKey : IEquatable<TKey>
    {
        DbSet<Scope<TKey>> Scopes { get; set; }
    }
}
