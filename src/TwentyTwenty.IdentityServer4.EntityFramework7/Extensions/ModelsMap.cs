using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;

namespace IdentityServer4.Core.Models
{
    public static class ModelsMap<TKey> where TKey : IEquatable<TKey>
    {
        static ModelsMap()
        {
            Mapper.CreateMap<Scope, Scope<TKey>>(MemberList.Source)
                .ForSourceMember(x => x.Claims, opts => opts.Ignore())
                .ForMember(x => x.AllowUnrestrictedIntrospection, opts => opts.MapFrom(src => src.AllowUnrestrictedIntrospection))
                .ForMember(x => x.ScopeClaims, opts => opts.MapFrom(src => src.Claims.Select(x => x)));
            Mapper.CreateMap<ScopeClaim, ScopeClaim<TKey>>(MemberList.Source);
            Mapper.CreateMap<Secret, ScopeSecret<TKey>>(MemberList.Source)
                .ForMember(x => x.Expiration, opt => opt.MapFrom(src => src.Expiration.HasValue ? src.Expiration.Value.UtcDateTime : default(DateTime?)));

            Mapper.CreateMap<Secret, ClientSecret<TKey>>(MemberList.Source)
                .ForMember(x => x.Expiration, opt => opt.MapFrom(src => src.Expiration.HasValue ? src.Expiration.Value.UtcDateTime : default(DateTime?)));
            Mapper.CreateMap<Client, Client<TKey>>(MemberList.Source)
                .ForMember(x => x.AllowPromptNone, opt => opt.MapFrom(src => src.AllowPromptNone))
                .ForMember(x => x.UpdateAccessTokenOnRefresh, opt => opt.MapFrom(src => src.UpdateAccessTokenClaimsOnRefresh))
                .ForMember(x => x.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => new ClientRedirectUri<TKey> { Uri = x })))
                .ForMember(x => x.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => new ClientPostLogoutRedirectUri<TKey> { Uri = x })))
                .ForMember(x => x.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => new ClientProviderRestriction<TKey> { Provider = x })))
                .ForMember(x => x.AllowedScopes, opt => opt.MapFrom(src => src.AllowedScopes.Select(x => new ClientScope<TKey> { Scope = x })))
                .ForMember(x => x.AllowedCorsOrigins, opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => new ClientCorsOrigin<TKey> { Origin = x })))
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new ClientClaim<TKey> { Type = x.Type, Value = x.Value })));
        }

        public static Scope<TKey> ToEntity(Scope s, Scope<TKey> dest = null)
        {
            if (s == null)
            {
                return null;
            }
            if (s.Claims == null)
            {
                s.Claims = new List<ScopeClaim>();
            }
            if (s.ScopeSecrets == null)
            {
                s.ScopeSecrets = new List<Secret>();
            }

            return Mapper.Map(s, dest);
        }

        public static Client<TKey> ToEntity(Client s, Client<TKey> dest = null)
        {
            if (s == null)
            {
                return null;
            }
            if (s.ClientSecrets == null)
            {
                s.ClientSecrets = new List<Secret>();
            }
            if (s.RedirectUris == null)
            {
                s.RedirectUris = new List<string>();
            }
            if (s.PostLogoutRedirectUris == null)
            {
                s.PostLogoutRedirectUris = new List<string>();
            }
            if (s.AllowedScopes == null)
            {
                s.AllowedScopes = new List<string>();
            }
            if (s.IdentityProviderRestrictions == null)
            {
                s.IdentityProviderRestrictions = new List<string>();
            }
            if (s.Claims == null)
            {
                s.Claims = new List<Claim>();
            }
            if (s.AllowedCorsOrigins == null)
            {
                s.AllowedCorsOrigins = new List<string>();
            }

            return Mapper.Map(s, dest);
        }
    }

    public static class MappingExtensions
    {
        public static Scope<TKey> ToEntity<TKey>(this Scope s, Scope<TKey> dest = null)
            where TKey : IEquatable<TKey>
        {
            return ModelsMap<TKey>.ToEntity(s, dest);
        }

        public static Client<TKey> ToEntity<TKey>(this Client s, Client<TKey> dest = null)
            where TKey : IEquatable<TKey>
        {
            return ModelsMap<TKey>.ToEntity(s, dest);
        }
    }
}