namespace Dailo.Api.DTOs.Auth;

public sealed record AccessTokenDto(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiration
);
