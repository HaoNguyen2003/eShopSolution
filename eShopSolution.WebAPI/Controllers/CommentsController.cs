using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.UpdateModel;
using eShopSolution.EntityLayer.Data;
using eShopSolution.RealTime.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.RegularExpressions;
using eShopSolution.WebAPI.Permission;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        private readonly IHubContext<CommentHub> _hubContext;
        public CommentsController(ICommentsService commentsService, IHubContext<CommentHub> hubContext)
        {
            _commentsService = commentsService;
            _hubContext = hubContext;
        }
        [HttpGet("GetAllCommentByProductID")]
        public async Task<IActionResult> GetAllCommentByProductID(int productID, int pageNumber, int pageSize) {
            var result = await _commentsService.GetAllCommentByProductID(productID,pageNumber,pageSize);
            return Ok(result.Value);
        }
        [HttpGet("GetCommentOfParentId")]
        public async Task<IActionResult> GetCommentOfParentId(int parentID, int pageNumber, int pageSize)
        {
            var result = await _commentsService.GetCommentOfParentId(parentID, pageSize, pageNumber);
            return Ok(result.Value);
        }
        [HttpPost("CreateComment")]
        [PermissionAuthorize(PermissionA.Comments + "." + AccessA.Create)]
        public async Task<IActionResult>CreateComment(AddComment addComment)
        {
            var result = await _commentsService.CreateComment(addComment);
            await _hubContext.Clients.Group($"ProductComment_{addComment.ProductID}").SendAsync("ReceiveComment", result.Value);
            return Ok(result);
        }
        [HttpPost("ReplyComment")]
        [PermissionAuthorize(PermissionA.Comments+"."+AccessA.Create)]
        public async Task<IActionResult> ReplyComment(ReplyComment replyComment,string con)
        {
            /*var hubContext = (IHubContext<CommentHub>)HttpContext.RequestServices.GetService(typeof(IHubContext<CommentHub>));
            //await hubContext.Groups.AddToGroupAsync(Context.ConnectionId, $"CommentParent_{replyComment.ParentCommentID}");*/
            var result = await _commentsService.ReplyComment(replyComment);
            //await hubContext.Clients.Group($"ProductComment_{replyComment.ParentCommentID}").SendAsync("ReceiveComment", result.Value);
            return Ok(result);
        }
        [HttpDelete]
        [PermissionAuthorize(PermissionA.Comments + "." + AccessA.Delete)]
        public async Task<IActionResult> DeleteComment(int ID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            var result = await _commentsService.DeleteComment(ID,userId);
            return Ok(result);
        }
        [HttpPut]
        [PermissionAuthorize(PermissionA.Comments + "." + AccessA.Update)]
        public async Task<IActionResult>UpdateComment(UpdateComment updateComment)
        {
            var result = await _commentsService.UpdateComment(updateComment);
            return Ok(result);
        }
        [HttpGet("level")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetLevel(int parentID, int level, int DisplayCommentLevelID)
        {
            var result = await _commentsService.FindLevelCommentParent(parentID, level, DisplayCommentLevelID);
            return Ok(result);
        }
    }
}
