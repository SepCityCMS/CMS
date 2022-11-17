namespace SepCityCMS.Server.Controllers.API
{

    public class ResponseHelper
    {

        public static long returnTotalPages(int totalCount, int recordsPerPage)
        {
            return (totalCount / recordsPerPage) + 1;
        }

    }

}
