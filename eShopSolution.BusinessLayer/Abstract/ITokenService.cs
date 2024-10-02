using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface ITokenService
    {
        TokenModel CreateToken(AppUser appUser);

    }
}
