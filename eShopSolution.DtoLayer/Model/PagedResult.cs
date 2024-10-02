namespace eShopSolution.DtoLayer.Model
{
    public class PagedResult
    {
        public List<ProductCardModel> Items { get; set; }
        public bool First { get; set; } = false;
        public int Page { get; set; } = 1;
        public int TotalPage { get; set; }
        public int TotalItem { get; set; }
        public int Size { get; set; }
        public bool Last { get; set; } = false;
    }
}
