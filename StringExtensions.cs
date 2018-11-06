namespace mvc_album_browser
{
    public static class StringExtensions
    {
        public static bool CaseInsensitiveEquals(this string str, string eq, bool trim = true)
        {
            str = trim ? str.Trim() : str;
            eq = trim ? eq.Trim() : eq;

            return str.ToUpper().Equals(eq.ToUpper());
        }
        public static bool CaseInsensitiveContains(this string str, string contains, bool trim = true)
        {
            str = trim ? str.Trim() : str;
            contains = trim ? contains.Trim() : contains;

            return str.ToUpper().Contains(contains.ToUpper());
        }
    }
}