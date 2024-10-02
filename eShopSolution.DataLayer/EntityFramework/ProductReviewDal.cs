using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class ProductReviewDal : GenericDal<ProductReviewModel, ProductReview>, IProductReviewDal
    {
        public ProductReviewDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Response<ProductReviewModel>> CreateProductReview(ProductReviewModel ProductReview)
        {
            try
            {
                var CurrentProductReview = await _context.ProductReviews
                            .FirstOrDefaultAsync(x => x.ProductID == ProductReview.ProductID
                            && x.DetailOrderID == ProductReview.DetailOrderID
                            && x.UserID == ProductReview.UserID);
                if (CurrentProductReview != null)
                    return new Response<ProductReviewModel>() { IsSuccess = false, Error = "You have rated products in this order" };
                var ProductReviewData = _mapper.Map<ProductReview>(ProductReview);
                var result = await _context.ProductReviews.AddAsync(ProductReviewData);
                await _context.SaveChangesAsync();
                return new Response<ProductReviewModel>() { IsSuccess = true, Value = _mapper.Map<ProductReviewModel>(ProductReviewData) };
            }catch (Exception ex)
            {
                return new Response<ProductReviewModel>() { IsSuccess = false, Error = ex.Message };
            }
        }

        public async Task<Response<PaginationProductReview>> GetProductReviewPage(int ProductID, int Page, int Size)
        {
            var response = new Response<PaginationProductReview>();

            try
            {
                var query = _context.ProductReviews
                                    .Where(x => x.ProductID == ProductID)
                                    .OrderByDescending(x => x.ReviewDate);
                var totalCount = await query.CountAsync();
                var reviews = await query.Skip((Page - 1) * Size)
                                         .Take(Size)
                                         .Select(x => new ProductReviewModel
                                         {
                                             Review = x.Review,
                                             ProductID = x.ProductID,
                                             UserID = x.UserID,
                                             Rating = x.Rating,
                                             DetailOrderID = x.DetailOrderID,
                                             CreateTime = x.ReviewDate,
                                             ID = x.ReviewID,
                                         })
                                         .ToListAsync();
                var remaining = totalCount - (Page * Size);
                var pagination = new PaginationProductReview
                {
                    ProductReviewModels = reviews,
                    pageNumber = Page,
                    pageSize = Size,
                    Remaining = remaining > 0 ? remaining : 0
                };
                response.Value = pagination;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
            }

            return response;
        }
    }
}
