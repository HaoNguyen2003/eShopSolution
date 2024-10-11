using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IUserService
    {
        Task<RegistrationResponseDto> CreateUserAsync(SignUp createUserDto);
        Task<Response<string>> ConfirmEmail(string token, string email);
        public Task<Response<Dictionary<string, string?>>> GenerateConfirmToken(string Email);
        public Task<Response<string>> CreateRoleAsync(AddRole createRoleDto);
        public Task<Response<List<RoleModel>>> GetAllRolesAsync();
        public Task<Response<RoleModel>> GetRolesByIDAsync(string ID);
        public Task<Response<List<AppUserModel>>> GetAllUsersAsync();
        public Task<Response<List<RoleModel>>> GetRoleAssignAsync(string id);
        public Task RoleAssignAsync(string userId, List<RoleModel> roleAssignDto);
        public Task<Response<string>> UpdateRoleAync(RoleModel updateRoleDto);
        public Task<Response<string>> UpdateUserAync(AppUserModel updateUserDto);
        public Task<Response<string>>UpdateAvatar(string avatar,string publicID, string id);
        public Task<Response<string>> DeletRole(string ID);
    }
}
