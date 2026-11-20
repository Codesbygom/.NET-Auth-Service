
using AuthService.Application.Common.Interfaces;
using AuthService.Application.DTOs;
using AuthService.Application.Services;
using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace AuthService.Application.Tests.Services;

public class AuthenticationServiceTests
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IPasswordHasher _passwordHasher = Substitute.For<IPasswordHasher>();
    private readonly ITokenService _tokenService = Substitute.For<ITokenService>();    
    private readonly AuthenticationService _sut;

    public AuthenticationServiceTests()
    {
        _sut = new AuthenticationService(_userRepository, _passwordHasher, _tokenService);
    }
    private static RegisterDto CreateRegisterDto() => new()
    {
        UserName = "ali",
        Email = "ali@test.com",
        Password = "123456",
        ConfirmPassword = "123456"
    };
    
    [Fact]
    public async Task RegisterAsync_WithNewUser_SavesUserWithUserRole()
    {
        var dto = CreateRegisterDto();

        await _sut.RegisterAsync(dto);

        await _userRepository.Received(1).CreateAsync()
    }
}