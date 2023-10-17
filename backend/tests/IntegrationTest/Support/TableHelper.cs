namespace FuncionalTest.Support
{
    public static class TableHelper
    {
        public static Dictionary<string, string> ToDictionary(this Table table)
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var key in table.Rows[0].Keys)
            {
                dictionary.Add(key, table.Rows[0][key]);
            }

            return dictionary;
        }
    }
}