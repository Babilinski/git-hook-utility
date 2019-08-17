# Unity Githooks Utility

**Add git-hooks to your unity project with a click**

Git-hooks are an easy eay to prevent your team from pushing and pulling empty directories when working with Unity and Git. The only issue is, git hooks do not transfer. This means that anytime you pull down a new project you have to re-add the git hooks. So I made a unity editor tool that adds the githooks to the unity project and it's submodules with a click of a button.

![Screenshot of the GitHook Utility Window](https://github.com/Babilinski/git-hook-utility/blob/master/screenshot.PNG?raw=true)
*Screenshot of the GitHook Utility Window*

# Features
This tool also allows you to:
  - Transfer the Githook files from a directory inside of your project, to the .git/hooks folder
  - Change your project's serialization settings to match Unity's Recommendations 


# Installation
  - Importing the .unity package
    - Download the package from this git repository 
    - Import the package into your project Assets/Import Package/Custom Package... 
    - Complete the Import Package Dialogue
  - Open the the  GitHooks Utility Window (Tools/GitHoot-Utility)
    - Naviage to the "tools" drop-down menu located at the top of Unity's Application Window
    
### Credits

The githooks that are included in the project come from git user Doitian: [doitian/unity-git-hooks](https://github.com/doitian/unity-git-hooks)

License
----

MIT

