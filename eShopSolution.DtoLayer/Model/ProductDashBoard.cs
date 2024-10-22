namespace eShopSolution.DtoLayer.Model
{
    public class ProductDashBoard
    {
        public int ID { get; set; }
        public int BrandID { get; set; }
        public int CategoryID { get; set; }
        public int GenderID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal PriceIn { get; set; }
        public decimal PriceOut { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public CollectionProductDashBoard? CollectionProductDashBoard { get; set; } = new CollectionProductDashBoard();
        public List<ColorModel> Colors { get; set; } = new List<ColorModel>();
    }
}
