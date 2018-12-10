using System.IO;
using UnityEngine;

namespace DIWidgetEditor
{
    public static class DIWidgetEditorUtility
    {
        [SerializeField] private static string _packagePath;

        [SerializeField] private static string _packageFullPath;

        private static string _folderPath = "Not Found";

        /// <summary>
        /// Returns the relative path of the package.
        /// </summary>
        public static string PackageRelativePath
        {
            get
            {
                if (string.IsNullOrEmpty(_packagePath))
                    _packagePath = GetPackageRelativePath();

                return _packagePath;
            }
        }

        private static string GetPackageRelativePath()
        {
            string packagePath = Path.GetFullPath("Packages/com.comcreate-info.di_widget");
            if (Directory.Exists(packagePath))
            {
                return "Packages/com.comcreate-info.di_widget";
            }

            packagePath = Path.GetFullPath("Assets/..");
            if (Directory.Exists(packagePath))
            {
                if (Directory.Exists(packagePath + "/Assets/Packages/com.comcreate-info.di_widget/Editor Resources"))
                {
                    return "Assets/Packages/com.comcreate-info.di_widget";
                }

                if (Directory.Exists(packagePath + "/Assets/DIWidget/Editor Resources"))
                {
                    return "Assets/DIWidget";
                }

                string[] matchingPaths = Directory.GetDirectories(packagePath, "DIWidget", SearchOption.AllDirectories);
                packagePath = ValidateLocation(matchingPaths, packagePath);
                if (packagePath != null) return packagePath;
            }

            return null;
        }

        private static string ValidateLocation(string[] paths, string projectPath)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                if (Directory.Exists(paths[i] + "/Editor Resources"))
                {
                    _folderPath = paths[i].Replace(projectPath, "");
                    _folderPath = _folderPath.TrimStart('\\', '/');
                    return _folderPath;
                }
            }

            return null;
        }
    }
}