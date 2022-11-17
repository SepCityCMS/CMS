// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="CommentsController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon;
    using SepCommon.Models;
    using System.Security;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class CommentsController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CommentsController : ApiController
    {
        /// <summary>
        /// Posts the comments.
        /// </summary>
        /// <param name="Comment">The comment.</param>
        /// <returns>ResponseHelper.CreateResponse.</returns>
        [HttpPost]
        public ResponseHelper.CreateResponse PostComments(Comments Comment)
        {
            var SEP = RequestHelper.AuthorizeRequest("|1|");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var CommentId = SepFunctions.GetIdentity();
                if (Comment.CommentID > 0) CommentId = Comment.CommentID;
                SepCommon.DAL.Comments.Comment_Save(Comment.CommentID, Comment.UserID, Comment.ModuleID, Comment.UniqueID, Comment.ReplyID, Comment.Message);
                var cResponse = new ResponseHelper.CreateResponse
                {
                    Id = CommentId
                };

                return cResponse;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Posts the comments like.
        /// </summary>
        /// <param name="Comment">The comment.</param>
        /// <returns>ResponseHelper.CreateResponse.</returns>
        [Route("api/comments/like")]
        [HttpPost]
        public ResponseHelper.CreateResponse PostCommentsLike(Comments Comment)
        {
            var SEP = RequestHelper.AuthorizeRequest("|1|");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var LikeId = SepFunctions.GetIdentity();
                if (Comment.LikeID > 0) LikeId = Comment.LikeID;
                SepCommon.DAL.Comments.Comment_Like_Save(Comment.LikeID, Comment.CommentID, Comment.ModuleID, Comment.UserLikes, Comment.UserDislikes, Comment.UserID);
                var cResponse = new ResponseHelper.CreateResponse
                {
                    Id = LikeId
                };

                return cResponse;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}