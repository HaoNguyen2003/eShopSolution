using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.Abstract
{
    public interface ICommentsDal
    {
        public Task<Response<PaginationComment>> GetAllCommentByProductID(int productID, int pageNumber, int pageSize);
        public Task<Response<PaginationComment>> GetCommentOfParentId(int parentId, int Pagesize, int PageNumber);
        public Task<Response<CommentModel>> CreateComment(AddComment addComment);
        public Task<Response<CommentModel>> ReplyComment(ReplyComment replyComment);
        public Task<Response<CommentModel>> UpdateComment(UpdateComment updateComment);
        public Task<Response<List<CommentModel>>> DeleteComment(int ID,string UserID);
        public Task<Tuple<int, int>> FindLevelCommentParent(int parentID, int level, int DisplayCommentLevelID);
    }
}
