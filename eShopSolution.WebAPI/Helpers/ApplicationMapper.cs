using AutoMapper;
using eShopSolution.CrawlData.Model;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.UpdateModel;

namespace eShopSolution.WebAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<AddBrand, BrandModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName))
                .ForMember(dest => dest.ImageURl, opt => opt.Ignore())
                .ForMember(dest => dest.PublicID, opt => opt.Ignore());

            CreateMap<UpdateBrand, BrandModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName))
                .ForMember(dest => dest.ImageURl, opt => opt.Ignore())
                .ForMember(dest => dest.PublicID, opt => opt.Ignore());

            CreateMap<AddCategory, CategoryModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.ImageURl, opt => opt.Ignore())
                .ForMember(dest => dest.PublicID, opt => opt.Ignore());

            CreateMap<UpdateCategory, CategoryModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.ImageURl, opt => opt.Ignore())
                .ForMember(dest => dest.PublicID, opt => opt.Ignore());

            CreateMap<AddSize, SizeModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.SizeName));

            CreateMap<UpdateSize, SizeModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.SizeName));

            CreateMap<AddColor, ColorModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.HexValue, opt => opt.MapFrom(src => src.HexValue));

            CreateMap<UpdateColor, ColorModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.HexValue, opt => opt.MapFrom(src => src.HexValue));

            CreateMap<AddGender, GenderModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<UpdateGender, GenderModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<AddPaymentMethod, PaymentMethodModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<UpdatePaymentMethod, PaymentMethodModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<AddOrderStatus, OrderStatusModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<UpdateOrderStatus, OrderStatusModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<AddShip, ShippingProvidersModel>()
               .ForMember(dest => dest.ID, opt => opt.Ignore())
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<UpdateShip, ShippingProvidersModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<AddCategoryAndBrand, CategoryAndBrandModel>()
               .ForMember(dest => dest.ID, opt => opt.Ignore())
               .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
               .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID));

            CreateMap<UpdateCategoryAndBrand, CategoryAndBrandModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
                .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID));

            CreateMap<AddProductColor, ProductColorModel>()
               .ForMember(dest => dest.ID, opt => opt.Ignore())
               .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
               .ForMember(dest => dest.ColorCombinationID, opt => opt.MapFrom(src => src.ColorCombinationID));

            CreateMap<UpdateProductColor, ProductColorModel>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.ColorCombinationID, opt => opt.MapFrom(src => src.ColorCombinationID));

            CreateMap<AddProduct, ProductModel>()
            .ForMember(dest => dest.ID, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID))
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(dest => dest.GenderID, opt => opt.MapFrom(src => src.GenderID))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.PriceIn, opt => opt.MapFrom(src => src.PriceIn))
            .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.PriceOut))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.check, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());

            CreateMap<DataInfomation, ProductModel>()
            .ForMember(dest => dest.ID, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.productName))
            .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.brandID))
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.categoryID))
            .ForMember(dest => dest.GenderID, opt => opt.MapFrom(src => src.genderID))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.productTitle))
            .ForMember(dest => dest.PriceIn, opt => opt.MapFrom(src => src.priceIn))
     .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.priceOut))
     .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.discount))
     .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
     .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
     .ForMember(dest => dest.check, opt => opt.MapFrom(src => 1))
     .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());

            CreateMap<UpdateProduct, ProductModel>()
            .ForMember(dest => dest.ID, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID))
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(dest => dest.GenderID, opt => opt.MapFrom(src => src.GenderID))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.PriceIn, opt => opt.MapFrom(src => src.PriceIn))
            .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.PriceOut))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
            .ForMember(dest => dest.check, opt => opt.MapFrom(src => 2))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<AddProductSizeInventory, ProductModel>()
            .ForMember(dest => dest.ID, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID))
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(dest => dest.GenderID, opt => opt.MapFrom(src => src.GenderID))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.PriceIn, opt => opt.MapFrom(src => src.PriceIn))
            .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.PriceOut))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.check, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());

            CreateMap<AddProductImage, ProductImageModel>()
            .ForMember(dest => dest.ID, opt => opt.Ignore())
            .ForMember(dest => dest.ProductColorID, opt => opt.MapFrom(src => src.ProductColorID))
            .ForMember(dest => dest.PublicID, opt => opt.Ignore())
            .ForMember(dest => dest.ImageURL, opt => opt.Ignore());

            CreateMap<UpdateProductImage, ProductImageModel>()
            .ForMember(dest => dest.ID, opt => opt.Ignore())
            .ForMember(dest => dest.ProductColorID, opt => opt.MapFrom(src => src.ProductColorID))
            .ForMember(dest => dest.PublicID, opt => opt.Ignore())
            .ForMember(dest => dest.ImageURL, opt => opt.Ignore());

            CreateMap<AddCollection, CollectionModel>()
            .ForMember(dest => dest.DetailQuantity, opt => opt.MapFrom(src => src.DetailQuantity))
            .ForMember(dest => dest.ColorIDs, opt => opt.MapFrom(src => src.ColorIDs))
            .ForMember(dest => dest.ListImageURL, opt => opt.Ignore());

            CreateMap<AddProductSizeAndColor, DetailQuantityProductModel>()
            .ForMember(dest => dest.ProductColorID, opt => opt.MapFrom(src => src.ProductColorID))
            .ForMember(dest => dest.SizeID, opt => opt.MapFrom(src => src.SizeID))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ID, opt => opt.Ignore());


            CreateMap<AddOrderModel, OrderModel>()
            .ForMember(dest => dest.UserID, opt => opt.Ignore())
            .ForMember(dest => dest.AddressID, opt => opt.MapFrom(src => src.AddressID))
            .ForMember(dest => dest.detailCarts, opt => opt.MapFrom(src => src.detailCarts))
            .ForMember(dest => dest.PaymentMethodID, opt => opt.MapFrom(src => src.PaymentMethodID))
            .ForMember(dest => dest.OrderStatusID, opt => opt.Ignore());

            CreateMap<AddProductReview, ProductReviewModel>()
            .ForMember(dest => dest.ID, opt => opt.Ignore())
            .ForMember(dest => dest.UserID, opt => opt.Ignore())
            .ForMember(dest => dest.Review, opt => opt.MapFrom(src => src.Review))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.DetailOrderID, opt => opt.MapFrom(src => src.DetailOrderID))
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
