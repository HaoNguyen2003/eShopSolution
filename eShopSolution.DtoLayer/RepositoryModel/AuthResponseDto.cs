using eShopSolution.DtoLayer.Model;

namespace eShopSolution.DtoLayer.RepositoryModel
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public TokenModel? Token { get; set; }
    }
}
