using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.Authentication;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace bdDevCRM.Service.Authentication;

public class TokenBlacklistService : ITokenBlacklistService
{
  private readonly IConfiguration _configuration;
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  public TokenBlacklistService(IConfiguration configuration, IRepositoryManager repository, ILoggerManager logger)
  {
    _configuration = configuration;
    _repository = repository;
    _logger = logger;
  }
  public async Task AddToBlacklistAsync(string token)
  {

    if (string.IsNullOrEmpty(token)) throw new NullModelBadRequestException("TokenBlacklist");

    var handler = new JwtSecurityTokenHandler();
    var jwtToken = handler.ReadJwtToken(token);
    var expiryDate = jwtToken.ValidTo;
    var tokenBlacklist = new TokenBlacklist
    {
      TokenId = Guid.NewGuid(),
      Token = token,
      ExpiryDate = expiryDate
    };

    await _repository.TokenBlacklist.AddToBlacklistAsync(tokenBlacklist);
    await _repository.SaveAsync();
  }

  public Task<bool> IsTokenBlacklisted(string token)
  {
    var isBlacklisted = _repository.TokenBlacklist.IsTokenBlacklistedAsync(token);
    return isBlacklisted;
  }
}
