using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using RestaurantOrderAPI.Data;
using RestaurantOrderAPI.DTOs;
using RestaurantOrderAPI.Iterfaces;
using RestaurantOrderAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantOrderAPI.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly MongoContext _context;
    private readonly PasswordHasher<User> _hasher;

    public AuthService(IConfiguration configuration, MongoContext context)
    {
        _configuration = configuration;
        _context = context;
        _hasher = new PasswordHasher<User>();
    }

    public string GenerateToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Secret"])
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _context.Users
            .Find(u => u.Email == request.Email)
            .FirstOrDefaultAsync();

        if (existingUser != null)
            throw new Exception("Email já cadastrado");

        var user = new User
        {
            Email = request.Email
        };

        user.PasswordHash = _hasher.HashPassword(user, request.Password);

        await _context.Users.InsertOneAsync(user);

        var token = GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Expiration = DateTime.UtcNow.AddHours(2)
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.Users
            .Find(u => u.Email == request.Email)
            .FirstOrDefaultAsync();

        if (user == null)
            throw new Exception("Usuário ou senha inválidos");

        var result = _hasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Usuário ou senha inválidos");

        var token = GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Expiration = DateTime.UtcNow.AddHours(2)
        };
    }
}