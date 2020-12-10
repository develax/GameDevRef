using System;
using System.IO;
using UnityEditor;

// c:\Program Files\Unity\Hub\Editor\2020.1.10f1\Editor\Data\Resources\ScriptTemplates\
public class NamespaceProcessor : UnityEditor.AssetModificationProcessor
{
    public static void OnWillCreateAsset(string metaFilePath)
    {
        string fileName = Path.GetFileNameWithoutExtension(metaFilePath);

        if (!fileName.EndsWith(".cs"))
            return;

        string assetsPath = Path.GetDirectoryName(metaFilePath);
        string actualFilePath = $"{Path.GetDirectoryName(metaFilePath)}\\{fileName}";
        string[] pathParts = assetsPath.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
        pathParts[0] = PlayerSettings.productName; // Replace "Assets" with ProductName.

        string content = File.ReadAllText(actualFilePath);
        string newcontent = content.Replace("#NAMESPACE#", string.Join(".", pathParts));

        if (content != newcontent)
        {
            File.WriteAllText(actualFilePath, newcontent);
            //AssetDatabase.Refresh();
        }
    }
}
