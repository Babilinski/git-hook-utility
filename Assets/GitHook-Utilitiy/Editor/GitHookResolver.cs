using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GitHookUtility
{
    /// <summary>
    /// Class that is responsible for moving the githook files from the assets folder to the .git hooks directory
    /// </summary>
    public static class GitHookResolver
    {

        private static string ProjectFolderPath
        {
            get { return Application.dataPath + "/../"; }
        }


        public static void MoveGitHooksToFolder()
        {
            AddHooksToMainDirectory();
            AddHooksToSubmodules();
        }

        private static void AddHooksToMainDirectory()
        {

            string gitHooksPath = ProjectFolderPath + "/.git/hooks/";
            AddGitHooksToFolder(gitHooksPath);
        }

        private static void AddGitHooksToFolder(string folderPath, string ignoreFilter = null)
        {
            if (!Directory.Exists(folderPath))
            {

                return;
            }


            string[] gitHookPaths = GitHookFinder.FindGitHooksInProject();

            for (int i = 0; i < gitHookPaths.Length; i++)
            {
                string sourcePath = gitHookPaths[i];
                string fileName = Path.GetFileName(sourcePath);
                if (string.IsNullOrEmpty(ignoreFilter) != true)
                {
                    if (fileName.Equals(ignoreFilter))
                    {
                        return;
                    }
                }

                string targetPath = folderPath + fileName;
                FileUtilities.CopyFile(sourcePath, targetPath, true);
            }
        }

        private static void AddHooksToSubmodules()
        {
            string submoduleFilePath = ProjectFolderPath + ".gitmodules";
            if (File.Exists(submoduleFilePath))
            {
                string[] subModulePaths = ParseModulesFile(submoduleFilePath);
                for (int i = 0; i < subModulePaths.Length; i++)
                {
                    string gitHooksPath = ProjectFolderPath + "/.git/modules/" + subModulePaths[i] + "/hooks/";
                    AddGitHooksToFolder(gitHooksPath, "post-checkout");
                }
            }
        }



        private static string[] ParseModulesFile(string path)
        {
            //Get all of the lines that start with "path" and then get the value after the = sign
            return File.ReadLines(path).Where(l => l.TrimStart().StartsWith("path")).Select(l =>
            {
                var line = l.Trim(); // remove spaces or tabs from start and end of line
                string[] token = line.Split(new[] {' ', '='}, StringSplitOptions.RemoveEmptyEntries);
                string submodulePath = token[1];
                //Format to access directory
                return submodulePath + "/";
            }).ToArray();

        }

        public static bool CheckGitHooks()
        {
            return MainContainsGitHooks() && SubmodulesContainGitHooks();
        }

        /// <summary>
        /// Check if githooks already exist in the directory
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns>returns true if all of the hooks exist</returns>
        private static bool FolderContainsAllGitHooks(string folderPath)
        {
            string[] gitHookPaths = GitHookFinder.FindGitHooksInProject();
            for (int i = 0; i < gitHookPaths.Length; i++)
            {
                string sourcePath = gitHookPaths[i];
                string fileName = Path.GetFileName(sourcePath);
                string targetPath = folderPath + fileName;
                if (!FileUtilities.FileExists(targetPath))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool MainContainsGitHooks()
        {
            string gitHooksPath = ProjectFolderPath + "/.git/hooks/";
            return FolderContainsAllGitHooks(gitHooksPath);
        }

        public static bool SubmodulesContainGitHooks()
        {
            string submoduleFilePath = ProjectFolderPath + ".gitmodules";
            if (File.Exists(submoduleFilePath))
            {
                string[] subModulePaths = ParseModulesFile(submoduleFilePath);
                for (int i = 0; i < subModulePaths.Length; i++)
                {
                    string gitHooksPath = ProjectFolderPath + "/.git/modules/" + subModulePaths[i] + "/hooks/";
                    bool doesContainHooks = FolderContainsAllGitHooks(gitHooksPath);
                    if (!doesContainHooks)
                    {
                        return false;
                    }

                }
            }

            return true;
        }
    }
}