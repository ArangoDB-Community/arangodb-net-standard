namespace ArangoDBNetStandard.CursorApi
{
    public class CursorResponseStatsNode
    {
        public long Id { get; set; }

        public long Calls { get; set; }

        public long Items { get; set; }

        public double Runtime { get; set; }
    }
}
