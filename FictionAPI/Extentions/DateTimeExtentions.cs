namespace FictionAPI.Extentions
{
    public static class DateTimeExtentions
    {
        public static String GetTimestamp(this DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssfff");
        }
    }
}
