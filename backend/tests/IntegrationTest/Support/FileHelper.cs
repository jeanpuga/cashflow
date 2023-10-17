using System.Reflection;

namespace FuncionalTest.Support
{
    public class FileHelper
    {
        public static string LoadFile(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return "";

            var assembly = Assembly.GetExecutingAssembly();
            if (assembly == null) return "";

            var resourceName = $"IntegrationTest.Support.Files.{filename}.xlsx";

            using Stream? stream = assembly?.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                using StreamReader reader = new(stream);
                return reader.ReadToEnd();
            }
            else
            {
                return "";
            }
        }

        public static string GetPath(string filename)
        {
            return Directory.GetCurrentDirectory() + "\\Support\\Files\\" + filename;
        }
    }
}