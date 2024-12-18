﻿using eShopSolution.CrawlData.Model;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IProductService : IGenericService<ProductModel, Product>
    {
        public Task<BaseRep<DetailProduct>> GetDetailProductByProductIDAndProductColorID(int ProductID, int ColorID);
        public Task<BaseRep<ProductDashBoard>> GetProductInDashBoardByProductIDAndProductColorID(int ProductID, int ColorID);
        public Task<BaseRep<List<ProductCardModel>>> GetAllProduct();
        public Task<BaseRep<List<String>>> DeleteProduct(int ID);
        public Task<BaseRep<PagedResult>> GetProductByFilterAndPage(FilterModel model, int Page, int Limit);
        public Task<BaseRep<string>> CreateProduct(ProductModel productModel, List<ProductDataNew>? newCollectionModels = null, List<CollectionModel>? oldCollectionModels = null);
    }
}
