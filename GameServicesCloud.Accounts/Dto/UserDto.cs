﻿namespace GameServicesCloud.Accounts;

public class UserDto {
    public string Email { get; set; } = null!;
    public bool HasVerifiedEmail { get; set; }
}