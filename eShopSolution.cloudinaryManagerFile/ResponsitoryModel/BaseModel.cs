namespace eShopSolution.cloudinaryManagerFile.ResponsitoryModel
{
    public class BaseModel
    {
        public bool IsSuccess { get; set; }
        public string? Errors { get; set; }
        public string? Message { get; set; }
        public string? Url { get; set; }
        public string? PublicID { get; set; }
    }
}
