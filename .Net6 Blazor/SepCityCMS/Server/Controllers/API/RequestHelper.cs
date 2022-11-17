namespace SepCityCMS.Server.Controllers.API
{
    public class RequestHelper
    {

        public static void getPages(ref int currentPage, ref int recordsPerPage)
        {
            if (recordsPerPage == 0)
            {
                recordsPerPage = Server.SepFunctions.toInt(Server.SepFunctions.Setup(992, "RecPerAPage"));
            }
            if (recordsPerPage < 5)
            {
                recordsPerPage = 5;
            }
            if (currentPage == 0)
            {
                currentPage = 1;
            }
        }
    }
}
