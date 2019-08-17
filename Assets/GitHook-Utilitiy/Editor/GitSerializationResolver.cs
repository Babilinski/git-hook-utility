using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GitHookUtility
{
    /// <summary>
    /// Class that contains functions for quickly checking/setting the serialization settings in unity
    /// </summary>
    public static class GitSerializationResolver
    {

        private const string versionControl = "Visible Meta Files";

        public static bool CheckGitSerialization()
        {
            return EditorSettings.serializationMode == SerializationMode.ForceText
                   && EditorSettings.externalVersionControl.Equals(versionControl);
        }

        public static void SetGitSerialization()
        {
            EditorSettings.serializationMode = SerializationMode.ForceText;
            EditorSettings.externalVersionControl = versionControl;
        }
    }
}
