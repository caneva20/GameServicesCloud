using System.ComponentModel.DataAnnotations;

namespace GameServicesCloud.UI;

public class LoginRequest {
    [Required] [EmailAddress] public string Email { get; set; } = null!;

    [Required]
    [MinLength(6)]
    [MaxLength(6)]
    public string LoginToken { get; set; } = null!;
}