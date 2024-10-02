namespace eShopSolution.DtoLayer.RepositoryModel
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public T? Value { get; set; }
    }
}
