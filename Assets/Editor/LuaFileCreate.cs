using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;

public class CreateLua {
    [MenuItem("Assets/Create/Lua Script", false, 80)]

    public static void CreateNewLua()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
            ScriptableObject.CreateInstance<CreateScriptAssetAction>(),
            GetSelectedPathOrFallback() + "/New Lua.lua",
            null,
            "Assets/Editor/Template/LuaComponent.lua");
    }

    public static string GetSelectedPathOrFallback()
    {
        string path = "Assets";
        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
}

class CreateScriptAssetAction : EndNameEditAction
{
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        // Create resources
        UnityEngine.Object obj = CreateAssetFromTemplate(pathName, resourceFile);
        ProjectWindowUtil.ShowCreatedAsset(obj);

    }

    internal static UnityEngine.Object CreateAssetFromTemplate(string pathName, string resourceFile)
    {
        string fullName = Path.GetFullPath(pathName);
        StreamReader reader = new StreamReader(resourceFile);
        string content = reader.ReadToEnd();
        reader.Close();

        content = content.Replace("#TIME", System.DateTime.Now.ToString());

        StreamWriter writer = new StreamWriter(fullName, false, new System.Text.UTF8Encoding());
        writer.Write(content);
        writer.Close();

        AssetDatabase.ImportAsset(pathName);
        AssetDatabase.Refresh();
        return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
    }
}