using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using TwentyTwenty.IdentityServer3.EntityFramework7.Serialization;
using Xunit;

namespace TwentyTwenty.IdentityServer3.EntityFramework7.Tests.Serialization
{
    public class ClientConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserializeAClient()
        {
            var client = new Client
            {
                ClientId = "123",
                Enabled = true,
                AbsoluteRefreshTokenLifetime = 5,
                AccessTokenLifetime = 10,
                AccessTokenType = AccessTokenType.Jwt,
                AllowRememberConsent = true,
                RedirectUris = new List<string> { "http://foo.com" }
            };
            var clientStore = new InMemoryClientStore(new Client[] { client });
            var converter = new ClientConverter(clientStore);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(converter);
            var json = JsonConvert.SerializeObject(client, settings);

            var result = JsonConvert.DeserializeObject<Client>(json, settings);
            Assert.Same(client, result);
        }
    }
}