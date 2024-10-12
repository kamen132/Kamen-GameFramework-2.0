using UnityEditor;
using UnityEngine;

namespace Kamen.Editor
{
    public partial class KamenEditorTool
    {
#if UNITY_EDITOR
        private static void RunMyBat(string workingDir)
        {
            var path = FormatPath(workingDir);
            if (!System.IO.File.Exists(path))
            {
                Debug.LogError("bat文件不存在：" + path);
            }
            else
            {
                RunBat(workingDir);
            }
        }

        [MenuItem("Kamen的工具箱/Config/生成配置数据")]
        private static void RunConfigBat()
        {
            string rootPath = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets/".Length);
            RunMyBat($"{rootPath}/Luban/gen.bat");
        }
#endif
    }
}