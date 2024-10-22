namespace eShopSolution.DtoLayer.Model
{
    public class CollectionProductDashBoard
    {
        public int ProductColorID { get; set; }
        public int ColorID { get; set; }
        public List<DetailQuantityProductDisplay> SizeAndQuantity { get; set; } = new List<DetailQuantityProductDisplay>();
        public List<ProductImageModel> productImageModels { get; set; } = new List<ProductImageModel>();
    }
}
