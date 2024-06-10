namespace AkaShop.Infrastructure
{
    //public class ClientConfiguration
    //{
        //public static IEnumerable<Client> GetClients()
        //{
        //    var apiGrantTypes = new List<string> { };
        //    apiGrantTypes.AddRange(GrantTypes.ResourceOwnerPassword);
        //    apiGrantTypes.AddRange(GrantTypes.ClientCredentials);

        //    var clients = new List<Client>{
        //        new() {
        //            ClientId = "AkaShopClient",
        //            AllowOfflineAccess = true,
        //            AllowedGrantTypes = apiGrantTypes,
        //            ClientSecrets = new[] { new Secret("AkaShopSecret".Sha256()) },
        //            AllowedScopes = new[] { "AkaShopApi" },
        //            AllowAccessTokensViaBrowser = true,
        //            AccessTokenLifetime = 3600 * 24 * 365 * 10, //10 years
        //        }
        //    };

        //    return clients;
        //}

        //public static IEnumerable<ApiResource> GetApiResources()
        //{
        //    var apiResource = new ApiResource[] {
        //        new ("AkaShopApi")
        //        {
        //            Name = "AkaShopApi",
        //            DisplayName = "AkaShop Api",
        //            //UserClaims = { GoogleRegistrationClaim.Claim.Type },
        //            Scopes = new[]
        //            {
        //                //"AkaShopToApi",
        //                "AkaShopApi",
        //            }
        //        }
        //    };
        //    return apiResource;
        //}

    //    public static IEnumerable<ApiScope> GetApiScopes()
    //    {
    //        var apiRess = new ApiScope[] {
    //            new ("AkaShopApi")
    //            //{
    //            //    Name = "AkaShopApi",
    //            //    DisplayName = "AKAPI",
    //            //}
    //        };
    //        return apiRess;
    //    }
    //}
}
