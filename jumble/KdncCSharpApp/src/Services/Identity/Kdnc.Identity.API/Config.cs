﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Test;

namespace Kdnc.Identity.API
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("kdncApi", "Kdnc API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ExternalApiClient",


                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"kdncApi"},
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                },

                new Client
                {
                    ClientId = "kdncweb",
                    ClientName = "Kdnc MVC Client",

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = {"http://localhost:7001/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:7001"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "kdncApi"
                    },
                    AllowedGrantTypes = GrantTypes.Hybrid
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "d10841fe-d702-434b-9050-745eea366b87",
                    Username = "abc",
                    Password = "abc",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Nuwan"),
                        new Claim("website", "http://nuwanlanka.com")
                    }
                }
            };
        }
    }
}
