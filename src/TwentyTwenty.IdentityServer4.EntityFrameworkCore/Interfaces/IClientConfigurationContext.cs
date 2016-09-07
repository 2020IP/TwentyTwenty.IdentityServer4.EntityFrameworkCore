using Microsoft.EntityFrameworkCore;
using System;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Interfaces
{
    public interface IClientConfigurationContext<TKey>
        where TKey : IEquatable<TKey>
    {
        DbSet<Client<TKey>> Clients { get; set; }
    }
}
