using AutoMapper;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.Helpers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Brand, BrandModel>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName))
            .ForMember(dest => dest.ImageURl, opt => opt.MapFrom(src => src.ImageURL))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.BrandID))
            .ForMember(dest => dest.PublicID, opt => opt.MapFrom(src => src.PublicID));

            CreateMap<BrandModel, Brand>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName))
            .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.ImageURl))
            .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PublicID, opt => opt.MapFrom(src => src.PublicID))
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryAndBrands, opt => opt.Ignore());

            CreateMap<Category, CategoryModel>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(dest => dest.ImageURl, opt => opt.MapFrom(src => src.ImageURL))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(dest => dest.PublicID, opt => opt.MapFrom(src => src.PublicID));

            CreateMap<CategoryModel, Category>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.ImageURl))
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.PublicID, opt => opt.MapFrom(src => src.PublicID))
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryAndBrands, opt => opt.Ignore());

            CreateMap<Sizes, SizeModel>()
            .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID));

            CreateMap<SizeModel, Sizes>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SizeName))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ProductSizeInventories, opt => opt.Ignore());

            CreateMap<Colors, ColorModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.HexValue, opt => opt.MapFrom(src => src.HexValue))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID));

            CreateMap<ColorModel, Colors>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.HexValue, opt => opt.MapFrom(src => src.HexValue))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ProductColors, opt => opt.Ignore());

            CreateMap<Gender, GenderModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID));

            CreateMap<GenderModel, Gender>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Products, opt => opt.Ignore());


            CreateMap<PaymentMethod, PaymentMethodModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.PaymentMethodID));

            CreateMap<PaymentMethodModel, PaymentMethod>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PaymentMethodID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Orders, opt => opt.Ignore());

            CreateMap<OrderStatus, OrderStatusModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StatusName))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.OrderStatusID));

            CreateMap<OrderStatusModel, OrderStatus>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.OrderStatusID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Orders, opt => opt.Ignore());


            CreateMap<ShippingProvider, ShippingProvidersModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProviderName))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ShippingProviderID));

            CreateMap<ShippingProvidersModel, ShippingProvider>()
            .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ShippingProviderID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Orders, opt => opt.Ignore());


            CreateMap<CategoryAndBrand, CategoryAndBrandModel>()
            .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID))
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID));

            CreateMap<CategoryAndBrandModel, CategoryAndBrand>()
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID))
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<ProductImages, ProductImageModel>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ProductColorID, opt => opt.MapFrom(src => src.ProductColorID))
            .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
            .ForMember(dest => dest.PublicID, opt => opt.MapFrom(src => src.PublicID));

            CreateMap<ProductImageModel, ProductImages>()
             .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
             .ForMember(dest => dest.ProductColorID, opt => opt.MapFrom(src => src.ProductColorID))
             .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
             .ForMember(dest => dest.PublicID, opt => opt.MapFrom(src => src.PublicID))
             .ForMember(dest => dest.ProductColors, opt => opt.Ignore());


            CreateMap<ProductColors, ProductColorModel>()
           .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
           .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
           .ForMember(dest => dest.ColorID, opt => opt.MapFrom(src => src.ColorID));

            CreateMap<ProductColorModel, ProductColors>()
           .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
           .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
           .ForMember(dest => dest.ColorID, opt => opt.MapFrom(src => src.ColorID))
           .ForMember(dest => dest.Product, opt => opt.Ignore())
           .ForMember(dest => dest.Color, opt => opt.Ignore());

            CreateMap<Product, ProductModel>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID))
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(dest => dest.GenderID, opt => opt.MapFrom(src => src.GenderID))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.PriceIn, opt => opt.MapFrom(src => src.PriceIn))
            .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.PriceOut))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate))
            .ForMember(dest => dest.check, opt => opt.MapFrom(src => 3));


            CreateMap<Product, DetailProduct>()
           .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
           .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.PriceOut))
           .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.collectionModel, opt => opt.Ignore())
           .ForMember(dest => dest.ColorItemModel, opt => opt.Ignore());

            CreateMap<Product, ProductCardModel>()
           .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
           .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.PriceOut))
           .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
           .ForMember(dest => dest.ColorItemModel, opt => opt.Ignore());

            CreateMap<Product, ProductDashBoard>()
           .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
           .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID))
           .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
           .ForMember(dest => dest.GenderID, opt => opt.MapFrom(src => src.GenderID))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
           .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.PriceOut))
           .ForMember(dest => dest.PriceIn, opt => opt.MapFrom(src => src.PriceIn))
           .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Colors, opt => opt.Ignore())
           .ForMember(dest => dest.CollectionProductDashBoard, opt => opt.Ignore());


            CreateMap<ProductModel, Product>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID))
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(dest => dest.GenderID, opt => opt.MapFrom(src => src.GenderID))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.PriceIn, opt => opt.MapFrom(src => src.PriceIn))
            .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.PriceOut))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ProductReviews, opt => opt.Ignore())
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Gender, opt => opt.Ignore())
            .ForMember(dest => dest.ProductReviews, opt => opt.Ignore())
            .ForMember(dest => dest.ProductColors, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                if (src.check == 1)
                {
                    dest.CreateDate = src.CreateDate != default ? src.CreateDate : DateTime.Now;
                }
                else if (src.check == 2)
                {

                    dest.UpdateDate = src.UpdateDate != default ? src.UpdateDate : DateTime.Now;
                }
            });

            CreateMap<DetailQuantityProductModel, ProductSizeInventory>()
            .ForMember(dest => dest.ProductSizeInventoryID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.ProductColorID, opt => opt.MapFrom(src => src.ProductColorID))
            .ForMember(dest => dest.SizeID, opt => opt.MapFrom(src => src.SizeID))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Size, opt => opt.Ignore())
            .ForMember(dest => dest.DetailOrders, opt => opt.Ignore())
            .ForMember(dest => dest.ProductColor, opt => opt.Ignore())
            .ForMember(dest => dest.DetailOrders, opt => opt.Ignore());


            CreateMap<ProductSizeInventory, DetailQuantityProductModel>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ProductSizeInventoryID))
            .ForMember(dest => dest.ProductColorID, opt => opt.MapFrom(src => src.ProductColorID))
            .ForMember(dest => dest.SizeID, opt => opt.MapFrom(src => src.SizeID))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<UserRefreshToken, UserRefreshTokenModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => src.Expiration));

            CreateMap<UserRefreshTokenModel, UserRefreshToken>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => src.Expiration));


            CreateMap<AddAddressShipInfo, Address>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.UserID, opt => opt.Ignore())
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.ProvinceID, opt => opt.MapFrom(src => src.ProvinceID))
            .ForMember(dest => dest.districtID, opt => opt.MapFrom(src => src.districtID))
            .ForMember(dest => dest.WardName, opt => opt.MapFrom(src => src.WardName))
            .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.ProvinceName))
            .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.DistrictName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.AddressInfo, opt => opt.MapFrom(src => src.AddressInfo))
            .ForMember(dest => dest.ConsigneeName, opt => opt.MapFrom(src => src.ConsigneeName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));


            CreateMap<Address, AddressShipInfo>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.ProvinceName))
            .ForMember(dest => dest.district, opt => opt.MapFrom(src => src.DistrictName))
            .ForMember(dest => dest.Ward, opt => opt.MapFrom(src => src.WardName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.AddressInfo, opt => opt.MapFrom(src => src.AddressInfo))
            .ForMember(dest => dest.ConsigneeName, opt => opt.MapFrom(src => src.ConsigneeName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<Address, AddAddressShipInfo>()
           .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
           .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
           .ForMember(dest => dest.ProvinceID, opt => opt.MapFrom(src => src.ProvinceID))
           .ForMember(dest => dest.districtID, opt => opt.MapFrom(src => src.districtID))
           .ForMember(dest => dest.WardCode, opt => opt.MapFrom(src => src.WardCode))
           .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.ProvinceName))
           .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.DistrictName))
           .ForMember(dest => dest.WardName, opt => opt.MapFrom(src => src.WardName))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
           .ForMember(dest => dest.AddressInfo, opt => opt.MapFrom(src => src.AddressInfo))
           .ForMember(dest => dest.ConsigneeName, opt => opt.MapFrom(src => src.ConsigneeName))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<OrderModel, Order>()
           .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
           .ForMember(dest => dest.AddressID, opt => opt.MapFrom(src => src.AddressID))
           .ForMember(dest => dest.OrderID, opt => opt.Ignore())
           .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src=>DateTime.Now))
           .ForMember(dest => dest.PaymentMethodID, opt => opt.MapFrom(src => src.PaymentMethodID))
           .ForMember(dest => dest.OrderStatusID, opt => opt.MapFrom(src => src.OrderStatusID))
           .ForMember(dest => dest.ShippingProviderID, opt => opt.MapFrom(src => src.ShippingProviderID));

            CreateMap<Order,OrderModel>()
           .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
           .ForMember(dest => dest.AddressID, opt => opt.MapFrom(src => src.AddressID))
           .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.OrderID))
           .ForMember(dest => dest.PaymentMethodID, opt => opt.MapFrom(src => src.PaymentMethodID))
           .ForMember(dest => dest.OrderStatusID, opt => opt.MapFrom(src => src.OrderStatusID))
           .ForMember(dest => dest.ShippingProviderID, opt => opt.MapFrom(src => src.ShippingProviderID));

            CreateMap<DetailOrderModel,DetailOrder>()
            .ForMember(dest => dest.ID, opt => opt.Ignore())
           .ForMember(dest => dest.ProductSizeInventoryID, opt => opt.MapFrom(src => src.ProductSizeInventoryID))
           .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.OrderID))
           .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
           .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<DetailOrder, DetailOrderModel>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
           .ForMember(dest => dest.ProductSizeInventoryID, opt => opt.MapFrom(src => src.ProductSizeInventoryID))
           .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.OrderID))
           .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
           .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));


            CreateMap<InfoPaymentModel, InfoPayment>()
          .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
         .ForMember(dest => dest.TxnRef, opt => opt.MapFrom(src => src.TxnRef))
         .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.OrderID))
         .ForMember(dest => dest.TransactionNo, opt => opt.MapFrom(src => src.TransactionNo))
         .ForMember(dest => dest.UserCreateBy, opt => opt.MapFrom(src => src.UserCreateBy));

         CreateMap<InfoPayment,InfoPaymentModel>()
        .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
        .ForMember(dest => dest.TxnRef, opt => opt.MapFrom(src => src.TxnRef))
        .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.OrderID))
        .ForMember(dest => dest.TransactionNo, opt => opt.MapFrom(src => src.TransactionNo))
        .ForMember(dest => dest.UserCreateBy, opt => opt.MapFrom(src => src.UserCreateBy));

        CreateMap<ProductReviewModel, ProductReview>()
       .ForMember(dest => dest.ReviewID, opt => opt.Ignore())
       .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
       .ForMember(dest => dest.DetailOrderID, opt => opt.MapFrom(src => src.DetailOrderID))
       .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
       .ForMember(dest => dest.Review, opt => opt.MapFrom(src => src.Review))
       .ForMember(dest => dest.ReviewDate, opt => opt.MapFrom(src => src.CreateTime));

        CreateMap<ProductReview,ProductReviewModel>()
       .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ReviewID))
       .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
       .ForMember(dest => dest.DetailOrderID, opt => opt.MapFrom(src => src.DetailOrderID))
       .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
       .ForMember(dest => dest.Review, opt => opt.MapFrom(src => src.Review))
       .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.ReviewDate));


            CreateMap<PermissionModel, Permission>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PermissionName, opt => opt.MapFrom(src => src.PermissionName));

            CreateMap<Permission, PermissionModel>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PermissionName, opt => opt.MapFrom(src => src.PermissionName));

            CreateMap<MenuModel, AspNetMenu>()
              .ForMember(dest => dest.ID, opt => opt.Ignore())
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.URL, opt => opt.MapFrom(src => src.URL))
              .ForMember(dest => dest.icon, opt => opt.MapFrom(src => src.icon))
              .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.title));


            CreateMap<AspNetMenu, MenuModel>()
              .ForMember(dest => dest.ID, opt => opt.MapFrom(src=>src.ID))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.URL, opt => opt.MapFrom(src => src.URL))
              .ForMember(dest => dest.icon, opt => opt.MapFrom(src => src.icon))
              .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.title));

        }
    }
}
