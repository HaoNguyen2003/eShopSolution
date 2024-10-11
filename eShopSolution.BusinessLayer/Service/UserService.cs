using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.BusinessLayer.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<RegistrationResponseDto> CreateUserAsync(SignUp createUserDto)
        {
            if (createUserDto.Password != createUserDto.ConfirmPassword)
            {
                return new RegistrationResponseDto() { IsSuccess = false, Errors = "Passwords do not match" };
            }

            var existingUser = await _userManager.FindByEmailAsync(createUserDto.Email);
            if (existingUser != null)
            {
                return new RegistrationResponseDto() { IsSuccess = false, Errors = "Email already exists" };
            }

            var user = new AppUser
            {
                LastName = createUserDto.LastName,
                FirstName = createUserDto.FirstName,
                UserName = createUserDto.Email,
                Email = createUserDto.Email,
                TwoFactorEnabled = true
            };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var param = new Dictionary<string, string?>()
                {
                    {"token",token},
                    {"email",user.Email }
                };
                return new RegistrationResponseDto() { IsSuccess = true, keyValuePairs = param };
            }
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new RegistrationResponseDto() { IsSuccess = true, Errors = string.Join(", ", errors) };
        }

        public async Task<Response<Dictionary<string, string?>>> GenerateConfirmToken(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                return new Response<Dictionary<string, string?>>() { IsSuccess = false, Error = "not found" };
            await _userManager.AddToRoleAsync(user, "Customer");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string?>()
                {
                    {"token",token},
                    {"email",user.Email}
                };
            return new Response<Dictionary<string, string?>>() { IsSuccess = true, Value = param };
        }


        public async Task<Response<string>> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new Response<string>() { IsSuccess = false, Error = "Not Found Account" };
            if (user.EmailConfirmed == true)
                return new Response<string>() { IsSuccess = false, Error = "Email has been confirmed" };
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return new Response<string>() { IsSuccess = false, Error = "Email Verified Fail: " + string.Join("; ", result.Errors.Select(e => e.Description)) };

            }
            return new Response<string>() { IsSuccess = true, Value = "Email Verified Successfully" };
        }



        public async Task<Response<string>> CreateRoleAsync(AddRole createRoleDto)
        {

            var roleExists = await _roleManager.RoleExistsAsync(createRoleDto.Name);
            if (roleExists)
                return new Response<string>() { IsSuccess = false, Error = "Role already exists" };
            var identityRole = new AppRole
            {
                Name = createRoleDto.Name,
                Description = createRoleDto.Description,

            };
            var result = await _roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
            {
                return new Response<string>() { IsSuccess = true, Error = "Role created successfully" };
            }
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new Response<string>() { IsSuccess = false, Error = $"Failed to create role: {errors}" };
        }

        public async Task<Response<List<RoleModel>>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles
                .Select(r => new RoleModel
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToListAsync();

            return new Response<List<RoleModel>>() { IsSuccess=true,Value=roles};
        }

        
        public async Task<Response<List<AppUserModel>>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.Select(u => new AppUserModel
            {
                Id = u.Id,
                Username = u.FirstName+u.LastName,
                Email = u.Email,
                avatar = u.ImageURL,
            }).ToListAsync();

            return new Response<List<AppUserModel>>() { IsSuccess=true,Value=users};
        }

        public async Task<Response<List<RoleModel>>> GetRoleAssignAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new Response<List<RoleModel>> { Error = "User not found", IsSuccess = false };
            var roles = await _userManager.GetRolesAsync(user);
            var roleModels = roles.Select(r => new RoleModel { Name = r }).ToList();
            return new Response<List<RoleModel>> { Value = roleModels, IsSuccess = true };
        }

        public async Task RoleAssignAsync(string userId, List<RoleModel> roleAssignDto)
        {
           
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found");
            var rolesAdd = roleAssignDto.Select(r=>r.Name).ToList();
            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = rolesAdd.Except(currentRoles).ToList();
            if (rolesToAdd.Any())
            {
                await _userManager.AddToRolesAsync(user, rolesToAdd);
            }
        }

        public async Task<Response<string>> UpdateRoleAync(RoleModel updateRoleDto)
        {
            var role = await _roleManager.FindByIdAsync(updateRoleDto.Id);
            if (role == null)
            {
                return new Response<string> { Error = "Role not found", IsSuccess = false };
            }
            role.Name = updateRoleDto.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return new Response<string> { Error = "Role updated successfully", IsSuccess = true };
            }
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new Response<string> { Error = $"Failed to update role: {errors}", IsSuccess = false};
        }

        public async Task<Response<string>> UpdateUserAync(AppUserModel updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(updateUserDto.Id);
            if (user == null)
            {
                return new Response<string>{ Error = "User not found", IsSuccess = false };
            }
            user.Email = updateUserDto.Email;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new Response<string>{ Error = "User updated successfully", IsSuccess = true };
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new Response<string>{ Error = $"Failed to update user: {errors}", IsSuccess = false };
        }

        public async Task<Response<string>> UpdateAvatar(string avatar, string publicID, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new Response<string>() { IsSuccess = false, Error = "Not Found User" };
            var PublicIdOld = user.PublicID;
            user.ImageURL = avatar;
            user.PublicID = publicID;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new Response<string> { IsSuccess = true, Value = PublicIdOld };
            }
            return new Response<string>
            {
                IsSuccess = false,
                Error = string.Join(", ", result.Errors.Select(e => e.Description))
            };

        }

        public async Task<Response<RoleModel>> GetRolesByIDAsync(string ID)
        {
            var role = await _roleManager.FindByIdAsync(ID);
            if (role == null)
            {
                return new Response<RoleModel> { Error = "Role not found", IsSuccess = false };
            }
            return  new Response<RoleModel> { IsSuccess = true,Value=new RoleModel() {Id=role.Id,Name=role.Name} };
        }

        public async Task<Response<string>> DeletRole(string ID)
        {
            var role = await _roleManager.FindByIdAsync(ID);
            if (role == null)
            {
                return new Response<string> { Error = "Role not found", IsSuccess = false };
            }
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return new Response<string> { Error = string.Join(", ", result.Errors), IsSuccess = false };
            return new Response<string> { IsSuccess = true };
        }
    }

}
