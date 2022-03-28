namespace ArangoDBNetStandard.ViewsApi.Models
{
    public class DeleteViewResponse :ResponseBase
    {
        public bool Result { get; set; }
        public string Id { get; set; }
    }
}
