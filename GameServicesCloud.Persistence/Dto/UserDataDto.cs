namespace GameServicesCloud.Persistence;

public class UserDataDto {
    public byte[] Data { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
}