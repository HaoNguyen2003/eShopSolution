using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.UpdateModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class CommentsDal : ICommentsDal
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CommentsDal(ApplicationContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task<Response<CommentModel>> CreateOrReplyComment(string userID, int productID, string content, int? parentCommentID = null)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user == null)
                return new Response<CommentModel> { IsSuccess = false, Error = "Not Found UserID" };
            var comment = new Comments()
            {
                UserID = userID,
                ProductID = productID,
                CommentDate = DateTime.Now,
                Content = content,
                ParentCommentID = parentCommentID
            };
            if (parentCommentID.HasValue)
            {
                Tuple<int, int> commentDisplayID = await FindLevelCommentParent(parentCommentID.Value, 1, parentCommentID.Value);
                comment.DisplayCommentLevelID = commentDisplayID.Item2;
            }
            try
            {
                await _context.comments.AddAsync(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new Response<CommentModel> { IsSuccess = false, Error = ex.Message };
            }
            var commentModel = new CommentModel()
            {
                Id = comment.ID,
                Content = comment.Content,
                appUserModel = new AppUserModel()
                {
                    Id = user.Id,
                    avatar = user.ImageURL,
                    Email = user.Email,
                    Username = $"{user.FirstName} {user.LastName}"
                },
                CreatedAt = comment.CommentDate ?? DateTime.Now,
                UpdatedAt = comment.CommentUpdate,
                HasChild = 0,
                ParentCommentId =comment.DisplayCommentLevelID,
                Replies = new List<CommentModel>(),
            };

            return new Response<CommentModel> { IsSuccess = true, Value = commentModel };
        }
        public async Task<Response<CommentModel>> CreateComment(AddComment addComment)
        {
            return await CreateOrReplyComment(addComment.UserID, addComment.ProductID, addComment.Content);
        }

        public async Task<Response<CommentModel>> ReplyComment(ReplyComment replyComment)
        {
            if (replyComment.ParentCommentID == 0)
                return new Response<CommentModel> { IsSuccess = false, Error = "Not Found ParentComment" };
            return await CreateOrReplyComment(replyComment.UserID, replyComment.ProductID, replyComment.Content, replyComment.ParentCommentID);
        }

        public async Task<Response<List<CommentModel>>> DeleteComment(int ID,string UserID)
        {
            var comment = await _context.comments.FindAsync(ID);
            if (comment == null)
                return new Response<List<CommentModel>> { IsSuccess = false, Error = "Not Found Comment" };
            if (comment.UserID != UserID)
                return new Response<List<CommentModel>> { IsSuccess = false, Error = "You do not have permission to delete this comment. " };
            await RemoveCommentID(ID);
            return new Response<List<CommentModel>> { IsSuccess = true };
        }
        private async Task<PaginationComment> GetPaginatedCommentsAsync(IQueryable<Comments> query, int pageSize, int pageNumber, bool includeReplies = false)
        {
            var totalComments = await query.CountAsync();
            var comments = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(c => c.AppUser)
                .ToListAsync();
            var commentIds = comments.Select(c => c.ID).ToList();
            var commentModels = comments.Select(c => new CommentModel
            {
                Id = c.ID,
                appUserModel = c.AppUser != null ? new AppUserModel
                {
                    Id = c.AppUser.Id,
                    avatar = c.AppUser.ImageURL,
                    Email = c.AppUser.Email,
                    Username = c.AppUser.FirstName + " " + c.AppUser.LastName
                } : null,
                Content = c.Content ?? string.Empty,
                CreatedAt = c.CommentDate ?? DateTime.Now,
                UpdatedAt = c.CommentDate ?? DateTime.Now,
                ParentCommentId = c.ParentCommentID,
                HasChild = _context.comments.Count(p => p.DisplayCommentLevelID == c.ID),
                Replies = includeReplies ? new List<CommentModel>() : null
            }).ToList();

            return new PaginationComment
            {
                comments = commentModels,
                pageNumber = pageNumber,
                pageSize = pageSize,
                Remaining = totalComments - (pageNumber * pageSize)
            };
        }
        public async Task<Response<PaginationComment>> GetAllCommentByProductID(int productID, int pageNumber, int pageSize)
        { 
            var commentsQuery = _context.comments
                .Where(c => c.ProductID == productID && c.ParentCommentID == null)
                .OrderByDescending(c => c.CommentDate)
                .Include(c => c.AppUser);
            var paginationComment = await GetPaginatedCommentsAsync(commentsQuery, pageSize, pageNumber,true);
            return new Response<PaginationComment>
            {
                IsSuccess = true,
                Value = paginationComment
            };
        }
        public async Task<Response<PaginationComment>> GetCommentOfParentId(int parentId, int Pagesize, int PageNumber)
        {
            var commentsQuery = _context.comments
                .Where(c => c.DisplayCommentLevelID == parentId)
                .OrderBy(c => c.CommentDate)
                .Include(c => c.AppUser);
            var paginationComment = await GetPaginatedCommentsAsync(commentsQuery, Pagesize, PageNumber, true);
            return new Response<PaginationComment>
            {
                IsSuccess = true,
                Value = paginationComment
            };
        }
        public async Task<Response<CommentModel>> UpdateComment(UpdateComment updateComment)
        {
            try
            {
                var comment = await _context.comments.FindAsync(updateComment.Id);
                if (comment == null)
                    return new Response<CommentModel>() { IsSuccess = false, Error = "Not Found Comment" };

                if (updateComment.UserID != comment.UserID)
                    return new Response<CommentModel>() { IsSuccess = false, Error = "You do not have permission to edit this comment." };

                var user = await _userManager.FindByIdAsync(updateComment.UserID);
                if (user == null)
                    return new Response<CommentModel>() { IsSuccess = false, Error = "Not Found User." };
                comment.CommentUpdate = DateTime.Now;
                comment.Content = updateComment.Content;
                await _context.SaveChangesAsync();
                var commentModel = new CommentModel()
                {
                    Id = comment.ID,
                    Content = comment.Content,
                    appUserModel = new AppUserModel()
                    {
                        Id = user.Id,
                        avatar = user.ImageURL,
                        Email = user.Email,
                        Username = $"{user.FirstName} {user.LastName}"
                    },
                    CreatedAt = comment.CommentDate ?? DateTime.Now,
                    UpdatedAt = comment.CommentUpdate,
                    HasChild = _context.comments.Count(p => p.DisplayCommentLevelID == comment.ID),
                    ParentCommentId = comment.ParentCommentID,
                    Replies = new List<CommentModel>(),
                };

                return new Response<CommentModel> { IsSuccess = true, Value = commentModel };
            }
            catch (Exception ex)
            {
                return new Response<CommentModel> { IsSuccess = false, Error = ex.Message };
            }
        }
        public async Task RemoveCommentID(int ID)
        {
            var comment = await _context.comments
                                .Include(c => c.Replies)
                                .FirstOrDefaultAsync(c => c.ID == ID);
            if (comment == null) return;
            if(!comment.Replies.Any()|| comment.Replies == null)
            {
                _context.comments.Remove(comment);
                await _context.SaveChangesAsync();
                return;
            }
            foreach (var item in comment.Replies)
            {
                await RemoveCommentID(item.ID);
            }
            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<int, int>> FindLevelCommentParent(int parentID, int level, int displayCommentLevelID)
        {
            var comment = await _context.comments.FindAsync(parentID);
            if (comment == null)
                return Tuple.Create(level, displayCommentLevelID);
            if (!comment.ParentCommentID.HasValue)
                return Tuple.Create(level, displayCommentLevelID);
            displayCommentLevelID = (level < 3) ? parentID : comment.ParentCommentID.Value;
            return await FindLevelCommentParent(comment.ParentCommentID.Value, level + 1, displayCommentLevelID);
        }
    }
}
