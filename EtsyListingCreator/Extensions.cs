namespace EtsyListingCreator
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        
        public static string QuickFormat(this string value, object arg1)
        {
            return string.Format(value, arg1);
        }
        public static string QuickFormat(this string value, object arg1, object arg2)
        {
            return string.Format(value, arg1, arg2);
        }
        public static string QuickFormat(this string value, object arg1, object arg2, object arg3)
        {
            return string.Format(value, arg1, arg2, arg3);
        }
        public static string QuickFormat(this string value, object[] values)
        {
            return string.Format(value, values);
        }

    }
}