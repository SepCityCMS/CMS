namespace SepCityCMS.Models.Config
{
    public class System
    {

        public class Root
        {
            public string EncryptionKey { get; set; }
            public string ConnectionString { get; set; }
            public string MailServerIP { get; set; }
            public string MailServerUser { get; set; }
            public string MailServerPass { get; set; }
            public string MailServerPort { get; set; }
        }

    }
}
