﻿namespace GameServicesCloud.Persistence;

public interface IUserDataService {
    Task<UserData?> Find(long userId);

    Task<bool> Save(UserData userData, byte[] data);

    Task<UserData> Create(long userId);
}