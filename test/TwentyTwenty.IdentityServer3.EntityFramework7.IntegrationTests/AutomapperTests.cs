using AutoMapper;
using System.Collections.Generic;
using TwentyTwenty.IdentityServer3.EntityFramework7.Entities;
using Xunit;
using Models = IdentityServer4.Core.Models;

namespace TwentyTwenty.IdentityServer3.EntityFramework7.IntegrationTests
{
    public class AutomapperTests
    {
        [Fact]
        public void AutomapperConfigurationIsValid()
        {
            Models.Scope s = new Models.Scope();

            var e = Models.MappingExtensions.ToEntity<int>(s);

            var s2 = new Scope<int>
            {
                ScopeClaims = new HashSet<ScopeClaim<int>>()
            };
            var m = s2.ToModel();

            Mapper.AssertConfigurationIsValid();
        }
    }
}