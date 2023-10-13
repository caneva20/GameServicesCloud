using Microsoft.AspNetCore.Authorization;

namespace GameServicesCloud.Accounts;

public static class Claims {
    public static class Account {
        public static class Claim {
            public const string Read = "account.claim.read";
        }

        public static class UserClaim {
            public const string Create = "account.userclaim.create";
            public const string Read = "account.userclaim.read";
            public const string Delete = "account.userclaim.delete";
        }
    }

    public static readonly List<string> DefaultClaims = new();

    public static readonly List<string> AllClaims = new() {
        Account.Claim.Read,
        Account.UserClaim.Create,
        Account.UserClaim.Read,
        Account.UserClaim.Delete
    };

    public static void AddPolicies(this AuthorizationOptions options) {
        foreach (var claim in AllClaims) {
            options.AddPolicy(claim, policy => policy.RequireClaim(claim));
        }
    }
}