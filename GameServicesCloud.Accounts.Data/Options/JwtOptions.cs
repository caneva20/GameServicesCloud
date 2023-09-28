﻿namespace GameServicesCloud.Accounts;

public class JwtOptions {
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string SecretKey { get; set; }
    public required int ExpirationTimeInMinutes { get; set; }
}