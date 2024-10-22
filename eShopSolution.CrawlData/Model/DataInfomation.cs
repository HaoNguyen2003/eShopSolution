using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.CrawlData.Model
{
    public class DataInfomation
    {
        public string productName { get; set; }
        public string productTitle { get; set; }
        public int brandID { get; set; }
        public int genderID { get; set; }
        public int categoryID { get; set; }
        public decimal priceIn { get; set; }
        public decimal priceOut { get; set; }
        public decimal discount { get; set; }
        public string description { get; set; }
    }

    public class ProductWayData
    {
        public string Color { get; set; }
        public List<AddDetailQuantityProduct> DetailQuantity { get; set; }
        public List<string> Imgs { get; set; }
    }
   
    public class ProductDataNew
    {
        public string Color { get; set; }
        public List<AddDetailQuantityProduct> DetailQuantity { get; set; } = new List<AddDetailQuantityProduct>();
        public List<CloudinaryImageModel> ListImageURL { get; set; } = new List<CloudinaryImageModel>();
    }
    public class ProductDataInfomation
    {
        public DataInfomation ProductInfo { get; set; }
        public List<ProductWayData> ProductwayData { get; set; } = new List<ProductWayData> { };
    }
}
