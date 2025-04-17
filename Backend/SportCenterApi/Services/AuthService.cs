using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportCenterApi.Entities;
using SportCenterApi.Models;
using SportCenterApi.Services.Interfaces;

namespace SportCenterApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly DbSportCenterContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(DbSportCenterContext dbContext, UserManager<AppUser> userManager, 
            IMapper mapper, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterDto model)
        {
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<AuthResponseDto?> LoginUserAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Email);
            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return null;

            var token = await _tokenService.GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Email = user.Email!,
                Token = token
            };
        }



    }
}