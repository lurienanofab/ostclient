namespace OstClient.Models
{
    public enum TicketSearchType
    {
        None = 0,
        ByEmail = 1,
        ByResource = 2
    }

    public static class TicketSearchTypeUtil
    {
        public static TicketSearchType Parse(string value)
        {
            //"by-resource", "by-email" or ""

            switch (value)
            {
                case "by-email":
                    return TicketSearchType.ByEmail;
                case "by-resource":
                    return TicketSearchType.ByResource;
                default:
                    return TicketSearchType.None;
            }
        }

        public static string ToString(TicketSearchType searchType)
        {
            //"by-resource", "by-email" or ""

            switch (searchType)
            {
                case TicketSearchType.ByEmail:
                    return "by-email";
                case TicketSearchType.ByResource:
                    return "by-resource";
                default:
                    return string.Empty;
            }
        }
    }
}
