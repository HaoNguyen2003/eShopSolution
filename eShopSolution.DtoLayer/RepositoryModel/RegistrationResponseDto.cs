namespace eShopSolution.DtoLayer.RepositoryModel
{
    public class RegistrationResponseDto
    {
        public bool IsSuccess { get; set; }
        public string? Errors { get; set; }
        public Dictionary<string, string?>? keyValuePairs { get; set; }
    }
}
