using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoListAPI.Common;
using ToDoListAPI.Data;
using ToDoListAPI.DTOs;
using ToDoListAPI.DTOs.Auth;
using ToDoListAPI.Helpers;
using ToDoListAPI.Models;
using ToDoListAPI.Repositories.Interfaces;
using ToDoListAPI.Services.Interface;

namespace ToDoListAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(UserManager<IdentityUser> userManager,
                           IUserRepository userRepository,
                           JwtHelper jwtHelper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                return Result<AuthResponseDto>.Failure("Email is already registered");

            var user = new IdentityUser
            {
                UserName = dto.Name,
                Email = dto.Email.ToLower(),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return Result<AuthResponseDto>.Failure(result.Errors.First().Description);

            var token = _jwtHelper.GenerateToken(user);

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                Token = token,
                Name = dto.Name,
                Email = user.Email!
            });
        }

        public async Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Result<AuthResponseDto>.Failure("Invalid email or password");

            var isValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isValidPassword)
                return Result<AuthResponseDto>.Failure("Invalid email or password");

            var token = _jwtHelper.GenerateToken(user);

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                Token = token,
                Name = user.UserName ?? "",
                Email = user.Email!
            });

        }
    }
    }

