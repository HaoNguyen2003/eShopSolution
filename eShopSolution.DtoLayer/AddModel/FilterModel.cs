namespace eShopSolution.DtoLayer.AddModel
{
    public class FilterModel
    {
        public string? TextSearch { get; set; } = ""; 
        public List<int> ListBrandID { get; set; } = new List<int>();
        public List<int> ListCategoryID { get; set; } = new List<int>();
        public List<int> ListColorID { get; set; } = new List<int>();
        public List<int> ListGenderID { get; set; } = new List<int>();
        public List<int> ListSizeID { get; set; } = new List<int>();
        public bool SortByPrice { get; set; } = false;
    }
}
