using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace DemoIdentityServer.Configuration
{
    public static class InMemoryConfig
    {
        public static IEnumerable<ApiScope> GetApiScopes() => new List<ApiScope> { new ApiScope("api1", "scope api1") };
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("api1", "resource api1")
                {
                    Scopes = { "api1" }
                }
            };
        public static List<TestUser> GetUsers() => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = Guid.NewGuid().ToString(),
                Username = "admin",
                Password = "Test123",
                Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Test")
                }
            },
            new TestUser
            {
                SubjectId = Guid.NewGuid().ToString(),
                Username = "user1",
                Password = "Test123",
                Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Test1")
                }
            }
        };
        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "api1",
                    ClientSecrets = new [] { new Secret("kpqaNGR3Lm$S(4Z^QtBaWkn2em($53".ToSha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "api1" }
                },
                new Client
                {
                    ClientId = "client1",
                    ClientName = "client1",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string> { "https://localhost:7110/signin-oidc" },
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile },
                    ClientSecrets = { new Secret("[2SpbwU>#qz_s£27/GhB5s".Sha512()) }
                }
            };
    }
}
