
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class ArticlesController : ControllerBase
    {
        [CheckOption("username", "ArticlesAccess")]
        [Route("api/articles/{id:long}")]
        [HttpGet]
        public Models.Articles GetArticles(long id)
        {
            return Server.DAL.Articles.Article_Get(id);
        }

        [CheckOption("username", "ArticlesAccess")]
        [Route("api/articles")]
        [HttpGet]
        public Models.API.SearchResponse<Models.Articles> GetArticles([FromQuery] int page, [FromQuery] int numRecords, [FromQuery] string keywords, [FromQuery] long CategoryId, [FromQuery] string sortBy, [FromQuery] string sortByDirection, [FromQuery] bool showAvailable)
        {
            API.RequestHelper.getPages(ref page, ref numRecords);

            List<Models.Articles> listArticles = Server.DAL.Articles.GetArticles(sortBy, sortByDirection, keywords, CategoryId, showAvailable: showAvailable);

            Models.API.SearchResponse<Models.Articles> response = new Models.API.SearchResponse<Models.Articles>();
            response.RecordCount = listArticles.Count;
            response.TotalPages = API.ResponseHelper.returnTotalPages(listArticles.Count, numRecords);
            response.Results = listArticles.Skip(numRecords * (page - 1)).Take(numRecords).ToList();

            return response;
        }

        [CheckOption("username", "ArticlesPost")]
        [Route("api/articles")]
        [HttpPost]
        public Models.API.APIResponse PostActivities([FromBody] Models.Articles Article)
        {
            Models.API.APIResponse cResponse = new Models.API.APIResponse();
            try
            {
                var ID = Article.ArticleID;
                cResponse.Message = Server.DAL.Articles.Article_Save(ref ID, Article.UserID, Article.CatID, Article.Headline, Article.Author, Article.Headline_Date, Article.Start_Date, Article.End_Date, Article.Summary, Article.Full_Article, Article.Source, Article.Article_URL, Article.Meta_Description, Article.Meta_Keywords, Article.Related_Articles, Article.Status, Article.PortalID);
                cResponse.Id = ID;
                cResponse.Success = true;
            }
            catch (Exception ex)
            {
                cResponse.Id = 0;
                cResponse.Success = false;
                cResponse.Message = ex.Message;
            }
            return cResponse;
        }

        [CheckOption("username", "ArticlesPost")]
        [Route("api/articles")]
        [HttpPut]
        public Models.API.APIResponse PutArticles([FromQuery] long ID, [FromBody] Models.Articles Article)
        {
            Models.API.APIResponse cResponse = new Models.API.APIResponse();
            try
            {
                cResponse.Message = Server.DAL.Articles.Article_Save(ref ID, Article.UserID, Article.CatID, Article.Headline, Article.Author, Article.Headline_Date, Article.Start_Date, Article.End_Date, Article.Summary, Article.Full_Article, Article.Source, Article.Article_URL, Article.Meta_Description, Article.Meta_Keywords, Article.Related_Articles, Article.Status, Article.PortalID);
                cResponse.Id = ID;
                cResponse.Success = true;
            }
            catch (Exception ex)
            {
                cResponse.Id = ID;
                cResponse.Success = false;
                cResponse.Message = ex.Message;
            }
            return cResponse;
        }

        [CheckOption("username", "ArticlesAdmin")]
        [Route("api/articles")]
        [HttpPut]
        public Models.API.APIResponse DeleteArticles([FromQuery] long ID)
        {
            var cResponse = new Models.API.APIResponse();

            try
            {
                cResponse.Message = Server.DAL.Articles.Article_Delete(Server.SepCore.Strings.ToString(ID));
                cResponse.Id = ID;
                cResponse.Success = true;
            }
            catch (Exception ex)
            {
                cResponse.Id = ID;
                cResponse.Success = false;
                cResponse.Message = ex.Message;
            }

            return cResponse;
        }

    }
}