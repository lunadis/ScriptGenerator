namespace ScriptGenerator.services.Utils
{
    public static class ExtensionMethods
    {
        public static string ReplaceInput(this string txt, string field, object value)
        {
            return txt.Replace(field, value.ToString());
        }
    }
}