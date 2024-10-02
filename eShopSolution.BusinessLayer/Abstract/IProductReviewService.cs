using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IProductReviewService:IGenericService<ProductReviewModel,ProductReview>
    {
        public Task<Response<PaginationProductReview>>GetProductReviewPage(int ProductID,int Page, int Size);
        public Task<Response<ProductReviewModel>> CreateProductReview(ProductReviewModel ProductReview);
    }
}
