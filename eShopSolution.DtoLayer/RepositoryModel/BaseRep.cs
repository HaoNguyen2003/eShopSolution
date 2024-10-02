namespace eShopSolution.DtoLayer.RepositoryModel
{
    public class BaseRep<T>
    {
        public int code { get; set; }
        public T Value { get; set; }
    }
}
