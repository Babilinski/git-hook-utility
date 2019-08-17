using System.IO;
using UnityEngine;

namespace GitHookUtility
{
    /// <summary>
    /// This class is used to copy/replace the githook files 
    /// </summary>
    public static class FileUtilities
    {

        public static void CopyFile(string sourcePath, string targetPath, bool replace)
        {
            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!Directory.Exists(Path.GetDirectoryName(targetPath)))
            {
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
            }

            if (!replace)
            {
                if (!System.IO.File.Exists(targetPath))
                {
                    Debug.Log("This file already exists" + targetPath);
                    return;
                }
            }
            else
            {
                System.IO.File.Copy(sourcePath, targetPath, true);
            }

        }

        public static bool FileExists(string targetPath)
        {
            if (System.IO.File.Exists(targetPath))
            {

                return true;
            }

            return false;
        }
    }
}