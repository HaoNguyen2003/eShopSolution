namespace eShopSolution.DtoLayer.Model
{
    public class DetailColorAndProduct
    {
        public int ProductColorID { get; set; }
        public string MixColor {  get; set; }
        public List<DetailQuantityProductDisplay> DetailQuantity { get; set; } = new List<DetailQuantityProductDisplay>();
        public List<string> ListImageURL { get; set; }
    }
}
