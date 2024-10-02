namespace eShopSolution.DtoLayer.Model
{
    public class ProductInCartModel
    {
        public int ProuductID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal PriceOut { get; set; }
        public decimal Discount { get; set; }
        public int ColorID { get; set; }
        public int SizeID { get; set; }
        public int Quantity { get; set; }
        public string? MessageOption {  get; set; }
        public List<DetailQuantityProductDisplay> DetailSizeAndQuatilyRemaining = new List<DetailQuantityProductDisplay>();


    }
}
