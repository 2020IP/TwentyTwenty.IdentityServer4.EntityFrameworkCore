using IdentityServer4.Core;
using Newtonsoft.Json;
using System.Security.Claims;
using IdentityModel;
using TwentyTwenty.IdentityServer4.EntityFramework7.Serialization;
using Xunit;

namespace TwentyTwenty.IdentityServer4.EntityFramework7.Tests.Serialization
{
    public class ClaimConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserializeAClaim()
        {
            var claim = new Claim(JwtClaimTypes.Subject, "alice");

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClaimConverter());
            var json = JsonConvert.SerializeObject(claim, settings);

            claim = JsonConvert.DeserializeObject<Claim>(json, settings);
            Assert.Equal(JwtClaimTypes.Subject, claim.Type);
            Assert.Equal("alice", claim.Value);
        }
    }
}