using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsDal _commentsDal;
        public CommentsService(ICommentsDal commentsDal) {
            _commentsDal = commentsDal;
        }
        public async Task<Response<CommentModel>> CreateComment(AddComment addComment)
        {
            return await _commentsDal.CreateComment(addComment);
        }
        public async Task<Tuple<int, int>> FindLevelCommentParent(int parentID, int level, int DisplayCommentLevelID)
        {
            return await _commentsDal.FindLevelCommentParent(parentID, level, DisplayCommentLevelID);
        }

        public async Task<Response<List<CommentModel>>> DeleteComment(int ID, string UserID)
        {
            return await _commentsDal.DeleteComment(ID, UserID);
        }

        public async Task<Response<PaginationComment>> GetAllCommentByProductID(int productID, int pageNumber, int pageSize)
        {
            return await _commentsDal.GetAllCommentByProductID(productID,pageNumber,pageSize);
        }

        public async Task<Response<PaginationComment>> GetCommentOfParentId(int parentId, int Pagesize, int PageNumber)
        {
            return await _commentsDal.GetCommentOfParentId(parentId, Pagesize, PageNumber);
        }

        public async Task<Response<CommentModel>> ReplyComment(ReplyComment replyComment)
        {
            return await _commentsDal.ReplyComment(replyComment);
        }

        public async Task<Response<CommentModel>> UpdateComment(UpdateComment updateComment)
        {
            return await _commentsDal.UpdateComment(updateComment);
        }
    }
}
