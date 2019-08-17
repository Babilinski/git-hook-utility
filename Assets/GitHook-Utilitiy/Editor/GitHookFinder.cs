using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GitHookUtility
{
    public static class GitHookFinder
    {

        private const string installFileName = "install-hooks";
        private const string postCheckoutFileName = "post-checkout";
        private const string postMergeFileName = "post-merge";
        private const string preCommitFileName = "pre-commit";

        public static string[] FindGitHooksInProject()
        {
            return new[] {GetInstallHook(), GetCheckoutHook(), GetMergeHook(), GetCommitHook()};
        }

        private static string GetInstallHook()
        {
            string guid = AssetDatabase.FindAssets(installFileName)[0];
            return AssetDatabase.GUIDToAssetPath(guid);
        }

        private static string GetCheckoutHook()
        {
            string guid = AssetDatabase.FindAssets(postCheckoutFileName)[0];
            return AssetDatabase.GUIDToAssetPath(guid);
        }

        private static string GetMergeHook()
        {
            string guid = AssetDatabase.FindAssets(postMergeFileName)[0];
            return AssetDatabase.GUIDToAssetPath(guid);
        }

        private static string GetCommitHook()
        {
            string guid = AssetDatabase.FindAssets(preCommitFileName)[0];
            return AssetDatabase.GUIDToAssetPath(guid);
        }

    }
}