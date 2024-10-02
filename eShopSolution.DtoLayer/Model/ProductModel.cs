namespace eShopSolution.DtoLayer.Model
{
    public class ProductModel
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
        public int check { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
