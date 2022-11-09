    public static class PathHelper {
        public static string WebRootPath { get; private set; }

        public static void SetWebRootPath(string value)
        {
            WebRootPath = value;
        }
        public static string GetFullPathNormalized(string path)
        {
            return Path.TrimEndingDirectorySeparator(Path.GetFullPath(path));
        }

        public static string MapPath(string path, string basePath = null)
        {
            if (string.IsNullOrEmpty(basePath))
            {
                basePath = WebRootPath;
            }

            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return GetFullPathNormalized(Path.Combine(basePath, path));
        }
    }
