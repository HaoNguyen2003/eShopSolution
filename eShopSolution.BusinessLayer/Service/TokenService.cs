using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.Configuration;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eShopSolution.BusinessLayer.Service
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly CustomTokenOption _tokenOption;
        private readonly IRBACService _rBACService;
        private readonly IUserService _userService;

        public TokenService(UserManager<AppUser> userManager,IUserService userService,IOptions<CustomTokenOption> options,IRBACService rBACService)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
            _rBACService = rBACService;
            _userService = userService;
        }
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }
        public List<PolicyModel> MergeObjectArrays(List<PolicyModel> array1, List<PolicyModel> array2)
        {
            return array1.Concat(array2)
                         .GroupBy(obj => obj.PermissionMenuID)
                         .Select(group => group.First())
                         .ToList(); 
        }

        private async Task<IEnumerable<Claim>> GetClaims(AppUser appUser, List<String> audiences)
        {
            
            var userRoles = await _userManager.GetRolesAsync(appUser);
            var PolicyModels = new List<PolicyModel>();
            foreach(var claim in userRoles)
            {
                var ClaimRole = await _userService.GetRolesByIDOrNameAsync(null, claim);
                var ResultPermissionRoles = await _rBACService.GetAllPermissionOfRole(ClaimRole.Value.Id);
                if (ResultPermissionRoles.IsSuccess) 
                {
                    PolicyModels = MergeObjectArrays(PolicyModels, ResultPermissionRoles.Value);
                }
            }
            var userList = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,appUser.Id),
            new Claim(JwtRegisteredClaimNames.Email,appUser.Email),
            new Claim("UserName",appUser.FirstName+appUser.LastName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            userList.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));
            userList.AddRange(PolicyModels.Select(policy => new Claim("Permission", policy.menu.Name+"."+policy.permission.PermissionName.ToString())));
            return userList;
        }
        public TokenModel CreateToken(AppUser appUser)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securiyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(securiyKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(

                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(appUser, _tokenOption.Audience).Result,
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new TokenModel
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };
            return tokenDto;
        }
    }
}
