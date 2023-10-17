using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace APPLICATION.Shared.Helpers
{
    public static class ResourcesExtension
    {
        public static async Task<string> ReadFile(string namespacePlace, string directory, string resource, TypeArchives type)
        {
            return await ReadFile($"{namespacePlace}.{directory}", resource, type);
        }

        public static async Task<string> ReadFile(this string namespacePlace, string resource, TypeArchives type)
        {
            var item = $"{namespacePlace}.{resource}.{type.ToString().ToLower()}";
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(item))
            {
                if (stream == null)
                {
                    throw new Exception($"Resource - File not found! {item}");
                }
                else
                {
                    using var reader = new StreamReader(stream);
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}