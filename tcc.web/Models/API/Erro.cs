namespace tcc.web.Models.API
{
    public class Erro
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string detail { get; set; }
        public string traceId { get; set; }
    }

}
