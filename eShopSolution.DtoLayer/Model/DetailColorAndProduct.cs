namespace eShopSolution.DtoLayer.Model
{
    public class DetailColorAndProduct
    {
        public int ColorID { get; set; }
        public List<DetailQuantityProductDisplay> DetailQuantity { get; set; } = new List<DetailQuantityProductDisplay>();
        public List<string> ListImageURL { get; set; }
    }
}
