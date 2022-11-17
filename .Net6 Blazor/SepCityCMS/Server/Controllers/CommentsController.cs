
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SepCityCMS.Server;

    public class CommentsController : ControllerBase
    {
        [CheckOption("username", "Everyone")]
        [HttpPost]
        public Models.API.APIResponse PostComments(Models.Comments Comment)
        {
            var CommentId = SepFunctions.GetIdentity();
            if (Comment.CommentID > 0) CommentId = Comment.CommentID;
            Server.DAL.Comments.Comment_Save(Comment.CommentID, Comment.UserID, Comment.ModuleID, Comment.UniqueID, Comment.ReplyID, Comment.Message);
            var cResponse = new Models.API.APIResponse
            {
                Id = CommentId
            };

            return cResponse;
        }

        [CheckOption("username", "Everyone")]
        [Route("api/comments/like")]
        [HttpPost]
        public Models.API.APIResponse PostCommentsLike(Models.Comments Comment)
        {
            var LikeId = SepFunctions.GetIdentity();
            if (Comment.LikeID > 0) LikeId = Comment.LikeID;
            Server.DAL.Comments.Comment_Like_Save(Comment.LikeID, Comment.CommentID, Comment.ModuleID, Comment.UserLikes, Comment.UserDislikes, Comment.UserID);
            var cResponse = new Models.API.APIResponse
            {
                Id = LikeId
            };

            return cResponse;
        }
    }
}