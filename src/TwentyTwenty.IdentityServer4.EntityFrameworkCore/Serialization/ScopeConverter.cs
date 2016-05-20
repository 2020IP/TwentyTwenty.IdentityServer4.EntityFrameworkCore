using Newtonsoft.Json;
using System;
using System.Linq;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Serialization
{
    public class ScopeLite
    {
        public string Name { get; set; }
    }

    public class ScopeConverter : JsonConverter
    {
        private readonly IScopeStore scopeStore;

        public ScopeConverter(IScopeStore scopeStore)
        {
            if (scopeStore == null) throw new ArgumentNullException("scopeStore");

            this.scopeStore = scopeStore;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Scope) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var source = serializer.Deserialize<ScopeLite>(reader);
            var scopes = AsyncHelper.RunSync(async () => await scopeStore.FindScopesAsync(new string[] { source.Name }));
            return scopes.Single();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var source = (Scope)value;

            var target = new ScopeLite
            {
                Name = source.Name
            };
            serializer.Serialize(writer, target);
        }
    }
}