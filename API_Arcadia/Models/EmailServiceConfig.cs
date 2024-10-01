namespace API_Arcadia.Models
{
    public class EmailServiceConfig
    {
        public Config? config { get; set; }
        public Credentials? AsAdmin {  get; set; }
        public Credentials? AsEmployee { get; set; }
        public Credentials? AsVisitor { get; set; }
    }

    public class Config
    {
        public string smtp { get; set; } = string.Empty;
        public int port { get; set; }
    }

    public class Credentials
    {
        public string id { get; set; } = string.Empty;
        public string pwd { get; set; } = string.Empty;
    }
}
