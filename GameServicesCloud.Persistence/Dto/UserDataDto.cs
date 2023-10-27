namespace GameServicesCloud.Persistence;

public class UserDataDto {
    public byte[] Data { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
}

public class UserDataAdminDto {
    public long Id { get; set; }
    public long UserId { get; set; }
    public double DataSize { get; set; }
    public DateTime? UpdatedAt { get; set; }
}