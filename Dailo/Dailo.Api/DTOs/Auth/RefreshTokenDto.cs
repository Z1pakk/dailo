namespace Dailo.Api.DTOs.Auth;

public sealed record RefreshTokenDto
{
    public required string RefreshToken { get; set; }
}
