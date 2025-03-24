using LadleMeThis.Data.Entity;
﻿using LadleMeThis.Models.AuthContracts;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.UserRepository;
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






        public UserService(IUserRepository userRepository, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
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
                //   DisplayName = user.FirstName,             if we want a normal name to be displayed, we have to extend the identity user or create a custom user class
                //   DateCreated = user.                       same with this
            }).ToList();

        }

        public async Task<IEnumerable<UserReviewDTO>> GetAllUsersInReviewFormatAsync()
        {
            var users = await _userManager.Users.ToListAsync();


            return users.Select(user => new UserReviewDTO
            {
                UserId = user.Id,
                //  DisplayName = user.DisplayName         no such thing atm
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
                //DisplayName = user.DisplayName,
                //DateCreated = user.DateCreated,
            };
        }

        public async Task<IdentityResult> CreateUserAsync(UserDTO userDto)
        {
            var user = new IdentityUser
            {
                UserName = userDto.Username,
                Email = userDto.Email,
                //FirstName = userDto.FirstName,
                //LastName = userDto.LastName,
                //DateCreated = DateTime.UtcNow
            };

            // Create the user and hash password automatically
            var result = await _userManager.CreateAsync(user, userDto.PasswordHash); // why is this called pw hash? here its not hashed yet

            if (!result.Succeeded)
            {
                return result; // this should automatically return errors depending on our settings, pw not long enough etc...
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateUserAsync(string userId, UserDTO userDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new KeyNotFoundException("User with given id not found!");

            user.UserName = userDto.Username;
            user.Email = userDto.Email;
            //user.DisplayName = userDto.DisplayName;

            // if empty, i assume the user dont want to change their pw
            if (!string.IsNullOrEmpty(userDto.PasswordHash))
            {
                var passwordValidator = new PasswordValidator<IdentityUser>();
                var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, user, userDto.PasswordHash);

                if (!passwordValidationResult.Succeeded)
                    return passwordValidationResult;

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userDto.PasswordHash);
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
                //FirstName = request.FirstName,
                //LastName = request.LastName
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return FailedRegistration(result, request.Email, request.Username);
            }

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                return new AuthResult(false, request.Email, request.Username, "")
                {
                    ErrorMessages = { { "RoleError", "The specified role does not exist." } }
                };
            }

            await _userManager.AddToRoleAsync(user, role);
            return new AuthResult(true, request.Email, request.Username, "");
        }





        public async Task<AuthResult> LoginAsync(AuthRequest authRequest)
        {
            var user = await _userManager.FindByEmailAsync(authRequest.Email) ?? await _userManager.FindByNameAsync(authRequest.Email);  // opportunity for the user to enter either the email or username (not the best naming)
            if (user == null)
            {
                return InvalidEmail(authRequest.Email);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, authRequest.Password);
            if (!isPasswordValid)
            {
                return InvalidPassword(authRequest.Email, user.UserName);
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
