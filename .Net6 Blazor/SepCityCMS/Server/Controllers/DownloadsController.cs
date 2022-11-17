
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SepCityCMS.Server;

    public class DownloadsController : ControllerBase
    {
        [CheckOption("username", "LibraryUpload")]
        [HttpPost]
        public Models.API.APIResponse PostDownloads(Models.Downloads Download)
        {
            var FileID = SepFunctions.GetIdentity();
            if (Download.FileID != 0) FileID = Download.FileID;

            Server.DAL.Downloads.Download_Save(FileID, SepFunctions.Session_User_ID(), Download.CatID, Download.Field1, Download.Field2, Download.Field3, Download.Field4, Download.eDownload, true, Download.FileName, Download.PortalID);

            var cResponse = new Models.API.APIResponse
            {
                Id = FileID
            };

            return cResponse;
        }
    }
}