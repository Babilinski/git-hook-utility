using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GitHookUtility
{
    public class GitHooksUtilityWindow : EditorWindow
    {

        //Spacing used in this window
        private const int Spacing = 10;

        //Condition that is set true if serialization settings are correct
        private static bool gitSerializationAdded;

        //Condition that is set true if there are existing git-hooks in the project.
        private static bool gitHooksAdded;


        [SerializeField] //Stores the Help Box's state
        private bool showGitHelp;


        // Add menu named "GitHook Utility" to the Tools menu
        [MenuItem("Tools/GitHook Utility", priority = 1)]
        private static void Init()
        {
            // Get existing open window or if none, make a new one:
            GitHooksUtilityWindow window =
                (GitHooksUtilityWindow) EditorWindow.GetWindow(typeof(GitHooksUtilityWindow),true,"GitHook Utility");

            window.Show();
        }

        private void OnFocus()
        {
            gitSerializationAdded = GitSerializationResolver.CheckGitSerialization();
            gitHooksAdded = GitHookResolver.CheckGitHooks();
        }

        private void OnGUI()
        {
            GUILayout.Space(Spacing);
            SerializationSettings();
            GUILayout.Space(Spacing);
            GitHookSettings();


        }

        /// <summary>
        /// Displays information about git-hooks found in the project and
        /// a button that adds the recommended git-hooks to the .git folder.
        /// </summary>
        private void GitHookSettings()
        {

            DrawTitle("Git Hook Settings");
            if (gitHooksAdded)
            {
                GUILayout.Label("Git hooks already exist.");
                return;
            }

            if (GUILayout.Button("Add Git Hooks"))
            {
                GitHookResolver.MoveGitHooksToFolder();
                gitHooksAdded = true;
            }

            GUILayout.Space(Spacing);

            DrawGitHooksHelpBox();

        }

        /// <summary>
        /// Draws a help box with information about git-hooks 
        /// </summary>
        private void DrawGitHooksHelpBox()
        {
            showGitHelp = EditorGUILayout.Foldout(showGitHelp, "GitHooks Information");
            if (!showGitHelp)
                return;
            GUILayout.Label(
                "It is assumed that Assets directory is located in the root directory of the repository."
                + " It can be configured using git config. The following tells the githook scripts that the assets directory is in 'client/Assets': "
                + "\r\n\r\ngit config unity3d.assets-dir client/Assets",
                EditorStyles.helpBox);
        }

        /// <summary>
        /// Shows information about projects serialization settings, and a button that
        /// automatically sets the project serialization to match unity's recommendation when using 
        /// git.
        /// </summary>
        private void SerializationSettings()
        {

            DrawTitle("Serialization Settings");
            if (gitSerializationAdded)
            {
                GUILayout.Label("Project Serialization Looks Good.");
                return;
            }

            EditorGUILayout.HelpBox(
                "The serialization for this project has not been setup correctly. Please consider fixing it.",
                MessageType.Error);

            if (GUILayout.Button("Fix Serialization"))
            {
                GitSerializationResolver.SetGitSerialization();
                gitSerializationAdded = true;
            }

            GUILayout.Space(Spacing);


            GUILayout.Space(Spacing);
        }

        /// <summary>
        /// Draws a a label with the Title styling.
        /// </summary>
        /// <param name="label"></param>
        private void DrawTitle(string label)
        {
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        }


    }
}