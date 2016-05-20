using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Serialization;
using Xunit;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Tests.Serialization
{
    public class ClaimsPrincipalConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserializeAClaimsPrincipal()
        {
            var claims = new Claim[]{
                new Claim(JwtClaimTypes.Subject, "alice"),
                new Claim(JwtClaimTypes.Scope, "read"),
                new Claim(JwtClaimTypes.Scope, "write"),
            };
            var ci = new ClaimsIdentity(claims, OidcConstants.AuthenticationMethods.Password);
            var cp = new ClaimsPrincipal(ci);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClaimsPrincipalConverter());
            var json = JsonConvert.SerializeObject(cp, settings);

            cp = JsonConvert.DeserializeObject<ClaimsPrincipal>(json, settings);
            Assert.Equal(OidcConstants.AuthenticationMethods.Password, cp.Identity.AuthenticationType);
            Assert.Equal(3, cp.Claims.Count());
            Assert.True(cp.HasClaim(JwtClaimTypes.Subject, "alice"));
            Assert.True(cp.HasClaim(JwtClaimTypes.Scope, "read"));
            Assert.True(cp.HasClaim(JwtClaimTypes.Scope, "write"));
        }
    }
}