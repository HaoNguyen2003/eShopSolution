namespace eShopSolution.DtoLayer.Model
{
    public class DetailProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal PriceOut { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public DetailColorAndProduct? collectionModel { get; set; }
        public List<ColorItemModel> ColorItemModel { get; set; } = new List<ColorItemModel>();
    }
}
