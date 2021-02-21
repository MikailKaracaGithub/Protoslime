using UnityEditor;
using UnityEngine;

using System.IO;

using GameDesign.Core.AssetBundle;

public class BundleBuilder : Editor
{
    static string _pathAssembly;

    [MenuItem("Game Design/Export/Build AssetBundles")]
    static void BuildAssetbundles()
    {
        if( SetAssembly())
        {
            AssetDatabase.SaveAssets();
            string path = EditorUtility.OpenFolderPanel("Where to save?", "", "");
            if (!string.IsNullOrEmpty(path))//In case they canceled the action
            {
                BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
                Debug.Log("Export was succesfull.");
            }
            ClearCache();
        }
        else
        {
            EditorUtility.DisplayDialog("Error", "No assembly found", "ok");
        }
     }

    static bool SetAssembly()
    {
        string[] assets = AssetDatabase.FindAssets("t:GameInfoData");
        if (assets != null && assets.Length > 0)
        {
            GameInfoData info = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(assets[0])) as GameInfoData;
            if (info)
            {
                string assemblyDef = info.AssemblyDefinition;
                DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath);
                dirInfo = new DirectoryInfo(Path.Combine(dirInfo.Parent.ToString(), "Library\\ScriptAssemblies"));
                string path = dirInfo + "\\" + assemblyDef + ".dll";

                if (File.Exists(path))//Is there an assembly?
                {
                    string nameAssembly = "assembly.bytes";
                    _pathAssembly = Path.Combine(Application.dataPath, nameAssembly);

                    ClearCache();//In case the file already exist

                    File.Copy(path, _pathAssembly);
                    AssetDatabase.Refresh();
                    info.AssemblyTextAsset = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/"+nameAssembly, typeof(TextAsset));
                    if( info.AssemblyTextAsset != null)
                    {
                        return true;
                    }
                }
                Debug.LogWarning("Assembly does not exist, or wrong name...");
            }
            Debug.LogWarning("Couldn't find you 'Game Info Data'");
        }

        return false;
    }

    static void ClearCache()
    {
        if (!string.IsNullOrEmpty(_pathAssembly))
        {
            if (File.Exists(_pathAssembly + ".meta"))
            {
                File.Delete(_pathAssembly + ".meta");
            }
            if (File.Exists(_pathAssembly))
            {
                File.Delete(_pathAssembly);
            }

            AssetDatabase.Refresh();
        }
    }
}
