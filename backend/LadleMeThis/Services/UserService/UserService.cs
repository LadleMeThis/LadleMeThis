using LadleMeThis.Data.Entity;
﻿using LadleMeThis.Models.AuthContracts;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Services.TokenService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync()
        {

            var users = await _userManager.Users.ToListAsync();

            return users.Select(user => new UserResponseDTO
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
            }).ToList();

        }

        public async Task<IEnumerable<UserReviewDTO>> GetAllUsersInReviewFormatAsync()
        {
            var users = await _userManager.Users.ToListAsync();


            return users.Select(user => new UserReviewDTO
            {
                UserId = user.Id,
            });
        }

        public async Task<UserResponseDTO> GetUserByIdAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new KeyNotFoundException("User with given id not found!");



            return new UserResponseDTO
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
            };
        }

     
        public async Task<IdentityResult> CreateUserAsync(RegistrationRequest registrationRequest)
        {
            var user = new IdentityUser
            {
                UserName = registrationRequest.Username,
                Email = registrationRequest.Email,

            };
            
            var result = await _userManager.CreateAsync(user, registrationRequest.Password);

            if (!result.Succeeded)
            {
                return result;
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateUserAsync(string userId, UserUpdateDTO userUpdateDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new KeyNotFoundException("User with given id not found!");

            user.UserName = userUpdateDto.Username;
            user.Email = userUpdateDto.Email;
            
            if (!string.IsNullOrEmpty(userUpdateDto.NewPasssword))
            {
                var passwordValidator = new PasswordValidator<IdentityUser>();
                var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, user, userUpdateDto.NewPasssword);

                if (!passwordValidationResult.Succeeded)
                    return passwordValidationResult;

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userUpdateDto.NewPasssword);
            }


            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new KeyNotFoundException("User with given id not found!");


            return await _userManager.DeleteAsync(user);

        }

        public async Task<AuthResult> RegisterAsync(RegistrationRequest request, string role)
        {
            var user = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email,
            };
            
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                return new AuthResult(false, request.Email, request.Username, "")
                {
                    ErrorMessages = { { "RoleError", "The specified role does not exist." } }
                };
            }
            
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return FailedRegistration(result, request.Email, request.Username);
            }
            
            await _userManager.AddToRoleAsync(user, role);
            return new AuthResult(true, request.Email, request.Username, "");
        }





        public async Task<AuthResult> LoginAsync(AuthRequest authRequest)
        {
            var user = await _userManager.FindByEmailAsync(authRequest.EmailOrUsername) ??
                       await _userManager.FindByNameAsync(authRequest.EmailOrUsername);  
            
            if (user == null)
            {
                return InvalidEmail(authRequest.EmailOrUsername);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, authRequest.Password);
            if (!isPasswordValid)
            {
                return InvalidPassword(authRequest.EmailOrUsername, user.UserName);
            }

            
            var accessToken = await _tokenService.CreateToken(user);

            return new AuthResult(true, user.Email, user.UserName, accessToken);
        }



        private static AuthResult InvalidEmail(string email)
        {
            var result = new AuthResult(false, email, "", "");
            result.ErrorMessages.Add("Bad credentials", "Invalid email/username");
            return result;
        }


        private static AuthResult InvalidPassword(string email, string userName)
        {
            var result = new AuthResult(false, email, userName, "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password");
            return result;
        }


        private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
        {
            var authResult = new AuthResult(false, email, username, "");

            foreach (var error in result.Errors)
            {
                authResult.ErrorMessages.Add(error.Code, error.Description);
            }

            return authResult;
        }
    }
}
