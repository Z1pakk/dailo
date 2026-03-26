// using Dailo.Api.Database;
// using Dailo.Api.DTOs.GitHub;
// using Dailo.Api.Entities;
// using Microsoft.EntityFrameworkCore;
//
// namespace Dailo.Api.Services;
//
// public sealed class GitHubAccessTokenService(
//     ApplicationDbContext dbContext,
//     EncryptionService encryptionService
// )
// {
//     public async Task StoreAsync(
//         string userId,
//         StoreGitHubAccessTokenDto accessTokenDto,
//         CancellationToken cancellationToken = default
//     )
//     {
//         GitHubAccessToken? existingToken = await GetAccessTokenAsync(userId, cancellationToken);
//
//         string encryptedToken = encryptionService.Encrypt(accessTokenDto.Token);
//
//         if (existingToken is not null)
//         {
//             existingToken.Token = encryptedToken;
//             existingToken.ExpiresAtUtc = DateTime.UtcNow.AddDays(accessTokenDto.ExpiresInDays);
//             dbContext.Update(existingToken);
//         }
//         else
//         {
//             GitHubAccessToken newToken = new()
//             {
//                 Id = $"gh_{Guid.NewGuid().ToString()}",
//                 UserId = userId,
//                 Token = encryptedToken,
//                 ExpiresAtUtc = DateTime.UtcNow.AddDays(accessTokenDto.ExpiresInDays),
//                 CreatedAtUtc = DateTime.UtcNow,
//             };
//             dbContext.GitHubAccessTokens.Add(newToken);
//         }
//
//         await dbContext.SaveChangesAsync(cancellationToken);
//     }
//
//     public async Task<string?> GetAsync(
//         string userId,
//         CancellationToken cancellationToken = default
//     )
//     {
//         GitHubAccessToken? accessToken = await GetAccessTokenAsync(userId, cancellationToken);
//
//         if (accessToken is null)
//         {
//             return null;
//         }
//
//         string decryptedToken = encryptionService.Decrypt(accessToken.Token);
//
//         return decryptedToken;
//     }
//
//     public async Task RevokeAsync(string userId, CancellationToken cancellationToken = default)
//     {
//         GitHubAccessToken? accessToken = await GetAccessTokenAsync(userId, cancellationToken);
//         if (accessToken is null)
//         {
//             return;
//         }
//
//         dbContext.GitHubAccessTokens.Remove(accessToken);
//         await dbContext.SaveChangesAsync(cancellationToken);
//     }
//
//     public async Task<GitHubAccessToken?> GetAccessTokenAsync(
//         string userId,
//         CancellationToken cancellationToken = default
//     )
//     {
//         return await dbContext
//             .GitHubAccessTokens.AsNoTracking()
//             .SingleOrDefaultAsync(gt => gt.UserId == userId, cancellationToken);
//     }
// }
