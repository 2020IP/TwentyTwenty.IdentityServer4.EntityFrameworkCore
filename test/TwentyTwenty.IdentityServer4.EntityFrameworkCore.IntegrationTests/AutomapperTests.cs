using AutoMapper;
using System;
using System.Collections.Generic;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;
using Xunit;
using Models = IdentityServer4.Core.Models;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.IntegrationTests
{
    public class AutomapperTests
    {
        [Fact]
        public void AutomapperConfigurationIsValid()
        {
            Models.Scope s = new Models.Scope();

            Models.MappingExtensions.ToEntity<int>(s);

            var s2 = new Scope<int>
            {
                ScopeClaims = new HashSet<ScopeClaim<int>>()
            };
            s2.ToModel();

            Mapper.AssertConfigurationIsValid();
        }
    }
}