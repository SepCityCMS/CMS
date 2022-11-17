namespace SepCityCMS.Models.Config
{
    public class Jwt
    {
        public class JwtInfo
        {
            public string Key { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
        }

        public class Root
        {
            public JwtInfo JwtInfo { get; set; }
        }
    }
}
