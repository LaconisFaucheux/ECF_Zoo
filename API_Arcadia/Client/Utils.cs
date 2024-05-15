namespace BlazorWasm.Client
{
    public static class Utils
    {
        public static string Capitalize1stLetter (string str)
        {
            return str.Replace(str[0], char.ToUpper(str[0]));
        }
    }
}
