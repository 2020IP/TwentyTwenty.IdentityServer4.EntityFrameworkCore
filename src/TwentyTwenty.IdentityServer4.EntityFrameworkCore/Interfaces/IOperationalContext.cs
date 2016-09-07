using Microsoft.EntityFrameworkCore;
using System;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Interfaces
{
    public interface IOperationalContext
    {
        DbSet<Consent> Consents { get; set; }
        DbSet<Token> Tokens { get; set; }
    }
}
