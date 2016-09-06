using IdentityServer4.Models;
using IdentityServer4.Quickstart;
using Newtonsoft.Json;
using System.Collections.Generic;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Serialization;
using Xunit;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Tests.Serialization
{
    public class ScopeConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserializeAScope()
        {
            var s1 = new Scope
            {
                Name = "email",
                Required = true,
                Type = ScopeType.Identity,
                Emphasize = true,
                DisplayName = "email foo",
                Description = "desc foo",
                Claims = new List<ScopeClaim> {
                    new ScopeClaim{Name = "email", Description = "email"}
                }
            };
            var s2 = new Scope
            {
                Name = "read",
                Required = true,
                Type = ScopeType.Resource,
                Emphasize = true,
                DisplayName = "foo",
                Description = "desc",
            };
            var converter = new ScopeConverter(new InMemoryScopeStore(new Scope[] { s1, s2 }));

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(converter);
            var json = JsonConvert.SerializeObject(s1, settings);

            var result = JsonConvert.DeserializeObject<Scope>(json, settings);
            Assert.Same(s1, result);
        }
    }
}