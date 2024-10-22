using eShopSolution.DtoLayer.AddModel;

namespace eShopSolution.DtoLayer.Model
{
    public class CollectionModel
    {
        public List<int> ColorIDs { get; set; }
        public List<AddDetailQuantityProduct> DetailQuantity { get; set; } = new List<AddDetailQuantityProduct>();
        public List<CloudinaryImageModel> ListImageURL { get; set; } = new List<CloudinaryImageModel>();
    }
}
