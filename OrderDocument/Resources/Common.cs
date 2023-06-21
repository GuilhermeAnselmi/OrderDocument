namespace OrderDocument.Resources
{
    public static class Common
    {
        public static string GetDocumentPath()
        {
            string path = $"{FileSystem.AppDataDirectory}/documents/";

            return path;
        }
    }
}
