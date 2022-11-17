namespace SepCommon.Models
{
    public class APICalls
    {
        public long APIID { get; set; }

        public string Method { get; set; }

        public string ApiURL { get; set; }

        public string ApiHeaders { get; set; }

        public string ApiBody { get; set; }

        public int Status { get; set; }

    }
}
